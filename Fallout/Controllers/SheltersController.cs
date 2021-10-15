using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Fallout.Models;
using System.Collections.Generic;
using System.Linq;

namespace Fallout.Controllers
{
  public class SheltersController : Controller
  {
    private readonly FalloutContext _db;

    public SheltersController(FalloutContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Shelter> model = _db.Shelters.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Shelter category)
    {
      _db.Shelters.Add(category);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisShelter = _db.Shelters
        .Include(category => category.JoinEntities)
        .ThenInclude(join => join.Survivor)
        .FirstOrDefault(category => category.ShelterId == id);
      return View(thisShelter);
    }

    public ActionResult Edit(int id)
    {
      var thisShelter = _db.Shelters.FirstOrDefault(category => category.ShelterId == id);
      return View(thisShelter);
    }

    [HttpPost]
    public ActionResult Edit(Shelter category)
    {
      _db.Entry(category).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisShelter = _db.Shelters.FirstOrDefault(category => category.ShelterId == id);
      return View(thisShelter);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisShelter = _db.Shelters.FirstOrDefault(category => category.ShelterId == id);
      _db.Shelters.Remove(thisShelter);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}