using System;
using System.Net.Mail;
using log4net;
using System.Text;

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
                string Password = "tom362461gmx";
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
        }
        #endregion

        #region SendPasswordResetEmail
        public static bool SendPasswordResetEmail(string emailaddress, int userid, string username)
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
                Email.Body = $"<p style='font-size:20px'>Bitte klicken Sie auf den Link um ihr Passwort zurück zu setzen </br >" +
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

                Email.Subject = "Clonestone- Ihre Rechnung!";
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

        #region SendBillAndConfirmationAnswer
        public static bool SendBillAndConfirmationAnswer(string emailaddress, int renr, string username, string packname, int amountofpacks, double billoverall, int userid)
        {
            double tax = billoverall / 6;
            tax = Math.Round(tax, 2);
            try
            {
                //----------------------------GMX
                MailMessage Email = new MailMessage();
                MailAddress Sender = new MailAddress("Thomas.Faul@gmx.at");
                Email.From = Sender;
                DateTime date = DateTime.Now;
                date = date.Date;
                date.ToString("MM/dd/yyyy");
                Email.Subject = "Clonestone_ Ihre Rechnung!";
                //Email.To.Add(emailaddress);
                Email.To.Add("thomas.faul@gmx.at");
                Email.IsBodyHtml = true;
                Email.Body = string.Format("<html><body style='color:grey; font-size:8px;'><font face = 'Helvetica, Arial, sans-serif'><div style='position: absolute; height: 10px; width: 600px;background-color:rgba(129, 75, 55, 0.8); padding: 30px;'>&nbsp;</div><p><span style ='font-family: Helvetica, Arial, sans-serif;'><span style ='font-family: Helvetica,Arial, sans-serif;'><br/><br/></span></span></p><div style='background-color:papayawhip; width: 600px; height: 800px; padding: 30px; margin-top: 30px;'><p style='font-size:10px'> Ihre Rechnung:</p><br/><p>Magical TeaParty Gmbh</p><p> Zipperstrasse 56 - 59 </p><p> 1110 Wien </p><p> (+43)0664 / 1525725 </p><p> magicaltea.party.at </p ><div> Datum:{0} <br/> Rechnungsnummer:&nbsp;{1}</div><br/><p style='font-size:10px'>Lieber &nbsp;Freund von Clonestone,</p><br/><p> Hier ist deine Bestellübersicht:</p><p> Du hast vom Package &nbsp;{3}, {4} &nbsp;Stück bestellt,</p><p> Umsatzsteuer:&nbsp;{5}&nbsp; Euro,</p><p style='font-size:12px'> Gesamtbetrag ink.Mwst &nbsp;{6}&nbsp; Euro,</><p> Danke für deinen Einkauf,</p><p>&nbsp;</p><p style='font-size:10px'> Das Clonestone - Team </p><p> Bei Fragen und Reklamationen:</p><p><strong> Support:</strong><a href='mailto:Support@clonestone.at'> Support@clonestone.at</a></p><p><strong> Marketing:<a href = 'mailto:Marketing@clonestone.at'> Marketing@clonestone.com </a></strong></p><p> Gerichtsstand Wien </p></div></div></body></html>", date, renr, username, packname, amountofpacks, tax, billoverall);
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