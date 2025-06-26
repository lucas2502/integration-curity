using Domain.Entities;

namespace Infrastructure.Repositories
{
    public class CredentialRepository
    {
        private readonly AppDbContext _db;

        public CredentialRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task SaveAsync(Credential credential)
        {
            _db.Credentials.Add(credential);
            await _db.SaveChangesAsync();
        }
    }
}