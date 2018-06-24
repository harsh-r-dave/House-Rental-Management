using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
using HouseRentalManagement.Models.AdminViewModels;
using HouseRentalManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Services
{
    public class RestrictionService : IRestrictionService
    {
        private readonly IRestrictionRepository _restrictionRepository;

        public RestrictionService(IRestrictionRepository restrictionRepository)
        {
            _restrictionRepository = restrictionRepository;
        }

        public async Task<(bool Success, IErrorDictionary Errors)> AddRestrictionAsync(ManageRestrictionViewModel model)
        {
            bool success = false;
            var errors = new ErrorDictionary();

            try
            {
                Restriction restriction = new Restriction();
                if (model.RestrictionId.HasValue && model.RestrictionId.Value > 0)
                {
                    // fetch existing restriction for update
                    restriction = await _restrictionRepository.FetchByIdAsync(model.RestrictionId.Value);
                }

                // update title/ name
                restriction.Title = model.RestrictionTitle;

                // save record
                if (await _restrictionRepository.SaveRestrictionAsync(restriction))
                {
                    success = true;
                }
            }
            catch (Exception e)
            {
            }

            return (success, errors);
        }

        public async Task<(bool Success, IErrorDictionary Errors, ManageRestrictionViewModel Model)> GetManageRestrictionViewModelAsync()
        {
            bool success = false;
            var model = new ManageRestrictionViewModel();
            var errors = new ErrorDictionary();

            try
            {
                // get list of facilities and prepare viewmodel
                var restrictions = await _restrictionRepository.ListRestrictionsAsync();
                if (restrictions != null)
                {
                    foreach (var restriction in restrictions)
                    {
                        model.ListRestrictionViewModel.Add(new ListRestrictionViewModel
                        {
                            RestrictionId = restriction.RestrictionId,
                            Title = restriction.Title
                        });
                    }
                }

                success = true;
            }
            catch (Exception e)
            {
                errors.AddError("", "Unable to fetch facilities, please try again");
            }

            return (Success: success, Errors: errors, Model: model);
        }

        public async Task<(bool Success, IErrorDictionary Errors)> DeleteRestrictionAsync(int id)
        {
            bool success = false;
            var errors = new ErrorDictionary();

            try
            {
                // fetch restriction by id
                Restriction restriction = await _restrictionRepository.FetchByIdAsync(id);

                // remove it
                if (restriction != null)
                {
                    success = await _restrictionRepository.DeleteRestrictionAsync(restriction);
                }
                else
                {
                    errors.AddError("", "Unable to locate restriction");
                }
            }
            catch (Exception e)
            {
                errors.AddError("", "Unexpected error occured while deleting restriction");
            }

            return (Success: success, Errors: errors);
        }
    }
}
