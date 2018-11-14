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

                SeedMedicos(db);
                SeedMedicoEspecialidade(db);
            }
        }

        private static void SeedMedicoEspecialidade(HospitalDbContext db)
        {
            if (db.MedicoEspecialidades.Any()) return;
            Medico manuel = db.Medicos.SingleOrDefault(e => e.Nome == "Manuel Santos");
            Medico elisabete = db.Medicos.SingleOrDefault(e => e.Nome == "Elisabete Eiras");
           

            if (manuel == null)
            {
                manuel = new Medico {
                    NumeroMecanografico = "M001",
                    Nome = "Manuel Santos",
                    Email = "manuelsantos@uls.guarda.com",
                    Contacto = "936571245",
                    CC = "15851657",
                    Data_Nascimento = new DateTime(1987, 6, 29),
                };
                db.Medicos.Add(manuel);
                db.SaveChanges();
            }

            if (elisabete == null)
            {
                elisabete = new Medico {
                    NumeroMecanografico = "M002",
                    Nome = "Elisabete Eiras",
                    Email = "elisabeteeiras@uls.guarda.com",
                    Contacto = "925641937",
                    CC = "16457832",
                    Data_Nascimento = new DateTime(1985, 4, 2),
                };
                db.Medicos.Add(elisabete);
                db.SaveChanges();
            }

            db.MedicoEspecialidades.AddRange(

                new MedicoEspecialidade { Nome = "Patologia", MedicoId = manuel.MedicoId },
                new MedicoEspecialidade { Nome = "Oncologia", MedicoId = elisabete.MedicoId }
                );

            db.SaveChanges();
        }

        private static void SeedMedicos(HospitalDbContext db)
        {
            if (db.Medicos.Any()) return;
            db.Medicos.AddRange(
                       new Medico
                       {
                           NumeroMecanografico = "M001",
                           Nome = "Manuel Santos",
                           Email = "manuelsantos@uls.guarda.com",
                           Contacto = "936571245",
                           CC = "15851657",
                           Data_Nascimento = new DateTime(1987, 6, 29),
                       },
                       new Medico
                       {
                           NumeroMecanografico = "M002",
                           Nome = "Elisabete Eiras",
                           Email = "elisabeteeiras@uls.guarda.com",
                           Contacto = "925641937",
                           CC = "16457832",
                           Data_Nascimento = new DateTime(1985, 4, 2),
                       }
                   );
            db.SaveChanges();

        }

        private static void SeedEnfermeiroEspecialidade(HospitalDbContext db)
        {
            if (db.EnfermeiroEspecialidades.Any()) return;

            /***/
            Enfermeiro marisa = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Marisa Reduto");
            Enfermeiro joao = db.Enfermeiros.SingleOrDefault(e => e.Nome == "João Silva");
            Enfermeiro armando = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Armando Manso");
            Enfermeiro andreia = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Andreia Cunha");

            if (marisa == null)
            {
                marisa = new Enfermeiro { NumeroMecanografico = "E001", Nome = "Marisa Reduto", Contacto = "966333222", Email = "marisareduto@uls.guarda", Data_Nascimento = new DateTime(1998, 6, 6), CC = "15823256" };
                db.Enfermeiros.Add(marisa);
                db.SaveChanges();
            }

            if (joao == null)
            {
                joao = new Enfermeiro { NumeroMecanografico = "E002", Nome = "João Silva", Contacto = "965241232", Email = "joaosilva@uls.guarda", Data_Nascimento = new DateTime(1989, 8, 16), CC = "15852556" };
                db.Enfermeiros.Add(joao);
                db.SaveChanges();
            }

            if (armando == null)
            {
                armando = new Enfermeiro { NumeroMecanografico = "E003", Nome = "Armando Manso", Contacto = "964521121", Email = "armandomanso@uls.guarda", Data_Nascimento = new DateTime(1987, 7, 1), CC = "13652544" };
                db.Enfermeiros.Add(armando);
                db.SaveChanges();
            }

            if (andreia == null)
            {
                andreia = new Enfermeiro { NumeroMecanografico = "E004", Nome = "Andreia Cunha", Contacto = "923654152", Email = "andreiacunha@uls.guarda", Data_Nascimento = new DateTime(1978, 10, 25), CC = "14245485" };
                db.Enfermeiros.Add(andreia);
                db.SaveChanges();
            }           
            /***/

            db.EnfermeiroEspecialidades.AddRange(
                
                new EnfermeiroEspecialidade { Nome = "Enfermagem Comunitária", EnfermeiroId = marisa.EnfermeiroId},
                new EnfermeiroEspecialidade { Nome = "Enfermagem Médico-Cirúrgica", EnfermeiroId = joao.EnfermeiroId },
                new EnfermeiroEspecialidade { Nome = "Enfermagem de Reabilitação", EnfermeiroId = armando.EnfermeiroId},
                new EnfermeiroEspecialidade { Nome = "Enfermagem de Reabilitação", EnfermeiroId = andreia.EnfermeiroId }
                );

            db.SaveChanges();
        }

        private static void SeedDiretorServico(HospitalDbContext db)
        {
            if (db.DiretorServico.Any()) return;

            db.DiretorServico.AddRange(

                new DiretorServico { Name = "António Barata", NumeroMecanografico = "D001", Contacto = "965417847", Email = "antoniobarata@uls.guarda", CC = "12547845", Morada = "Guarda" }

                );

            db.SaveChanges();
        }

        private static void SeedEnfermeiros(HospitalDbContext db)
        {
            if (db.Enfermeiros.Any()) return;

            db.Enfermeiros.AddRange(
                
                new Enfermeiro { NumeroMecanografico = "E001", Nome = "Marisa Reduto", Contacto = "966333222", Email = "marisareduto@uls.guarda", Data_Nascimento = new DateTime(1998,6,6) , CC = "15823256" },
                new Enfermeiro { NumeroMecanografico = "E002", Nome = "João Silva", Contacto = "965241232", Email = "joaosilva@uls.guarda", Data_Nascimento = new DateTime(1989,8,16), CC = "15852556" },
                new Enfermeiro { NumeroMecanografico = "E003", Nome = "Armando Manso", Contacto = "964521121", Email = "armandomanso@uls.guarda", Data_Nascimento = new DateTime(1987,7,1), CC = "13652544" },
                new Enfermeiro { NumeroMecanografico = "E004", Nome = "Andreia Cunha", Contacto = "923654152", Email = "andreiacunha@uls.guarda", Data_Nascimento = new DateTime(1978,10,25), CC = "14245485" }            

                );

            db.SaveChanges();
        }
    }
}
