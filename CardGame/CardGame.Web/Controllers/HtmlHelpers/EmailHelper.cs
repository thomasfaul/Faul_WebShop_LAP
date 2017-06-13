using System;
using System.Net.Mail;
using log4net;
using System.Text;
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
                ////----------------------------GMX
                //MailMessage Email = new MailMessage();
                //MailAddress Sender = new MailAddress("thomas.faul@gmx.at");
                //Email.From = Sender;
                ////Email.To.Add(emailaddress);
                //Email.To.Add("thomas.faul@gmx.at");
                //Email.Subject = emailsubject;
                //Email.Body = text;
                //string ServerName = "mail.gmx.net";
                //string Port = "587";
                //SmtpClient MailClient = new SmtpClient(ServerName, int.Parse(Port));
                ////Postausgangsserver definieren

                //string UserName = "thomas.faul@gmx.at";
                //string Password = "tom362461gmx";
                //System.Net.NetworkCredential Credentials = new System.Net.NetworkCredential(UserName, Password);
                //MailClient.Credentials = Credentials; // Anmeldeinformationen setzen*/
                //MailClient.Send(Email); // Email senden
                //return true;


                //----------------------------BBRZ




                string UserName = "Thomas.Faul@qualifizierung.or.at";
                string Password = "tom362461!";
                System.Net.NetworkCredential Credentials = new System.Net.NetworkCredential(UserName, Password);

                MailMessage Email = new MailMessage();
                MailAddress Sender = new MailAddress("Thomas.Faul@qualifizierung.or.at");
                Email.From = Sender;


                Email.To.Add(/*emailaddress*/"Thomas.Faul@qualifizierung.or.at");
                //Email.To.Add("thomas.faul@gmx.at");
                Email.Subject = emailsubject;
                Email.Body = text;


                string ServerName = "srv08.itccn.loc";
                string Port = "25";
                SmtpClient MailClient = new SmtpClient(ServerName, int.Parse(Port)); //Postausgangsserver definieren

                MailClient.Credentials = Credentials; // Anmeldeinformationen setzen*/
                MailClient.EnableSsl = true;

                MailClient.Send(Email); // Email senden
                return true;
            }
            catch (Exception e)
            {
                //Debugger.Break();
                log.Error("Emailhelper-EmailBBRZ", e);
                return true;
            }
        }
        #endregion

        #region SendPasswordResetEmail
        public static bool SendPasswordResetEmail(string emailaddress, int userid, string username)
        {
            try
            {
                //----------------------------GMX
                //MailMessage Email = new MailMessage();
                //MailAddress Sender = new MailAddress("Thomas.Faul@gmx.at");
                //Email.From = Sender;
                ////Email.To.Add(emailaddress);
                //Email.To.Add("thomas.faul@gmx.at");
                //Email.Subject = "Ihr Passwort zurücksetzen!";

                //Email.IsBodyHtml = true;
                //Email.Body = $"<p style='font-size:20px'>Bitte klicken Sie auf den Link um ihr Passwort zurück zu setzen </br >" +
                //            $"<p style='font-size:20px'>If its not your with to reset your password please ignore this Email </br >" +
                //            $"<p style='font-size:20px'>http://localhost:56538/Account/ResetPassword/{userid}</br >" +
                //            "<p><b>MTP-Gmbh.</b><br/ > Simmeringer Hauptstrasse XX<br/>1030 Wien<br/> UID:78946513</p>";
                //string ServerName = "mail.gmx.net";
                //string Port = "587";
                //SmtpClient MailClient = new SmtpClient(ServerName, int.Parse(Port));
                //string UserName = "thomas.faul@gmx.at";
                //string Password = "tom362461gmx";
                //System.Net.NetworkCredential Credentials = new System.Net.NetworkCredential(UserName, Password);
                //MailClient.Credentials = Credentials; // Anmeldeinformationen setzen*/
                //MailClient.Send(Email); // Email senden
                //return true;


                //----------------------------BBRZ


                SmtpClient client = new SmtpClient("srv08.itccn.loc");
                client.Credentials = new NetworkCredential("Thomas.Faul@qualifizierung.or.at", "tom362461!");
                client.Port = 25;
                client.EnableSsl = false;

                MailMessage mess = new MailMessage();
                mess.IsBodyHtml = true;
                mess.From = new MailAddress("Thomas.Faul@qualifizierung.or.at");
                mess.To.Add(new MailAddress(/*$"{emailaddress}"*/"Thomas.Faul@qualifizierung.or.at"));
                mess.Subject = "Ihr Passwort zurücksetzen!";
                mess.Body = $"<p style='font-size:20px'>Bitte klicke  auf den Link um ihr Passwort zurück zu setzen, momentan kannst du dich mit 123user! einloggen</br >" +
                            $"<p style='font-size:20px'>Falls das nicht ihr Account ist,bitten  wir dich diese Botschaft zu ignorieren </br >" +
                            $"<p style='font-size:20px'>http://localhost:56538/Account/ResetPassword/{userid}</br >" +
                            "<p><b>MTP-Gmbh.</b><br/ > Simmeringer Hauptstrasse XX<br/>1030 Wien<br/> UID:78946513</p>";
                client.EnableSsl = false;
                client.Send(mess);
                return true;
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
                //MailMessage Email = new MailMessage();
                //MailAddress Sender = new MailAddress("Thomas.Faul@gmx.at");
                //Email.From = Sender;

                //Email.Subject = "Passwort zurücksetzen";
                ////Email.To.Add(emailaddress);
                //Email.To.Add("thomas.faul@gmx.at");
                //Email.IsBodyHtml = true;
                //Email.Body = $"<p style='font-size:20px'>Bitte klicken Sie auf den Link um ihr Passwort zurück zu setzen </br >" +
                //            $"<p style='font-size:20px'>Wenn dieses Email nicht für dich bestimmt ist, bitte dieses Email zu ignorieren, dein neues Passwort ist: 123user!, bitte innerhalb von 10 Minuten im Profil verändern</br >" +
                //            $"<p style='font-size:20px'>http://localhost:56538/Account/ResetPassword/{userid}</br >" +
                //            "<p><b>MTP-Gmbh.</b><br/ > Simmeringer Hauptstrasse XX<br/>1030 Wien<br/> UID:78946513</p>";
                //string ServerName = "mail.gmx.net";
                //string Port = "587";
                //SmtpClient MailClient = new SmtpClient(ServerName, int.Parse(Port));
                //string UserName = "thomas.faul@gmx.at";
                //string Password = "tom362461gmx";
                //System.Net.NetworkCredential Credentials = new System.Net.NetworkCredential(UserName, Password);
                //MailClient.Credentials = Credentials; // Anmeldeinformationen setzen*/
                //MailClient.Send(Email); // Email senden
                //return true;




                //----------------------------BBRZ


                SmtpClient client = new SmtpClient("srv08.itccn.loc");
                client.Credentials = new NetworkCredential("Thomas.Faul@qualifizierung.or.at", "tom362461!");
                client.Port = 25;
                client.EnableSsl = false;



                MailMessage mess = new MailMessage();
                mess.IsBodyHtml = true;
                mess.From = new MailAddress("Thomas.Faul@qualifizierung.or.at");
                //mess.To.Add("emailaddress")
                mess.To.Add("Thomas.Faul@qualifizierung.or.at");
                mess.Subject = "Ihr Passwort wurde geändert";
                mess.Body = $"<p style='font-size:20px'>Dein Passwort wurde geändert </br >" +
                            $"<p style='font-size:20px'>Falls du dies nicht gemacht hast, bitte kontaktiere uns rasch </br >" +
                            "<p><b>MTP-Gmbh.</b><br/ > Simmeringer Hauptstrasse XX<br/>1030 Wien<br/> UID:78946513</p>";

                client.EnableSsl = false;
                client.Send(mess);
                return true;
            }
            catch (Exception e)
            {

                log.Error("Emailhelper-SendPasswordResetEmailAnswer", e);
                return true;
            }
        }
        #endregion

        #region SendBillAndConfirmationAnswer
        public static bool SendBillAndConfirmationAnswer(string emailaddress, int renr, string username, string packname, int amountofpacks, double billoverall, int userid, string payment)
        {
            double tax = billoverall / 6;
            tax = Math.Round(tax, 2);
            try
            {
                //----------------------------GMX
                //MailMessage Email = new MailMessage();
                //MailAddress Sender = new MailAddress("Thomas.Faul@gmx.at");
                //Email.From = Sender;
                //DateTime date = DateTime.Now;
                //Email.Subject = "Liebe Grüsse vom Clonestone- Team";
                ////Email.To.Add(emailaddress);
                //Email.To.Add("thomas.faul@gmx.at");
                //Email.IsBodyHtml = true;
                //Email.Body = string.Format("<html><body style='color:grey; font-size:8px;'><font face = 'Helvetica, Arial, sans-serif'><div style='position: absolute; height: 10px; width: 450px;background-color:rgba(129, 75, 55, 0.8); padding: 30px;'>&nbsp;</div><p><span style ='font-family: Helvetica, Arial, sans-serif;'><span style ='font-family: Helvetica,Arial, sans-serif;'><br/><br/></span></span></p><div style='background-color:papayawhip; width: 450px; height: 800px; padding: 30px; margin-top: 20px;'><p style='font-size:16px' color:darkorange'> Deine Rechnung:</p><br/><p>Magical TeaParty Gmbh</p><p> Zipperstrasse 56 - 59 </p><p> 1110 Wien </p><p> (+43)0664 / 1525725 </p><p> magicaltea.party.at </p ><div style='text-align:right'> Datum:{0}</div><br/><p>Rechnungsnummer:&nbsp;{1}</p><br/><br/><p style='font-size:12px'>Liebe(r) &nbsp;{2},</p><p> Hier ist deine Bestellübersicht:</p><br/><p> Du hast {4} &nbsp;Stück vom <span style='font-weight:bold'>Package &nbsp;{3}</span> bestellt und erhalten. Die Umsatzsteuer beträgt <span style='font-weight:bold'> {5}&nbsp; Euro </span>.  Der Gesamtbetrag inklusive Mehrwertsteuer beträgt&nbsp;<span style='font-weight:bold'>{6}&nbsp; Euro</span>,und der Betrag wurde mit {7} bezahlt.<p/><p> Danke für deinen Einkauf,</p><p>&nbsp;</p><p style='font-size:12px'> Das Clonestone - Team </p><p> Bei Fragen und Reklamationen:</p><p><strong> Support:</strong><a href='mailto:Support@clonestone.at'> Support@clonestone.at</a></p><p><strong> Marketing:<a href = 'mailto:Marketing@clonestone.at'> Marketing@clonestone.com </a></strong></p><p> Gerichtsstand Wien </p></div></div></body></html>", date, renr, username, packname, amountofpacks, tax, billoverall, payment);
                //string ServerName = "mail.gmx.net";
                //string Port = "587";
                //SmtpClient MailClient = new SmtpClient(ServerName, int.Parse(Port));
                //string UserName = "thomas.faul@gmx.at";
                //string Password = "tom362461gmx";
                //System.Net.NetworkCredential Credentials = new System.Net.NetworkCredential(UserName, Password);
                //MailClient.Credentials = Credentials; // Anmeldeinformationen setzen*/
                //MailClient.Send(Email); // Email senden
                //return true;




                //----------------------------BBRZ

                MailMessage Email = new MailMessage();
                MailAddress Sender = new MailAddress("Thomas.Faul@qualifizierung.or.at");
                Email.From = Sender;
                DateTime date = DateTime.Now;
                Email.Subject = "Liebe Grüsse vom Clonestone- Team";
                //Email.To.Add(emailaddress);
                Email.To.Add("Thomas.Faul@qualifizierung.or.at");
                Email.IsBodyHtml = true;

                Email.Body = string.Format("<html><body style='color:grey; font-size:8px;'><font face = 'Helvetica, Arial, sans-serif'><div style='position: absolute; height: 10px; width: 450px;background-color:rgba(129, 75, 55, 0.8); padding: 30px;'>&nbsp;</div><p><span style ='font-family: Helvetica, Arial, sans-serif;'><span style ='font-family: Helvetica,Arial, sans-serif;'><br/><br/></span></span></p><div style='background-color:papayawhip; width: 450px; height: 800px; padding: 30px; margin-top: 20px;'><p style='font-size:16px' color:darkorange'> Deine Rechnung:</p><br/><p>Magical TeaParty Gmbh</p><p> Zipperstrasse 56 - 59 </p><p> 1110 Wien </p><p> (+43)0664 / 1525725 </p><p> magicaltea.party.at </p ><div style='text-align:right'> Datum:{0}</div><br/><p>Rechnungsnummer:&nbsp;{1}</p><br/><br/><p style='font-size:12px'>Liebe(r) &nbsp;{2},</p><p> Hier ist deine Bestellübersicht:</p><br/><p> Du hast {4} &nbsp;Stück vom <span style='font-weight:bold'>Package &nbsp;{3}</span> bestellt und erhalten. Die Umsatzsteuer beträgt <span style='font-weight:bold'> {5}&nbsp; Euro </span>.  Der Gesamtbetrag inklusive Mehrwertsteuer beträgt&nbsp;<span style='font-weight:bold'>{6}&nbsp; Euro</span>,und der Betrag wurde mit {7} bezahlt.<p/><p> Danke für deinen Einkauf,</p><p>&nbsp;</p><p style='font-size:12px'> Das Clonestone - Team </p><p> Bei Fragen und Reklamationen:</p><p><strong> Support:</strong><a href='mailto:Support@clonestone.at'> Support@clonestone.at</a></p><p><strong> Marketing:<a href = 'mailto:Marketing@clonestone.at'> Marketing@clonestone.com </a></strong></p><p> Gerichtsstand Wien </p></div></div></body></html>", date, renr, username, packname, amountofpacks, tax, billoverall, payment);
                string ServerName = "srv08.itccn.loc";
                string Port = "25";
                SmtpClient MailClient = new SmtpClient(ServerName, int.Parse(Port));
                string UserName = "Thomas.Faul@qualifizierung.or.at";
                string Password = "tom362461!";
                System.Net.NetworkCredential Credentials = new System.Net.NetworkCredential(UserName, Password);
                MailClient.Credentials = Credentials; // Anmeldeinformationen setzen*/

                MailClient.EnableSsl = false;
                MailClient.Send(Email); // Email senden
                return true;

            }
            catch (Exception e)
            {

                log.Error("Emailhelper- SendBillAndConfirmationAnswer", e);
                return true;
            }
        }
        #endregion

    }
}