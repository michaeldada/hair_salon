using System.Collections.Generic;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace HairSalon
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["index.cshtml", AllStylists];
      };
      Post["/addStylist"] = _ => {
        Stylist newStylist = new Stylist(Request.Form["stylist-name"]);
        newStylist.Save();
        List<Stylist> AllStylists = Stylist.GetAll();
        return View["index.cshtml", AllStylists];
      };
  //
    Post["/addClient/{id}"] = parameters => {
      Stylist SelectedStylist = Stylist.Find(parameters.id);
      Client newClient = new Client(Request.Form["client-name"], SelectedStylist.GetId());

      if ((Client.FindName(Request.Form["client-name"])).GetId() == 0)
        {
          newClient.Save();
        }


      return View["stylist.cshtml", SelectedStylist];
    };
  //
  //   Get["/client/{id}"] = parameters => {
  //     Client SelectedRestaurant = Restaurant.Find(parameters.id);
  //     return View["restaurant.cshtml", SelectedRestaurant];
  //   };
  //
    Get["/stylist/{id}"] = parameters => {
      Stylist SelectedStylist = Stylist.Find(parameters.id);
      return View["stylist.cshtml", SelectedStylist];
    };
  //
    Get["/editClient/{id}"] = parameters => {
      Client SelectedClient = Client.Find(parameters.id);
      return View["client_edit.cshtml", SelectedClient];
    };

    Patch["/editClient/{id}"] = parameters => {
      Client SelectedClient = Client.Find(parameters.id);
      SelectedClient.Update(Request.Form["client-name"]);
      Stylist SelectedStylist = Stylist.Find(SelectedClient.GetStylistId());
      return View["stylist.cshtml", SelectedStylist ];
    };

    Delete["/deleteStylist/{id}"] = parameters => {
      Stylist SelectedStylist = Stylist.Find(parameters.id);
      SelectedStylist.Delete();
      List<Stylist> AllStylists = Stylist.GetAll();
      return View["index.cshtml", AllStylists ];
    };
    Delete["/deleteClient/{id}"] = parameters => {
      Client SelectedClient = Client.Find(parameters.id);
      SelectedClient.Delete();
      Stylist SelectedStylist = Stylist.Find(SelectedClient.GetStylistId());
      return View["stylist.cshtml", SelectedStylist];
    };
  //
  //   Post["/search_results"] = _ => {
  //   Stylist foundClient = Restaurant.FindName(Request.Form["search"]);
  //   return View["search_results.cshtml", foundRestaurant];
  // };
  //

    }
  }
}
