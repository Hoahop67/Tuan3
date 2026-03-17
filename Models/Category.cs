using System.ComponentModel.DataAnnotations;

namespace Tuan3.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        [Required] // Đảm bảo rằng CategoryName không được để trống
        public string? CategoryName { get; set; }
        // Navigation property để liên kết với Product
        public List<Product>? Products { get; set; }

    }
}
