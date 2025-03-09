using AutoMapper;
using Bookly.APIs.DTOs;
using Bookly.APIs.Entities;

namespace Bookly.APIs.Helpers

{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Book, BookToReturnDto>()
                .ForMember(d => d.Author, O => O.MapFrom(s => s.Author.Name))
                .ForMember(d=>d.PictureUrl,O=>O.MapFrom<BooksPictureUrlResolver>());
            CreateMap<BookDto, Book>().ReverseMap();
            CreateMap<BookForAuthorDto, Book>().ReverseMap();
            CreateMap<Author, AuthorToReturnDto>()
                .ForMember(d=>d.Books,O=>O.MapFrom(s=>s.Books.Select(a=>a.Title)));
            CreateMap<AuthorDto, Author>().ReverseMap();
        }
    }
}
