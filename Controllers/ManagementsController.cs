using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeeklyFirstTask.DataAccess;
using WeeklyFirstTask.Models;

namespace WeeklyFirstTask.Controllers
{
    public class ManagementsController : Controller
    {
        private readonly DataContext _conn;
        private readonly INotyfService _notyf;

        public ManagementsController(DataContext conn, INotyfService notyf)
        {

            _conn = conn;
            _notyf = notyf;


        }
        public IActionResult DoctorDetails()
        {
            var data =  _conn.GetDoctors.ToList();
            return View(data);

        }
        [HttpPost]

        public IActionResult DoctorDetails(Doctor doctors)
        {
           
            var getdoctordetails = _conn.GetDoctors.SingleOrDefault(x => x.DId == doctors.DId);

            if (ModelState.IsValid)
            {
                if (doctors.GetDate  == getdoctordetails.GetDate && doctors.GetTime == getdoctordetails.GetTime)
                {
                    var getdata = (from D in _conn.GetDoctors
                                   where D.DId == getdoctordetails.DId
                                   select new Doctor
                                   {
                                       DId = D.DId,
                                       DName = D.DName,
                                       Contact = D.Contact,
                                       Specialization = D.Specialization
                                   }).SingleOrDefault();
                    _notyf.Custom("Doctor Already Appointed", 5, "#ff00ff", "fa fa-home");
                   return View("AppointDetails",getdata);
                }
                else
                {
                    getdoctordetails.GetDate = doctors.GetDate;
                    getdoctordetails.GetTime = doctors.GetTime;
                    _conn.GetDoctors.Update(getdoctordetails);
                    _conn.SaveChanges();
                    _notyf.Custom("You have successfully appoint a doctor", 5, "#0000FF", "fa fa-home");
                }
            }
            else
            {
                return View("AppointDetails");
            }
            return RedirectToAction("DoctorDetails", "Managements");
        }



        public IActionResult AppointDetails(int id)
        {
            var getdata = (from D in _conn.GetDoctors where D.DId == id select new Doctor
                           {

                               DId = D.DId,
                               DName = D.DName,
                               Contact = D.Contact,
                               Specialization = D.Specialization
                           }).SingleOrDefault();

          
            return View(getdata);
             
        }
    }
}

