using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace DiplomaDataModel.Diploma
{
    [DuplicateValidator]
    public class Choice
    {
        public Choice()
        {
            SelectionDate = DateTime.Now;
        }

        [Key]
        public int ChoiceId { get; set; }

        public int YearTermId { get; set; }        
        [ForeignKey("YearTermId")]
        public virtual YearTerm YearTerms { get; set; }

        [RegularExpression(@"^A00\d{6}$")]
        [StringLength(9, ErrorMessage = "Format A00######")]
        [DisplayName("Student Number")]       
        public string StudentId { get; set; }

        [Required(ErrorMessage = "First name is required.")]
        [StringLength(40, ErrorMessage = "First name cannot be longer than 40 characters.")]
        [Display(Name = "First Name")]
        public string StudentFirstName { get; set; }

        [Required(ErrorMessage = "Last name is required.")]
        [Display(Name = "Last Name")]
        [StringLength(40, ErrorMessage = "Last name cannot be longer than 40 characters.")]
        public string StudentLastName { get; set; }

        [Required(ErrorMessage = "First Choice is required.")]
        [Display(Name = "First Choice: ")]
        [ForeignKey("FirstOption")]
        public int? FirstChoiceOptionId { get; set; }
        [ForeignKey("FirstChoiceOptionId")]
        public Option FirstOption { get; set; }

        [Required(ErrorMessage = "Second Choice is required.")]
        [Display(Name = "Second Choice: ")]
        [ForeignKey("SecondOption")]
        public int? SecondChoiceOptionId { get; set; }
        [ForeignKey("SecondChoiceOptionId")]
        public Option SecondOption { get; set; }

        [Required(ErrorMessage = "Third Choice is required.")]
        [Display(Name = "Third Choice: ")]
        [ForeignKey("ThirdOption")]
        public int? ThirdChoiceOptionId { get; set; }
        [ForeignKey("ThirdChoiceOptionId")]
        public Option ThirdOption { get; set; }

        [Required(ErrorMessage = "Fourth Choice is required.")]
        [Display(Name = "Fourth Choice: ")]
        [ForeignKey("FourthOption")]
        public int? FourthChoiceOptionId { get; set; }
        [ForeignKey("FourthChoiceOptionId")]
        public Option FourthOption { get; set; }

        //[ScaffoldColumn(false)]        
        [Column(TypeName = "DateTime2")]
        public DateTime SelectionDate { get; set; }
    }
}
