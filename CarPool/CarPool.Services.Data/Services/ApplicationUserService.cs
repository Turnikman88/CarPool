using AutoMapper;
using CarPool.Data;
using CarPool.Data.Models.DatabaseModels;
using CarPool.Services.Mapping.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarPool.Services.Data.Services
{
    public class ApplicationUserService
    {
        private readonly CarPoolDBContext _db;
        private readonly IMapper _mapper;

        public ApplicationUserService(CarPoolDBContext db, IMapper mapper)
        {
            this._db = db;
            _mapper = mapper;
        }
        public void test()
        {
            var a = this._mapper.Map<ApplicationUserDTO>(new ApplicationUser { });
        }
    }
}
