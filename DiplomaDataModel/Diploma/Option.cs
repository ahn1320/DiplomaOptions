using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.Diploma
{
    public class Option
    {
        [Key, Column(Order = 0)]
        [Required]
        public int OptionId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Status is required.")]
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
    }
}
