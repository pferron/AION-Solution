
#region Using

using System;
using System.IO;
using System.Security.Cryptography;

#endregion

namespace Meck.Logging
{
    public class Encryption
    {
        #region Private Members

        private static byte[] key = { };
        private static byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xab, 0xcd, 0xef };

        public const string EncryptionPrefix = "MECK-LOGGING-ENCRYPTION/";

        #endregion

        #region Member Properties

        private static string KeyString
        {
            get { return "!#$a_7?9%?$h]^?3+($u}@~3"; }
        }

        #endregion

        #region Shared Methods

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Decrypt(string input)
        {
            input = input.Replace(EncryptionPrefix, string.Empty);

            byte[] bytes = new byte[input.Length + 1];
            MemoryStream ms = null;
            CryptoStream cs = null;
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();

            try
            {
                if ((input == string.Empty))
                    return string.Empty;

                key = System.Text.Encoding.UTF8.GetBytes(KeyString.Substring(0, 24));
                input = input.Replace(" ", "+");
                bytes = Convert.FromBase64String(input);

                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateDecryptor(key, IV), CryptoStreamMode.Write);

                cs.Write(bytes, 0, bytes.Length);
                cs.FlushFinalBlock();

                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                return encoding.GetString(ms.ToArray());

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while decrypting the data.", ex);
            }
            finally
            {
                if ((ms != null))
                {
                    ms.Close();
                    ((IDisposable)ms).Dispose();
                }
                if ((cs != null))
                {
                    cs.Close();
                    ((IDisposable)cs).Dispose();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Encrypt(string input)
        {

            MemoryStream ms = null;
            CryptoStream cs = null;
            TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();

            try
            {
                if ((input == string.Empty))
                    return string.Empty;

                key = System.Text.Encoding.UTF8.GetBytes(KeyString.Substring(0, 24));
                byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input);

                ms = new MemoryStream();
                cs = new CryptoStream(ms, des.CreateEncryptor(key, IV), CryptoStreamMode.Write);

                cs.Write(bytes, 0, bytes.Length);
                cs.FlushFinalBlock();

                return EncryptionPrefix + Convert.ToBase64String(ms.ToArray());

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while encrypting the QueryString.", ex);
            }
            finally
            {
                if ((ms != null))
                {
                    ms.Close();
                    ((IDisposable)ms).Dispose();
                }
                if ((cs != null))
                {
                    cs.Close();
                    ((IDisposable)cs).Dispose();
                }
            }
        }

        #endregion

    }
}

