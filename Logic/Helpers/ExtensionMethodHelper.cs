using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using NetCore.Logic.Helpers;

namespace NetCore.Logic.Helpers
{
    public static class ExtensionMethodHelper
    {

        /*
            
            Wordpress store the passwords in a table 'wp_users', and they encrypt passwords using MD5. 
            
        */

        //https://stackoverflow.com/questions/11454004/calculate-a-md5-hash-from-a-string
        public static string ConvertToMD5Hash(this string value)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(value);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}
