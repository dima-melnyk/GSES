using GSES.BusinessLogic.Consts;
using GSES.BusinessLogic.Extensions;
using GSES.BusinessLogic.Services.Interfaces;
using GSES.DataAccess.Entities;
using GSES.DataAccess.Repositories.Interfaces;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace GSES.BusinessLogic.Services
{
    public class SubscriberService : ISubscriberService
    {
        private readonly ISubscriberRepository subscriberRepository;
        private readonly SmtpClient smtpClient;

        public SubscriberService(ISubscriberRepository subscriberRepository, SmtpClient smtpClient)
        {
            this.subscriberRepository = subscriberRepository;
            this.smtpClient = smtpClient;
        }

        public Task AddSubscriberAsync(Subscriber subscriber) => this.subscriberRepository.AddAsync(subscriber);

        public async Task SendRateForAllTheSubscribers()
        {
            var subscribers = await this.subscriberRepository.GetAsync();
            var emails = subscribers.Select(s => s.Email);

            var subject = string.Format(EmailConsts.EmailSubject, RateConsts.BitcoinCode, RateConsts.HryvnyaCode);
            var body = string.Format(EmailConsts.EmailBody, RateConsts.BitcoinCode, RateConsts.HryvnyaCode);

            await this.smtpClient.SendMessagesOnEmails(emails, subject, body);
        }
    }
}
