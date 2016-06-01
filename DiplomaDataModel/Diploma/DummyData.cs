using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OptionsWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiplomaDataModel.Diploma
{
    public class DummyData
    {
        public static List<YearTerm> GetYearTerm()
        {
            List<YearTerm> yearTerm = new List<YearTerm>()
            {
                new YearTerm {YearTermId = 1, Year=2015, Term="20", IsDefault=false},
                new YearTerm {YearTermId = 2, Year=2015, Term="30", IsDefault=false},
                new YearTerm {YearTermId = 3, Year=2016, Term="10", IsDefault=false},
                new YearTerm {YearTermId = 4, Year=2016, Term="30", IsDefault=true},
            };
            return yearTerm;
        }

        public static List<Option> GetOption()
        {
            List<Option> option = new List<Option>()
            {
                new Option {OptionId = 1, Title="Data Communications", IsActive=true},
                new Option {OptionId = 2, Title="Client Server", IsActive=true},
                new Option {OptionId = 3, Title="Digital Processing", IsActive=true},
                new Option {OptionId = 4, Title="Information Systems", IsActive=true},
                new Option {OptionId = 5, Title="Database", IsActive=false},
                new Option {OptionId = 6, Title="Web & Mobile", IsActive=true},
                new Option {OptionId = 7, Title="Tech Pro", IsActive=false},
            };
            return option;
        }
    }    
}
