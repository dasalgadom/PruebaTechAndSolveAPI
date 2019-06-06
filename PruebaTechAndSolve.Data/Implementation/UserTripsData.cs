using AutoMapper;
using PruebaTechAndSolve.Data.Interface;
using PruebaTechAndSolve.Dto;
using PruebaTechAndSolve.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PruebaTechAndSolve.Data.Implementation
{
    public class UserTripsData : IUserTripsData
    {
        protected PruebaEntities _context;

        public UserTripsData()
        {
            _context = new PruebaEntities();
        }

        public async Task<UserTripsDto> CreateAsync(UserTripsDto usertripsdto)
        {
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<UserTripsDto, UserTrips>());
            var entity = Mapper.Map<UserTripsDto, UserTrips>(usertripsdto);
            _context.UserTrips.Add(entity);
            await _context.SaveChangesAsync();
            Mapper.Reset();
            Mapper.Initialize(cfg => cfg.CreateMap<UserTrips, UserTripsDto>());
            var result = Mapper.Map<UserTrips, UserTripsDto>(entity);
            return result;
        }
    }
}
