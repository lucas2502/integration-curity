using Domain.Entities;
using Domain.Events;

namespace Domain.Interfaces
{
    public interface ICurityCredentialService
    {
        Task<Credential> CreateCredentialAsync(CurityEvent data);
    }
}