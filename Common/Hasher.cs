using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class Hasher
    {
        private const int _saltByteSize = 128 / 8;
        private const int _hashByteSize = 256 / 8;
        private const int _pbkdf2Iteration = 3500;

        public static byte[] Hash(string passwordToHash, byte[] salt)
        {
            var hash = GenerateHashValue(passwordToHash, salt);
            var iterationCountByte = BitConverter.GetBytes(_pbkdf2Iteration);
            var result = new byte[_hashByteSize + _saltByteSize + iterationCountByte.Length];

            Buffer.BlockCopy(salt, 0, result, 0, _saltByteSize);
            Buffer.BlockCopy(hash, 0, result, _saltByteSize, _hashByteSize);
            Buffer.BlockCopy(iterationCountByte, 0, result, _saltByteSize + _hashByteSize, iterationCountByte.Length);

            return result;

        }

        public static byte[] GenerateRandomSalt()
        {
            var salt = new byte[_saltByteSize];
            using (var CryptoProvider = new RNGCryptoServiceProvider())
            {
                CryptoProvider.GetBytes(salt);
            }
            return salt;
        }

        private static byte[] GenerateHashValue(string password, byte[] salt)
        {
            byte[] hashValue;

            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _pbkdf2Iteration))
            {
                hashValue = pbkdf2.GetBytes(_hashByteSize);
            }

            return hashValue;
        }
    }
}
