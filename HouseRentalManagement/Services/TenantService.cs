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
                            HouseAddress = tenant.House?.AddressLine1
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

        public async Task<(bool Success, IErrorDictionary Errors)> DeleteTenantAsync(Guid id)
        {
            bool success = false;
            var errors = new ErrorDictionary();

            try
            {
                //// fetch facility by id
                //House house = await _houseRepository.FetchHouseByIdAsync(id);

                //// remove it
                //if (house != null)
                //{
                //    success = await _houseRepository.DeleteHouseAsync(house);
                //}
                //else
                //{
                //    errors.AddError("", "Unable to locate house");
                //}
            }
            catch (Exception e)
            {
                errors.AddError("", "Unexpected error occured while deleting facility");
            }

            return (Success: success, Errors: errors);
        }

        public async Task<(bool Success, IErrorDictionary Errors)> EditTenantAsync(AddTenantViewModel model)
        {
            bool success = false;
            var errors = new ErrorDictionary();

            try
            {
                //if (model != null)
                //{
                //    House house = await _houseRepository.FetchHouseByIdAsync(model.HouseId);
                //    if (house != null)
                //    {
                //        house.AddressLine1 = model.AddressLine1;
                //        house.AddressLine2 = model.AddressLine2;
                //        house.City = model.City;
                //        house.PostalCode = model.PostalCode;
                //        house.Province = model.Province;
                //        house.Country = model.Country;
                //        house.AvailableFrom = model.DateAvailableFrom ?? DateTime.Now;
                //        house.AvailableTo = model.DateAvailableTo ?? DateTime.Now.AddMonths(4);
                //        house.Rent = model.Rent ?? 0;
                //        house.Description = model.Description;
                //        house.ParakingSpace = model.ParkingSpace;
                //        house.AuditUtc = DateTime.UtcNow;
                //    }

                //    // save record

                //    if (await _houseRepository.UpdateHouseAsync(house))
                //    {
                //        success = true;
                //    }
                //    else
                //    {
                //        errors.AddError("", "Unexpected error occured while saving data");
                //    }
                //}
                //else
                //{
                //    errors.AddError("", "Invalid data submitted");
                //}
            }
            catch (Exception e)
            {
                errors.AddError("", "Unexpected error occured while saving data");
            }

            return (Success: success, Errors: errors);
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
    }
}
