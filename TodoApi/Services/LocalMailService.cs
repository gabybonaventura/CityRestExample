using System;
using System.Diagnostics;

namespace TodoApi.Services
{
    public class LocalMailService : IMailService
    {
        private string _mailTo = Startup.Configuration["mailSettings:mailToAddress"];
        private string _mailFrom = Startup.Configuration["mailSettings:mailFromAddress"];

        public void Send(string subject, string messenge)
        {
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with LocalMailServices");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Messenge: {messenge}");
        }
    }
}
