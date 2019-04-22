using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class ExtensionMethods
    {

        public static string ToBase64String(this string input)
        {
            var bytes = Encoding.ASCII.GetBytes(input);
            return Convert.ToBase64String(bytes);
        }


        public static string ToBase64String(this byte[] input)
        {
            return Convert.ToBase64String(input);
        }


        public static byte[] ToByteArray(this string input)
        {
            return Convert.FromBase64String(input);
        }

        public static string ConvertToUnsecureString(this SecureString securePassword)
        {
            if (securePassword == null)
                return string.Empty;
            //throw new ArgumentNullException("securePassword");

            IntPtr unmanagedString = IntPtr.Zero;
            try
            {
                unmanagedString = Marshal.SecureStringToGlobalAllocUnicode(securePassword);
                return Marshal.PtrToStringUni(unmanagedString);
            }
            finally
            {
                Marshal.ZeroFreeGlobalAllocUnicode(unmanagedString);
            }
        }

    }
}
