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
                .ForMember(d => d.Author, O => O.MapFrom(s => s.Author.Name));
            CreateMap<BookDto, Book>().ReverseMap();
            CreateMap<Author, AuthorToReturnDto>()
                .ForMember(d=>d.Books,O=>O.MapFrom(s=>s.Books.Select(a=>a.Title)));
        }
    }
}
