using AutoMapper;
using Data.Core.Domain;
using OTM.Models.GroupViewModels;

namespace OTM
{
    public class MainProfile : Profile
    {
        public MainProfile()
        {
            CreateMap<Group, CreateGroupViewModel>();
            CreateMap<Group, EditGroupViewModel>().ForMember(dest => dest.Students,
                opt => opt.Ignore());
            CreateMap<User, EditStudentInGroup>();
        }
    }
}
