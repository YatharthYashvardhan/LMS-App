using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YYvMiniProject2.Data.Models
{
    public class BookModel
    {
        [Key]
        public Guid Book_Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        [Range(0, 5, ErrorMessage = "Rating should be between 0 and 5")]
        [RegularExpression(@"^\d+(\.\d{1})?$")]
        public decimal Rating { get; set; }

        [Required(ErrorMessage = "Author is required")]
        public string? Author { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public string? Genre { get; set; }

        public bool IsBookAvailable { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "LentByUser is required")]
        public string? LentByUser { get; set; }
        public string? CurrentlyBorrowedByUser { get; set; }
    }

}
