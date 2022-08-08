using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace GangManagementSystem.API
{
    public class ErrorApi : BaseApi
    {
        private class Error_LogDto
        {
            public int ID { get; set; }
            public string App_Name { get; set; }
            public string Source { get; set; }
            public string Method { get; set; }
            public DateTime Error_Date { get; set; }
            public DateTime Error_Time { get; set; }
            public string Computer { get; set; }
            public string Error_Message { get; set; }
            public string Stack_Trace { get; set; }
        }
        public static void Log(System.Exception ex)
        {
            Log(null, ex);
        }

        public static void Log(string prefix, System.Exception ex)
        {
            if (HttpContext.Current.Items.Contains("preventRecursion") && (bool)HttpContext.Current.Items["preventRecursion"] == true)
                return;

            HttpContext.Current.Items["preventRecursion"] = true;
            string messages = GetAllMessages(ex);
            if (prefix != null)
                messages = prefix + ": " + messages;

            try
            {
                var err = new Error_LogDto()
                {
                    App_Name = LocalApplication,
                    Computer = Environment.MachineName,
                    Error_Date = DateTime.Today,
                    Error_Message = messages,
                    Error_Time = DateTime.Now,
                    Method = ex.TargetSite.Name.ToString(),
                    Source = ex.Source.Trim(),
                    Stack_Trace = ex.StackTrace.Trim()
                };
                if (PostEndpoint("Error/Log", err) == null)
                    SendErrorEmail(ex);
            }
            catch (System.Exception)
            {
                SendErrorEmail(ex);
            }

            HttpContext.Current.Items["preventRecursion"] = false;
        }

        public static void SendErrorEmail(System.Exception ex)
        {
            try
            {
                //var _message = new MailMessage
                //{
                //    IsBodyHtml = true,
                //    From = new MailAddress(Settings.Default.EngSysEmail),
                //    Subject = LocalApplication + " ERROR - " + ex.Message
                //};

                //AddAddresses(Settings.Default.AdminEmail, _message.To);
                //AddAddresses(Settings.Default.EngSysEmail, _message.Bcc);

                string user = HttpContext.Current.User.Identity.Name.ToUpper();
                user = user.Substring(user.LastIndexOf("\\") + 1);

                //_message.Body = "<html><head><style>body{font-family:\"Consolas\",\"Verdana\"; font-size: 10pt;}</style><body>";
                //_message.Body += "<b>Message:</b> <b>Error while logging exception to service.</b> " + GetAllMessages(ex) + "<br/>";
                //_message.Body += "<b>Date:</b> " + DateTime.Today.ToShortDateString() + " " + DateTime.Now.ToShortTimeString() + "<br/>";
                //_message.Body += "<b>Application:</b> " + LocalApplication + "<br/>";
                //_message.Body += "<b>Computer:</b> " + Environment.MachineName + "<br/>";
                //_message.Body += "<b>Method:</b> " + ex.TargetSite.Name + "<br/>";
                //_message.Body += "<b>Source:</b> " + ex.Source + "<br/>";
                //_message.Body += "<b>User:</b> " + user + "<br/>";
                //_message.Body += "<br/>";
                //_message.Body += "<b>Stack Trace:</b><br>";
                //_message.Body += ex.StackTrace.Replace("\r\n", "<br/>");

                //if (!string.IsNullOrEmpty(Settings.Default.OverrideEmail))
                //{
                //    _message.To.Clear();
                //    _message.Bcc.Clear();
                //    AddAddresses(Settings.Default.OverrideEmail, _message.To);

                //    _message.Subject = "[FOR TESTING ONLY] " + _message.Subject;

                //    _message.Body = _message.Body + "<br/><br/>This message would have been mailed<br/>To: " + Settings.Default.AdminEmail + "<br>BCC: " + Settings.Default.EngSysEmail;
                //}
                //else if(Settings.Default.AppLevel.ToUpper() != "P")
                //{
                //    _message.Subject = "[FOR TESTING ONLY] " + _message.Subject;
                //}

                //_message.Body += "</body></html>";

                //var _smtp = new SmtpClient(Settings.Default.SMTPServer, 25);
                //_smtp.Send(_message);

                //_message.Dispose();
                //_smtp.Dispose();

                HttpContext.Current.Items["sentErrorEmail"] = true;
            }
            catch
            {

            }
        }

        private static string GetAllMessages(System.Exception ex)
        {
            string messages = ex.Message;
            System.Exception current = ex;
            while (current.InnerException != null)
            {
                current = current.InnerException;
                messages = messages + " " + current.Message;
            }
            return messages;
        }

        private static void AddAddresses(string input, MailAddressCollection addresses)
        {
            if (!string.IsNullOrEmpty(input))
            {
                foreach (string item in input.Split(';').Where(x => !string.IsNullOrEmpty(x)))
                    addresses.Add(item.Trim());
            }
        }
    }
}