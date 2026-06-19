using BansiModelos;
using Newtonsoft.Json;

namespace BansiLibreriaAccesoDatos.Helpers;
public class HttpMethods
{
    private static HttpClient httpClient = new()
    {
        BaseAddress = new Uri("https://localhost:7216/api/Examen"),
    };
    public static async Task<Examen> WSConsultarAsync(int id)
    {
        var query = $"{httpClient.BaseAddress}/{id}";
        var response = await httpClient.GetAsync(query);
        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<Examen>(responseBody);
        }
        return new Examen();
    }
    public static async Task<List<Examen>> WSConsultarAsync(FiltroBusqueda filtros)
    {
        var query = $"{httpClient.BaseAddress}/ObtenerExamenes?Nombre={filtros.Nombre}&Descripcion={filtros.Descripcion}";
        if (string.IsNullOrEmpty(filtros.Nombre) || string.IsNullOrEmpty(filtros.Descripcion))
            query = $"{httpClient.BaseAddress}";
        if(filtros.IdExamen!=0)
            query = $"{httpClient.BaseAddress}/{filtros.IdExamen}";
        var response = await httpClient.GetAsync(query);
        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Examen>>(responseBody);
        }
        return new List<Examen>();
    }
    public static async Task<ApiResponse> WSAgregarAsync(Examen examen)
    {
        var json = JsonConvert.SerializeObject(examen);
        StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync($"{httpClient.BaseAddress}", content);
        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse>(responseBody);
        }
        return new ApiResponse() { Exito = false, Descripcion = "Error al agregar el examen" };
    }
    public static async Task<ApiResponse> WSActualizarAsync(Examen examen)
    {
        var json = JsonConvert.SerializeObject(examen);
        StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await httpClient.PutAsync($"{httpClient.BaseAddress}/{examen.IdExamen}", content);

        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse>(responseBody);
        }
        return new ApiResponse() { Exito = false, Descripcion = "Error al actualizar el examen" };
    }
    public static async Task<ApiResponse> WSEliminarAsync(Examen examen)
    {
        var json = JsonConvert.SerializeObject(examen);
        StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
        var response = await httpClient.DeleteAsync($"{httpClient.BaseAddress}/{examen.IdExamen}");
        if (response.IsSuccessStatusCode)
        {
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ApiResponse>(responseBody);
        }
        return new ApiResponse() { Exito = false, Descripcion = "Error al eliminar el examen" };
    }
}
