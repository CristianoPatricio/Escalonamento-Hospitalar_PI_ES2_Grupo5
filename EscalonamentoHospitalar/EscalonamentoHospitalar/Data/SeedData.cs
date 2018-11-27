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

                SeedEspecialidadeEnfermeiros(db);
                SeedEnfermeiros(db);
                SeedDiretorServico(db);                         
                SeedEnfermeiroEspecialidade(db);
                SeedTurnos(db);
                SeedHorarioEnfermeiros(db);
                SeedMedicos(db);
                SeedMedicoEspecialidade(db);
                SeedPacientes(db);
                
            }
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

        private static void SeedHorarioEnfermeiros(HospitalDbContext db)
        {
            if (db.HorariosEnfermeiro.Any()) return;
         
            Enfermeiro enfermeiro = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Marisa Reduto");
            Turno turno1 = db.Turnos.SingleOrDefault(t => t.Nome == "MANHÃ");
            DateTime dataInicioT1 = new DateTime(2018, 11, 21, 8, 0, 0);
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1, Duracao = 8, DataFimTurno = dataInicioT1.AddHours(8) ,TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro.EnfermeiroId});

            enfermeiro = db.Enfermeiros.SingleOrDefault(e => e.Nome == "João Silva");
            turno1 = db.Turnos.SingleOrDefault(t => t.Nome == "MANHÃ");
            dataInicioT1 = new DateTime(2018, 11, 21, 8, 0, 0);
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1, Duracao = 8, DataFimTurno = dataInicioT1.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro.EnfermeiroId });

            enfermeiro = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Armando Manso");
            Turno turno2 = db.Turnos.SingleOrDefault(t => t.Nome == "TARDE");
            DateTime dataInicioT2 = new DateTime(2018, 11, 21, 16, 0, 0);
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT2, Duracao = 8, DataFimTurno = dataInicioT2.AddHours(8), TurnoId = turno2.TurnoId, EnfermeiroId = enfermeiro.EnfermeiroId });

            enfermeiro = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Andreia Cunha");
            Turno turno3 = db.Turnos.SingleOrDefault(t => t.Nome == "NOITE");
            DateTime dataInicioT3 = new DateTime(2018, 11, 22, 0, 0, 0);
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT3, Duracao = 8, DataFimTurno = dataInicioT3.AddHours(8), TurnoId = turno3.TurnoId, EnfermeiroId = enfermeiro.EnfermeiroId });

            db.SaveChanges();
        }

        private static void SeedTurnos(HospitalDbContext db)
        {
            if (db.Turnos.Any()) return;

            db.Turnos.AddRange(
                
                new Turno { Nome = "MANHÃ" },
                new Turno { Nome = "TARDE" },
                new Turno { Nome = "NOITE" }      
                
                );

            db.SaveChanges();
        }

        private static void SeedEspecialidadeEnfermeiros(HospitalDbContext db)
        {
            if (db.EspecialidadesEnfermeiros.Any()) return;

            db.EspecialidadesEnfermeiros.AddRange(
                
                new EspecialidadeEnfermeiro { Especialidade = "Enfermagem Comunitária"},
                new EspecialidadeEnfermeiro { Especialidade = "Enfermagem Médico-Cirúrgica" },
                new EspecialidadeEnfermeiro { Especialidade = "Enfermagem de Reabilitação" },
                new EspecialidadeEnfermeiro { Especialidade = "Enfermagem de Saúde Infantil e Pediátrica" },
                new EspecialidadeEnfermeiro { Especialidade = "Enfermagem de Saúde Materna e Obstétrica" },
                new EspecialidadeEnfermeiro { Especialidade = "Enfermagem de Saúde Mental e Psiquiátrica" }

                );

            db.SaveChanges();
        }
        
        private static void SeedEnfermeiroEspecialidade(HospitalDbContext db)
        {
            if (db.EnfermeirosEspecialidades.Any()) return;

            EspecialidadeEnfermeiro especialidade = GetEspecialidadeCreatingIfNeed(db, "Enfermagem Comunitária");
            Enfermeiro enfermeiro = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Marisa Reduto");
            db.EnfermeirosEspecialidades.Add(new EnfermeiroEspecialidade { EspecialidadeEnfermeiroId = especialidade.EspecialidadeEnfermeiroId, EnfermeiroId = enfermeiro.EnfermeiroId, Data_Registo = new DateTime(2018, 11, 16) });

            especialidade = GetEspecialidadeCreatingIfNeed(db, "Enfermagem Médico-Cirúrgica");
            enfermeiro = db.Enfermeiros.SingleOrDefault(e => e.Nome == "João Silva");
            db.EnfermeirosEspecialidades.Add(new EnfermeiroEspecialidade { EspecialidadeEnfermeiroId = especialidade.EspecialidadeEnfermeiroId, EnfermeiroId = enfermeiro.EnfermeiroId, Data_Registo = new DateTime(2018, 11, 16) });

            especialidade = GetEspecialidadeCreatingIfNeed(db, "Enfermagem de Reabilitação");
            enfermeiro = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Armando Manso");
            db.EnfermeirosEspecialidades.Add(new EnfermeiroEspecialidade { EspecialidadeEnfermeiroId = especialidade.EspecialidadeEnfermeiroId, EnfermeiroId = enfermeiro.EnfermeiroId, Data_Registo = new DateTime(2018, 11, 16) });

            enfermeiro = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Andreia Cunha");
            db.EnfermeirosEspecialidades.Add(new EnfermeiroEspecialidade { EspecialidadeEnfermeiroId = especialidade.EspecialidadeEnfermeiroId, EnfermeiroId = enfermeiro.EnfermeiroId, Data_Registo = new DateTime(2018, 11, 16) });

            db.SaveChanges();
        }
      
        private static void SeedDiretorServico(HospitalDbContext db)
        {
            if (db.DiretorServico.Any()) return;

            db.DiretorServico.AddRange(

                new DiretorServico { Nome = "António Barata", Codigo = "D001", Contacto = "965417847", Email = "antoniobarata@uls.guarda", CC = "12547845"}

                );

            db.SaveChanges();
        }

        
        private static void SeedEnfermeiros(HospitalDbContext db)
        {
            if (db.Enfermeiros.Any()) return;

            EspecialidadeEnfermeiro comunitaria = GetEspecialidadeCreatingIfNeed(db, "Enfermagem Comunitária");
            EspecialidadeEnfermeiro reabilitacao = GetEspecialidadeCreatingIfNeed(db, "Enfermagem de Reabilitação");
            EspecialidadeEnfermeiro saudeInfantil = GetEspecialidadeCreatingIfNeed(db, "Enfermagem de Saúde Infantil e Pediátrica");
            EspecialidadeEnfermeiro saudeMaterna = GetEspecialidadeCreatingIfNeed(db, "Enfermagem de Saúde Materna e Obstétrica");
            EspecialidadeEnfermeiro saudeMental = GetEspecialidadeCreatingIfNeed(db, "Enfermagem de Saúde Mental e Psiquiátrica");
            EspecialidadeEnfermeiro medicoCirurgica = GetEspecialidadeCreatingIfNeed(db, "Enfermagem Médico-Cirúrgica");
       
            db.Enfermeiros.AddRange(
                
                new Enfermeiro { NumeroMecanografico = "E001", Nome = "Marisa Reduto", EspecialidadeEnfermeiroId = comunitaria.EspecialidadeEnfermeiroId,  Contacto = "966333222", Email = "marisareduto@uls.guarda", Data_Nascimento = new DateTime(1998,6,6) , CC = "15823256", Filhos = false, Data_Nascimento_Filho = null },
                new Enfermeiro { NumeroMecanografico = "E002", Nome = "João Silva", EspecialidadeEnfermeiroId = medicoCirurgica.EspecialidadeEnfermeiroId, Contacto = "965241232", Email = "joaosilva@uls.guarda", Data_Nascimento = new DateTime(1989,8,16), CC = "15852556", Filhos = true, Data_Nascimento_Filho = new DateTime(2016, 7, 10) },
                new Enfermeiro { NumeroMecanografico = "E003", Nome = "Armando Manso", EspecialidadeEnfermeiroId = reabilitacao.EspecialidadeEnfermeiroId, Contacto = "964521121", Email = "armandomanso@uls.guarda", Data_Nascimento = new DateTime(1987,7,1), CC = "13652544", Filhos = true, Data_Nascimento_Filho = new DateTime(2018, 8, 23) },
                new Enfermeiro { NumeroMecanografico = "E004", Nome = "Andreia Cunha", EspecialidadeEnfermeiroId = reabilitacao.EspecialidadeEnfermeiroId, Contacto = "923654152", Email = "andreiacunha@uls.guarda", Data_Nascimento = new DateTime(1978,10,25), CC = "14245485", Filhos = false, Data_Nascimento_Filho = null }            

                );

            db.SaveChanges();
        }

        private static EspecialidadeEnfermeiro GetEspecialidadeCreatingIfNeed(HospitalDbContext db, string name)
        {
            EspecialidadeEnfermeiro especialidade = db.EspecialidadesEnfermeiros.SingleOrDefault(c => c.Especialidade == name);

            if (especialidade == null)
            {
                especialidade = new EspecialidadeEnfermeiro { Especialidade = name };
                db.Add(especialidade);
                db.SaveChanges();
            }

            return especialidade;
        }
    }
}
