using System.Data.Entity;

namespace DiplomaDataModel.Diploma
{
    public class DiplomaContext : DbContext
    {
        public DiplomaContext() : base("DefaultConnection")
        { }

        public DbSet<YearTerm> YearTerms { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Choice> Choices { get; set; }

    }
}
