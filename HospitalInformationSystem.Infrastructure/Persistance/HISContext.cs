using HospitalInformationSystem.Application.Common.Interfaces;
using HospitalInformationSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalInformationSystem.Infrastructure.Persistance
{
    public class HISContext : DbContext, IHISContext
    {
        public HISContext(DbContextOptions<HISContext> options) : base(options) { }
        public DbSet<Patient> Patients { get; set; }
    }
}
