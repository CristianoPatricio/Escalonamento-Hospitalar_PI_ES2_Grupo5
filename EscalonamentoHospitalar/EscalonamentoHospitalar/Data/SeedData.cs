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
            }
        }

        private static void SeedHorarioEnfermeiros(HospitalDbContext db)
        {
            if (db.HorariosEnfermeiro.Any()) return;

            Turno turno1 = db.Turnos.SingleOrDefault(t => t.Nome == "MANHÃ");
            Turno turno2 = db.Turnos.SingleOrDefault(t => t.Nome == "TARDE");
            Turno turno3 = db.Turnos.SingleOrDefault(t => t.Nome == "NOITE");

            DateTime dataInicioT1 = new DateTime(2018, 11, 21, 8, 0, 0);
            DateTime dataInicioT1CFilhos = new DateTime(2018, 11, 21, 9, 0, 0);
            DateTime dataInicioT2 = new DateTime(2018, 11, 21, 16, 0, 0);
            DateTime dataInicioT3 = new DateTime(2018, 11, 22, 0, 0, 0);

            Enfermeiro enfermeiro1 = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Marisa Reduto");    
            Enfermeiro enfermeiro2 = db.Enfermeiros.SingleOrDefault(e => e.Nome == "João Silva"); //Com Filho
            Enfermeiro enfermeiro3 = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Armando Manso"); //Com Filho
            Enfermeiro enfermeiro4 = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Andreia Cunha");
            Enfermeiro enfermeiro5 = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Joana Albuquerque");
            Enfermeiro enfermeiro6 = db.Enfermeiros.SingleOrDefault(e => e.Nome == "João Tomás");
            Enfermeiro enfermeiro7 = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Carlos Manso");
            Enfermeiro enfermeiro8 = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Bruno Martins");
            Enfermeiro enfermeiro9 = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Ariana Gonçalves");
            Enfermeiro enfermeiro10 = db.Enfermeiros.SingleOrDefault(e => e.Nome == "André Silva");
            Enfermeiro enfermeiro11 = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Carlos Almeida"); //Com Filho
            Enfermeiro enfermeiro12 = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Fábio Castro"); //Com Filho

            //2 feira
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1CFilhos, Duracao = 8, DataFimTurno = dataInicioT1CFilhos.AddHours(8) ,TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro12.EnfermeiroId});           
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1, Duracao = 8, DataFimTurno = dataInicioT1.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro8.EnfermeiroId });                   
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1CFilhos, Duracao = 8, DataFimTurno = dataInicioT1CFilhos.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro2.EnfermeiroId });           
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1, Duracao = 8, DataFimTurno = dataInicioT1.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro6.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1CFilhos, Duracao = 8, DataFimTurno = dataInicioT1CFilhos.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro3.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT2, Duracao = 8, DataFimTurno = dataInicioT2.AddHours(8), TurnoId = turno2.TurnoId, EnfermeiroId = enfermeiro11.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT2, Duracao = 8, DataFimTurno = dataInicioT2.AddHours(8), TurnoId = turno2.TurnoId, EnfermeiroId = enfermeiro10.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT2, Duracao = 8, DataFimTurno = dataInicioT2.AddHours(8), TurnoId = turno2.TurnoId, EnfermeiroId = enfermeiro9.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT3, Duracao = 8, DataFimTurno = dataInicioT3.AddHours(8), TurnoId = turno3.TurnoId, EnfermeiroId = enfermeiro7.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT3, Duracao = 8, DataFimTurno = dataInicioT3.AddHours(8), TurnoId = turno3.TurnoId, EnfermeiroId = enfermeiro4.EnfermeiroId });

            //3 feira
            dataInicioT1 = new DateTime(2018, 11, 22, 8, 0, 0);
            dataInicioT1CFilhos = new DateTime(2018, 11, 22, 9, 0, 0);
            dataInicioT2 = new DateTime(2018, 11, 22, 16, 0, 0);
            dataInicioT3 = new DateTime(2018, 11, 23, 0, 0, 0);

            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1CFilhos, Duracao = 8, DataFimTurno = dataInicioT1CFilhos.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro3.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1, Duracao = 8, DataFimTurno = dataInicioT1.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro5.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1CFilhos, Duracao = 8, DataFimTurno = dataInicioT1CFilhos.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro12.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1, Duracao = 8, DataFimTurno = dataInicioT1.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro9.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1CFilhos, Duracao = 8, DataFimTurno = dataInicioT1CFilhos.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro11.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT2, Duracao = 8, DataFimTurno = dataInicioT2.AddHours(8), TurnoId = turno2.TurnoId, EnfermeiroId = enfermeiro2.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT2, Duracao = 8, DataFimTurno = dataInicioT2.AddHours(8), TurnoId = turno2.TurnoId, EnfermeiroId = enfermeiro1.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT2, Duracao = 8, DataFimTurno = dataInicioT2.AddHours(8), TurnoId = turno2.TurnoId, EnfermeiroId = enfermeiro6.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT3, Duracao = 8, DataFimTurno = dataInicioT3.AddHours(8), TurnoId = turno3.TurnoId, EnfermeiroId = enfermeiro10.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT3, Duracao = 8, DataFimTurno = dataInicioT3.AddHours(8), TurnoId = turno3.TurnoId, EnfermeiroId = enfermeiro4.EnfermeiroId });

            //4 feira
            dataInicioT1 = new DateTime(2018, 11, 23, 8, 0, 0);
            dataInicioT1CFilhos = new DateTime(2018, 11, 23, 9, 0, 0);
            dataInicioT2 = new DateTime(2018, 11, 23, 16, 0, 0);
            dataInicioT3 = new DateTime(2018, 11, 24, 0, 0, 0);

            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1CFilhos, Duracao = 8, DataFimTurno = dataInicioT1CFilhos.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro2.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1, Duracao = 8, DataFimTurno = dataInicioT1.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro1.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1CFilhos, Duracao = 8, DataFimTurno = dataInicioT1CFilhos.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro12.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1, Duracao = 8, DataFimTurno = dataInicioT1.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro5.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1CFilhos, Duracao = 8, DataFimTurno = dataInicioT1CFilhos.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro3.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT2, Duracao = 8, DataFimTurno = dataInicioT2.AddHours(8), TurnoId = turno2.TurnoId, EnfermeiroId = enfermeiro11.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT2, Duracao = 8, DataFimTurno = dataInicioT2.AddHours(8), TurnoId = turno2.TurnoId, EnfermeiroId = enfermeiro10.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT2, Duracao = 8, DataFimTurno = dataInicioT2.AddHours(8), TurnoId = turno2.TurnoId, EnfermeiroId = enfermeiro4.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT3, Duracao = 8, DataFimTurno = dataInicioT3.AddHours(8), TurnoId = turno3.TurnoId, EnfermeiroId = enfermeiro8.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT3, Duracao = 8, DataFimTurno = dataInicioT3.AddHours(8), TurnoId = turno3.TurnoId, EnfermeiroId = enfermeiro9.EnfermeiroId });

            //5 feira
            dataInicioT1 = new DateTime(2018, 11, 24, 8, 0, 0);
            dataInicioT1CFilhos = new DateTime(2018, 11, 24, 9, 0, 0);
            dataInicioT2 = new DateTime(2018, 11, 24, 16, 0, 0);
            dataInicioT3 = new DateTime(2018, 11, 25, 0, 0, 0);

            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1CFilhos, Duracao = 8, DataFimTurno = dataInicioT1CFilhos.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro12.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1, Duracao = 8, DataFimTurno = dataInicioT1.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro7.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1CFilhos, Duracao = 8, DataFimTurno = dataInicioT1CFilhos.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro2.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1, Duracao = 8, DataFimTurno = dataInicioT1.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro6.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1CFilhos, Duracao = 8, DataFimTurno = dataInicioT1CFilhos.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro3.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT2, Duracao = 8, DataFimTurno = dataInicioT2.AddHours(8), TurnoId = turno2.TurnoId, EnfermeiroId = enfermeiro5.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT2, Duracao = 8, DataFimTurno = dataInicioT2.AddHours(8), TurnoId = turno2.TurnoId, EnfermeiroId = enfermeiro4.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT2, Duracao = 8, DataFimTurno = dataInicioT2.AddHours(8), TurnoId = turno2.TurnoId, EnfermeiroId = enfermeiro8.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT3, Duracao = 8, DataFimTurno = dataInicioT3.AddHours(8), TurnoId = turno3.TurnoId, EnfermeiroId = enfermeiro1.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT3, Duracao = 8, DataFimTurno = dataInicioT3.AddHours(8), TurnoId = turno3.TurnoId, EnfermeiroId = enfermeiro9.EnfermeiroId });

            //6 feira
            dataInicioT1 = new DateTime(2018, 11, 25, 8, 0, 0);
            dataInicioT1CFilhos = new DateTime(2018, 11, 25, 9, 0, 0);
            dataInicioT2 = new DateTime(2018, 11, 25, 16, 0, 0);

            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1CFilhos, Duracao = 8, DataFimTurno = dataInicioT1CFilhos.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro11.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1, Duracao = 8, DataFimTurno = dataInicioT1.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro6.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1CFilhos, Duracao = 8, DataFimTurno = dataInicioT1CFilhos.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro12.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1, Duracao = 8, DataFimTurno = dataInicioT1.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro5.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT1CFilhos, Duracao = 8, DataFimTurno = dataInicioT1CFilhos.AddHours(8), TurnoId = turno1.TurnoId, EnfermeiroId = enfermeiro10.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT2, Duracao = 8, DataFimTurno = dataInicioT2.AddHours(8), TurnoId = turno2.TurnoId, EnfermeiroId = enfermeiro4.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT2, Duracao = 8, DataFimTurno = dataInicioT2.AddHours(8), TurnoId = turno2.TurnoId, EnfermeiroId = enfermeiro1.EnfermeiroId });
            db.HorariosEnfermeiro.Add(new HorarioEnfermeiro { DataInicioTurno = dataInicioT2, Duracao = 8, DataFimTurno = dataInicioT2.AddHours(8), TurnoId = turno2.TurnoId, EnfermeiroId = enfermeiro7.EnfermeiroId });      

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
        
            enfermeiro = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Joana Albuquerque");
            db.EnfermeirosEspecialidades.Add(new EnfermeiroEspecialidade { EspecialidadeEnfermeiroId = especialidade.EspecialidadeEnfermeiroId, EnfermeiroId = enfermeiro.EnfermeiroId, Data_Registo = new DateTime(2018, 11, 16) });
            
            enfermeiro = db.Enfermeiros.SingleOrDefault(e => e.Nome == "João Tomás");
            db.EnfermeirosEspecialidades.Add(new EnfermeiroEspecialidade { EspecialidadeEnfermeiroId = especialidade.EspecialidadeEnfermeiroId, EnfermeiroId = enfermeiro.EnfermeiroId, Data_Registo = new DateTime(2018, 11, 16) });
   
            enfermeiro = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Carlos Manso");
            db.EnfermeirosEspecialidades.Add(new EnfermeiroEspecialidade { EspecialidadeEnfermeiroId = especialidade.EspecialidadeEnfermeiroId, EnfermeiroId = enfermeiro.EnfermeiroId, Data_Registo = new DateTime(2018, 11, 16) });

            enfermeiro = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Bruno Martins");
            db.EnfermeirosEspecialidades.Add(new EnfermeiroEspecialidade { EspecialidadeEnfermeiroId = especialidade.EspecialidadeEnfermeiroId, EnfermeiroId = enfermeiro.EnfermeiroId, Data_Registo = new DateTime(2018, 11, 16) });
           
            enfermeiro = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Ariana Gonçalves");
            db.EnfermeirosEspecialidades.Add(new EnfermeiroEspecialidade { EspecialidadeEnfermeiroId = especialidade.EspecialidadeEnfermeiroId, EnfermeiroId = enfermeiro.EnfermeiroId, Data_Registo = new DateTime(2018, 11, 16) });

            enfermeiro = db.Enfermeiros.SingleOrDefault(e => e.Nome == "André Silva");
            db.EnfermeirosEspecialidades.Add(new EnfermeiroEspecialidade { EspecialidadeEnfermeiroId = especialidade.EspecialidadeEnfermeiroId, EnfermeiroId = enfermeiro.EnfermeiroId, Data_Registo = new DateTime(2018, 11, 16) });
        
            enfermeiro = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Carlos Almeida");
            db.EnfermeirosEspecialidades.Add(new EnfermeiroEspecialidade { EspecialidadeEnfermeiroId = especialidade.EspecialidadeEnfermeiroId, EnfermeiroId = enfermeiro.EnfermeiroId, Data_Registo = new DateTime(2018, 11, 16) });

            enfermeiro = db.Enfermeiros.SingleOrDefault(e => e.Nome == "Fábio Castro");
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
                new Enfermeiro { NumeroMecanografico = "E004", Nome = "Andreia Cunha", EspecialidadeEnfermeiroId = reabilitacao.EspecialidadeEnfermeiroId, Contacto = "923654152", Email = "andreiacunha@uls.guarda", Data_Nascimento = new DateTime(1978,10,25), CC = "14245485", Filhos = false, Data_Nascimento_Filho = null },
                new Enfermeiro { NumeroMecanografico = "E342", Nome = "Joana Albuquerque", EspecialidadeEnfermeiroId = comunitaria.EspecialidadeEnfermeiroId, Contacto = "968951742", Email = "joanaalbuquerque@uls.guarda", Data_Nascimento = new DateTime(1998, 6, 6), CC = "15823256", Filhos = false, Data_Nascimento_Filho = null },
                new Enfermeiro { NumeroMecanografico = "E392", Nome = "João Tomás", EspecialidadeEnfermeiroId = medicoCirurgica.EspecialidadeEnfermeiroId, Contacto = "965254874", Email = "joaotomas@uls.guarda", Data_Nascimento = new DateTime(1989, 8, 16), CC = "15852556", Filhos = false, Data_Nascimento_Filho = null },
                new Enfermeiro { NumeroMecanografico = "E783", Nome = "Carlos Manso", EspecialidadeEnfermeiroId = reabilitacao.EspecialidadeEnfermeiroId, Contacto = "964521741", Email = "carlosmanso@uls.guarda", Data_Nascimento = new DateTime(1987, 7, 1), CC = "13652544", Filhos = false, Data_Nascimento_Filho = null },
                new Enfermeiro { NumeroMecanografico = "E124", Nome = "Bruno Martins", EspecialidadeEnfermeiroId = reabilitacao.EspecialidadeEnfermeiroId, Contacto = "923254152", Email = "brunomartins@uls.guarda", Data_Nascimento = new DateTime(1978, 10, 25), CC = "14245485", Filhos = false, Data_Nascimento_Filho = null },
                new Enfermeiro { NumeroMecanografico = "E451", Nome = "Ariana Gonçalves", EspecialidadeEnfermeiroId = comunitaria.EspecialidadeEnfermeiroId, Contacto = "969876222", Email = "arianagoncalves@uls.guarda", Data_Nascimento = new DateTime(1998, 6, 6), CC = "15823256", Filhos = false, Data_Nascimento_Filho = null },
                new Enfermeiro { NumeroMecanografico = "E762", Nome = "André Silva", EspecialidadeEnfermeiroId = medicoCirurgica.EspecialidadeEnfermeiroId, Contacto = "968745232", Email = "andresilva@uls.guarda", Data_Nascimento = new DateTime(1989, 8, 16), CC = "15852556", Filhos = false, Data_Nascimento_Filho = null },
                new Enfermeiro { NumeroMecanografico = "E983", Nome = "Carlos Almeida", EspecialidadeEnfermeiroId = reabilitacao.EspecialidadeEnfermeiroId, Contacto = "967845621", Email = "carlosalmeida@uls.guarda", Data_Nascimento = new DateTime(1987, 7, 1), CC = "13652544", Filhos = true, Data_Nascimento_Filho = new DateTime(2018, 8, 23) },
                new Enfermeiro { NumeroMecanografico = "E125", Nome = "Fábio Castro", EspecialidadeEnfermeiroId = reabilitacao.EspecialidadeEnfermeiroId, Contacto = "923256452", Email = "fabiocastro@uls.guarda", Data_Nascimento = new DateTime(1978, 10, 25), CC = "14245485", Filhos = true, Data_Nascimento_Filho = new DateTime(2018, 8, 23) }
                
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
