using HouseRentalManagement.Classes;
using HouseRentalManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Services
{
    public class ErrorDictionary : IErrorDictionary
    {
        public ErrorDictionary()
        {
            ErrorMessages = new List<ErrorMessage>();
        }

        private List<ErrorMessage> ErrorMessages { get; set; }

        public void AddError(string key, string errorMessage)
        {
            ErrorMessages.Add(new ErrorMessage { Key = key, Description = errorMessage });
        }

        public List<ErrorMessage> GetErrors()
        {
            return ErrorMessages;
        }

        public bool HasErrors => ErrorMessages.Count > 0;
    }
}
