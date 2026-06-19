using BansiLibreriaAccesoDatos.Helpers;
using BansiModelos;

namespace BansiLibreriaAccesoDatos;

public class ClsExamen
{
    private readonly TipoAccesoDatos tipoAccesoDatos;

    public ClsExamen(TipoAccesoDatos tipoAccesoDatos)
    {
        this.tipoAccesoDatos = tipoAccesoDatos;
    }
    public async Task<Examen> ConsultarExamenAsync(int id)
    {
        if (tipoAccesoDatos == TipoAccesoDatos.WebService)
            return await HttpMethods.WSConsultarAsync(id);
        else if (tipoAccesoDatos == TipoAccesoDatos.StoredProcedures)
            return StoredProceduresMethods.SPConsultar(id);
        return new Examen();
    }
    public async Task<List<Examen>> ConsultarExamenAsync(FiltroBusqueda filtros)
    {
        if (tipoAccesoDatos == TipoAccesoDatos.WebService)
            return await HttpMethods.WSConsultarAsync(filtros);
        else if (tipoAccesoDatos == TipoAccesoDatos.StoredProcedures)
            return StoredProceduresMethods.SPConsultar(filtros);
        return new List<Examen>();
    }
    public async Task<ApiResponse> AgregarExamenAsync(Examen examen)
    {
        if (tipoAccesoDatos == TipoAccesoDatos.WebService)
            return await HttpMethods.WSAgregarAsync(examen);
        else if (tipoAccesoDatos == TipoAccesoDatos.StoredProcedures)
            return StoredProceduresMethods.SPAgregar(examen);
        return new ApiResponse();
    }
    public async Task<ApiResponse> ActualizarExamenAsync(Examen examen)
    {
        if (tipoAccesoDatos == TipoAccesoDatos.WebService)
            return await HttpMethods.WSActualizarAsync(examen);
        else if (tipoAccesoDatos == TipoAccesoDatos.StoredProcedures)
            return StoredProceduresMethods.SPActualizar(examen);
        return new ApiResponse();
    }
    public async Task<ApiResponse> EliminarExamenAsync(Examen examen)
    {
        if (tipoAccesoDatos == TipoAccesoDatos.WebService)
            return await HttpMethods.WSEliminarAsync(examen);
        else if (tipoAccesoDatos == TipoAccesoDatos.StoredProcedures)
            return StoredProceduresMethods.SPEliminar(new Examen() { IdExamen = examen.IdExamen, Nombre = "", Descripcion = "" });
        return new ApiResponse();
    }
}