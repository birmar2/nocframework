using BusinessLayer.Interfaces;
using BusinessLayer.Models;
using Common;
using Common.Extensions;
using Common.Models;
using DataLayer.Interfaces;

namespace BusinessLayer
{
    public class UserService : IUserService
    {
        private readonly IDataService dataRepo;
        private readonly IMapper mapper;
        private ILogService logService;

        public UserService(IDataService dataService, IMapper mapper, ILogService logService)
        {
            this.dataRepo = dataService;
            this.mapper = mapper;
            this.logService = logService;
        }

        /// <summary>
        /// Changes password for the user. Returns with a class containing error messages and a boolean representing the success of the password change.
        /// </summary>
        /// <param name="user">Logged in user</param>
        /// <param name="newPassword">New password</param>
        /// <param name="oldPassword">Old password for authentication</param>
        /// <returns></returns>
        public ValidationResult ChangePassword(string email, string newPassword)
        {
            User user = GetUser(email);
            logService.Create("Jelszóváltási kísérlet", user.Name);
            //Reusing ValidationResult from the validators, it passes error messages to the Presentation Layer.
            ValidationResult result = new ValidationResult();


            //Using userPassChange DTO with automapper
            UserPassChange userPassChng = new UserPassChange();
            mapper.Map(user, userPassChng);
            userPassChng.NewPassword = newPassword;

            //Validating new password
            ValidationResult newPass = Validator<UserPassChange>.Validate(userPassChng);

            if (!newPass.IsValid)
            {
                result.Errors.AddRange(newPass.Errors);
                logService.Create($"Jelszó változtatás sikertelen: az új jelszó nem felel meg a követelményeknek.", user.Name);
            }
            else
            {
                userPassChng.HashedNewPassword = Hasher.Hash(userPassChng.NewPassword, userPassChng.PasswordSalt.ToByteArray()).ToBase64String();
                //checking the last 5 passwords
                if (userPassChng.OldPasswords.Contains(userPassChng.HashedNewPassword) || userPassChng.HashedNewPassword == userPassChng.Password)
                {
                    result.Errors.Add("Az új jelszónak különböznie kell a korábbi 5 jelszótól");

                    logService.Create($"Jelszó váltás sikertelen: a felhasználó a régi jelszót kísérlete meg felhasználni.");
                }
                else //Shifting user's passwords & updating the repo
                {
                    if (userPassChng.OldPasswords.Count > 3)
                        userPassChng.OldPasswords.Dequeue();

                    userPassChng.OldPasswords.Enqueue(userPassChng.Password);
                    userPassChng.Password = userPassChng.HashedNewPassword;
                    //giving back passwords to user & updating repo
                    mapper.Map(userPassChng, user);
                    dataRepo.UpdateUser(user);
                    logService.Create($"A felhasználó jelszavát frissítettük.");
                }
            }
            return result;
        }

        public bool CreateUser(User toBeCreated)
        {
            if (dataRepo.GetUser(toBeCreated.Email) is User)
            {
                logService.Create($"Sikertelen refisztráció a felhasználó részéről {toBeCreated.ToShortString()} (Ilyen felhasználó már létezik.)");
                return false;
            }
            else
            {
                var salt = Hasher.GenerateRandomSalt();
                toBeCreated.PasswordSalt = salt.ToBase64String();
                var hashedPassword = Hasher.Hash(toBeCreated.Password, salt);
                toBeCreated.Password = hashedPassword.ToBase64String();
                toBeCreated.OldPasswords.Add(toBeCreated.Password);
                if (dataRepo.InsertUser(toBeCreated))
                {
                    logService.Create($"A felhasználó részéről sikeres regisztráció.", toBeCreated.ToShortString());
                    return true;
                }
                else
                {
                    logService.Create($"Sikertelen regisztráció a felhasználó részéről {toBeCreated.ToShortString()} (Unexpected error occured)");
                    return false;
                }
            }
        }

        public bool DeleteUser(User toDelete)
        {
            return dataRepo.DeleteUser(toDelete);
        }

        private User GetUser(string email)
        {
            return dataRepo.GetUser(email);
        }

        public bool LoginUser(string username, string password)
        {
            var userRecord = dataRepo.GetUser(username);
            if (userRecord != null)
            {
                logService.Create($"Sikeres bejelentkezés.", userRecord.ToShortString());
                var hashedpwd = Hasher.Hash(password, userRecord.PasswordSalt.ToByteArray()).ToBase64String();
                return hashedpwd == userRecord.Password ? true : false;
            }
            else
            {
                logService.Create($"Sikertelen bejelentkezés.", username);
                return false;
            }
        }

        public bool UpdateUser(User toBeUpdated)
        {

            if (dataRepo.UpdateUser(toBeUpdated))
            {
                logService.Create($"Felhasználó adatai frissítve:", toBeUpdated.ToShortString());
                return true;
            }
            else
            {
                logService.Create($"Felhasználó adatai frissítve, de váratlan hiba történt", toBeUpdated.ToShortString());
                return false;
            }
        }
        public bool UpdateUserSetting(DisplayableUserData toBeUpdatedSettings)
        {
            var fulldata = dataRepo.GetUser(toBeUpdatedSettings.Id);

            fulldata.RequestTimeout = toBeUpdatedSettings.RequestTimeout;
            fulldata.TimeZoneId = toBeUpdatedSettings.TimeZoneId;

            return UpdateUser(fulldata);
        }

        public DisplayableUserData GetUserData(int id)
        {
            var userRecord = dataRepo.GetUser(id);

            DisplayableUserData userSettings = new DisplayableUserData
            {
                Id = userRecord.Id,
                Name = userRecord.Name,
                Email = userRecord.Email,
                RequestTimeout = userRecord.RequestTimeout,
                TimeZoneId = userRecord.TimeZoneId
            };
            return userSettings;
        }
        public DisplayableUserData GetUserData(string email)
        {
            var userRecord = GetUser(email);
            var result = new DisplayableUserData()
            {
                Id = userRecord.Id,
                Name = userRecord.Name,
                Email = userRecord.Email,
                RequestTimeout = userRecord.RequestTimeout,
                TimeZoneId = userRecord.TimeZoneId
            };
            return result;
        }
    }
}
