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
            if (db.Enfermeiro.Any()) return;

            db.Enfermeiro.AddRange(
                
                new Enfermeiro { NumeroMecanografico = "E001", Nome = "Marisa Reduto", Contacto = "966333222", Email = "marisareduto@uls.guarda", Data_Nascimento = new DateTime(6,7,1998), CC = "15823256" },
                new Enfermeiro { NumeroMecanografico = "E002", Nome = "João Silva", Contacto = "965241232", Email = "joaosilva@uls.guarda", Data_Nascimento = new DateTime(16, 8, 1989), CC = "15852556" },
                new Enfermeiro { NumeroMecanografico = "E003", Nome = "Armando Manso", Contacto = "964521121", Email = "armandomanso@uls.guarda", Data_Nascimento = new DateTime(6, 1, 1987), CC = "13652544" },
                new Enfermeiro { NumeroMecanografico = "E004", Nome = "Andreia Cunha", Contacto = "923654152", Email = "andreiacunha@uls.guarda", Data_Nascimento = new DateTime(25, 10, 1978), CC = "14245485" }

                );

            db.SaveChanges();
        }
    }
}
