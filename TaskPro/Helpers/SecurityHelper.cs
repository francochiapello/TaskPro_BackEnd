using System.Security.Cryptography;
using System.Text;

namespace TaskPro.Helpers
{
    public class SecurityHelper
    {
        private string Key;
        public void setKey(string key)
        {
            this.Key = key;
        }
        public string Encrypt(string password)
        {
            try
            {
                byte[] b = System.Text.ASCIIEncoding.ASCII.GetBytes(password);
                string encrypted = Convert.ToBase64String(b);
                return encrypted;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string Descrypt(string password)
        {
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(password);
                using (Aes encryptor = Aes.Create())
                {
                    Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(this.Key, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }
                        password = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }

                return password;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
