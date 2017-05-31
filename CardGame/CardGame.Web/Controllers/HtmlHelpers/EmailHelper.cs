using System;
using System.Net.Mail;
using System.Diagnostics;
using log4net;
using System.IO;
using System.Net;

namespace CardGame.Web.HtmlHelpers
{
    public class EmailHelper
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
          


        #region SEND EMAIL
        public static bool SendEmail(string emailaddress, string emailsubject, string text)
        {
          
            try
            {
                //----------------------------GMX
                MailMessage Email = new MailMessage();
                MailAddress Sender = new MailAddress("thomas.faul@gmx.at");
                Email.From = Sender;
                //Email.To.Add(emailaddress);
                Email.To.Add("thomas.faul@gmx.at");
                Email.Subject = emailsubject;
                Email.Body = text;
                string ServerName = "mail.gmx.net";
                string Port = "587";
                SmtpClient MailClient = new SmtpClient(ServerName, int.Parse(Port));
                    //Postausgangsserver definieren

                string UserName = "thomas.faul@gmx.at";
                string Password ="tom362461gmx";
                System.Net.NetworkCredential Credentials = new System.Net.NetworkCredential(UserName, Password);
                MailClient.Credentials = Credentials; // Anmeldeinformationen setzen*/
                MailClient.Send(Email); // Email senden
                return true;


                //----------------------------BBRZ
                //MailMessage Email = new MailMessage();
                //MailAddress Sender = new MailAddress("Thomas.Faul@qualifizierung.or.at");
                //Email.From = Sender;
                //Email.To.Add(emailaddress);
                //Email.Subject = emailsubject;
                //Email.Body = text;
                //string ServerName = "srv08.itccn.loc";
                //string Port = "25";
                //SmtpClient MailClient = new SmtpClient(ServerName, int.Parse(Port)); //Postausgangsserver definieren

                //string UserName = "thomas.faul@gmx.at";
                //string Password = "tom362461!";
                //System.Net.NetworkCredential Credentials = new System.Net.NetworkCredential(UserName, Password);
                //MailClient.Credentials = Credentials; // Anmeldeinformationen setzen*/
                //MailClient.Send(Email); // Email senden
                //return true;
            }
            catch (Exception e)
            {
                //Debugger.Break();
                log.Error("Emailhelper-EmailBBRZ", e);
                return true;
            }
            #endregion


        }

        #region SendPasswordResetEmail
        public static bool SendPasswordResetEmail(string emailaddress, int userid,string username)
        {
            try
            {
                //----------------------------GMX
                MailMessage Email = new MailMessage();
                MailAddress Sender = new MailAddress("Thomas.Faul@gmx.at");
                Email.From = Sender;
                //Email.To.Add(emailaddress);
                Email.To.Add("thomas.faul@gmx.at");
                Email.Subject = "Ihr Passwort zurücksetzen!";

                Email.IsBodyHtml = true;
                Email.Body=$"<p style='font-size:20px'>Bitte klicken Sie auf den Link um ihr Passwort zurück zu setzen </br >" +
                            $"<p style='font-size:20px'>If its not your with to reset your password please ignore this Email </br >" +
                            $"<p style='font-size:20px'>http://localhost:56538/Account/ResetPassword/{userid}</br >" +
                            "<p><b>MTP-Gmbh.</b><br/ > Simmeringer Hauptstrasse XX<br/>1030 Wien<br/> UID:78946513</p>";
                string ServerName = "mail.gmx.net";
                string Port = "587";
                SmtpClient MailClient = new SmtpClient(ServerName, int.Parse(Port));
                string UserName = "thomas.faul@gmx.at";
                string Password = "tom362461gmx";
                System.Net.NetworkCredential Credentials = new System.Net.NetworkCredential(UserName, Password);
                MailClient.Credentials = Credentials; // Anmeldeinformationen setzen*/
                MailClient.Send(Email); // Email senden
                return true;


                //----------------------------BBRZ


                //SmtpClient client = new SmtpClient("srv08.itccn.loc");
                //client.Credentials = new NetworkCredential("Thomas.Faul@qualifizierung.at", "tom362461!");
                //client.Port = 25;
                //client.EnableSsl = false;
                //MailMessage mess = new MailMessage();
                //mess.IsBodyHtml = true;
                //mess.From = new MailAddress("Thomas.Faul@qualifizierung.at");
                //mess.To.Add(new MailAddress($"{emailaddress}"));
                //mess.Subject = "Ihr Passwort zurücksetzen!";
                //mess.Body = $"<p style='font-size:20px'>Bitte klicken Sie auf den Link um ihr Passwort zurück zu setzen </br >" +
                //            $"<p style='font-size:20px'>If its not your with to reset your password please ignore this Email </br >" +
                //            $"<p style='font-size:20px'>http://localhost:56538/Account/ResetPassword/{userid}</br >" +
                //            "<p><b>MTP-Gmbh.</b><br/ > Simmeringer Hauptstrasse XX<br/>1030 Wien<br/> UID:78946513</p>";
                //client.Send(mess);
                //return true;
            }
            catch (Exception e)
            {

                log.Error("Emailhelper", e);
                return true;
            }
        }
        #endregion

