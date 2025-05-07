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
        public string GetConnectionString(string baseConnectionString)
        {
            var httpContext = _httpContextAccessor.HttpContext;

            var token = httpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                token = httpContext.Request.Cookies["AuthToken"];
            }
            else
            {
                var databaseName = ExtractDatabaseNameFromToken(token);
                return ValidateConnectionString(baseConnectionString, databaseName);
            }

            return "";
            
        }
        private string ExtractDatabaseNameFromToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwtToken = handler.ReadToken(token.Replace("Bearer ", "")) as JwtSecurityToken;
            var databaseName = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "bsCo")?.Value;
            return databaseName ?? throw new Exception("Token vacio");
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
