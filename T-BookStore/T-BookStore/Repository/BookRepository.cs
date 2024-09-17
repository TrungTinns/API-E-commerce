using AutoMapper;
using Microsoft.EntityFrameworkCore;
using T_BookStore.Data;
using T_BookStore.Models;

namespace T_BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IMapper _mapper;

        public BookRepository(BookStoreContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddBookAsync(BookModel model)
        {
            var newBook = _mapper.Map<Book>(model);
            _context.Books!.Add(newBook);
            await _context.SaveChangesAsync();
            return newBook.Id;
        }

        public async Task DeleteBookAsync(int id)
        {
            
        }

        public Task DeleteBookAsync(int id, BookModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<BookModel>> GetAllBookAsync()
        {
            var books = await _context.Books!.ToListAsync();
            return _mapper.Map<List<BookModel>>(books);
        }

        public async Task<BookModel> GetBookAsync(int id)
        {
            var book = await _context.Books.FindAsync();
            return _mapper.Map<BookModel>(book);
        }
    }
}
