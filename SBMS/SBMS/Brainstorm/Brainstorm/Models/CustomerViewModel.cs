using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Brainstorm.Model.Model;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Brainstorm.Models
{
    public class CustomerViewModel
    {

        public int Id { get; set; }


        [Required(ErrorMessage = "Code Field Cannot be empty")]//annotations
        [MaxLength(4, ErrorMessage = "Max length is 4")]
        //[RegularExpression("^[0-9]*$", ErrorMessage = "Code must be numeric")]
        [Display(Name = "Customer Code:")]
        public string Code { get; set; }
        [Required(ErrorMessage = "Name Cannot be empty")]
       [RegularExpression(@"^[a-zA-Z]+[ a-zA-Z-_]*$", ErrorMessage = "Use Characters only")]
        [Display(Name = "Customer Name:")]
        public string Name { get; set; }

        public string Address { get; set; }
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [Display(Name = "Email:")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Contact No. Cannot be empty")]//annotations
        [MaxLength(11, ErrorMessage = "Max length is 11")]
        [MinLength(11, ErrorMessage = "Min length is 11")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Contact must contain numbers only")]
        [Display(Name = "Contact No.:")]
        public string Contact { get; set; }
        [Required(ErrorMessage = "Input Loyalty Point")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Invalid Reorder Level")]
        [Display(Name = "Loyalty point:")]
        public int LoyaltyPoint { get; set; }


        public List<Customer> Customers { get; set; }
    }
}