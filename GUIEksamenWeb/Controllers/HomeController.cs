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


        public IActionResult AddSensorToLocationView(int Id)
        {
            Sensor newSensor = new Sensor
            {
                LocationId = Id
            };
            return View(newSensor);
        }

        public IActionResult AddSensorToLocation(Sensor sensor)
        {

            _repository.InsertSensor(sensor);

            return RedirectToAction("Index");
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
