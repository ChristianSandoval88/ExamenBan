using BansiModelos;
using Microsoft.AspNetCore.Mvc;

namespace BansiAPI.Repositorios.Interfaces;

public interface IExamenRepositorio
{
    Task<List<Examen>> ConsultarExamenes();
    Task<Examen> ConsultarExamen(int id);
    Task<ApiResponse> AgregarExamen(Examen examen);
    Task<ApiResponse> ActualizarExamen(Examen examen);
    Task<ApiResponse> EliminarExamen(int id);
    Task<List<Examen>> ConsultarExamenes([FromQuery] string Nombre, string Descripcion);
}
