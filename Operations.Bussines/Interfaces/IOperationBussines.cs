using Business.Result.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Operations.Bussines.Interfaces
{
    public interface IOperationBussines
    {
        Task<BusinessResult<List<string>>> CalculateTrip(List<int> elements);
    }
}
