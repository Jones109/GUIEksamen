using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GUIEksamenWeb.Controllers
{
    public class LocationController : Controller
    {



        private UserManager<AppUser> _userManager;
        private SignInManager<AppUser> _signInManager;
        private Repository _repository;


        public LocationController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _repository = new Repository();
        }

        public IActionResult AllLocations()
        {

            return View(_repository.GetAllLocations());
        }

        public IActionResult Search(string searchText)
        {
            if (searchText != null)
            {
                return View("AllLocations", _repository.GetAllLocations().Where(x => x.Name.Contains(searchText)));
            }

            return View("AllLocations", _repository.GetAllLocations());
        }
    }
}