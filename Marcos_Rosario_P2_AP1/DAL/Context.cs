
using Marcos_Rosario_P2_AP1.Models;
using Microsoft.EntityFrameworkCore;

namespace Marcos_Rosario_P2_AP1.DAL
{
	public class Context : DbContext
	{
		public Context(DbContextOptions<Context> options) : base(options) { }

		public DbSet<Ciudades> Ciudades { get; set; }
		public DbSet<Cursos> Cursos { get; set; }
		public DbSet<CursosDetalle> CursosDetalle { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Ciudades>().HasData(
				new List<Ciudades>()
				{
					new()
					{
						CiudadId = 1,
						Nombre = "Tenares",
					},
					new()
					{
						CiudadId = 2,
						Nombre = "San Fracisco de Macoris"
					},
					new()
					{
						CiudadId = 3,
						Nombre = "Santiago"
					}
				}
				
				
			);
			modelBuilder.Entity<CursosDetalle>()
			.HasOne(cd => cd.Cursos)
			.WithMany(c => c.Detalles)
			.HasForeignKey(cd => cd.CursoId)
			.OnDelete(DeleteBehavior.NoAction);
			base.OnModelCreating(modelBuilder);
		}
	}
}