        #region SendPasswordResetEmailAnswer
        public static bool SendPasswordResetEmailAnswer(string emailaddress, int userid)
        {
            try
            {
                //----------------------------GMX
                MailMessage Email = new MailMessage();
                MailAddress Sender = new MailAddress("Thomas.Faul@gmx.at");
                Email.From = Sender;
                
                Email.Subject = "Ihr Passwort zurücksetzen!";
                //Email.To.Add(emailaddress);
                Email.To.Add("thomas.faul@gmx.at");
                Email.IsBodyHtml = true;
                Email.Body = $"<p style='font-size:20px'>Bitte klicken Sie auf den Link um ihr Passwort zurück zu setzen </br >" +
                            $"<p style='font-size:20px'>Wenn dieses Email nicht für dich bestimmt ist, bitte dieses Email zu ignorieren, dein neues Passwort ist: 123user!, bitte innerhalb von 10 Minuten im Profil verändern</br >" +
                            $"<p style='font-size:20px'>http://localhost:56538/Account/ResetPassword/{userid}</br >" +
                            "<p><b>MTP-Gmbh.</b><br/ > Simmeringer Hauptstrasse XX<br/>1030 Wien<br/> UID:78946513</p>";
                string ServerName = "mail.gmx.net";
                string Port = "587";
                SmtpClient MailClient = new SmtpClient(ServerName, int.Parse(Port));
                string UserName = "thomas.faul@gmx.at";
                string Password = "tom362461gmx";
                System.Net.NetworkCredential Credentials = new System.Net.NetworkCredential(UserName, Password);
                MailClient.Credentials = Credentials; // Anmeldeinformationen setzen*/
                MailClient.Send(Email); // Email senden
                return true;




                //----------------------------BBRZ
                //SmtpClient client = new SmtpClient("srv08.itccn.loc");
                //client.Credentials = new NetworkCredential("Thomas.Faul@qualifizierung.at", "++++++");
                //client.Port = 25;
                //client.EnableSsl = false;
                //MailMessage mess = new MailMessage();
                //mess.IsBodyHtml = true;
                //mess.From = new MailAddress("Thomas.Faul@qualifizierung.at");
                //mess.Subject = "Ihr Passwort wurde geändert";
                //mess.Body = $"<p style='font-size:20px'>Dein neues Passwort ist:123user, um dies zu ändern bitte einloggen und ändere es in deinen Profileinstellungen </br >" +
                //            $"<p style='font-size:20px'>If its not your with to reset your password please ignore this Email </br >" +
                //            "<p><b>MTP-Gmbh.</b><br/ > Simmeringer Hauptstrasse XX<br/>1030 Wien<br/> UID:78946513</p>";


                //client.Send(mess);
                //return true;
            }
            catch (Exception e)
            {

                log.Error("Emailhelper-SendPasswordResetEmailAnswer", e);
                return true;
            }
        }
        #endregion
    }
}
