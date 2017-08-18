using Microsoft.AspNetCore.Mvc;
using HairSalon.Models;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace HairSalon.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
        return View(Stylist.GetAll());
      }

      [HttpPost("/")]
      public ActionResult IndexPost()
      {
        Console.WriteLine( Request.Form["image"]);
        string newName = Request.Form["name"];
        string newImage = Request.Form["image"];
        Stylist newStylist = new Stylist(newName, newImage);
        newStylist.Save();
        return View("Index", Stylist.GetAll());
      }

      [HttpGet("/StylistForm")]
      public ActionResult StylistForm()
      {
        return View();
      }

      [HttpGet("/RemoveAllStylists")]
      public ActionResult IndexRemoveStylists()
      {
        Stylist.DeleteAll();
        return View("Index",Stylist.GetAll());
      }

      [HttpGet("/StylistDetails/{id}")]
      public ActionResult StylistDetails(int id)
      {
        Stylist selectedStylist = Stylist.Find(id);

        Dictionary<string, object> model= new Dictionary<string, object>{};

        model.Add("stylist", selectedStylist);
        model.Add("clients", selectedStylist.GetAllClients());

        return View(model);
      }

      [HttpPost("/StylistDetails/{id}")]
      public ActionResult StylistDetailsPost(int id)
      {

        Stylist selectedStylist = Stylist.Find(id);
        string clientName = Request.Form["name"];
        string clientPhone = Request.Form["phone"];

        Client newClient = new Client(clientName,clientPhone,id);
        newClient.Save();
        Dictionary<string, object> model= new Dictionary<string, object>{};

        model.Add("stylist", selectedStylist);
        model.Add("clients", selectedStylist.GetAllClients());

        return View("StylistDetails",model);
      }

      [HttpGet("/{id}/ClientForm")]
      public ActionResult ClientForm(int id)
      {
        Stylist selectedStylist = Stylist.Find(id);
        return View(selectedStylist);
      }


    }
}
