using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecurityServiceProvider
{
    public static class HashServiceHelper
    {
        private static byte[] ComputeHashSHA256(byte[] inBytes)
        {
            byte[] outBytes;
            using (SHA256 sha256 = new SHA256Managed())
            {
                outBytes = sha256.ComputeHash(inBytes);
            }
            return outBytes;
        }

        private static byte[] ComputeHashMD5(byte[] inBytes)
        {
            byte[] outBytes;
            using (MD5 md5 = new MD5CryptoServiceProvider())
            {
                outBytes = md5.ComputeHash(inBytes);
            }
            return outBytes;
        }

        public static string GetSHA256HashCode(string source, Encoding encoding)
        {
            byte[] inbytes = encoding.GetBytes(source);
            return BitConverter.ToString(ComputeHashSHA256(inbytes)).Replace("-", "");
        }

        public static string GetMD5HashCode(string source, Encoding encoding)
        {
            byte[] inbytes = encoding.GetBytes(source);
            return BitConverter.ToString(ComputeHashMD5(inbytes)).Replace("-", "");
        }
    }
}
