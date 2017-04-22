using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

namespace CardGame.Web.HtmlHelpers
{
    public class EmailHelper
    {
        public static bool SendEmail(string emailaddress, string emailsubject, string text )
        {
            MailMessage Email = new MailMessage();
            MailAddress Sender = new MailAddress("thomas.faul@gmx.at");
            Email.From = Sender;
            Email.To.Add(emailaddress);
            Email.Subject = emailsubject;

            Email.Body = text;

            string ServerName = "mail.gmx.net";
            string Port = "587";

            SmtpClient MailClient = new SmtpClient(ServerName, int.Parse(Port)); // Postausgangsserver definieren

            string UserName = "thomas.faul@gmx.at";
 
            string Password ="tom362461gmx";
            System.Net.NetworkCredential Credentials = new System.Net.NetworkCredential(UserName, Password);
            MailClient.Credentials = Credentials; // Anmeldeinformationen setzen*/
            MailClient.Send(Email); // Email senden
            return true;

        }
    }
}