using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marcos_Rosario_P2_AP1.Models
{
	public class CursosDetalle
	{
		[Key]
		public int DetalleId { get; set; }

		public int CursoId { get; set; }

		public int CiudadId { get; set; }

		public double Valor { get; set; }

		[ForeignKey("CursoId")]
		[InverseProperty("CursosDetalle")]
		public virtual Cursos Cobro { get; set; } = null!;
	}
}
