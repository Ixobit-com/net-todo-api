using AutoMapper;
using Todo.API.Models;
using Todo.Data.Domain.Models;
using Todo.Data.Domain.Models.Identity;
using Todo.Logic.Domain.Models;

namespace Todo.API.Configurations {
    public static class AutoMapperConfiguration {
        public static void ConfigureAutoMapper(this IServiceCollection services) {
            services.AddScoped(provider => new MapperConfiguration(cfg => {
                cfg.AddProfile(new TokenProfile());
                cfg.AddProfile(new ClientProfile());
                cfg.AddProfile(new ScopeProfile());
                cfg.AddProfile(new UserProfile());
                cfg.AddProfile(new RoleProfile());
                cfg.AddProfile(new EmailTemplateProfile());
                cfg.AddProfile(new LabelProfile());
                cfg.AddProfile(new SprintProfile());
                cfg.AddProfile(new TicketProfile());
            }).CreateMapper());
        }
    }

    public class TokenProfile : Profile {
        public TokenProfile() {
            CreateMap<AccessTokenDto, AccessTokenModel>();
        }
    }

    public class ClientProfile : Profile {
        public ClientProfile() {
            CreateMap<ClientDto, Client>()
                .ForMember(trg => trg.Scopes, opt => opt.Ignore())
                .AfterMap((src, dest, context) => {
                    if (dest.Scopes == null) {
                        dest.Scopes = new List<ClientScope>();
                    }

                    dest.Scopes.Clear();

                    if (src.Scopes != null) {
                        foreach (var scope in src.Scopes) {
                            dest.Scopes.Add(new ClientScope { ScopeId = scope.Id });
                        }
                    }
                })
                .ReverseMap()
                .ForMember(trg => trg.Scopes, opt => opt.MapFrom(src => src.Scopes.Select(x => x.Scope))); ;

            CreateMap<Client, ClientModel>();
            CreateMap<Client, ClientDetailsModel>()
                .ForMember(trg => trg.Scopes, opt => opt.MapFrom(src => src.Scopes.Select(x => x.Scope)));
            CreateMap<Client, ClientKeyValueModel>();

            CreateMap<ClientCreateModel, ClientDto>()
                .ForMember(trg => trg.Scopes, opt => opt.MapFrom(src => src.ScopeIds.Select(x => new ScopeDto { Id = x })));
            CreateMap<ClientUpdateModel, ClientDto>()
                .ForMember(trg => trg.Scopes, opt => opt.MapFrom(src => src.ScopeIds.Select(x => new ScopeDto { Id = x })));
        }
    }

    public class ScopeProfile : Profile {
        public ScopeProfile() {
            CreateMap<Scope, ScopeModel>();
            CreateMap<Scope, ScopeDetailsModel>();
            CreateMap<Scope, ScopeKeyValueModel>();
        }
    }

    public class UserProfile : Profile {
        public UserProfile() {
            CreateMap<User, UserModel>();
            CreateMap<User, UserDetailsModel>()
                .ForMember(trg => trg.Roles, opt => opt.MapFrom(src => src.UserRoles.Select(x => x.Role)));
            CreateMap<User, ProfileModel>();
            CreateMap<User, UserKeyValueModel>()
                .ForMember(trg => trg.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            CreateMap<UserCreateModel, UserDto>()
                .ForMember(trg => trg.Roles, opt => opt.MapFrom(src => src.RoleIds.Select(x => new RoleDto { Id = x })));
            CreateMap<UserUpdateModel, UserDto>()
                .ForMember(trg => trg.Roles, opt => opt.MapFrom(src => src.RoleIds.Select(x => new RoleDto { Id = x })));
            CreateMap<ProfileUpdateModel, ProfileDto>();

            CreateMap<UserDto, User>()
                .ForMember(trg => trg.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }

    public class RoleProfile : Profile {
        public RoleProfile() {
            CreateMap<Role, RoleModel>();
            CreateMap<Role, RoleDetailsModel>();
            CreateMap<Role, RoleKeyValueModel>();
        }
    }

    public class EmailTemplateProfile : Profile {
        public EmailTemplateProfile() {
            CreateMap<EmailTemplateDto, EmailTemplate>()
                .ReverseMap();

            CreateMap<EmailTemplate, EmailTemplateModel>();
            CreateMap<EmailTemplate, EmailTemplateDetailsModel>();

            CreateMap<EmailTemplateUpdateModel, EmailTemplateDto>();
        }
    }

    public class LabelProfile : Profile {
        public LabelProfile() {
            CreateMap<LabelDto, Label>()
                .ReverseMap();

            CreateMap<Label, LabelModel>();
            CreateMap<Label, LabelDetailsModel>();
            CreateMap<Label, LabelKeyValueModel>();

            CreateMap<LabelCreateModel, LabelDto>();
            CreateMap<LabelUpdateModel, LabelDto>();
        }
    }

    public class SprintProfile : Profile {
        public SprintProfile() {
            CreateMap<SprintDto, Sprint>()
                .ReverseMap();

            CreateMap<Sprint, SprintModel>();
            CreateMap<Sprint, SprintDetailsModel>();
            CreateMap<Sprint, SprintKeyValueModel>();

            CreateMap<SprintCreateModel, SprintDto>();
            CreateMap<SprintUpdateModel, SprintDto>();
        }
    }

    public class TicketProfile : Profile {
        public TicketProfile() {
            CreateMap<TicketDto, Ticket>()
                .ForMember(trg => trg.Labels, opt => opt.Ignore())
                .AfterMap((src, dest, context) => {
                    if (dest.Labels == null) {
                        dest.Labels = new List<TicketLabel>();
                    }

                    dest.Labels.Clear();

                    if (src.Labels != null) {
                        foreach (var label in src.Labels) {
                            dest.Labels.Add( new TicketLabel { LabelId = label.Id });
                        }
                    }
                })
                .ReverseMap()
                .ForMember(trg => trg.Labels, opt => opt.MapFrom(src => src.Labels.Select(x => x.Label)));

            CreateMap<Ticket, TicketModel>();
            CreateMap<Ticket, TicketDetailsModel>()
                .ForMember(trg => trg.Labels, opt => opt.MapFrom(src => src.Labels.Select(x => x.Label)));
            CreateMap<Ticket, TicketKeyValueModel>();

            CreateMap<TicketCreateModel, TicketDto>()
                .ForMember(trg => trg.Labels, opt => opt.MapFrom(src => src.LabelIds.Select(x => new LabelDto { Id = x })));
            CreateMap<TicketUpdateModel, TicketDto>()
                .ForMember(trg => trg.Labels, opt => opt.MapFrom(src => src.LabelIds.Select(x => new LabelDto { Id = x })));
        }
    }
}