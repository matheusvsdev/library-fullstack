using AutoMapper;
using BackendLibrary.Dtos.Book;
using BackendLibrary.Dtos.Category;
using BackendLibrary.Models;

namespace BackendLibrary.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Book, BookResponse>();
            CreateMap<Category, CategoryResponse>();

            CreateMap<CreateBookRequest, Book>()
                .ForMember(dest => dest.Categories, opt => opt.Ignore());
                
            CreateMap<UpdateBookRequest, Book>()
                .ForMember(dest => dest.Categories, opt => opt.Ignore());;

            CreateMap<CreateCategoryRequest, Category>();
            CreateMap<UpdateCategoryRequest, Category>();
        }
    }
}