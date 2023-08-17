using AutoMapper;
using FirstApi.Models;
using FirstApi.Tables;

namespace FirstApi
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, StudentModel>()
                .ForMember(des => des.Major_Name, src => src.MapFrom(b => b.Major.Name)).ReverseMap();
       

        }
    }
}
