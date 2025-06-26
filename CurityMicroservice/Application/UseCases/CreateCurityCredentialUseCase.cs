using Domain.Entities;
using Domain.Events;
using Domain.Interfaces;

namespace Application.UseCases
{
    public class CreateCurityCredentialUseCase
    {
        private readonly ICurityCredentialService _curityService;

        public CreateCurityCredentialUseCase(ICurityCredentialService curityService)
        {
            _curityService = curityService;
        }

        public async Task<Credential> ExecuteAsync(CurityEvent data)
        {
            return await _curityService.CreateCredentialAsync(data);
        }
    }
}