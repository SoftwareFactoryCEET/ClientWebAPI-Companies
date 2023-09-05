using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplicationCompanies.Models;
using Newtonsoft.Json;

namespace WebApplicationCompanies.Controllers
{
    public class CompaniesController : Controller
    {

        static HttpClient client = new HttpClient();


        // GET: CompaniesController
        public ActionResult Index()
        {

            IEnumerable<CompanyViewModel> companies = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:5001/api/companies");
                //HTTP GET
                var responseTask = client.GetAsync("companies");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var contentTask = result.Content.ReadAsStringAsync();
                    contentTask.Wait();
                    var json = contentTask.Result;
                    companies = JsonConvert.DeserializeObject<List<CompanyViewModel>>(json); ;
                }
                else //web api sent error response 
                {
                    //log response status here..

                    companies = Enumerable.Empty<CompanyViewModel>();

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
            }
            return View(companies);            
        }

        // GET: CompaniesController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompaniesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompaniesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompaniesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompaniesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CompaniesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompaniesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
