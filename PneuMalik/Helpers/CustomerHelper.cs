using System;
using System.Configuration;
using System.Web;

namespace PneuMalik.Helpers
{
    public class CustomerHelper
	{

        public CustomerHelper()
        {

            _context = HttpContext.Current;
            _vector = ConfigurationManager.AppSettings["SifrovaniInicializacniVektor"];
            _key = ConfigurationManager.AppSettings["SifrovaniKlic"];
        }

        public string Id
        {
            get
            {
                return getCustomerId();
            }
            set
            {
                setCustomerId(value);
            }
        }

        private string getCustomerId()
        {

            if (_context.Request.Cookies["UserID"] != null && _context.Request.Cookies["UserID"].Value.ToString().Trim() != "")
            {
                // sifrovani - vraci desifrovanou hodnotu ID
                try
                {
                    return fncDecrypt(_context.Request.Cookies["UserID"].Value);
                }
                // pro pirpad, ze desifrovani selze
                catch
                {
                    Guid tempCartId = Guid.NewGuid();
                    return tempCartId.ToString();
                }
            }
            else
            {
                // vrat null hodnotu
                Guid newUserGuid = Guid.NewGuid();

                // sifrovani - ulozeni zasifrovaneho Guid do Cookie
                _context.Response.Cookies["UserID"].Value = fncEncrypt(newUserGuid.ToString());
                return newUserGuid.ToString();
            }
        }

        private void setCustomerId(string id)
        {

            if (string.IsNullOrEmpty(id) || id == "0")
            {
                // vrat null hodnotu
                Guid tempCartId = Guid.NewGuid();

                 _context.Response.Cookies["UserID"].Value = fncEncrypt(tempCartId.ToString());
            }
            else
            {
                _context.Response.Cookies["UserID"].Value = fncEncrypt(id);
            }
        }

        /// <summary>
        /// metoda zasifruje text
        /// </summary>
        public string fncEncrypt(string ptext)
        {

            // Vytvořit instanci AES/Rijdael algoritmu
            var aes = new System.Security.Cryptography.RijndaelManaged();

            // Nastavit CBC block mode
            aes.Mode = System.Security.Cryptography.CipherMode.CBC;

            // Nastavit standardní PKCS7 padding posledního bloku
            aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            // Nastavit klic
            aes.Key = Convert.FromBase64String(_key);

            // Nastavit iv
            aes.IV = Convert.FromBase64String(_vector);

            // Převést text na pole bajtů
            byte[] plainData = System.Text.Encoding.UTF8.GetBytes(ptext);

            // Zašifrovat data
            byte[] cipherData;
            using (System.Security.Cryptography.ICryptoTransform enc = aes.CreateEncryptor())
            {
                cipherData = enc.TransformFinalBlock(plainData, 0, plainData.Length);
            }

            //vraci zasifrovany text
            return Convert.ToBase64String(cipherData);
        }

        /// <summary>
        /// metoda desifruje text
        /// </summary>
        private string fncDecrypt(string ctext)
        {
            // Vytvořit instanci AES/Rijdael algoritmu
            var aes = new System.Security.Cryptography.RijndaelManaged();

            // Nastavit CBC block mode
            aes.Mode = System.Security.Cryptography.CipherMode.CBC;

            // Nastavit standardní PKCS7 padding posledního bloku
            aes.Padding = System.Security.Cryptography.PaddingMode.PKCS7;

            // Načíst klíč
            aes.Key = Convert.FromBase64String(_key);

            // Načíst IV
            aes.IV = Convert.FromBase64String(_vector);

            // Načíst  šifrovaná data 
            byte[] cipherData = Convert.FromBase64String(ctext);

            // Dešifrovat data
            byte[] plainData;
            using (System.Security.Cryptography.ICryptoTransform dec = aes.CreateDecryptor())
            {
                plainData = dec.TransformFinalBlock(cipherData, 0, cipherData.Length);
            }
            string plainText = System.Text.Encoding.UTF8.GetString(plainData);

            // vraci text
            return plainText;
        }

        private readonly HttpContext _context;
        private readonly string _vector;
        private readonly string _key;
    }
}