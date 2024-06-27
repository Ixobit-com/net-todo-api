using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using Todo.Common;
using Todo.Common.Constants;
using Todo.Common.Email.Data;
using Todo.Common.Helpers;
using Todo.Common.Logging;
using Todo.Common.Settings;
using Todo.Data.Domain.Contracts;
using Todo.Data.Domain.Models.Identity;
using Todo.Logic.Domain.Contracts;
using Todo.Logic.Domain.Models.Filters;
using Todo.Logic.Services.Base;

namespace Todo.Logic.Services {
    public class ClientService : BaseCrudService<Client, Guid, ClientFilters, ClientOrderByField>, IClientService {
        private readonly IEmailService _emailService;
        private readonly ClientHasherSettings _clientHasherSettings;

        public ClientService(
            IUnitOfWork unitOfWork,
            Logger logger,
            IMapper mapper,
            IEmailService emailService,
            IOptions<ClientHasherSettings> clientHasherSettings)
            : base(unitOfWork, logger, mapper) {
            _emailService = emailService;
            _clientHasherSettings = clientHasherSettings.Value;
        }

        public async Task<ServiceResult> ResetAsync(Guid id) {
            try {
                var client = _unitOfWork.GetRepository<Client>()
                    .Get(x => x.Id == id, true)
                    .FirstOrDefault();

                if (client == null) {
                    return ServiceResult.Failed(ResultCodes.Authorization.CLIENT_NOT_FOUND);
                }

                var credentials = ResetClientCredentials(client);

                await _unitOfWork.CommitAsync();

                await _emailService.SendCreateNewClientEmailAsync(
                    new CreateNewClientEmailData {
                        Name = client.Name,
                        Key = client.Key,
                        Secret = credentials.Secret
                    });

                return ServiceResult.Successed();
            }
            catch (Exception ex) {
                _logger.Error(ex, $"Reset client credentials exception occured. Details: [{id}]");
                return ServiceResult.InternalServerError(ex.Message);
            }
        }

        protected override Guid GetNewKey() {
            return Guid.NewGuid();
        }

        protected override async Task OnCreatingAsync<TCreateDto>(Client entity, TCreateDto dto) {
            var credentials = ResetClientCredentials(entity);

            // Store it in DTO to send it to admin via email
            dto.SetMetadata(MetadataNames.Client.Secret, credentials.Secret);
        }

        protected override async Task OnCreatedAsync<TCreateDto>(Client entity, TCreateDto dto) {
            string secret = dto.GetMetadata<string>(MetadataNames.Client.Secret);

            await _emailService.SendCreateNewClientEmailAsync(
                new CreateNewClientEmailData {
                    Name = entity.Name,
                    Key = entity.Key,
                    Secret = secret
                });
        }

        protected override List<Expression<Func<Client, bool>>>? GetAdvancedConditions(ClientFilters filters) {
            var conditions = new List<Expression<Func<Client, bool>>>();

            if (!String.IsNullOrEmpty(filters.SearchText)) {
                conditions.Add(x => x.Name.ToLower().Contains(filters.SearchText.ToLower()));
            }

            if (filters.IsActive.HasValue) {
                conditions.Add(x => x.IsActive == filters.IsActive.Value);
            }

            return conditions;
        }

        protected override IQueryable<Client> OrderBy(IQueryable<Client> query, ClientOrderByField orderBy, bool desc) {
            switch (orderBy) {
                case ClientOrderByField.Name:
                    return (desc) ? query.OrderByDescending(x => x.Name) : query.OrderBy(x => x.Name);
                case ClientOrderByField.IsActive:
                    return (desc) ? query.OrderByDescending(x => x.IsActive) : query.OrderBy(x => x.IsActive);
                default:
                    return base.OrderBy(query, orderBy, desc);
            }
        }

        protected override IQueryable<Client> GetEntityIncludesToUpdate(IQueryable<Client> query) {
            return query
                .Include(x => x.Scopes)
                    .ThenInclude(x => x.Scope);
        }

        private (string Key, string Secret) ResetClientCredentials(Client client) {
            string secret = ClientHelper.GenerateSecret();

            client.Key = ClientHelper.GenerateKey();
            client.SecretSalt = ClientHelper.GenerateSalt();
            client.SecretHash = ClientHelper.ComputeHash(secret, client.SecretSalt, _clientHasherSettings.Pepper, _clientHasherSettings.Iterations);

            return (client.Key, secret);
        }
    }
}