using YYvMiniProject2.Data.Models;

namespace YYvMiniProject2.Buisness.IRepository
{
    public interface IBookRepository
    {
        Task<Guid> addBooks(BookModel bookModel);
        Task<BookModel?> borrowUser(Guid id, UserDataModel dataModel);
        Task<bool> changeToken(UserDataModel dataModel);
        Task<IEnumerable<BookModel>> GetAllBooks();
        Task<BookModel?> removeUser(Guid id);
        Task<bool> UpdateToken(UserDataModel dataModel);
    }
}