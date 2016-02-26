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
  //   Get["/restaurant/{id}/edit"] = parameters => {
  //     Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
  //     return View["restaurant_edit.cshtml", SelectedRestaurant];
  //   };
  //
  //   Patch["/restaurant/{id}/edit"] = parameters => {
  //     Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
  //     SelectedRestaurant.Update(Request.Form["restaurant-name"]);
  //     List<Cuisine> AllCuisines = Cuisine.GetAll();
  //     return View["index.cshtml", AllCuisines ];
  //   };
  //
  //   Delete["/restaurant/{id}/delete"] = parameters => {
  //     Restaurant SelectedRestaurant = Restaurant.Find(parameters.id);
  //     SelectedRestaurant.Delete();
  //     List<Cuisine> AllCuisines = Cuisine.GetAll();
  //     return View["index.cshtml", AllCuisines ];
  //   };
  //
  //   Post["/search_results"] = _ => {
  //   Restaurant foundRestaurant = Restaurant.FindName(Request.Form["search"]);
  //   return View["search_results.cshtml", foundRestaurant];
  // };
  //

    }
  }
}
