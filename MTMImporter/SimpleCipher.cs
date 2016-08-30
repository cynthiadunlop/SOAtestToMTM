using System.Text;
using System;
using System.Security.Cryptography;
namespace SOAtestToMTM
{
    public static class SimpleCipher
    {
        private static readonly byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static readonly byte[] iv = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

        public static string Encrypt(string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        public static string Decrypt(string text)
        {
            try
            {
                SymmetricAlgorithm algorithm = DES.Create();
                ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
                byte[] inputbuffer = Convert.FromBase64String(text);
                byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
                return Encoding.Unicode.GetString(outputBuffer);
            }
            catch (Exception e)
            {
                throw new ImportException("Unable to decrypt password, please use the encrypted password generated by MTMImporter.exe encrypt 'password' command");
            }
        }

    }
}