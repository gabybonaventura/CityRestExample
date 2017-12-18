using System;
namespace TodoApi.Services
{
    public interface IMailService
    {
        void Send(string subject, string messenge);
    }
}
