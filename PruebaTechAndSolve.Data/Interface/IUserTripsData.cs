using PruebaTechAndSolve.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTechAndSolve.Data.Interface
{
    public interface IUserTripsData
    {
        Task<UserTripsDto> CreateAsync(UserTripsDto usertripsdto);
    }
}
