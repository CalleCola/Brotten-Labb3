using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Brotten.Models;

namespace Brotten.Controllers
{
    public class IntagenController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
 

        [HttpGet]
        public IActionResult IntagenFormular()
        {
            return View("IntagenFormular");
        }
        [HttpPost]
        public IActionResult IntagenFormular(Tbl_Intagen ig)
        {
            
                IntagsMetoder im = new IntagsMetoder();
                int i = 0;
                string error = "";
                i = im.InsertIntagen(ig, out error);
                ViewBag.error = error;
                ViewBag.antal = i;
                if (i == 1)
                {
                    return RedirectToAction("SelectIntagen");
                }
                else
                {
                    return View("InsertIntagen");
                }
            

            
        }


        public IActionResult TaBortIntagen(Tbl_Intagen ig)
        {
           

            IntagsMetoder im = new IntagsMetoder();
            int i = 0;
            string error = "";
            i = im.DeleteIntagen(ig, out error);
            HttpContext.Session.SetString("antal", i.ToString());
            return RedirectToAction("SelectIntagen");
        }

        public IActionResult DeleteButton(int Intagen_nr)
        {
            IntagsMetoder im = new IntagsMetoder();
            int i = 0;
            string error = "";
            Tbl_Intagen ig = new Tbl_Intagen { Intagen_nr = Intagen_nr }; 
            i = im.DeleteBtn(ig, out error);
            HttpContext.Session.SetString("antal", i.ToString());
            return RedirectToAction("SelectIntagen");
        }

        public IActionResult SelectIntagen() 
        {

            List<Tbl_Intagen> Register = new List<Tbl_Intagen>();
            IntagsMetoder im = new IntagsMetoder();
            string error = "";
            Register = im.GetIntagenWithDataSet(out error);
            ViewBag.error = error;
            ViewBag.Antal = HttpContext.Session.GetString("antal");
            return View(Register);
        }

        public IActionResult UppdateraIntagen(Tbl_Intagen ig)
        {
           
                IntagsMetoder im = new IntagsMetoder();
                int i = 0;
                string error = "";
                i = im.Uppdatera(ig, out error);
                ViewBag.error = error;
                ViewBag.antal = i;
                if (i == 1)
                { return RedirectToAction("SelectIntagen"); }
                else
                {
                    return View("UppdateraIntagen");
                }
        }

        public IActionResult FilterResults(string filterBy, string filterValue)
        {
            IntagsMetoder im = new IntagsMetoder();
            List<Tbl_Intagen> searchResults = im.FilterIntagna(filterBy, filterValue);
            return View("FilterResults", searchResults);
        }

      

        public IActionResult FilterByCategory(string brottTyp)
        {
            IntagsMetoder im = new IntagsMetoder();
            List<Tbl_Intagen> searchResults;

            if (!string.IsNullOrEmpty(brottTyp))
            {
                searchResults = im.FilterByCategory(brottTyp);
            }
            else
            {
                searchResults = im.GetIntagenWithDataSet(out string errorMsg);
            }

            List<string> distinctBrottTyper = im.GetDistinctBrottTyper();
            ViewBag.BrottTyper = distinctBrottTyper;

            return View("FilterByCategory", searchResults);
        }



        public IActionResult FilterResultas(string brottTyp)
        {
            IntagsMetoder im = new IntagsMetoder();
            List<Tbl_Intagen> searchResults = im.FilterByCategory(brottTyp);
            return View(searchResults);
        }

        public IActionResult SearchById(int id)
        {
            IntagsMetoder im = new IntagsMetoder();
            List<Tbl_Intagen> searchResults = im.SearchById(id);
            return View(searchResults);
        }













    }
}
