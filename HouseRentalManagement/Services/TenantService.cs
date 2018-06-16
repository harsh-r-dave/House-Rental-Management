using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
using HouseRentalManagement.Models.AdminViewModels;
using HouseRentalManagement.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace HouseRentalManagement.Services
{
    public class TenantService : ITenantService
    {
        private readonly ILogger _logger;
        private readonly ITenantRepository _tenantRepository;

        public TenantService(ILogger<TenantService> logger,
            ITenantRepository tenantRepository)
        {
            _logger = logger;
            _tenantRepository = tenantRepository;
        }

        public async Task<(bool Success, ListTenantViewModel Model)> ListTenantsAsync()
        {
            bool success = false;
            var model = new ListTenantViewModel();

            try
            {
                // get all the tenants
                var tenants = await _tenantRepository.ListTenantsAsync();
                if (tenants != null)
                {
                    foreach (var tenant in tenants)
                    {
                        var parts = new string[] { tenant.LastName, tenant.FirstName };
                        var fullName = string.Join(",", parts.Where(a => !string.IsNullOrEmpty(a)));
                        model.TenantCollection.Add(new AddTenantViewModel()
                        {
                            HouseId = tenant.HouseId,
                            TenantId = tenant.TenantId,
                            FullName = fullName,
                            PhoneNumber = tenant.PhoneNumber,
                            ReferenceName = tenant.ReferenceName,
                            ReferencePhone = tenant.ReferencePhone,
                            HouseAddress = tenant.House?.AddressLine1,
                            IsOnWaitList = tenant.IsOnWaitList
                        });
                    }
                }

                success = true;
            }
            catch (Exception ex)
            {
                _logger.LogError("TenantService/ListTenantsAsync - Exception:{@Ex}", new object[] { ex });
            }

            return (Success: success, Model: model);
        }

        public async Task<(bool Success, IErrorDictionary Errors, Guid Id)> AddTenantAsync(AddTenantViewModel model)
        {
            bool success = false;
            var errors = new ErrorDictionary();
            var id = Guid.NewGuid();

            try
            {
                if (model != null)
                {
                    // prepare tenant
                    Tenant tenant = null;
                    if (model.TenantId != Guid.Empty)
                    {
                        tenant = await _tenantRepository.FetchTenantByIdAsync(model.TenantId);
                    }
                    if (tenant == null)
                    {
                        tenant = new Tenant();
                    }
                    tenant.FirstName = model.FirstName;
                    tenant.LastName = model.LastName;
                    tenant.Email = model.Email;
                    tenant.PhoneNumber = model.PhoneNumber;
                    tenant.Occupation = model.Occupation;
                    tenant.ReferenceName = model.ReferenceName;
                    tenant.ReferencedEmail = model.ReferenceEmail;
                    tenant.ReferencePhone = model.ReferencePhone;
                    tenant.HouseId = model.HouseId ?? null;
                    tenant.IsOnWaitList = model.IsOnWaitList;

                    // save record
                    var result = await _tenantRepository.AddTenantAsync(tenant);
                    if (result.Success)
                    {
                        id = result.id;
                        success = true;
                    }
                    else
                    {
                        errors.AddError("", "Unexpected error occured while saving data");
                    }
                }
                else
                {
                    errors.AddError("", "Invalid data submitted");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("TenantService/ListTenantsAsync - Exception:{@Ex}", new object[] { ex });
            }

            return (Success: success, Errors: errors, Id: id);
        }

        public async Task<(bool Success, IErrorDictionary Errors, AddTenantViewModel Model)> GetEditTenantViewModelAsync(Guid id)
        {
            var success = false;
            var errors = new ErrorDictionary();
            var model = new AddTenantViewModel();

            try
            {
                // fetch tenant by id
                if (id != Guid.Empty)
                {
                    Tenant tenant = await _tenantRepository.FetchTenantByIdAsync(id);
                    if (tenant != null)
                    {
                        // prepare viewemodel
                        model.FirstName = tenant.FirstName;
                        model.LastName = tenant.LastName;
                        model.Email = tenant.Email;
                        model.PhoneNumber = tenant.PhoneNumber;
                        model.Occupation = tenant.Occupation;
                        model.ReferenceEmail = tenant.ReferencedEmail;
                        model.ReferenceName = tenant.ReferenceName;
                        model.ReferencePhone = tenant.ReferencePhone;
                        model.HouseId = tenant.HouseId;
                        model.TenantId = id;
                        model.IsOnWaitList = tenant.IsOnWaitList;
                    }
                    else
                    {
                        errors.AddError("", "Unable to locate the tenant details");
                    }

                    // set success
                    success = true;
                }
            }
            catch (Exception)
            {
                errors.AddError("", "Unexpected error occurred while processing your request");
            }

            return (Success: success, Errors: errors, Model: model);
        }       

        public async Task<(bool Success, String Error, bool NoTenants, ListTenantViewModel Model)> FetchHouseTenantListAsync(Guid houseId)
        {
            bool success = false;
            var error = string.Empty;
            var model = new ListTenantViewModel()
            {
                TenantCollection = new List<AddTenantViewModel>()
            };
            bool noTenants = false;

            try
            {
                if (houseId != Guid.NewGuid())
                {
                    var houseTenants = await _tenantRepository.FetchTenantsListByHouseIdAsync(houseId);
                    if (houseTenants != null && houseTenants.Any())
                    {
                        foreach (var tenant in houseTenants)
                        {
                            var parts = new string[] { tenant.LastName, tenant.FirstName };
                            var fullName = string.Join(",", parts.Where(a => !string.IsNullOrEmpty(a)));

                            model.TenantCollection.Add(new AddTenantViewModel()
                            {
                                FullName = fullName,
                                PhoneNumber = tenant.PhoneNumber,
                                Occupation = tenant.Occupation,
                                ReferenceName = tenant.ReferenceName,
                                ReferencePhone = tenant.ReferencePhone,
                                TenantId = tenant.TenantId
                            });
                        }

                        success = true;                        
                    }
                    else
                    {
                        noTenants = true;
                        error = "This house doesn't have any tenants.";
                    }
                }
                else
                {
                    error = "Invalid house Id";
                }
            }
            catch (Exception)
            {
                error = "Unexpected error occurred while processing your request";
            }

            return (Success: success, Error: error, NoTenants: noTenants, Model: model);
        }

        public async Task<(bool Success, string Error)> AddTenantToHouseAsync(Guid houseId, Guid tenantId)
        {
            bool success = false;
            string error = string.Empty;

            try
            {
                if (houseId != Guid.Empty && tenantId != Guid.Empty)
                {
                    // fetch tenant
                    var tenant = await _tenantRepository.FetchTenantByIdAsync(tenantId);
                    if (tenant != null)
                    {
                        tenant.HouseId = houseId;
                        tenant.IsOnWaitList = false;
                        success = await _tenantRepository.UpdateTenantAsync(tenant);
                    }
                    else
                    {
                        error = "Unable to locate tenant info";
                    }
                }
                else
                {
                    error = "Invalid Id";
                }
            }
            catch (Exception ex)
            {
                error = "Unexpected error occurred while processing your request";
                _logger.LogError("TenantService/AddTenantToHouseAsync - exception:{@Ex}", new object[] { ex });
            }

            return (success, error);
        }

        public async Task<ICollection<TenantDropdownViewModel>> GetTenantListForHouseEditPageAsync(Guid houseId)
        {
            ICollection<TenantDropdownViewModel> model = new List<TenantDropdownViewModel>();

            try
            {
                model = await _tenantRepository.FetchTenantListForHouseEditPageAsync(houseId);
            }
            catch (Exception ex)
            {
                _logger.LogError("TenantService/GetTenantListForHouseEditPageAsync - exception:{@Ex}", new object[] { ex });
            }
            return model;
        }

        public async Task<ICollection<TenantDropdownViewModel>> GetTenantWaitListDropdownAsync()
        {
            ICollection<TenantDropdownViewModel> model = new List<TenantDropdownViewModel>();

            try
            {
                model = await _tenantRepository.GetTenantWaitListDropdownAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError("TenantService/GetTenantWaitListDropdownAsync - exception:{@Ex}", new object[] { ex });
            }
            return model;
        }

        public async Task<(bool Success, string Error)> RemoveTenantFromHouseAsync(Guid tenantId)
        {
            bool success = false;
            string error = string.Empty;
            try
            {
                if (tenantId != Guid.Empty)
                {
                    var tenant = await _tenantRepository.FetchTenantByIdAsync(tenantId);
                    if (tenant != null)
                    {
                        tenant.HouseId = null;
                        success = await _tenantRepository.UpdateTenantAsync(tenant);
                    }
                }
                else
                {
                    error = "Invalid Id";
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("TenantService/RemoveTenantFromHouseAsync - exception:{@Ex}", new object[] { ex });
            }
            return (Success: success, Error: error);
        }

        public async Task<(bool Success, IErrorDictionary Errors)> DeleteTenantAsync(Guid id)
        {
            bool success = false;
            var errors = new ErrorDictionary();

            try
            {
                // fetch tenant
                Tenant tenant = await _tenantRepository.FetchTenantByIdAsync(id);

                // remove it
                if (tenant != null)
                {
                    success = await _tenantRepository.DeleteTenantAsync(tenant);
                }
                else
                {
                    errors.AddError("", "Unable to locate tenant");
                }
            }
            catch (Exception ex)
            {
                errors.AddError("", "Unexpected error occured while deleting facility");
                _logger.LogError("TenantService/DeleteTenantAsync - exception:{@Ex}", new object[] { ex });
            }

            return (Success: success, Errors: errors);
        }
    }
}
