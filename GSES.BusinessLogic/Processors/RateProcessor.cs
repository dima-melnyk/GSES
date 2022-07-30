﻿using GSES.BusinessLogic.Consts;
using GSES.BusinessLogic.Extensions;
using GSES.BusinessLogic.Models.Rate;
using GSES.BusinessLogic.Processors.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace GSES.BusinessLogic.Processors
{
    public class RateProcessor : IRateProcessor
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public RateProcessor(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task<BaseRateModel> GetRateAsync()
        {
            httpClient.DefaultRequestHeaders.Add(RateConsts.KeyHeaderName, configuration[RateConsts.ConfigApiKey]);

            var url = string.Format(RateConsts.HttpDomain, RateConsts.BitcoinCode, RateConsts.HryvnyaCode);
            return await httpClient.GetModelFromRequest<CoinApiRateModel>(url);
        }
    }
}
