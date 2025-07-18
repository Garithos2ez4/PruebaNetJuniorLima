using MatriculaApi.Data;
using MatriculaApi.Dtos;
using MatriculaApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MatriculaApi.Services
{
    public class MatriculaService
    {
        private readonly AplicacionDbContext _context; // <- este es tu DbContext

        public MatriculaService(AplicacionDbContext context)
        {
            _context = context;
        }
        public async Task<List<MatriculaDto>> ObtenerMatriculas()
        {
            return await _context.matricula
                .Include(m => m.Estudiante)
                .Include(m => m.Curso)
                .Select(m => new MatriculaDto
                {
                    EstudianteId = m.EstudianteId,
                    CursoId = m.CursoId,
                    FechaMatricula = m.FechaMatricula,
                    Estado = m.Estado,
                    NombreCurso = m.Curso.Nombre,
                    NombreEstudiante = m.Estudiante.Nombre
                })
                .ToListAsync();
        }

        public async Task<string> CrearMatricula(MatriculacreadaDTO dto)
        {
            // 1. Validar existencia
            var student = await _context.estudiante.FindAsync(dto.EstudianteId);
            var course = await _context.curso.FindAsync(dto.CursoId);

            if (student == null || course == null)
                return "Estudiante o curso no existen";

            // 2. No duplicar matrícula
            var exists = await _context.matricula.AnyAsync(e =>
                e.EstudianteId == dto.EstudianteId && e.CursoId == dto.CursoId);
            if (exists) return "Ya está matriculado en este curso";

            // 3. Validar fecha
            if (dto.FechaMatricula > DateTime.Now)
                return "La fecha no puede ser futura";

            // 4. Guardar
            var enrollment = new Matricula
            {
                EstudianteId = dto.EstudianteId,
                CursoId = dto.CursoId,
                FechaMatricula = dto.FechaMatricula,
                Estado = "Activa"
            };

            _context.matricula.Add(enrollment);
            await _context.SaveChangesAsync();

            return "OK";
       
    }
        public async Task<string> ActualizarEstado(int id, string nuevoEstado)
        {
            var matricula = await _context.matricula.FindAsync(id);
            if (matricula == null)
                return "Matrícula no encontrada";

            if (matricula.Estado == "Finalizada" && nuevoEstado == "Cancelada")
                return "No se puede cancelar una matrícula finalizada";

            matricula.Estado = nuevoEstado;
            _context.matricula.Update(matricula);
            await _context.SaveChangesAsync();
            return "OK";
        }

        public async Task<string> EliminarMatricula(int id)
        {
            // 1. Buscar matrícula
            var matricula = await _context.matricula.FindAsync(id);

            if (matricula == null)
                return "Matrícula no encontrada.";

            // 2. Validar que el estado sea "Cancelada"
            if (matricula.Estado != "Cancelada")
                return "Solo se puede eliminar una matrícula con estado 'Cancelada'.";

            // 3. Eliminar
            _context.matricula.Remove(matricula);

            try
            {
                await _context.SaveChangesAsync();
                return "OK";
            }
            catch (Exception ex)
            {
                return $"Error al eliminar matrícula: {ex.Message}";
            }
        }

    }
}
