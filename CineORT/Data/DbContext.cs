using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CineORT.Models;

    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext (DbContextOptions<DbContext> options)
            : base(options)
        {
        }
        public DbSet<CineORT.Models.Clasificacion> Clasificacion { get; set; } = default!;
        public DbSet<CineORT.Models.Cliente> Cliente { get; set; } = default!;
        public DbSet<CineORT.Models.Funcion> Funcion { get; set; } = default!;
        public DbSet<CineORT.Models.Genero> Genero { get; set; } = default!;
        public DbSet<CineORT.Models.Pelicula> Pelicula { get; set; } = default!;
        public DbSet<CineORT.Models.Reserva> Reserva { get; set; } = default!;
        public DbSet<CineORT.Models.Sala> Sala { get; set; } = default!;


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("data source = DESKTOP-4NTPRFU; Initial Catalog=CineORT; Integrated Security=true; Encrypt=True; TrustServerCertificate=true");
        }

}
