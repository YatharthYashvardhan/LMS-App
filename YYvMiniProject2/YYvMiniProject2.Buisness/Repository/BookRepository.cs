using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YYvMiniProject2.Buisness.IRepository;
using YYvMiniProject2.Data.DbContext;
using YYvMiniProject2.Data.Models;

namespace YYvMiniProject2.Buisness.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BookRepository(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IEnumerable<BookModel>> GetAllBooks()
        {
            return await _context.Books.ToListAsync();
        }

        public async Task<Guid> addBooks(BookModel bookModel)
        {
            var newBook = new BookModel()
            {
                Name = bookModel.Name,
                Author = bookModel.Author,
                Genre = bookModel.Genre,
                Description = bookModel.Description,
                LentByUser = bookModel.LentByUser,
                CurrentlyBorrowedByUser = "Currently Not Borrowed",
                Rating = bookModel.Rating,
                IsBookAvailable = true

            };
            await _context.AddAsync(newBook);
            await _context.SaveChangesAsync();
            return bookModel.Book_Id;
        }
        public async Task<BookModel?> borrowUser(Guid id, UserDataModel dataModel)
        {
            var user = await _context.Books.FindAsync(id);
            user.IsBookAvailable = false;
            user.CurrentlyBorrowedByUser = dataModel.CurrentlyBorrowedByUser;

            _context.Books.Update(user);
            _context.SaveChanges();
            return user;
        }

        public async Task<bool> UpdateToken(UserDataModel dataModel)
        {
            var borrower = await _userManager.FindByEmailAsync(dataModel.CurrentlyBorrowedByUser);
            var lender = await _userManager.FindByEmailAsync(dataModel.LentByUser);

            if (borrower != null && lender != null)
            {
                borrower.bookToken = borrower.bookToken - 1;
                lender.bookToken = lender.bookToken + 1;

                await _userManager.UpdateAsync(borrower);
                await _userManager.UpdateAsync(lender);

                return true;
            }
            return false;
        }

        public async Task<BookModel?> removeUser(Guid id)
        {
            var user = await _context.Books.FindAsync(id);
            user.IsBookAvailable = true;
            user.CurrentlyBorrowedByUser = "Currently Not Borrowed";

            _context.Books.Update(user);
            _context.SaveChanges();
            return user;
        }

        public async Task<bool> changeToken(UserDataModel dataModel)
        {
            var borrower = await _userManager.FindByEmailAsync(dataModel.CurrentlyBorrowedByUser);
            var lender = await _userManager.FindByEmailAsync(dataModel.LentByUser);

            if (borrower != null && lender != null)
            {
                borrower.bookToken = borrower.bookToken + 1;
                lender.bookToken = lender.bookToken - 1;

                await _userManager.UpdateAsync(borrower);
                await _userManager.UpdateAsync(lender);

                return true;
            }
            return false;
        }

        //public async Task<BookModel> getBookById(Guid id)
        //{
        //    return await _context.Books.FindAsync(id);
        //}
        //public async Task<int> deleteBookById(Guid id)
        //{
        //   var carCollection = await _context.Books.FindAsync(id);
        //    if (carCollection != null)
        //    {
        //        _context.Books.Remove(carCollection);
        //        await _context.SaveChangesAsync();
        //    }
        //    return StatusCodes.Status200OK;
        //}

    }
}
