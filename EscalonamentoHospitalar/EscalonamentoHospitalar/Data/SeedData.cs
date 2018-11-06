using EscalonamentoHospitalar.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Data
{
    public static class SeedData
    {
        internal static void Populate(IServiceProvider applicationServices)
        {
            using (var serviceScope = applicationServices.CreateScope())
            {

                var db = serviceScope.ServiceProvider.GetService<HospitalDbContext>();

                SeedEnfermeiros(db);
                SeedDiretorServico(db);
                SeedEnfermeiroEspecialidade(db);
            }
        }

        private static void SeedEnfermeiroEspecialidade(HospitalDbContext db)
        {
            
        }

        private static void SeedDiretorServico(HospitalDbContext db)
        {
            
        }

        private static void SeedEnfermeiros(HospitalDbContext db)
        {
           
        }
    }
}
