using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common.Helper
{
    public static class Utility
    {
        //Hashed Encryption Process
        public static string Encrypt(string _txtPwd)
        {
            MD5CryptoServiceProvider MD5 = new MD5CryptoServiceProvider();
            MD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(_txtPwd));
            byte[] result = MD5.Hash;
            StringBuilder _str = new StringBuilder();
            for (int i = 1; i < result.Length; i++)
            {
                _str.Append(result[i].ToString("x2"));
            }
            return _str.ToString();
        }
        public static DateTime GetCurrentDate()
        {
            TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("Indian Standard Time");
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, INDIAN_ZONE);
        }

        #region convert List to DataTable
        //public static DataTable ToDataTable<T>(this IList<T> data)
        //{
        //    PropertyDescriptorCollection props =
        //        TypeDescriptor.GetProperties(typeof(T));
        //    DataTable table = new DataTable();
        //    for (int i = 0; i < props.Count; i++)
        //    {
        //        PropertyDescriptor prop = props[i];
        //        table.Columns.Add(prop.Name, prop.PropertyType);
        //    }
        //    object[] values = new object[props.Count];
        //    foreach (T item in data)
        //    {
        //        for (int i = 0; i < values.Length; i++)
        //        {
        //            values[i] = props[i].GetValue(item);
        //        }
        //        table.Rows.Add(values);
        //    }
        //    return table;
        //}


        //    public static bool SendEmail(string subject, string body, string toEmailId)
        //    {
        //        string fromEmaild = System.Configuration.ConfigurationManager.AppSettings["FromEmailId"].ToString();
        //        string emailPassword = ConfigurationManager.AppSettings["MailPassword"].ToString();
        //        string EmailIds = toEmailId;
        //        MailAddress from = new MailAddress("bhargavirbrr@gmail.com", fromEmaild);
        //        MailAddress to = new MailAddress(EmailIds);

        //        using (MailMessage mail = new MailMessage(from, to))
        //        {
        //            try
        //            {
        //                mail.Subject = subject;
        //                mail.Body = body;
        //                mail.IsBodyHtml = true;
        //                System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
        //                smtp.Host = ConfigurationManager.AppSettings["SMTPServer"];
        //                bool enableSSLValue = Convert.ToBoolean(ConfigurationManager.AppSettings["EnableSSL"]);
        //                smtp.EnableSsl = enableSSLValue;
        //                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
        //                smtp.UseDefaultCredentials = false;
        //                NetworkCredential networkCredential = new NetworkCredential(fromEmaild, emailPassword);
        //                smtp.Credentials = networkCredential;
        //                smtp.EnableSsl = true;
        //                smtp.Port = 587;
        //                smtp.Send(mail);
        //            }
        //            catch
        //            { throw; }
        //            return true;
        //        }
        //    }
        #endregion
    }
}
