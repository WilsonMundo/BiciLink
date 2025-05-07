namespace BiciReserva.Middelwares
{
    public class DecodificaUrl
    {
        private readonly RequestDelegate _next;

        public DecodificaUrl(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var request = context.Request;
            var path = request.Path.Value;

            
            var decodedPath = Uri.UnescapeDataString(path ?? "");

            
            request.Path = decodedPath;

            await _next(context);
        }
    }
}
