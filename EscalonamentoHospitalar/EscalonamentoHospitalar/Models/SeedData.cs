using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EscalonamentoHospitalar.Models
{
    public static class SeedData
    {
        public static void Populate(IServiceProvider applicationServices) 
        {
            using (var serviceScope = applicationServices.CreateScope())
            {
                var db = serviceScope.ServiceProvider.GetService<HospitalDbContext>();
                if (db.Medico.Any()) return;

                db.Medico.AddRange(
                        new Medico {
                            MedicoId = 1,
                            NumeroMecanografico = "M001",
                            Nome = "Manuel Santos",
                            Especialidade = "Patologia Clínica",
                            Email = "manuelsantos@uls.guarda.com",
                            Contacto = "936571245",
                            CC = "15851657",
                            Data_Nascimento = new DateTime(1987, 6, 29),
                        },
                        new Medico
                        {
                            MedicoId = 2,
                            NumeroMecanografico = "M002",
                            Nome = "Elisabete Eiras",
                            Especialidade = "Estomatologia",
                            Email = "elisabeteeiras@uls.guarda.com",
                            Contacto = "925641937",
                            CC = "16457832",
                            Data_Nascimento = new DateTime(1985, 4, 2),
                        }
                    );
                if (db.Paciente.Any()) return;
                db.Paciente.AddRange(
                    new Paciente
                    {
                       
                        Nome = "Rita",
                        Morada = "Rua das Tulipas",
                        Cod_Postal = "6300-775",
                        Email = "ritabca@mail.pt",
                        CC = "14725870",
                        Data_Nascimento = new DateTime(1990, 2, 1),
                        Numero_Utente = "123456789",
                        Contacto = "210257416",                      
                    },
                    new Paciente
                    {
                        
                        Nome = "Rita",
                        Morada = "Rua das Tulipas",
                        Cod_Postal = "6300-775",
                        Email = "ritabca@mail.pt",
                        CC = "14725870",
                        Data_Nascimento = new DateTime(1990, 2, 1),
                        Numero_Utente = "123456789",
                        Contacto = "210257416",
                    },
                    new Paciente
                    {
                        
                        Nome = "Rita",
                        Morada = "Rua das Tulipas",
                        Cod_Postal = "6300-775",
                        Email = "ritabca@mail.pt",
                        CC = "14725870",
                        Data_Nascimento = new DateTime(1990, 2, 1),
                        Numero_Utente = "123456789",
                        Contacto = "210257416",
                    },
                    new Paciente
                    {
                        
                        Nome = "Rita",
                        Morada = "Rua das Tulipas",
                        Cod_Postal = "6300-775",
                        Email = "ritabca@mail.pt",
                        CC = "14725870",
                        Data_Nascimento = new DateTime(1990, 2, 1),
                        Numero_Utente = "123456789",
                        Contacto = "210257416",
                    },
                    new Paciente
                    {

                        Nome = "Rita",
                        Morada = "Rua das Tulipas",
                        Cod_Postal = "6300-775",
                        Email = "ritabca@mail.pt",
                        CC = "14725870",
                        Data_Nascimento = new DateTime(1990, 2, 1),
                        Numero_Utente = "123456789",
                        Contacto = "210257416",
                    }
                    );
                db.SaveChanges();
            }
        }
    }
}
