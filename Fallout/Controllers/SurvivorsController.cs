using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Fallout.Models;

namespace Fallout.Controllers
{
  public class SurvivorsController : Controller
  {
    private readonly FalloutContext _db;

    public SurvivorsController(FalloutContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      return View(_db.Survivors.OrderBy(survivor => survivor.Arrived).ToList());
    }

    public ActionResult Create()
    {
      ViewBag.ShelterId = new SelectList(_db.Shelters, "ShelterId", "Name");
      return View();
    }

    public ActionResult Details(int id)
    {
      var thisSurvivor = _db.Survivors
        .Include(survivor => survivor.JoinEntities)
        .ThenInclude(join => join.Shelter)
        .FirstOrDefault(survivor => survivor.SurvivorId == id);
      return View(thisSurvivor);
    }

    [HttpPost]
    public ActionResult Create(Survivor survivor, int ShelterId)
    {
      _db.Survivors.Add(survivor);
      _db.SaveChanges();
      if (ShelterId != 0)
      {
        _db.ShelterSurvivor.Add(new ShelterSurvivor() { ShelterId = ShelterId, SurvivorId = survivor.SurvivorId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Edit(int id)
    {
      var thisSurvivor = _db.Survivors.FirstOrDefault(survivor => survivor.SurvivorId == id);
      ViewBag.ShelterId = new SelectList(_db.Shelters, "ShelterId", "Name");
      return View(thisSurvivor);
    }

    [HttpPost]
    public ActionResult Edit(Survivor survivor, int ShelterId)
    {
      if (ShelterId != 0)
      {
        _db.ShelterSurvivor.Add(new ShelterSurvivor() { ShelterId = ShelterId, SurvivorId = survivor.SurvivorId });
      }
      _db.Entry(survivor).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
        var thisSurvivor = _db.Survivors.FirstOrDefault(survivor => survivor.SurvivorId == id);
        return View(thisSurvivor);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
        var thisSurvivor = _db.Survivors.FirstOrDefault(survivor => survivor.SurvivorId == id);
        _db.Survivors.Remove(thisSurvivor);
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public ActionResult AddShelter(int id)
    {
      var thisSurvivor = _db.Survivors.FirstOrDefault(survivor => survivor.SurvivorId == id);
      ViewBag.ShelterId = new SelectList(_db.Shelters, "ShelterId", "Name");
      return View(thisSurvivor);
    }

    [HttpPost]
    public ActionResult AddShelter(Survivor survivor, int ShelterId)
    {
      if (ShelterId != 0)
      {
        _db.ShelterSurvivor.Add(new ShelterSurvivor() { ShelterId = ShelterId, SurvivorId = survivor.SurvivorId });
      }
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    [HttpPost]
    public ActionResult DeleteShelter(int joinId)
    {
      var joinEntry = _db.ShelterSurvivor.FirstOrDefault(entry => entry.ShelterSurvivorId == joinId);
      _db.ShelterSurvivor.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}