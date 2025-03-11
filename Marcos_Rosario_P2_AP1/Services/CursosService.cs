using Marcos_Rosario_P2_AP1.DAL;
using Marcos_Rosario_P2_AP1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Marcos_Rosario_P2_AP1.Services
{
	public class CursosService(IDbContextFactory<Context> DbFactory)
	{
		private async Task<bool> Existe(int CursoId)
		{
			await using var contexto = await DbFactory.CreateDbContextAsync();
			return await contexto.Cursos.AnyAsync(c => c.CursoId == CursoId);
		}

		private async Task<bool> Insertar(Cursos cursos)
		{
			await using var contexto = await DbFactory.CreateDbContextAsync();
			await AfectarCiudad(cursos.Detalles.ToArray(), true);
			contexto.Cursos.Add(cursos);
			return await contexto.SaveChangesAsync() > 0;
		}

		private async Task AfectarCiudad(CursosDetalle[] detalles, bool resta = true)
		{
			await using var contexto = await DbFactory.CreateDbContextAsync();

			foreach (var detalle in detalles)
			{
				var ciudad = await contexto.Ciudades.SingleOrDefaultAsync(c => c.CiudadId == detalle.CiudadId);
				if (ciudad != null)
				{
					if (resta)
						ciudad.Monto -= detalle.Valor;
					else
						ciudad.Monto += detalle.Valor;
				}
			}

			await contexto.SaveChangesAsync();
		}

		private async Task<bool> Modificar(Cursos curso)
		{
			await using var contexto = await DbFactory.CreateDbContextAsync();

			var CursoExistente = await contexto.Cursos
				.FirstOrDefaultAsync(e => e.CursoId == curso.CursoId);

			if (CursoExistente == null)
				return false;

			await AfectarCiudad(CursoExistente.Detalles.ToArray(), false);

			foreach (var detalle in CursoExistente.Detalles)
			{
				if (!curso.Detalles.Any(d => d.DetalleId == detalle.DetalleId))
				{
					contexto.CursosDetalle.Remove(detalle);
				}
			}

			await AfectarCiudad(curso.Detalles.ToArray(), true);

			contexto.Entry(CursoExistente).CurrentValues.SetValues(curso);

			foreach (var detalle in curso.Detalles)
			{
				var detalleExistente = CursoExistente.Detalles
					.FirstOrDefault(d => d.DetalleId == detalle.DetalleId);

				if (detalleExistente != null)
				{
					contexto.Entry(detalleExistente).CurrentValues.SetValues(detalle);
				}
				else
				{
					CursoExistente.Detalles.Add(detalle);
				}
			}

			return await contexto.SaveChangesAsync() > 0;
		}

		public async Task<bool> Guardar(Cursos cursos)
		{
			if (!await Existe(cursos.CursoId))
				return await Insertar(cursos);
			else
				return await Modificar(cursos);
		}

		public async Task<bool> Eliminar(int cursoId)
		{
			await using var contexto = await DbFactory.CreateDbContextAsync();
			var encuesta = await contexto.Cursos
				.Include(e => e.Detalles)
				.FirstOrDefaultAsync(e => e.CursoId == cursoId);

			if (encuesta == null)
				return false;

			await AfectarCiudad(encuesta.Detalles.ToArray(), false); // Revertir cambios en ciudades

			contexto.CursosDetalle.RemoveRange(encuesta.Detalles);
			contexto.Cursos.Remove(encuesta);

			var cantidad = await contexto.SaveChangesAsync();
			return cantidad > 0;
		}

		public async Task<Cursos?> Buscar(int cursoId)
		{
			await using var contexto = await DbFactory.CreateDbContextAsync();
			return await contexto.Cursos
				.Include(e => e.Detalles)
				.AsNoTracking()
				.FirstOrDefaultAsync(e => e.CursoId == cursoId);
		}

		public async Task<List<Cursos>> Listar(Expression<Func<Cursos, bool>> criterio)
		{
			await using var contexto = await DbFactory.CreateDbContextAsync();
			return await contexto.Cursos
				.Include(e => e.Detalles)
				.ThenInclude(d => d.Ciudades)
				.AsNoTracking()
				.Where(criterio)
				.ToListAsync();
		}
	}
}
