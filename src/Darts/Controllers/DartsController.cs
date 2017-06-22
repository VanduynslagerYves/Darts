using System;
using System.Collections.Generic;
using Darts.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Darts.Models.ViewModels.SpelerViewModels;

namespace Darts.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class DartsController : Controller
    {
        private readonly ISpelerRepository _spelerRepository;
        private readonly IWedstrijdRepository _wedstrijdRepository;
        private readonly ISpelerWedstrijdRepository _spelerWedstrijdRepository;

        public DartsController(ISpelerRepository spelerRepository,
            IWedstrijdRepository wedstrijdRepository,
            ISpelerWedstrijdRepository spelerWedstrijdRepository)
        {
            _spelerRepository = spelerRepository;
            _wedstrijdRepository = wedstrijdRepository;
            _spelerWedstrijdRepository = spelerWedstrijdRepository;
        }

        [AllowAnonymous]
        //[Authorize(Policy ="AdminOnly")]
        //[Authorize(Policy ="Speler")]
        public IActionResult Index()
        {
            IEnumerable<Speler> spelers = _spelerRepository.GetAll();
            return View(spelers);
        }

        [AllowAnonymous]
        public IActionResult Detail(int id)
        {
            //Speler s = _spelerRepository.GetById(id);
            //ViewData["Speler"] = s.Voornaam + " " + s.Naam;
            IEnumerable<SpelerWedstrijd> wedstrijden = _spelerWedstrijdRepository.GetBySpelerId(id);
            return PartialView("_ResultPartial",wedstrijden);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Speler speler = _spelerRepository.GetById(id);
            if (speler == null)
                return NotFound();
            //ViewData["Locations"] = GetLocationsAsSelectList(brewer.Location?.PostalCode);
            return View(new EditViewModel(speler));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditViewModel spelerEditViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Speler speler = _spelerRepository.GetById(spelerEditViewModel.SpelerId);
                    MapSpelerEditViewModelToSpeler(spelerEditViewModel, speler);
                    _spelerRepository.SaveChanges();
                    TempData["message"] = $"{speler.Naam} {speler.Voornaam} werd succesvol gewijzigd!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            //ViewData["Locations"] = GetLocationsAsSelectList(brewerEditViewModel?.PostalCode);
            return View(spelerEditViewModel);
        }

        [HttpGet]
        public IActionResult Create()
        {
            //ViewData["Locations"] = GetLocationsAsSelectList(null);
            return View(nameof(Edit), new EditViewModel(new Speler()));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EditViewModel spelerEditViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    Speler speler = new Speler();
                    MapSpelerEditViewModelToSpeler(spelerEditViewModel, speler);
                    _spelerRepository.Add(speler);
                    _spelerRepository.SaveChanges();
                    TempData["message"] = $"{speler.Naam} {speler.Voornaam} werd succesvol toegevoegd!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                }
            }
            //ViewData["Locations"] = GetLocationsAsSelectList(brewerEditViewModel?.PostalCode);
            return View(nameof(Edit), spelerEditViewModel);
        }

        public IActionResult Delete(int id)
        {
            Speler speler = _spelerRepository.GetById(id);
            if (speler == null)
                return NotFound();
            ViewData[nameof(Speler.Naam)] = speler.Naam;
            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Speler speler = null;
            try
            {
                speler = _spelerRepository.GetById(id);
                _spelerRepository.Remove(speler);
                _spelerRepository.SaveChanges();
                TempData["message"] = $"{speler.Naam} {speler.Voornaam} werd succesvol verwijderd!";
            }
            catch
            {
                TempData["error"] = $"Er ging iets verkeerd, speler {speler?.Naam} {speler?.Voornaam} werd niet verwijderd...";
            }
            return RedirectToAction(nameof(Index));
        }

        //private SelectList GetLocationsAsSelectList(string postalCode)
        //{
        //    return new SelectList(
        //        _locationRepository.GetAll().OrderBy(l => l.Name),
        //        nameof(Location.PostalCode),
        //        nameof(Location.Name),
        //        postalCode);
        //}

        private void MapSpelerEditViewModelToSpeler(EditViewModel spelerEditViewModel, Speler speler)
        {
            speler.Naam = spelerEditViewModel.Naam;
            speler.Email = spelerEditViewModel.Email;
            speler.Voornaam = spelerEditViewModel.Voornaam;
        }
    }
}
