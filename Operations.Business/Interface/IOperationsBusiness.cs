using Business.Result.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Operations.Business.Interface
{
    public interface IOperationsBusiness
    {
        Task<BusinessResult<bool>> CalculateTrip(List<int> elements);
        Task<BusinessResult<bool>> GetElementsDay(List<int> elements);
    }
}
