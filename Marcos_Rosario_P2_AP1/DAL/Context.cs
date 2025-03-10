
using Marcos_Rosario_P2_AP1.Models;
using Microsoft.EntityFrameworkCore;

namespace Marcos_Rosario_P2_AP1.DAL
{
	public class Context : DbContext
	{
		public Context(DbContextOptions<Context> options) : base(options) { }

		public DbSet<Modelos> Models { get; set; }
	}
}
