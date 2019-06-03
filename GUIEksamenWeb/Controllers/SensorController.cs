using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Database.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GUIEksamenWeb.Controllers
{
    public class SensorController : Controller
    {
        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private Repository _repository;


        public SensorController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repository = new Repository();
        }

        public IActionResult SensorsByLocationId(int locId)
        {
            return View(_repository.GetSensorsByLocationId(locId));
        }


        public IActionResult AddSensorToLocation(int locId)
        {


            return View(new Sensor { LocationId = locId });
        }


        public IActionResult AddSensorToLocationSubmit(Sensor sensor)
        {

            _repository.InsertSensor(sensor);

            return RedirectToAction("AllLocations", "Location");
        }

       
    }
}