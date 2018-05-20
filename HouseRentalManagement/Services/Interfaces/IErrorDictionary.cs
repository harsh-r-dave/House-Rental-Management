using HouseRentalManagement.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HouseRentalManagement.Services.Interfaces
{
    public interface IErrorDictionary
    {
        void AddError(string key, string errorMessage);
        List<ErrorMessage> GetErrors();
        bool HasErrors { get; }
    }
}
