using GSES.BusinessLogic.Processors;
using GSES.BusinessLogic.Services.Interfaces;
using System.Threading.Tasks;

namespace GSES.BusinessLogic.Services
{
    public class RateService : IRateService
    {
        private readonly RateProcessor rateProcessor;

        public RateService(RateProcessor rateProcessor)
        {
            this.rateProcessor = rateProcessor;
        }

        public async Task<double> GetRateAsync()
        {
            var model = await rateProcessor.GetRateAsync();

            return model.Rate;
        }
    }
}
