using GSES.DataAccess.Entities;
using System.Threading.Tasks;

namespace GSES.BusinessLogic.Services.Interfaces
{
    public interface ISubscriberService
    {
        Task AddSubscriberAsync(Subscriber subscriber);

        Task SendRateForAllTheSubscribers();
    }
}
