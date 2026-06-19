using BansiModelos;
using System.Data;
using Microsoft.Data.SqlClient;
using Dapper;
namespace BansiLibreriaAccesoDatos.Helpers;

public class StoredProceduresMethods
{
    private static List<string> connectionStrings = new()
    {
        "data source=(localdb)\\mssqllocaldb;initial catalog=BansiApp;integrated security=True;MultipleActiveResultSets=True;"
    };
    public static List<Examen> SPConsultar(FiltroBusqueda filtros)
    {
        var registros = new List<Examen>();
        using (var connection = new SqlConnection(connectionStrings.First()))
        {
            try
            {
                connection.Open();
                var results = connection.Query<Examen>(
                    "spConsultar",
                    new { Nombre = filtros.Nombre, Descripcion = filtros.Descripcion },
                    commandType: CommandType.StoredProcedure);

                registros = results?.ToList() ?? new List<Examen>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return registros;
    }
    public static Examen SPConsultar(int id)
    {
        var registros = new List<Examen>();
        using (var connection = new SqlConnection(connectionStrings.First()))
        {
            try
            {
                connection.Open();
                var results = connection.Query<Examen>(
                    "spConsultar",
                    new { id = id },
                    commandType: CommandType.StoredProcedure);

                registros = results?.ToList() ?? new List<Examen>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return registros.FirstOrDefault();
    }
    public static ApiResponse SPAgregar(Examen examen) => 
        StoredProcedureResponse("spAgregar", examen);
    
    public static ApiResponse SPActualizar(Examen examen) =>
        StoredProcedureResponse("spActualizar", examen);

    public static ApiResponse SPEliminar(Examen examen)=>
        StoredProcedureResponse("spEliminar", examen);

    private static ApiResponse StoredProcedureResponse(string spName, Examen examen)
    {
        var response = new ApiResponse();
        using (var connection = new SqlConnection(connectionStrings.First()))
        {
            try
            {
                connection.Open();

                var param = new
                {
                    Id = examen.IdExamen,
                    Nombre = string.IsNullOrWhiteSpace(examen.Nombre) ? null : examen.Nombre,
                    Descripcion = string.IsNullOrWhiteSpace(examen.Descripcion) ? null : examen.Descripcion
                };

                var row = connection.QueryFirstOrDefault(spName, param, commandType: CommandType.StoredProcedure);

                if (row is not null)
                {
                    if (row is IDictionary<string, object> dict && dict.Count > 0)
                    {
                        var values = dict.Values.ToArray();
                        response.Exito = Convert.ToInt32(values.ElementAtOrDefault(0) ?? 1) == 0;
                        response.Descripcion = values.ElementAtOrDefault(1)?.ToString();
                    }
                    else
                    {
                        var type = row.GetType();
                        var firstProp = type.GetProperties().FirstOrDefault();
                        var secondProp = type.GetProperties().Skip(1).FirstOrDefault();
                        if (firstProp != null)
                        {
                            var firstVal = firstProp.GetValue(row);
                            response.Exito = Convert.ToInt32(firstVal ?? 1) == 0;
                        }
                        if (secondProp != null)
                            response.Descripcion = secondProp.GetValue(row)?.ToString();
                    }
                }
                else
                {
                    response.Exito = false;
                    response.Descripcion = "No response from stored procedure.";
                }
            }
            catch (Exception ex)
            {
                response.Exito = false;
                response.Descripcion = ex.Message;
            }
        }
        return response;
    }
}
