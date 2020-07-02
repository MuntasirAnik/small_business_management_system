using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Brainstorm.Model.Model;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Brainstorm.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Code Field Cannot be empty")]//annotations
        [MaxLength(4, ErrorMessage = "Max length is 4")]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Code must be numeric")]
        [Display(Name = "Category Code:")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Cannot be empty")]
        //[RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [Display(Name = "Category Name:")]
        public string Name { get; set; }
        public List<Category> Categories { get; set; }

    }
}