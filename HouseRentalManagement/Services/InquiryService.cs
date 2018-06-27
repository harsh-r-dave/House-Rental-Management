using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Models;
using HouseRentalManagement.Models.InquiryViewModels;
using HouseRentalManagement.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Services
{
    public class InquiryService : IInquiryService
    {
        private ILogger _logger;
        private readonly IInquiryRepository _inquiryRepository;

        public InquiryService(ILogger<InquiryService> logger,
            IInquiryRepository inquiryRepository)
        {
            _logger = logger;
            _inquiryRepository = inquiryRepository;
        }

        public async Task<(bool Success, string Error)> SubmitNewInquiryAsync(AddInquiryViewModel model)
        {
            bool success = false;
            string error = string.Empty;
            try
            {
                Inquiry inquiry = new Inquiry()
                {
                    Name = model.Name,
                    EmailAddress = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    Message = model.Message,
                    Read = false,
                    CreateDate = DateTime.Now
                };

                success = await _inquiryRepository.SaveInquiryAsync(inquiry);
            }
            catch (Exception ex)
            {
                error = "An unexpected error occured while submitting your inquiry.";

                _logger.LogError("InquiryService/SubmitNewInquiryAsync - exception:{@Ex}", new object[] { ex });
            }
            return (Success: success, Error: error);
        }

        public async Task<ListInquiryViewModel> GetReadInquiriesAsync()
        {
            var model = new ListInquiryViewModel()
            {
                Inquiries = new List<InquiryViewModel>()
            };

            try
            {
                var inquiries = await _inquiryRepository.GetReadInquiriesAsync();
                if (inquiries != null && inquiries.Count > 0)
                {
                    foreach (var item in inquiries)
                    {
                        model.Inquiries.Add(GetInquiryViewModelFromInquiry(item));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("InquiryService/SubmitNewInquiryAsync - exception:{@Ex}", new object[] { ex });
            }

            return model;
        }

        public async Task<ListInquiryViewModel> GetUnreadInquiriesAsync()
        {
            var model = new ListInquiryViewModel()
            {
                Inquiries = new List<InquiryViewModel>()
            };

            try
            {
                var inquiries = await _inquiryRepository.GetUnreadInquiriesAsync();
                if (inquiries != null && inquiries.Count > 0)
                {
                    foreach (var item in inquiries)
                    {
                        model.Inquiries.Add(GetInquiryViewModelFromInquiry(item));
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("InquiryService/GetUnreadInquiriesAsync - exception:{@Ex}", new object[] { ex });
            }

            return model;
        }

        private InquiryViewModel GetInquiryViewModelFromInquiry(Inquiry inquiry)
        {
            var model = new InquiryViewModel();

            model.InquiryId = inquiry.InquiryId;
            model.Email = inquiry.EmailAddress;
            model.PhoneNumber = inquiry.PhoneNumber;
            model.Name = inquiry.Name;
            model.Message = inquiry.Message;
            model.IsRead = inquiry.Read;
            model.InquiryDate = inquiry.CreateDate;

            return model;
        }

        public async Task<(bool Success, string Error, string Message, bool IsRead)> GetMessageByIdAsync(int id)
        {
            bool success = false;
            string error = string.Empty;
            string message = string.Empty;
            bool isRead = false;

            try
            {
                if (id > 0)
                {
                    var inquiry = await _inquiryRepository.GetInquiryByIdAsync(id);
                    if (inquiry != null)
                    {
                        message = inquiry.Message;
                        isRead = inquiry.Read;
                        success = true;
                    }
                    else
                    {
                        error = "Unable to find message for requested inquiry, please contact IT support";
                    }
                }
                else
                {
                    error = "Invalid inquiry id";
                }
            }
            catch (Exception ex)
            {
                error = "Something went wrong while processing your request.";
                _logger.LogError("InquiryService/GetUnreadInquiriesAsync - exception:{@Ex}", new object[] { ex });
            }

            return (Success: success, Error: error, Message: message, IsRead: isRead);
        }

        public async Task<(bool Success, string Error)> MarkMessageReadByIdAsync(int id)
        {
            bool success = false;
            string error = string.Empty;

            try
            {
                if (id > 0)
                {
                    var inquiry = await _inquiryRepository.GetInquiryByIdAsync(id);
                    if (inquiry != null)
                    {
                        inquiry.Read = true;
                        success = await _inquiryRepository.SaveInquiryAsync(inquiry);
                    }
                    else
                    {
                        error = "Unable to find inquiry, please contact IT support";
                    }
                }
                else
                {
                    error = "Invalid inquiry id";
                }
            }
            catch (Exception ex)
            {
                error = "Something went wrong while processing your request.";
                _logger.LogError("InquiryService/MarkMessageReadByIdAsync - exception:{@Ex}", new object[] { ex });
            }

            return (Success: success, Error: error);
        }

        public async Task<(bool Success, string Error)> DeleteInquiryByIdAsync(int id)
        {
            bool success = false;
            string error = string.Empty;

            try
            {
                if (id > 0)
                {
                    var inquiry = await _inquiryRepository.GetInquiryByIdAsync(id);
                    if (inquiry != null)
                    {
                        success = await _inquiryRepository.DeleteAsync(inquiry);
                    }
                    else
                    {
                        error = "Unable to find inquiry, please contact IT support";
                    }
                }
                else
                {
                    error = "Invalid inquiry id";
                }
            }
            catch (Exception ex)
            {
                error = "Something went wrong while processing your request.";
                _logger.LogError("InquiryService/DeleteInquiryByIdAsync - exception:{@Ex}", new object[] { ex });
            }

            return (Success: success, Error: error);
        }

        public async Task<(bool Success, string Error)> DeleteAllUnreadInquiryAsync()
        {
            bool success = false;
            string error = string.Empty;

            try
            {
                success = await _inquiryRepository.DeleteAllUnreadInquiriesAsync();

            }
            catch (Exception ex)
            {
                error = "Something went wrong while processing your request.";
                _logger.LogError("InquiryService/DeleteInquiryByIdAsync - exception:{@Ex}", new object[] { ex });
            }

            return (Success: success, Error: error);
        }

        public async Task<(bool Success, string Error)> DeleteAllReadInquiryAsync()
        {
            bool success = false;
            string error = string.Empty;

            try
            {
                success = await _inquiryRepository.DeleteAllReadInquiriesAsync();

            }
            catch (Exception ex)
            {
                error = "Something went wrong while processing your request.";
                _logger.LogError("InquiryService/DeleteInquiryByIdAsync - exception:{@Ex}", new object[] { ex });
            }

            return (Success: success, Error: error);
        }
    }
}
