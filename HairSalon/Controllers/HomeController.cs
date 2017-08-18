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

      [HttpGet("/StylistDetails/{stylistId}/{clientId}")]
      public ActionResult ClientDetails(int stylistId, int clientId)
      {
        Stylist selectedStylist = Stylist.Find(stylistId);
        Client selectedClient = Client.Find(clientId);

        Dictionary<string, object> model= new Dictionary<string, object>{};

        model.Add("stylist", selectedStylist);
        model.Add("client", selectedClient);
        model.Add("allStylists", Stylist.GetAll());

        return View("ClientDetails",model);
      }

      [HttpPost("/StylistDetails/{stylistId}/{clientId}/Edited")]
      public ActionResult StylistDetailsPostEditedClient(int stylistId, int clientId)
      {
        Stylist thisStylist = Stylist.Find(stylistId);

        string clientNewName = Request.Form["name"];
        string clientNewPhone = Request.Form["phone"];
        string newStylistName = Request.Form["stylist"];

        Stylist selectedStylist = Stylist.Search(newStylistName);

        Client updatedClient = Client.Find(clientId);

        updatedClient.UpdateStylist(selectedStylist.GetId());
        updatedClient.UpdateName(clientNewName);
        updatedClient.UpdatePhone(clientNewPhone);

        Dictionary<string, object> model= new Dictionary<string, object>{};

        model.Add("stylist", thisStylist);
        model.Add("clients", thisStylist.GetAllClients());

        return View("StylistDetails",model);
      }


    }
}
