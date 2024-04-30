﻿using Microsoft.EntityFrameworkCore;
using PuntoVenta.Dominio.Entity;

namespace PuntoVenta.Infraestructura.Data
{
	public class ApplicationDbContext : DbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}

		public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Categoria> Categoria { get; set; }
        public DbSet<Producto> Producto { get; set; }
    }
}