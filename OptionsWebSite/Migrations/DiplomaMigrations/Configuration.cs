namespace OptionsWebSite.Migrations.DiplomaMigrations
{
    using DiplomaDataModel.Diploma;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DiplomaDataModel.Diploma.DiplomaContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\DiplomaMigrations";
        }

        protected override void Seed(DiplomaDataModel.Diploma.DiplomaContext context)
        {
            context.Options.AddOrUpdate(
                  m => m.OptionId,
                  DummyData.GetOption().ToArray()
            );
            context.SaveChanges();

            context.YearTerms.AddOrUpdate(
                  m => m.YearTermId,
                  DummyData.GetYearTerm().ToArray()
            );
            context.SaveChanges();
        }
    }
}
