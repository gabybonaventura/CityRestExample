using System;
using System.Diagnostics;

namespace TodoApi.Services
{
    public class CloudMailService : IMailService
    {
        private string _mailTo = "gbonaventura@x-28.com";
        private string _mailFrom = "gabybonaventura@hotmail.com";

        void IMailService.Send(string subject, string messenge)
        {
            Debug.WriteLine($"Mail from {_mailFrom} to {_mailTo}, with CloudMailServices");
            Debug.WriteLine($"Subject: {subject}");
            Debug.WriteLine($"Messenge: {messenge}");
        }
    }
}
