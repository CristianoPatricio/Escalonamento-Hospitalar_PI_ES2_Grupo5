using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public static class SeedDataEnfermeiros
    {
        public static void Populate(IServiceProvider appServices)
        {
            using (var serviceScope = appServices.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<HospitalDbContext>();

                if (db.Enfermeiros.Any()) return;
    
                db.Enfermeiros.AddRange(

                    new Enfermeiros
                    {
                        EnfermeiroID = 1,
                        NumeroMecanografico = "E001",
                        Nome = "Marisa Reduto",
                        Especialidade = "Enfermagem",
                        Contacto = "960000342",
                        Email = "marisareduto@uls.guarda.com",
                       Data_Nascimento = new DateTime(1998, 6, 8),
                        CC = "15896325",
                        EspecialidadeId = 1
                    },

                    new Enfermeiros
                    {
                        EnfermeiroID = 2,
                        NumeroMecanografico = "E002",
                        Nome = "Eduardo Cabrita",
                        Especialidade = "Enfermagem",
                        Contacto = "960000342",
                        Email = "eduardocabrita@uls.guarda.com",
                        Data_Nascimento = new DateTime(1998, 6, 8),
                        CC = "15896325",
                        EspecialidadeId = 1
                    }
                    );

                if (db.DiretorServico.Any()) return;

                db.DiretorServico.AddRange(

                    new DiretorServico
                    {
                        DiretorServicoID = 1,
                        Name = "Cristiano",
                        NumeroMecanografico = "D001",
                        Contacto = "968233322",
                        Email = "diretor@gmail.com",
                        CC = "15236325",
                        Morada = "Rua Principal"
                    }
                );
                db.SaveChanges();
            }
        }
    }
}
