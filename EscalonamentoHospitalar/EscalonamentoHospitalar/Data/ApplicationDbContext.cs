using System;
using System.Collections.Generic;
using System.Text;
using EscalonamentoHospitalar.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EscalonamentoHospitalar.Data
{
    public class HospitalUsersDataBase : IdentityDbContext
    {
        public HospitalUsersDataBase()
        {
        }

        public HospitalUsersDataBase(DbContextOptions<HospitalUsersDataBase> options)
            : base(options)
        {
        }

    }
}
