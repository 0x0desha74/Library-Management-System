using AutoMapper;
using Bookly.APIs.DTOs;
using Bookly.APIs.Entities;

namespace Bookly.APIs.Helpers
{
    public class BooksPictureUrlResolver : IValueResolver<Book,BookToReturnDto,string>
    {
        private IConfiguration _config;

        public BooksPictureUrlResolver(IConfiguration config)
        {
            _config = config;
        }

        public string Resolve(Book source, BookToReturnDto destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_config["ApiBaseUrl"]}{source.PictureUrl}";
            }
            return string.Empty;
        }
    }
}
