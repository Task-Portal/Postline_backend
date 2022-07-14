using AutoMapper;
using Entities.Models;
using Shared.DataTransferObjects;
using Shared.DataTransferObjects.ForAuth;
using Shared.DataTransferObjects.ForCreation;
using Shared.DataTransferObjects.ForShow;
using Shared.DataTransferObjects.ForUpdate;

namespace Postline
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserForRegistrationDto, User>()  
                .ForMember(u => u.UserName, opt => opt.MapFrom(x => x.Email));;
            CreateMap<User,UserDto>();


            CreateMap<Post, PostDto>()
                .ForMember(pd => pd.CategoryName, p => p.MapFrom(x => x.Category.CategoryName))
                .ForMember(pd=>pd.FirstName,p=>p.MapFrom(x=>x.User.FirstName))
                .ForMember(pd=>pd.LastName,p=>p.MapFrom(x=>x.User.LastName))
                .ReverseMap();
            CreateMap<PostForCreationDto, Post>();
                // .ForMember(p => p.User.Id, pc => pc.MapFrom(x => x.UserId))
                // .ForMember(p => p.Category.Id, pc => pc.MapFrom(x => x.CategoryId));
            CreateMap<PostForUpdateDto, Post>();
            
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CategoryForCreationDto, Category>();
            CreateMap<CategoryForUpdateDto, Category>();
            
            CreateMap<Comment, CommentDto>();

        }
        
        // CreateMap<Customer, CustomerModel>()
        // .ForMember(cm => cm.ReceiptsIds, s => s.MapFrom(x => x.Receipts.Select(y => y.Id)))
        // .ForMember(cm => cm.DiscountValue, s => s.MapFrom(x => x.DiscountValue))
        // .ForMember(cm => cm.Name, s => s.MapFrom(x => x.Person.Name))
        // .ForMember(cm => cm.Surname, s => s.MapFrom(x => x.Person.Surname))
        // .ForMember(cm => cm.BirthDate, s => s.MapFrom(x => x.Person.BirthDate))
        // .ReverseMap();
    }
}