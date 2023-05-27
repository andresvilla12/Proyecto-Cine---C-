using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Cine.API.Data;
using Cine.Shared.Entities;
using System.Diagnostics.Metrics;
using Cine.API.Helpers;
using Cine.Shared.Enums;

namespace Cine.API.Data
{
    public class SeedDb
    {
        private readonly DataContext _context;
        private readonly IUserHelper _userHelper;

        public SeedDb(DataContext context, IUserHelper userHelper)
        {
            _context = context;
            _userHelper = userHelper;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckColombiaAsync();
            await CheckGeneroAsync();
            await CheckRolesAsync();
            await CheckUserAsync("1", "JAS", "CJV", "jas@yopmail.com", "31289098", "CR 78- 98", UserType.Admin);


        }

        private async Task<User> CheckUserAsync(string document, string firstName, string lastName, string email, string phone, string address, UserType userType)
        {
            var user = await _userHelper.GetUserAsync(email);
            if (user == null)
            {
                user = new User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    UserName = email,
                    PhoneNumber = phone,
                    Address = address,
                    Document = document,
                    Ciudad = _context.Ciudad.FirstOrDefault(),
                    UserType = userType,
                };

                await _userHelper.AddUserAsync(user, "123456");
                await _userHelper.AddUserToRoleAsync(user, userType.ToString());
            }

            return user;
        }

        private async Task CheckRolesAsync()
        {
            await _userHelper.CheckRoleAsync(UserType.Admin.ToString());
            await _userHelper.CheckRoleAsync(UserType.User.ToString());
        }


        private async Task CheckColombiaAsync()
        {
            if (!_context.Colombia.Any())
            {
                _context.Colombia.Add(new Colombia
                {
                    Name = "Antioquia",
                    Ciudad = new List<Ciudad>()

            {
                new Ciudad(){Name="Medellín"},
                new Ciudad(){Name="Sabaneta"},
                new Ciudad(){Name="La Estrella"},
                new Ciudad(){Name="Envigado"},
                new Ciudad(){Name="Itagui"},
                },

                });



                _context.Colombia.Add(new Colombia
                {
                    Name = "Atlantico",
                    Ciudad = new List<Ciudad>()

            {
                new Ciudad(){Name="Barranquilla"},
                new Ciudad(){Name="Soledad"},
                new Ciudad(){Name="SabanaGrande"},
                new Ciudad(){Name="Malambo"},
                new Ciudad(){Name="Galapa"},
                },
                });

                _context.Colombia.Add(new Colombia
                {
                    Name = "Cundinamarca",
                    Ciudad = new List<Ciudad>()

            {
                new Ciudad(){Name="Bogota"},
                new Ciudad(){Name="Soacha"},
                new Ciudad(){Name="Zipaquira"},
                new Ciudad(){Name="Facatativa"},
                new Ciudad(){Name="Mosquera"},
                },
                });

                _context.Colombia.Add(new Colombia
                {
                    Name = "Valle",
                    Ciudad = new List<Ciudad>()

            {
                new Ciudad(){Name="Cartago"},
                new Ciudad(){Name="Valle"},
                new Ciudad(){Name="Tulua"},
                new Ciudad(){Name="Cali"},
                new Ciudad(){Name="Candelaria"},
                },
                });
            }
                

                await _context.SaveChangesAsync();
        }



        //private async Task CheckGeneroAsync()
        //{
        //    if (!_context.Genero.Any())
        //    {
        //        _context.Genero.Add(new Genero { Name = "Terror" });
        //        _context.Genero.Add(new Genero { Name = "Acción" });
        //        _context.Genero.Add(new Genero { Name = "Ciencia Ficción" });
        //        _context.Genero.Add(new Genero { Name = "Animada" });
        //        _context.Genero.Add(new Genero { Name = "Infantil" });
        //        _context.Genero.Add(new Genero { Name = "Sin clasificar" });
        //        _context.Genero.Add(new Genero { Name = "Colombianas" });
        //        _context.Genero.Add(new Genero { Name = "Romanticas" });
        //        _context.Genero.Add(new Genero { Name = "Clase S" });
        //        _context.Genero.Add(new Genero { Name = "Orientales" });
        //        _context.Genero.Add(new Genero { Name = "Aventura" });
        //        _context.Genero.Add(new Genero { Name = "Drama" });
        //        _context.Genero.Add(new Genero { Name = "Documental" });
        //    }

