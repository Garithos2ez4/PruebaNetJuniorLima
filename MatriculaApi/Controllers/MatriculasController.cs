using Autofac.Core;
using MatriculaApi.Dtos;
using MatriculaApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace MatriculaApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatriculasController : ControllerBase
    {
        private readonly MatriculaService _service;
        public MatriculasController(MatriculaService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerMatriculas()
        {
            var matriculas = await _service.ObtenerMatriculas();
            return Ok(matriculas);
        }

        [HttpPost]
        public async Task<IActionResult> CrearMatricula([FromBody] MatriculacreadaDTO dto)
        {
            if (dto == null)
                return BadRequest("Datos de matrícula inválidos");
            var result = await _service.CrearMatricula(dto);
            if (result == "OK")
                return Ok("Matrícula creada exitosamente");
            else
                return BadRequest(result);
        }

                            [HttpPut("{id}/estado")]
        public async Task<IActionResult> ActualizarEstado(int id, [FromBody] ActualizarEstadoDto dto)
        {
            if (dto == null || string.IsNullOrEmpty(dto.Estado))
                return BadRequest("Datos de matrícula inválidos");
            var result = await _service.ActualizarEstado(id, dto.Estado);
            if (result == "OK")
                return Ok("Estado actualizado exitosamente");
            else
                return BadRequest(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _service.EliminarMatricula(id);
            return result == "OK" ? Ok("Matrícula eliminada") : BadRequest(result);
        }
    }
}
