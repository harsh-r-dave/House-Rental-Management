using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
using HouseRentalManagement.Models.SiteConfigViewModels;
using HouseRentalManagement.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Services
{
    public class SiteConfigService : ISiteConfigService
    {
        private ILogger _logger;
        private readonly ISiteConfigRepository _siteConfigRepository;

        public SiteConfigService(ILogger<SiteConfigService> logger,
            ISiteConfigRepository siteConfigRepository)
        {
            _logger = logger;
            _siteConfigRepository = siteConfigRepository;
        }

        public async Task<(bool Success, AddSiteConfigViewModel Model)> GetAddSiteConfigViewModelAsync()
        {
            var success = false;
            var model = new AddSiteConfigViewModel();

            try
            {
                var siteConfig = await _siteConfigRepository.GetSiteConfigAsync();
                if (siteConfig != null)
                {
                    model.Email = siteConfig.PrimaryEmail;
                    model.WhatsappNumber = siteConfig.WhatsappNumber;
                    model.PhoneNumber = siteConfig.PhoneNumber;
                    model.IsWhatasappNumberSameAsPhoneNumber = siteConfig.PhoneNumber == siteConfig.WhatsappNumber;

                    success = true;
                }                
            }
            catch (Exception ex)
            {
                _logger.LogError("SiteConfigService/GetAddSiteConfigViewModelAsync - exception:{@Ex}", args: new object[] { ex });
            }

            return (Success: success, Model: model);
        }

        public async Task<(bool Success, string Error)> SaveSiteConfigAsync(AddSiteConfigViewModel model)
        {
            var success = false;
            string error = string.Empty;

            try
            {
                SiteConfig siteConfig = null;
                siteConfig = await _siteConfigRepository.GetSiteConfigAsync();
                if (siteConfig == null)
                {
                    siteConfig = new SiteConfig();         
                }

                siteConfig.PrimaryEmail = model.Email;
                siteConfig.PhoneNumber = model.PhoneNumber;
                siteConfig.WhatsappNumber = model.IsWhatasappNumberSameAsPhoneNumber ? model.PhoneNumber : model.WhatsappNumber;

                // save record
                success = await _siteConfigRepository.SaveSiteConfigAsync(siteConfig);
            }
            catch (Exception ex)
            {
                error = "Something went wrong while processing your request.";

                _logger.LogError("SiteConfigService/SaveSiteConfigAsync - exception:{@Ex}", args: new object[] { ex });
            }

            return (Success: success, Error: error);
        }
    }
}
