using System.ComponentModel.DataAnnotations;

namespace Marcos_Rosario_P2_AP1.Models
{
	public class Modelos
	{
		[Key]
		public int CiudadId { get; set; }

		public string Nombre { get; set; }

		public double Monto { get; set; }


	}
}
