using T_BookStore.Data;
using T_BookStore.Models;

namespace T_BookStore.Repository
{
    public interface IBookRepository
    {
        public Task<List<BookModel>> GetAllBookAsync();
        public Task<BookModel> GetBookAsync(int id);
        public Task<int> AddBookAsync(BookModel model);
        public Task DeleteBookAsync(int id);
        public Task DeleteBookAsync(int id, BookModel model);

    }
}
    