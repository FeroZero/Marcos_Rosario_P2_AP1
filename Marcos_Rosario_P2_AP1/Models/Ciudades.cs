using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Marcos_Rosario_P2_AP1.Models
{
	public class Ciudades
	{
		[Key]
		public int CiudadId { get; set; }

		public string Nombre { get; set; }

		public double Monto { get; set; }

		[InverseProperty("Ciudades")]
		public virtual ICollection<Cursos> Cursos { get; set; } = new List<Cursos>();
	}
}
