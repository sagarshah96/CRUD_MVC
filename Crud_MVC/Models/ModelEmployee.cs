using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Crud_MVC.Models
{
    public class ModelEmployee
    {
        public int EmployeeId { get; set; }
        [Required(ErrorMessage ="This Field is Required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This Field is Required")]
        public string Position { get; set; }
        public string Office { get; set; }
        public Nullable<int> Age { get; set; }
        public Nullable<int> Salary { get; set; }
    }
}