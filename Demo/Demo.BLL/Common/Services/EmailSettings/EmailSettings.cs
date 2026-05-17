using Demo.DAL.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Common.Services.EmailSettings
{
    public class EmailSettings : IEmailSettings
    {
        public void SendMail(Email email)
        {
            var client = new SmtpClient("smtp.gmail.com", 587);
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("farahmohmedofficial@gmail.com", "dizojqvdtkhkkdks"); //Google generates password // add your Credentials
            client.Send("farahmohmedofficial@gmail.com", email.TO, email.Subject, email.Body);                                              



        }
    }
}
