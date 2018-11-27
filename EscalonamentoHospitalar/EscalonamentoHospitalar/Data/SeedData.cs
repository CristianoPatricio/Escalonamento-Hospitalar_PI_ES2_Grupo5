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

                SeedPacientes(db);
                SeedGrau(db);

                SeedPatologia(db);

                SeedRegime(db);

                SeedEstado(db);

                SeedTratamentos(db);

                SeedRegras(db);


                
                
            }
        }

       private static void SeedRegras(HospitalDbContext db)
        {
         if (db.Regras.Any()) return;
         db.Regras.AddRange(

             new Regra
                {

                    RegrasEscalonamento ="HorasEnfermeirosDia",
                    Numero = 8,

                },

                 new Regra
                 {


                     RegrasEscalonamento = "HorasEnfermeirosSemana",
                     Numero = 35,
                 },
                 new Regra
                 {

                     RegrasEscalonamento = "HorasMedicosDia",
                     Numero = 8,

                 },
                 new Regra
                 {


                     RegrasEscalonamento = "HorasMedicosSemana",
                     Numero = 40,
                 },
                 new Regra
                 {


                     RegrasEscalonamento = "Folga Enfermeiro",
                     Numero = 1,
                 },
                 new Regra
                 {


                     RegrasEscalonamento = "Folga Medico",
                     Numero = 1,
                 },
                 new Regra
                 {


                     RegrasEscalonamento = "Idade Filho",
                     Numero = 3,
                 }
            );

            db.SaveChanges();
        }
    

        private static void SeedEstado(HospitalDbContext db)
        {
            if (db.Estado.Any()) return;
            db.Estado.AddRange(
                       new Estado
                       {
                           Nome = "Decorrer"
                       },
                          new Estado
                          {
                              Nome = "Concluido"
                          }
               );

            db.SaveChanges();
        }

        private static void SeedRegime(HospitalDbContext db)
        {
            if (db.Regime.Any()) return;
            db.Regime.AddRange(
                       new Regime
                       {
                           TipoRegime = "Semanal"
                       },
                          new Regime
                          {
                              TipoRegime = "Quinzenal"
                          },
                          new Regime
                          {
                              TipoRegime = "Mensal"
                          },
                          new Regime
                          {
                              TipoRegime = "Trimestral"
                             
                          }
               );

            db.SaveChanges();
        }
    

        private static void SeedGrau(HospitalDbContext db)
        {
         if (db.Grau.Any()) return;
         db.Grau.AddRange(
                    new Grau
                       {
                          TipoGrau = "1"
                       },
                       new Grau
                       {
                           TipoGrau = "2"
                       }
            );

            db.SaveChanges();
        }

        private static void SeedPatologia(HospitalDbContext db)
        {
            if (db.Patologia.Any()) return;
         db.Patologia.AddRange(
                    new Patologia
                       {
                          Nome = "Pulmonar"
                       },
                       new Patologia
                       {
                           Nome = "Intestinal"
                       }
            );
        }





        private static void SeedTratamentos(HospitalDbContext db)
            {
                if (db.Tratamento.Any()) return;

                Patologia Pulmonar = db.Patologia.SingleOrDefault(e => e.Nome == "Pulmonar");
                Patologia Intestinal = db.Patologia.SingleOrDefault(e => e.Nome == "Instetinal ");

            Paciente Barbara = db.Pacientes.SingleOrDefault(e => e.Nome == "Barbara ");
            Paciente Andre = db.Pacientes.SingleOrDefault(e => e.Nome == "Andre ");

            Grau Grau1 = db.Grau.SingleOrDefault(e => e.TipoGrau == "1 ");
            Grau Grau2 = db.Grau.SingleOrDefault(e => e.TipoGrau == "2 ");

            Regime Semanal = db.Regime.SingleOrDefault(e => e.TipoRegime == "Semanal ");
            Regime Mensal = db.Regime.SingleOrDefault(e => e.TipoRegime == "Mensal ");

            Medico Manuel   = db.Medicos.SingleOrDefault(e => e.Nome == "Manuel Santos");
            Medico Elisabete  = db.Medicos.SingleOrDefault(e => e.Nome == "Elisabete Eiras");

            Estado Decorrer = db.Estado.SingleOrDefault(e => e.Nome == "Decorrer");
            Estado Concluido = db.Estado.SingleOrDefault(e => e.Nome == "Concluido");



            db.Tratamento.AddRange(


                    
                    new Tratamento
                    {
                        PatologiaId= Pulmonar.PatologiaId,
                        PacienteId = Barbara.PacienteId,
                        GrauId = Grau1.GrauId,
                        RegimeId = Semanal.RegimeId,
                        DataInicio = new DateTime(2018, 11, 09),
                        DataFim = new DateTime(2018, 12, 31),
                        DuracaoCiclo = "00:30",
                        MedicoId = Manuel.MedicoId,
                        EstadoId = Decorrer.EstadoId,
                        
                    },
                    new Tratamento
                    {
                        PatologiaId = Pulmonar.PatologiaId,
                        PacienteId = Andre.PacienteId,
                        GrauId = Grau2.GrauId,
                        RegimeId = Mensal.RegimeId,
                        DataInicio = new DateTime(2018, 11, 09),
                        DataFim = new DateTime(2018, 12, 31),
                        DuracaoCiclo = "01:10",
                        MedicoId = Elisabete.MedicoId,
                        EstadoId = Concluido.EstadoId,

                    }
                     );



                    

                db.SaveChanges();
            }
                 
            

            private static void SeedPacientes(HospitalDbContext db)
        {
            if (db.Pacientes.Any()) return;
               db.Pacientes.AddRange(
                
                   new Paciente { 


                    Nome = "Rita",
                    Morada = "Rua das Tulipas",
                    Cod_Postal = "6300-775",
                    Email = "ritabca@mail.pt",
                    CC = "147258709",
                    Data_Nascimento = new DateTime(1990, 2, 1),
                    Numero_Utente = "123456789",
                    Contacto = "210257416",
                },

                    new Paciente { 
         

                    Nome = "Barbara",
                    Morada = "Rua Francisco Sa Carneiro",
                    Cod_Postal = "6300-225",
                    Email = "barbara_R@sapo.pt",
                    CC = "147187301",
                    Data_Nascimento = new DateTime(1995, 06, 12),
                    Numero_Utente = "135756789",
                    Contacto = "912378563",
               },
           
        new Paciente { 

                    Nome = "Andre",
                    Morada = "Rua 25 de Abril",
                    Cod_Postal = "6000-710",
                    Email = "andre@mail.pt",
                    CC = "177858705",
                    Data_Nascimento = new DateTime(1968, 08, 08),
                    Numero_Utente = "198736789",
                    Contacto = "912345678",
                },

             new Paciente { 
                    Nome = "Leandro",
                    Morada = "Rua da Boa Vista",
                    Cod_Postal = "3000-105",
                    Email = "leandro@mail.pt",
                    CC = "123858703",
                    Data_Nascimento = new DateTime(1975, 4, 25),
                    Numero_Utente = "123475632",
                    Contacto = "969525305",
             },
                
            new Paciente { 

                    Nome = "Tiago",
                    Morada = "Rua dos Combatentes",
                    Cod_Postal = "1000-025",
                    Email = "tiago@mail.pt",
                    CC = "198235870",
                    Data_Nascimento = new DateTime(1940, 12, 25),
                    Numero_Utente = "195304678",
                    Contacto = "270284532",
            }
            );

            db.SaveChanges();
        }

      

       
            

        private static void SeedMedicoEspecialidade(HospitalDbContext db)
        {
            if (db.MedicoEspecialidade.Any()) return;
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

            db.MedicoEspecialidade.AddRange(

                new MedicoEspecialidade { Nome = "Manuel Santos", MedicoId = manuel.MedicoId },
                new MedicoEspecialidade { Nome = "Elisabete Eiras", MedicoId = elisabete.MedicoId }
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
