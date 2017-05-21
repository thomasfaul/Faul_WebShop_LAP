using System.Text;
using System.Security.Cryptography;
using log4net;
using System.Diagnostics;

namespace CardGame.DAL.Logic
{
    public class Helper
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region GENERATE HASH
        /// <summary>
        /// Takes a string and generates the hashed string
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string GenerateHash(string s)
        {
            try
            {
                log.Info("Helper-GenerateHash");
                var bytes = System.Text.Encoding.UTF8.GetBytes(s);
                using (SHA512 sha = new SHA512Managed())
                {
                    var hash = sha.ComputeHash(bytes);
                    return GetHexNotation(hash);
                }
            }
            catch (System.Exception e)
            {
                Debugger.Break();
                throw e;
            }
        }
        #endregion

        #region GENERATE SALT
        /// <summary>
        /// Generate a Salt, gives back a string salt
        /// </summary>
        /// <returns></returns>
        public static string GenerateSalt()
        {
            try
            {
                log.Info("Helper-Salt");
                var salt = new byte[64];
                // generieren ein neues byte 64 array
                var rng = new RNGCryptoServiceProvider();
                rng.GetNonZeroBytes(salt);
                //befüllen mit werten die nicht null sind
                return GetHexNotation(salt);
                // Umwandlung
            }
            catch (System.Exception e)
            {
                Debugger.Break();
                throw e;
            }
        }
        #endregion

        #region GET HEX NOTATION
        /// <summary>
        /// Takes the salt and returns a String
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        private static string GetHexNotation(byte[] bytes)
        {
            try
            {
                log.Info("Helper-GetHexNotation");
                var hashStringBuilder = new StringBuilder(128);

                foreach (var b in bytes)
                {
                    hashStringBuilder.Append(b.ToString("X2"));
                }

                return hashStringBuilder.ToString();

            }
            catch (System.Exception e)
            {
                Debugger.Break();
                throw e;
            };
        }
        #endregion
    }
}
