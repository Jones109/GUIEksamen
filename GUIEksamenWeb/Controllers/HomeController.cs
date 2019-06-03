using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using GUIEksamenWeb.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;

namespace GUIEksamenWeb.Controllers
{
    public class HomeController : Controller
    {


        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private Repository _repository;


        public HomeController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repository = new Repository();
            _repository.CreateDB();
        }

        public IActionResult Index()
        {
            return View(_repository.GetAllLocations());
        }


        public IActionResult AddSensorToLocation(int id)
        {


            return View(new Sensor{LocationId = id});
        }

        public IActionResult AddSensorToLocationSubmit(Sensor sensor)
        {

            _repository.InsertSensor(sensor);

            return RedirectToAction("Index");
        }

        public IActionResult Search(string searchText)
        {
            return View("Index", _repository.GetAllLocations().Where(x=>x.Name.Contains(searchText)));
        }



        public IActionResult Sensors(int id)
        {
            return View(_repository.GetSensorsByLocationId(id));
        }


        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
