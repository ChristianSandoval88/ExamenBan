using BansiAPI.Repositorios.Interfaces;
using BansiModelos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BansiAPI.Repositorios;

public class ExamenRepositorio(AppDbContext dbContext) : IExamenRepositorio
{
    public async Task<ApiResponse> ActualizarExamen(Examen examen)
    {
        var examenBusqueda = await dbContext.Examenes.FindAsync(examen.IdExamen);
        if (examenBusqueda != null)
        {
            examenBusqueda.Nombre = examen.Nombre;
            examenBusqueda.Descripcion = examen.Descripcion;
            return await dbContext.SaveChangesAsync() > 0
            ? new ApiResponse { Exito = true, Descripcion = "Examen actualizado" } :
            new ApiResponse { Exito = false, Descripcion = "Error al actualizar el examen" };
        }
        return new ApiResponse { Exito = false, Descripcion = "No se encontró un examen con el Id proporcionado" };
    }

    public async Task<ApiResponse> AgregarExamen(Examen examen)
    {
        dbContext.Examenes.Add(examen);
        return await dbContext.SaveChangesAsync() > 0
            ? new ApiResponse { Exito = true, Descripcion = "Examen agregado" } :
            new ApiResponse { Exito = false, Descripcion = "Error al agregar el examen" };
    }

    public async Task<List<Examen>> ConsultarExamenes() =>
        await dbContext.Examenes.ToListAsync();
    public async Task<List<Examen>> ConsultarExamenes(string Nombre, string Descripcion) =>
        await dbContext.Examenes.Where(x=> x.Nombre!.Trim().ToLower().Contains(Nombre.Trim().ToLower())||x.Descripcion!.Trim().ToLower().Contains(Descripcion.Trim().ToLower())).ToListAsync();

    public async Task<Examen> ConsultarExamen(int id) =>
        await dbContext.Examenes.FirstOrDefaultAsync(e => e.IdExamen == id);

    public async Task<ApiResponse> EliminarExamen(int id)
    {
        var examenBusqueda = await dbContext.Examenes.FindAsync(id);
        if (examenBusqueda != null)
        {
            dbContext.Examenes.Remove(examenBusqueda);
            return await dbContext.SaveChangesAsync() > 0
            ? new ApiResponse { Exito = true, Descripcion = "Examen actualizado" } :
            new ApiResponse { Exito = false, Descripcion = "Error al actualizar el examen" };
        }
        return new ApiResponse { Exito = false, Descripcion = "No se encontró un examen con el Id proporcionado" };
    }
}
