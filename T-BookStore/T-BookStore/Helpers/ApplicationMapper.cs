using AutoMapper;
using T_BookStore.Data;
using T_BookStore.Models;

namespace T_BookStore.Helpers
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Book, BookModel>().ReverseMap();
        }
    }
}
