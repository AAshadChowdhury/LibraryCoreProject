using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryCoreProject.Models
{
    public class DAL
    {
    }
    public class Category
    {
        [Key]
        public string catid { get; set; }
        public string Categoryname { get; set; }
        public string Department { get; set; }
        public IList<Book> Book { get; set; }
    }
    public partial class Book
    {
        [Key]
        public string Bookcode { get; set; }
        public string Bookname { get; set; }
        [ForeignKey("Category")]
        public string catid { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public Nullable<decimal> cost { get; set; }
        [Column(TypeName = "decimal(18,4)")]
        public Nullable<decimal> rate { get; set; }
        public DateTime date { get; set; }
        public string picture { get; set; }
        public Category Category { get; set; }
    }
    public class Category_book
    { 

        public string CatId { get; set; }
        [Required(ErrorMessage = "Please enter Dept Name")]
        [Display(Name = "DeptName")]
        [MaxLength(50)]
        public string CategoryName { get; set; }
        public string department { get; set; }
        public string BookCode { get; set; }//primary key and foreign Foreign Key
        public string BookName { get; set; }
        public double Cost { get; set; }
        public double Rate { get; set; }
        public DateTime date { get; set; }
        public string picture { get; set; }
        public string sid { get; set; }
    }

}
