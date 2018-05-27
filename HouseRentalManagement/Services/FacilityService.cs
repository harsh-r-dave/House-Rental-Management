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
    public class FacilityService : IFacilityService
    {
        private readonly IFacilityRepository _facilityRepository;

        public FacilityService(IFacilityRepository facilityRepository)
        {
            _facilityRepository = facilityRepository;
        }

        public async Task<(bool Success, IErrorDictionary Errors)> AddFacilityAsync(ManageFacilityViewModel model)
        {
            bool success = false;
            var errors = new ErrorDictionary();

            try
            {
                Facility facility = new Facility();
                if (model.FacilityId.HasValue && model.FacilityId.Value != Guid.NewGuid())
                {
                    // fetch existing facility for update
                    facility = await _facilityRepository.FetchByIdAsync(model.FacilityId.Value);
                }

                // update title/ name
                facility.Name = model.FacilityTitle;

                // save record
                if (await _facilityRepository.SaveFacilityAsync(facility))
                {
                    success = true;
                }
            }
            catch (Exception e)
            {
            }

            return (success, errors);
        }

        public async Task<(bool Success, IErrorDictionary Errors, ManageFacilityViewModel Model)> GetManageFacilityViewModelAsync()
        {
            bool success = false;
            var model = new ManageFacilityViewModel();
            var errors = new ErrorDictionary();

            try
            {
                // get list of facilities and prepare viewmodel
                var facilities = await _facilityRepository.ListFacilitiesAsync();
                if (facilities != null)
                {
                    foreach (var facility in facilities)
                    {
                        model.ListFacilityViewModel.Add(new ListFacilityViewModel
                        {
                            FacilityId = facility.FacilityId,
                            Title = facility.Name
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

        public async Task<(bool Success, IErrorDictionary Errors)> DeleteFacilityAsync(Guid id)
        {
            bool success = false;
            var errors = new ErrorDictionary();

            try
            {
                // fetch facility by id
                Facility facility = await _facilityRepository.FetchByIdAsync(id);

                // remove it
                if (facility != null)
                {
                    success = await _facilityRepository.DeleteFacilityAsync(facility);
                }
                else
                {
                    errors.AddError("", "Unable to locate facility");
                }
            }
            catch (Exception e)
            {
                errors.AddError("", "Unexpected error occured while deleting facility");
            }

            return (Success: success, Errors: errors);
        }
    }
}
