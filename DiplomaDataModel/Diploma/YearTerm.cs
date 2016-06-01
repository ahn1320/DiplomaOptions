 using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.Diploma
{
    public class YearTerm
    {
        [Key, Column(Order = 0)]
        [Required]
        public int YearTermId { get; set; }

        [Required(ErrorMessage = "Year required.")]
        [Display(Name = "Year")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Term required.")]
        [MaxLength(50)]
        public string Term { get; set; }

        [Required(ErrorMessage = "Status required.")]
        [Display(Name = "Default")]
        public bool IsDefault { get; set; }
    }
}
