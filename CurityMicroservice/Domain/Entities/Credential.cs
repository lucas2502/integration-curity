namespace Domain.Entities
{
    public class Credential
    {
        public string ClientId { get; }
        public string ClientSecret { get; }

        public Credential(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
        }
    }
}