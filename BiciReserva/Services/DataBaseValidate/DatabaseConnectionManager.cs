using System.IdentityModel.Tokens.Jwt;

namespace BiciReserva.Services.DataBaseValidate
{
    public class DatabaseConnectionManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DatabaseConnectionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        
        public string ValidateConnectionString(string baseConnectionString, string databaseName)
        {
            string conexion = string.Empty;

            if (!string.IsNullOrEmpty(baseConnectionString))
            {
                conexion = baseConnectionString.Replace("{XXXXX}", databaseName);
            }
            else
                throw new Exception("Cadena de conexion vacia");

            return conexion;
        }

    }
}
