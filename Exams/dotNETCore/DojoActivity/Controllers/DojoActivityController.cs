using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using beltexam.Models;

namespace beltexam.Controllers
{
    public class DojoActivityController : Controller
    {
        private beltexamContext _dbcontext;

        //Constructor
        public DojoActivityController(beltexamContext context)
        {
            _dbcontext = context;
        }

        [HttpGet]
        [Route("Home")]
        public IActionResult Dashboard()
        {
            var email = HttpContext.Session.GetString("email");
            if (email != null) 
            {
                var activities = _dbcontext.Activities
                        .Include(act => act.User)
                        .Include(act => act.Participants)
                            .ThenInclude(act => act.UserParticipant)
                        .ToList();
                
                ViewBag.Activities = activities;
                ViewBag.Firstname = HttpContext.Session.GetString("firstname");
                ViewBag.Email = email;

                return View("Dashboard");
            }
            return RedirectToAction("Login", "Home");
        }

        [HttpGet]
        [HttpPost]
        [Route("create")]
        public IActionResult Create(DojoActivityCreateViewModel model)
        {
            var email = HttpContext.Session.GetString("email");
            var user = _dbcontext.Users.SingleOrDefault(dbUser => dbUser.Email == email);

            if (ModelState.IsValid)
            {
                var activity = new DojoActivity
                {
                    Title = model.Title,
                    Date = model.Date,
                    Time = model.Time,
                    Duration = model.Duration,
                    DurationText = model.DurationText,
                    Description = model.Description,
                    User = user
                };

                _dbcontext.Activities.Add(activity);
                _dbcontext.SaveChanges();

                return RedirectToAction("Dashboard");
            }

            return View(model);
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var email = HttpContext.Session.GetString("email");
            var user = _dbcontext.Users.SingleOrDefault(dbUser => dbUser.Email == email);
            var activity = _dbcontext.Activities.Include(act => act.User)
                            .SingleOrDefault(act => act.DojoActivityId == id);
            if(activity != null)
            {
                if (activity.User.UserId == user.UserId)
                {
                    _dbcontext.Activities.Remove(activity);
                    _dbcontext.SaveChanges();
                }
            }
            return RedirectToAction("Dashboard");
        }
        [HttpGet]
        [Route("view/{id}")]
        public IActionResult ViewActivity(int id)
        {
            var activity = _dbcontext.Activities
            .Include(act => act.User)
            .Include(act => act.Participants)
                .ThenInclude(act => act.UserParticipant)
            .SingleOrDefault(act => act.DojoActivityId == id);
            if(activity != null)
            {
                return View(activity);
            }
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("join/{id}")]
        public IActionResult Join(int id)
        {
            var activity = _dbcontext.Activities.Include(act => act.User)
                            .SingleOrDefault(act => act.DojoActivityId == id);
            if(activity != null)
            {
                var email = HttpContext.Session.GetString("email");
                var user = _dbcontext.Users.SingleOrDefault(dbUser => dbUser.Email == email);
                var participant = new Participant
                {
                    CreatedAt = DateTime.UtcNow,
                    UserParticipant = user,
                    Event = activity
                };
                activity.Participants.Add(participant);
                _dbcontext.Activities.Update(activity);
                _dbcontext.SaveChanges();

                return RedirectToAction("ViewActivity", new { id = activity.DojoActivityId });
            }
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        [Route("leave/{id}")]
        public IActionResult Leave(int id)
        {
           var activity = _dbcontext.Activities.Include(act => act.User)
           .Include(act => act.Participants)
            .ThenInclude(act => act.UserParticipant)
                            .SingleOrDefault(act => act.DojoActivityId == id);
            if(activity != null)
            {
                var email = HttpContext.Session.GetString("email");
                var participant = activity.Participants.SingleOrDefault(part => part.UserParticipant.Email == email);
                if (participant != null) 
                {
                    activity.Participants.Remove(participant);
                    _dbcontext.Activities.Update(activity);
                    _dbcontext.SaveChanges();
                }
            }
            return RedirectToAction("Dashboard");
        }
    }
}