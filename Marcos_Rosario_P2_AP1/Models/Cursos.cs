using System.ComponentModel.DataAnnotations;

namespace Marcos_Rosario_P2_AP1.Models
{
	public class Cursos
	{
		[Key]
		public int CursoId { get; set; }

		[Required]
		public DateOnly Fecha { get; set; } = DateOnly.FromDateTime(DateTime.Now);

		[Required]
		[RegularExpression(@"^[a-zA-Z1-9\s]+$", ErrorMessage = "Caracteres no permitidos")]
		[StringLength(50,ErrorMessage = "Limite Excedido.")]
		public string Asignatura { get; set; }

		[Required]
		[Range(0, 1000000,ErrorMessage = "Limite Excedido.")]
		public double Monto { get; set; }
	}
}
