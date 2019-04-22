using BusinessLayer;
using BusinessLayer.Interfaces;
using BusinessLayer.Support._interfaces;
using Common;
using Common.Interfaces;
using Common.Models;
using LiteDB;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using static Common.Helpers;

namespace PresentationLayer
{
    public partial class Loginform : Form
    {
        // switch between MySQL or LiteDB
        private readonly bool isMySQL = false;
        private IUIFactory uiFactory;
        private IFrameWork frameWork;
        private EventMediator eventMediator;
        private IUserService userService;
        //private string uName;

        public Loginform()
        {

            eventMediator = new EventMediator();
            // feliratkozás az ErrorMessage Event-re
            // hibaüzenet esetén az OnErrorMessage metódus megjeleníti a hibaüzenetet
            eventMediator.ErrorMessage += OnErrorMessage;

            FactorySupport factorySupport = new FactorySupport();
            Directory.CreateDirectory(@"C:\Log");
            Log.Logger = new LoggerConfiguration().WriteTo.File(@"C:\Log\Log.txt", rollingInterval: RollingInterval.Hour).CreateLogger();
            Directory.CreateDirectory(@"C:\db");
            LiteRepository repo = new LiteRepository(ApplicationConfig.DbConnectionString);

            frameWork = factorySupport.Create(isMySQL, repo, eventMediator);

            InitializeComponent();

            // példa: lekéri a GetService() -vel a UIFactory szervízt
            // utána kirajzol egy button-t a felhasználói felületen.

            uiFactory = (IUIFactory)(frameWork.GetService(typeof(IUIFactory)));

            if (uiFactory == null)
            {
                throw new Exception("Hibás UIFactory!");
            }

            // Betölti az egész adatbázist a memóriába
            //MessageBox.Show("Beolvasom az adatbázist a memóriába");
            frameWork.LoadDatabase();


            // példa: lekéri a GetService() -vel az IErrorservice szervízt
            // utána megjeleníti a hibaüzenetet

            IErrorService errorService = (IErrorService)(frameWork.GetService(typeof(IErrorService)));

            if (errorService == null)
            {
                throw new Exception("Hibás ErrorService!");
            }

            /*IError error = new Error(ErrorType.InputError, "Példa a beviteli hibára");
            errorService.Write(error);

            // hibeüzenet 2. példa
            IError errorExample2 = new Error(ErrorType.DatabaseError, "Példa: Adatbázis üzenet a datalayertől");
            errorService.Write(errorExample2);*/

            userService = (IUserService)(frameWork.GetService(typeof(IUserService)));

            InitializeComponent();
        }

        private void OnErrorMessage(object sender, ErrorMessageEventArgs e)
        {
            // Az errorService által küldött üzenetet egy label-en megjeleníti, 
            // vagy errorType-tól függőn MesseageBox-ban is megjelenítheti a hibaüzenetet
            if (e.errorType == ErrorType.DatabaseError)
            {
                MessageBox.Show(e.message);
            }
            else
            {
                errorLabel.Text = e.message;
            }
        }

        private void createAdmin(string userName, string userMail, string userPw)
        {
            var user = new User()
            {
                Name = userName,
                Email = userMail,
                Password = userPw
            };
            userService.CreateUser(user);
        }

        private void submitbtn_Click(object sender, EventArgs e)
        {
            //string uName = username.Text;
            string pw = password.Text;

            System.Windows.Forms.MessageBox.Show(uName);

            var user = new User()
            {
                Name = uName,
                Email = uName,
                Password = pw
            };
            userService.CreateUser(user);
            userService.LoginUser(uName,pw);
        }

        private void username_KeyPress(object sender, KeyPressEventArgs e)
        {
            uName = username.Text;
        }
    }
}
