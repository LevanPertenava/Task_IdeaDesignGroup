using DatabaseRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_IdeaDesignGroup.Models;
using Utility;

namespace Task_IdeaDesignGroup.Controllers
{
    public class PersonController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public PersonController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> PersonsList(string search, int page = 1, byte pageSize = 2)
        {
            if (page < 1)
                page = 1;

            List<PersonModel> model = new();
            IEnumerable<Persons> persons;

            if (!string.IsNullOrEmpty(search))
            {
                persons = await _unitOfWork.Persons.SearchPerson(search);
            }
            else
            {
                persons = _unitOfWork.Persons.Get();
            }

            foreach (var person in persons)
            {
                var personMapped = ModelMapHandler.BuildModelMapping(person, new PersonModel());
                personMapped.Gender = (Models.Gender?)person.GenderId;
                model.Add(personMapped);
            }


            int personsCount = model.Count;
            int totalPages = 0;

            if (personsCount != 0)
                totalPages = personsCount % pageSize != 0 ? personsCount / pageSize + 1 : personsCount / pageSize;

            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchText = search;

            model = MyTools.Paging(model, page, pageSize).ToList();
            return View(model);
        }

        [HttpGet("Details")]
        public IActionResult PersonDetails(Guid id)
        {
            var person = GetPersonModel(id);
            if (person is null)
            {
                return View("NotFound");
            }
            return View(person);
        }

        [HttpGet]
        public IActionResult AddOrEdit(Guid? id)
        {
            PersonModel model = new();

            if (id is null)
            {
                ViewBag.Title = "Add Person";
                return View(model);
            }
            var person = GetPersonModel(id.Value);
            if (person is null)
            {
                return View("NotFound");
            }
            return View(person);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(Guid? id, PersonModel model)
        {
            if (!await IsPersonalNumberInUse(model))
            {
                ModelState.AddModelError("", "Invalid Personal Id");
            }
            if (ModelState.IsValid)
            {
                Persons person = new Persons()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PersonalNumber = model.PersonalNumber,
                    GenderId = (byte)model.Gender.Value,
                    PhoneNumber = model.PhoneNumber,
                    City = model.City,
                    DateOfBirth = model.DateOfBirth.Value,
                    IsActive = model.IsActive
                };
                if (id is null)
                {
                    _unitOfWork.Persons.Insert(person);
                }
                else
                {
                    person.Id = model.Id;
                    _unitOfWork.Persons.Update(person);
                }
                return RedirectToAction("PersonsList");
            }
            return View(model);
        }

        public IActionResult Delete(Guid id)
        {
            var person = _unitOfWork.Persons.GetById(id);
            if (person is null)
            {
                return View("NotFound");
            }
            _unitOfWork.Persons.Delete(person);
            return RedirectToAction("PersonsList");
        }
        private PersonModel GetPersonModel(Guid id)
        {
            var person = _unitOfWork.Persons.GetById(id);
            if (person is not null)
            {
                var model = ModelMapHandler.BuildModelMapping(person, new PersonModel());
                model.Gender = (Models.Gender?)person.GenderId;
                return model;
            }
            return null;
        }

        private async Task<bool> IsPersonalNumberInUse(PersonModel model)
        {
            var person = await _unitOfWork.Persons.FindByPersonalNumberAsync(model.PersonalNumber);

            return person is null || person.Id == model.Id;
        }
    }
}
