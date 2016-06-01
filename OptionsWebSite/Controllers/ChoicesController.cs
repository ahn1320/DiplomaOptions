using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomaDataModel.Diploma;
using System.Collections;

namespace OptionsWebSite.Controllers
{
    public class ChoicesController : Controller
    {
        private DiplomaContext db = new DiplomaContext();


        List<string> detail = new List<string>()
        {
            "Detail",
            "Chart"
        };

        // GET: Choices      
        public ActionResult Index()
        {
            var firstChoice = db.YearTerms
                .ToDictionary(c => c.YearTermId, c => c.Year + "/" + c.Term);

            ViewBag.FirstList = new SelectList(firstChoice, "Key", "Value");

            var secondChoice = new SelectList(detail);
            ViewBag.SecondList = secondChoice;

            var choice = db.Choices.Include(c => c.FirstOption).Include(c => c.FourthOption).Include(c => c.SecondOption).Include(c => c.ThirdOption).Include(c => c.YearTerms);
            return View(choice.ToList());
        }

        public ActionResult Filtering(string YearTermID, string SecondList)
        {
            int id = Int32.Parse(YearTermID);
            var choices = db.Choices.Include(c => c.FirstOption).Include(c => c.FourthOption).Include(c => c.SecondOption).Include(c => c.ThirdOption).Include(c => c.YearTerms)
                .Where(y => y.YearTermId == id).ToList();


            HashSet<Option> options = new HashSet<Option>();

            foreach (var choice in choices)
            {
                options.Add(choice.FirstOption);
                options.Add(choice.SecondOption);
                options.Add(choice.ThirdOption);
                options.Add(choice.FourthOption);
            }

            List<int> firstChoice = new List<int>();
            List<int> secondChoice = new List<int>();
            List<int> thirdChoice = new List<int>();
            List<int> fourthChoice = new List<int>();
            List<string> optionTitles = new List<string>();
            foreach (var option in options)
            {
                optionTitles.Add(option.Title);
                firstChoice.Add(db.Choices.Where(c => c.FirstOption.Title == option.Title
                && c.YearTermId == id).Count());
                secondChoice.Add(db.Choices.Where(c => c.SecondOption.Title == option.Title
                && c.YearTermId == id).Count());
                thirdChoice.Add(db.Choices.Where(c => c.ThirdOption.Title == option.Title
                && c.YearTermId == id).Count());
                fourthChoice.Add(db.Choices.Where(c => c.FourthOption.Title == option.Title
                && c.YearTermId == id).Count());
            }

            ViewBag.FirstCount = firstChoice.ToArray();
            ViewBag.SecondCount = secondChoice.ToArray();
            ViewBag.ThirdCount = thirdChoice.ToArray();
            ViewBag.FourthCount = fourthChoice.ToArray();
            ViewBag.Opt = optionTitles.ToArray();

            if (SecondList == "Chart")
                return PartialView("_ChartPartial", choices.ToList());

            return PartialView("_ReportPartial", choices.ToList());
        }


        // GET: Choices/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        // GET: Choices/Create
        [Authorize(Roles = "Admin, Student")]
        public ActionResult Create()
        {
            var getIsDefault = db.YearTerms.FirstOrDefault(s => s.IsDefault == true);

            string term = null;
            string year = null;
            string success = "Form successfully submited";

            switch (getIsDefault.Term)
            {
                case "10":
                    term = "Winter";
                    break;
                case "20":
                    term = "Summer";
                    break;
                case "30":
                    term = "Fall";
                    break;
            }

            switch (getIsDefault.Year)
            {
                case 2015:
                    year = "2015";
                    break;
                case 2016:
                    year = "2016";
                    break;
            }

            ViewBag.FirstChoiceOptionId = new SelectList(db.Options.Where(c => c.IsActive == true).ToList(), "OptionId", "Title");
            ViewBag.FourthChoiceOptionId = new SelectList(db.Options.Where(c => c.IsActive == true).ToList(), "OptionId", "Title");
            ViewBag.SecondChoiceOptionId = new SelectList(db.Options.Where(c => c.IsActive == true).ToList(), "OptionId", "Title");
            ViewBag.ThirdChoiceOptionId = new SelectList(db.Options.Where(c => c.IsActive == true).ToList(), "OptionId", "Title");
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "Term");
            ViewBag.term = term;
            ViewBag.year = year;
            ViewBag.success = success;

            return View();
        }

        // POST: Choices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin, Student")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ChoiceId,YearTermId,StudentId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId")] Choice choice)
        {
            if (ModelState.IsValid)
            {
                //get the yeartermid from the YearTerms db where Isdefault==true,  
                var getIsDefault = db.YearTerms.FirstOrDefault(s => s.IsDefault == true).YearTermId;
                //assign it to Choice db yeartermid; so when the user submits the form, proper Term is saved. 
                choice.YearTermId = getIsDefault;

                HashSet<object> dbRecord = new HashSet<object>();
                HashSet<object> currentRecord = new HashSet<object>();
                string studentID = choice.StudentId;
                string yeartermID = choice.YearTermId.ToString();

                //query in choices db for studentid and yeartermid
                var query = from category in db.Choices
                            select new
                            {
                                studentID = category.StudentId,
                                yeartermID = category.YearTermId.ToString()
                            };

                //for each query add into dbrecord. for each index two objects are stored
                foreach (var categoryInfo in query)
                {
                    dbRecord.Add(categoryInfo);
                }

                //add two objects in currentrecord index
                currentRecord.Add(new { studentID, yeartermID });

                //if the objects stored in dbrecord overlaps objects stored in currentrecord,
                if (!dbRecord.Overlaps(currentRecord))
                {
                    //saves
                    db.Choices.Add(choice);
                    db.SaveChanges();
                    return View("Success");                    
                }

                else
                    ModelState.AddModelError("", "You can submit only once per term");
            }
            
            ViewBag.FirstChoiceOptionId = new SelectList(db.Options.Where(c => c.IsActive == true).ToList(), "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(db.Options.Where(c => c.IsActive == true).ToList(), "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(db.Options.Where(c => c.IsActive == true).ToList(), "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(db.Options.Where(c => c.IsActive == true).ToList(), "OptionId", "Title", choice.ThirdChoiceOptionId);
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "Term", choice.YearTermId);

            return View(choice);
        }


        // GET: Choices/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            ViewBag.FirstChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.ThirdChoiceOptionId);
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "Term", choice.YearTermId);
            return View(choice);
        }

        // POST: Choices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ChoiceId,YearTermId,StudentId,StudentFirstName,StudentLastName,FirstChoiceOptionId,SecondChoiceOptionId,ThirdChoiceOptionId,FourthChoiceOptionId,SelectionDate")] Choice choice)
        {
            if (ModelState.IsValid)
            {
                db.Entry(choice).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FirstChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FirstChoiceOptionId);
            ViewBag.FourthChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.FourthChoiceOptionId);
            ViewBag.SecondChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.SecondChoiceOptionId);
            ViewBag.ThirdChoiceOptionId = new SelectList(db.Options, "OptionId", "Title", choice.ThirdChoiceOptionId);
            ViewBag.YearTermId = new SelectList(db.YearTerms, "YearTermId", "Term", choice.YearTermId);
            return View(choice);
        }

        // GET: Choices/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Choice choice = db.Choices.Find(id);
            if (choice == null)
            {
                return HttpNotFound();
            }
            return View(choice);
        }

        // POST: Choices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Choice choice = db.Choices.Find(id);
            db.Choices.Remove(choice);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
