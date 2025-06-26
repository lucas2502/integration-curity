using System.Net.Http;
using System.Net.Http.Json;
using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;

namespace Infrastructure.Services
{
    public class CurityCredentialService : ICurityCredentialService
    {
        private readonly HttpClient _httpClient;

        public CurityCredentialService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Credential> CreateCredentialAsync(CurityEvent data)
        {
            var response = await _httpClient.PostAsJsonAsync("/credentials", data);
            response.EnsureSuccessStatusCode();
            var result = await response.Content.ReadFromJsonAsync<CurityResponse>();
            return new Credential(result!.ClientId, result.ClientSecret);
        }

        private class CurityResponse
        {
            public string ClientId { get; set; } = default!;
            public string ClientSecret { get; set; } = default!;
        }
    }
}