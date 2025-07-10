using System.Net.Http.Headers;

namespace FinalProjectConsume.Helpers
{
    public class TokenHandler : DelegatingHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TokenHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            // Get the token from the session (you might need to adapt this based on how you store the token)
            var token = _httpContextAccessor.HttpContext.Session.GetString("AuthToken");

            if (!string.IsNullOrEmpty(token))
            {
                // Add the token to the Authorization header
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
