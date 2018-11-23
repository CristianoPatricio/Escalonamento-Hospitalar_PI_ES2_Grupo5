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
                SeedEspecialidadeEnfermeiros(db);
                SeedEnfermeiroEspecialidade(db);
                SeedTurnos(db);
                SeedHorarioEnfermeiros(db);
            }
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

                new DiretorServico { Name = "António Barata", NumeroMecanografico = "D001", Contacto = "965417847", Email = "antoniobarata@uls.guarda", CC = "12547845", Morada = "Guarda" }

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
