using AutoMapper;

namespace service.Mapping
{
    /// <summary>
    /// Class defining the AutoMapper mapping profiles for mapping between different object types.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Constructor for the MappingProfile class.
        /// </summary>
        public MappingProfile()
        {
            // Map from the 'User' entity in the database to the 'User' model in
            // the service layer. Also, concatenate FirstName and LastName to populate the 'Name' property.
            CreateMap<database.Models.User, service.Models.User>()
                .ForMember(u => u.Name, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

            // Map from the 'Job' model in the service layer to the 'Job' entity in the database.
            CreateMap<service.Models.Job, database.Models.Job>();

            // Map from the 'Job' entity in the database to the 'Job' model in the service layer.
            CreateMap<database.Models.Job, service.Models.Job>();

            // Map from the 'SingleSignOn' model in the service layer to the 'SingleSignOn' entity in the database.
            CreateMap<service.Models.SingleSignOn, database.Models.SingleSignOn>();

            // Map from the 'ProviderData' model in the service layer to the 'ProviderData' entity in the database.
            CreateMap<service.Models.ProviderData, database.Models.ProviderData>();
        }
    }
}
