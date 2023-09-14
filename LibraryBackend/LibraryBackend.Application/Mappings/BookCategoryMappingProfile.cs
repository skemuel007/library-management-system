using AutoMapper;
using LibraryBackend.Application.Dtos.Request;
using LibraryBackend.Application.Dtos.Response;
using LibraryBackend.Core.Entities;

namespace LibraryBackend.Application.Mappings;

public class BookCategoryMappingProfile : Profile
{
    public BookCategoryMappingProfile()
    {
        CreateMap<CreateBookDto, Book>().ReverseMap();
        CreateMap<Book, BookResponseDto>().ReverseMap();
        CreateMap<CreateCategoryDto, Category>().ReverseMap();
        CreateMap<Category, CategoryResponseDto>().ReverseMap();
    }
}