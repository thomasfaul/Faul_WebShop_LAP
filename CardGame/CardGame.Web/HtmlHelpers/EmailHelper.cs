using System;
using System.Net.Mail;
using System.Diagnostics;
using log4net;

namespace CardGame.Web.HtmlHelpers
{
    public class EmailHelper
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        #region SEND EMAIL
        public static bool SendEmail(string emailaddress, string emailsubject, string text)
        {
            #region EMAILBBRZ
            try
            {
                MailMessage Email = new MailMessage();
                MailAddress Sender = new MailAddress("Thomas.Faul@qualifizierung.or.at");
                Email.From = Sender;
                Email.To.Add(emailaddress);
                Email.Subject = emailsubject;

                Email.Body = text;

                string ServerName = "srv08.itccn.loc";
                string Port = "25";


                SmtpClient MailClient = new SmtpClient(ServerName, int.Parse(Port)); // Postausgangsserver definieren

                string UserName = "thomas.faul@gmx.at";

                string Password = "tom362461!";
                System.Net.NetworkCredential Credentials = new System.Net.NetworkCredential(UserName, Password);
                MailClient.Credentials = Credentials; // Anmeldeinformationen setzen*/
                MailClient.Send(Email); // Email senden
                return true;
            }
            catch (Exception e)
            {
                //Debugger.Break();
                log.Error("Emailhelper-EmailBBRZ",e);
                return true;
            }
            #endregion

            #region EMAIL GMX
        
            //MailMessage Email = new MailMessage();
            //MailAddress Sender = new MailAddress("thomas.faul@gmx.at");

            //Email.From = Sender;
            //Email.To.Add(emailaddress);
            //Email.Subject = emailsubject;

            //Email.Body = text;

            //string ServerName = "mail.gmx.net";
            //string Port = "587";



            //SmtpClient MailClient = new SmtpClient(ServerName, int.Parse(Port)); // Postausgangsserver definieren

            //string UserName = "thomas.faul@gmx.at";

            ////string Password ="tom362461gmx";
            //string Password = "tom362461!";
            //System.Net.NetworkCredential Credentials = new System.Net.NetworkCredential(UserName, Password);
            //MailClient.Credentials = Credentials; // Anmeldeinformationen setzen*/
            //MailClient.Send(Email); // Email senden
            //return true;

            #endregion
        }
#endregion
    }
}