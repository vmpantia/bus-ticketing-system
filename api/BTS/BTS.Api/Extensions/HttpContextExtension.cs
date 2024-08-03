namespace BTS.Api.Extensions
{
    public static class HttpContextExtension
    {
        public static string GetBearerToken(this HttpContext context)
        {
            var authHeader = context.Request.Headers["Authorization"];
            if (authHeader.Count > 0 && authHeader[0].StartsWith("Bearer "))
                return authHeader[0].Substring("Bearer ".Length).Trim();

            return string.Empty;
        }
    }
}
