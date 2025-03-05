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
        }
    }
}
