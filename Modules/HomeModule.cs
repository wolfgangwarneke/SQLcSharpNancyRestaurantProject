using System.Collections.Generic;
using System;
using Nancy;
using Nancy.ViewEngines.Razor;

namespace Restaurants
{
  public class HomeModule : NancyModule
  {
    public HomeModule()
    {
      Get["/"] = _ => {
        List<Cuisine> allCuisine = Cuisine.GetAll();
        return View["index.cshtml", allCuisine];
      };
      Post["/cuisine/new"] = _ => {
        Cuisine newCuisine = new Cuisine(Request.Form["cuisineName"]);
        newCuisine.Save();
        List<Cuisine> allCuisine = Cuisine.GetAll();
        return View["index.cshtml", allCuisine];
      };
      Get["/cuisine/delete"] = _ => {
        List<Cuisine> allCuisine = Cuisine.GetAll();
        return View["remove_cuisine.cshtml", allCuisine];
      };
      Delete["/cuisine/delete/"] = _ => {
        int searchId = Request.Form["cuisineName"];
        Cuisine SelectedCuisine = Cuisine.Find(searchId);
        SelectedCuisine.Delete();
        List<Cuisine> allCuisine = Cuisine.GetAll();
        return View["index.cshtml", allCuisine];
      };
      Get["/cuisine/delete/all"] = _ => {
        return View["delete_all_confirmation.cshtml"];
      };
      Post["/cuisine/delete/all/confirmation"] = _ => {
        if (Request.Form["confirm"]) Cuisine.DeleteAll();
        List<Cuisine> allCuisine = Cuisine.GetAll();
        return View["index.cshtml", allCuisine];
      };
      Get["/cuisine/edit"] = _ => {
        List<Cuisine> allCuisine = Cuisine.GetAll();
        return View["edit_cuisine.cshtml", allCuisine];
      };

      Patch["/cuisine/edit"] = _ => {
        int searchId = Request.Form["cuisineName"];
        Cuisine SelectedCuisine = Cuisine.Find(searchId);
        SelectedCuisine.Update(Request.Form["newName"]);
        List<Cuisine> allCuisine = Cuisine.GetAll();
        return View["index.cshtml", allCuisine];
      };

      // Get["/categories/{id}"] = parameters => {
      //   Dictionary<string, object> model = new Dictionary<string, object>();
      //   var SelectedCategory = Category.Find(parameters.id);
      //   var CategoryTasks = SelectedCategory.GetTasks();
      //   model.Add("category", SelectedCategory);
      //   model.Add("tasks", CategoryTasks);
      //   return View["category.cshtml", model];
      // };
    }
  }
}
