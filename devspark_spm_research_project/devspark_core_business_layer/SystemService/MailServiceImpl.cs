using devspark_core_business_layer.SystemService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace devspark_core_business_layer.SystemService
{
    public class MailServiceImpl: IMailService
    {
        public MailServiceImpl()
        {

        }

        public async Task<bool> SendGoogleMail(string receiverMail, string mailSubject, string mailBody)
        {
            bool status = false;
            string senderEmail = "thirangamicrosoft@gmail.com";
            string senderPassword = "svxg mlqh bavo wtfh";

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential(senderEmail, senderPassword),
                EnableSsl = true,
            };

            var mail = new MailMessage
            {
                From = new MailAddress(senderEmail, "DevSpark"),
                Subject = mailSubject,
                Body = mailBody,
                IsBodyHtml = true
            };

            mail.To.Add("malshan.rathnayake@iits.biz");
            mail.To.Add(receiverMail);

            try
            {
                smtpClient.Send(mail);
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }

            return status;
        }
    }
}
