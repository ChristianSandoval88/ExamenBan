using BansiAPI.Repositorios.Interfaces;
using BansiModelos;
using Microsoft.AspNetCore.Mvc;

namespace BansiAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ExamenController(IExamenRepositorio repositorio) : ControllerBase
{
    [HttpGet("ObtenerExamenes")]
    public async Task<List<Examen>> Get([FromQuery]string? Nombre, string? Descripcion)
    { 
        if(string.IsNullOrWhiteSpace(Nombre)&& string.IsNullOrWhiteSpace(Descripcion))
            return await repositorio.ConsultarExamenes();
        return await repositorio.ConsultarExamenes(Nombre!, Descripcion!);
    }

    [HttpGet]
    public async Task<List<Examen>> Get() =>
        await repositorio.ConsultarExamenes();

    [HttpGet("{id}")]
    public async Task<Examen> Get(int id) =>
        await repositorio.ConsultarExamen(id);

    [HttpPost]
    public async Task<ApiResponse> Post([FromBody] Examen examen)
    {
        if (string.IsNullOrEmpty(examen.Nombre) || string.IsNullOrEmpty(examen.Descripcion))
            return new ApiResponse() { Exito = false, Descripcion = "El nombre y la descripción son obligatorios" };
        if (examen.IdExamen != 0)
            return new ApiResponse() { Exito = false, Descripcion = "El IdExamen debe ser 0 para agregar un nuevo examen" };

        return await repositorio.AgregarExamen(examen);
    }

    [HttpPut("{id}")]
    public async Task<ApiResponse> Put(int id, [FromBody] Examen examen)
    {
        if (string.IsNullOrEmpty(examen.Nombre) || string.IsNullOrEmpty(examen.Descripcion))
            return new ApiResponse() { Exito = false, Descripcion = "El nombre y la descripción son obligatorios" };
        if (examen.IdExamen == 0)
            return new ApiResponse() { Exito = false, Descripcion = "El IdExamen debe ser diferente de 0 para actualizar un examen" };

        if (await Get(id) is null)
            return new ApiResponse() { Exito = false, Descripcion = "No se encontró un examen con el Id proporcionado" };

        return await repositorio.ActualizarExamen(examen);
    }

    [HttpDelete("{id}")]
    public async Task<ApiResponse> Delete(int id)
    {
        if (id == 0)
            return new ApiResponse() { Exito = false, Descripcion = "El IdExamen debe ser diferente de 0 para eliminar un examen" };

        if (await Get(id) is null)
            return new ApiResponse() { Exito = false, Descripcion = "No se encontró un examen con el Id proporcionado" };

        return await repositorio.EliminarExamen(id);
    }
}
