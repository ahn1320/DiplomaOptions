using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel
{
    public class DuplicateValidator : ValidationAttribute
    {                
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {   
            //value of [1,2,3,4]
            var choiceArray = new[] { value.GetType().GetProperty("FirstChoiceOptionId").GetValue(value),
                                      value.GetType().GetProperty("SecondChoiceOptionId").GetValue(value),
                                      value.GetType().GetProperty("ThirdChoiceOptionId").GetValue(value),
                                      value.GetType().GetProperty("FourthChoiceOptionId").GetValue(value)
                                    };
            
            //find the # of distinct values in the array and compare them with the total # of values in its array
            if(choiceArray.Distinct().Count() != choiceArray.Count())
            {
                return new ValidationResult("No Duplication for Options");
            }                                      
            return ValidationResult.Success;
        }

    }
}