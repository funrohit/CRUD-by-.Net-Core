using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using mycore.Models;
using mycore.mydata;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace mycore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            myrohitContext database = new myrohitContext();

            List<EmpModel> ee = new List<EmpModel>();

            var res = database.Mytable.ToList();


            foreach (var item in res)
            {
                ee.Add(new EmpModel
                {
                Id=item.Id,
                Email=item.Email,
                Name=item.Name

                });             
            }
            return View(ee);
        }
        [HttpGet]
        public IActionResult Add()
        {
       

            return View();
        }

        public IActionResult Add(EmpModel obj)
        {
            myrohitContext database = new myrohitContext();

            Mytable tt = new Mytable();

            tt.Id = obj.Id;
            tt.Email = obj.Email;
            tt.Name = obj.Name;
            if (obj.Id == 0)
            {
                database.Mytable.Add(tt);
                database.SaveChanges();

            }
            else
            {
                database.Entry(tt).State = System.Data.EntityState.Modified;
                database.SaveChanges();
                    
            }


            return RedirectToAction("Add", "home");
        }

        public IActionResult del(int id)
        {
            myrohitContext database = new myrohitContext();
            Mytable tt = new Mytable();

            var dell = database.Mytable.Where(i => i.Id == id).FirstOrDefault();
            database.Mytable.Remove(dell);
            database.SaveChanges();
            return RedirectToAction("index", "home");
        }
        public IActionResult editing(int id)
        {
            myrohitContext database = new myrohitContext();
            EmpModel tt = new EmpModel();
            var edit = database.Mytable.Where(i => i.Id == id).FirstOrDefault();

            tt.Id=edit.Id;
            tt.Email=edit.Email;
            tt.Name = edit.Name;

            database.SaveChanges();
            return View();
        }


    }
}
