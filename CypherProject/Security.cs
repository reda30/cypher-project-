using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace CypherProject
{
    class Security
    {
        public string GetEncrypto(string inputPass, string inputText)
        {
            try
            {
                byte[] inputPassByte = Encoding.UTF8.GetBytes(inputPass);
                byte[] iv = { 0x05, 0x56, 0x78, 0x55, 0x86, 0x79, 0x77, 0x99 };
                byte[] inputTextByte = Encoding.UTF8.GetBytes(inputText);

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream crypto = new CryptoStream(memoryStream, des.CreateEncryptor(inputPassByte, iv), CryptoStreamMode.Write);
                crypto.Write(inputTextByte, 0, inputTextByte.Length);
                crypto.FlushFinalBlock();
                crypto.Dispose();
                crypto.Close();

                return Convert.ToBase64String(memoryStream.ToArray());


            }
            catch
            {

                return null;
            }


        }

        public string GetDecrypto(string inputPass, string inputText)
        {
            try
            {
                byte[] inputPassByte = Encoding.UTF8.GetBytes(inputPass);
                byte[] iv = { 0x05, 0x56, 0x78, 0x55, 0x86, 0x79, 0x77, 0x99 };
                byte[] inputTextByte = Convert.FromBase64String(inputText);

                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                MemoryStream memoryStream = new MemoryStream();
                CryptoStream cryptoStream = new CryptoStream(memoryStream, des.CreateDecryptor(inputPassByte, iv), CryptoStreamMode.Write);
                cryptoStream.Write(inputTextByte, 0, inputTextByte.Length);

                cryptoStream.FlushFinalBlock();
                cryptoStream.Dispose();
                cryptoStream.Close();

                return Encoding.UTF8.GetString(memoryStream.ToArray());

            }
            catch
            {

                return null;
            }
        }
    }
}
