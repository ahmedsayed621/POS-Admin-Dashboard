using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template.BL.Models;
using System.Net;
using System.Net.Mail;

namespace Template.BL.Helper
{
    public static class SendMailHelper
    {
        public static string MailSender(MailVM model)
        {
            try
            {

                using (var smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.EnableSsl = true;
                    smtp.Credentials = new NetworkCredential("elgendya160@gmail.com", "@@@AAA321_321");
                    smtp.Send("elgendya160@gmail.com", model.Email, model.Title, model.Message);
                }


                return "Mail Sent Successfully";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
