﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Domain.Entity;

namespace TaskManagement.Domain.IRepository
{
    public interface IAppUserRepository:IGenericRepository<AppUser>
    {
        public AppUser Login();
        public void Register(string username, string password);

        public string HashPassword(string password);
    }
}
