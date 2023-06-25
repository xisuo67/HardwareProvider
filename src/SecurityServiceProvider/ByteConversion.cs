using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityServiceProvider
{
    public static class ByteConversion
    {
        public static string BytesToHexString(byte[] bytes)
        {
            try
            {
                string hexStr = "";
                if (bytes != null && bytes.Length > 0)
                {
                    foreach (byte b in bytes)
                    {
                        hexStr += b.ToString("X2");
                    }
                }
                return hexStr;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public static byte[] HexStringToBytes(string source)
        {
            try
            {
                List<byte> bytes = new List<byte>();
                for (int i = 0; i < source.Length; i += 2)
                {
                    string hexStr = source.Substring(i, 2);
                    bytes.Add(byte.Parse(hexStr, System.Globalization.NumberStyles.HexNumber));
                }
                return bytes.ToArray();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public static string BytesToBase64String(byte[] bytes)
        {
            try
            {
                string hexStr = "";
                if (bytes != null && bytes.Length > 0)
                {
                    hexStr = Convert.ToBase64String(bytes);
                }
                return hexStr;
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }

        public static byte[] Base64StringToBytes(string source)
        {
            try
            {
                List<byte> bytes = new List<byte>();
                if (!string.IsNullOrEmpty(source))
                {
                    bytes.AddRange(Convert.FromBase64String(source));
                }
                return bytes.ToArray();
            }
            catch (Exception exp)
            {
                throw exp;
            }
        }
    }
}