        //    await _context.SaveChangesAsync();
        //}

        private async Task CheckGeneroAsync()
        {
            if (!_context.Genero.Any())
            {
                _context.Genero.Add(new Genero
                {
                    Name = "Ciencia Ficción",
                    Pelicula = new List<Pelicula>()
            {
                new Pelicula()
                {
                    Name = "Los guardianes de la galaxia",
                    Funciones = new List<Funcion>() {
                        new Funcion() { Name = "Viernes 1 Pm" },
                        new Funcion() { Name = "Sabado 4 Pm" },
                        new Funcion() { Name = "Lunes 2 Pm" },
                        new Funcion() { Name = "Jueves 6 Pm" },
                        new Funcion() { Name = "Miercoles 7 Pm" },
                    }
                },
                new Pelicula()
                {
                    Name = "Avatar 2",
                    Funciones = new List<Funcion>() {
                        new Funcion() { Name = "Lunes 3 Pm" },
                        new Funcion() { Name = "Martes 6 Pm" },
                        new Funcion() { Name = "Jueves 4 Pm" },
                        new Funcion() { Name = "Domingo 1 Pm" },
                        new Funcion() { Name = "Viernes 12 Pm" },
                    }
                },
            }
                });
                _context.Genero.Add(new Genero
                {
                    Name = "Animación",
                    Pelicula = new List<Pelicula>()
            {
                new Pelicula()
                {
                    Name = "Super mario bros",
                    Funciones = new List<Funcion>() {
                        new Funcion() { Name = "Lunes 7 Pm" },
                        new Funcion() { Name = "Martes 3 Pm" },
                        new Funcion() { Name = "Jueves 8 Pm" },
                        new Funcion() { Name = "Domingo 6 Pm" },
                        new Funcion() { Name = "Viernes 9 Pm" },
                    }
                },
                new Pelicula()
                {
                    Name = "Las momias y el anillo perdido",
                    Funciones = new List<Funcion>() {
                       new Funcion() { Name = "Lunes 2:30 Pm" },
                        new Funcion() { Name = "Martes 6 Pm" },
                        new Funcion() { Name = "Jueves 10 Pm" },
                        new Funcion() { Name = "Domingo 9 Pm" },
                        new Funcion() { Name = "Viernes 1:45 Pm" },
                    }
                },
            }
                });

                _context.Genero.Add(new Genero
                {
                    Name = "Terror",
                    Pelicula = new List<Pelicula>()
            {
                new Pelicula()
                {
                    Name = "El despertar",
                    Funciones = new List<Funcion>() {
                        new Funcion() { Name = "Viernes 1 Pm" },
                        new Funcion() { Name = "Sabado 4 Pm" },
                        new Funcion() { Name = "Lunes 2 Pm" },
                        new Funcion() { Name = "Jueves 6 Pm" },
                        new Funcion() { Name = "Miercoles 7 Pm" },
                    }
                },
                new Pelicula()
                {
                    Name = "Terrifier 2",
                    Funciones = new List<Funcion>() {
                        new Funcion() { Name = "Lunes 3 Pm" },
                        new Funcion() { Name = "Martes 6 Pm" },
                        new Funcion() { Name = "Jueves 4 Pm" },
                        new Funcion() { Name = "Domingo 1 Pm" },
                        new Funcion() { Name = "Viernes 12 Pm" },
                    }
                },
            }
                });
            }

            await _context.SaveChangesAsync();
        }



    }
}

