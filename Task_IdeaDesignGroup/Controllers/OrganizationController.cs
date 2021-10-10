using DatabaseRepository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_IdeaDesignGroup.Models;
using Task_IdeaDesignGroup.ViewModel;
using Utility;

namespace Task_IdeaDesignGroup.Controllers
{
    public class OrganizationController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrganizationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> OrganizationsList(string search, int page = 1, byte pageSize = 2)
        {
            if (page < 1)
                page = 1;

            List<OrganizationModel> model = new();
            IEnumerable<Organizations> organizations;

            if (!string.IsNullOrEmpty(search))
            {
                organizations = await _unitOfWork.Organizations.SearchOrganization(search);
            }
            else
            {
                organizations = _unitOfWork.Organizations.Get();
            }

            foreach (var organization in organizations)
            {
                var organizationMapped = ModelMapHandler.BuildModelMapping(organization, new OrganizationModel());
                model.Add(organizationMapped);
            }


            int organizationsCount = model.Count;
            int totalPages = 0;

            if (organizationsCount != 0)
                totalPages = organizationsCount % pageSize != 0 ? organizationsCount / pageSize + 1 : organizationsCount / pageSize;

            ViewBag.Page = page;
            ViewBag.TotalPages = totalPages;
            ViewBag.SearchText = search;

            model = MyTools.Paging(model, page, pageSize).ToList();
            return View(model);
        }

        [HttpGet("CompanyDetails")]
        public async Task<IActionResult> OrganizationDetails(Guid id)
        {
            var persons = _unitOfWork.Organizations.GetPersonsInOrganization(id);
            var organization = GetOrganizationModel(id);
            if (organization is null)
            {
                return View("NotFound");
            }
            foreach (var person in await persons)
            {
                var mappedModel = ModelMapHandler.BuildModelMapping(person, new PersonModel());
                mappedModel.Gender = (Models.Gender?)person.GenderId;
                organization.Persons.Add(mappedModel);
            }
            return View(organization);
        }

        [HttpGet]
        public IActionResult AddOrEdit(Guid? id)
        {
            OrganizationModel model = new();

            if (id is null)
            {
                ViewBag.Title = "Add Organization";
                return View(model);
            }
            var organization = GetOrganizationModel(id.Value);
            if (organization is null)
            {
                return View("NotFound");
            }
            return View(organization);
        }

        [HttpPost]
        public IActionResult AddOrEdit(Guid? id, OrganizationModel model)
        {
            if (ModelState.IsValid)
            {
                var organization = ModelMapHandler.BuildModelMapping(model, new Organizations());
                if (id is null)
                {
                    _unitOfWork.Organizations.Insert(organization);
                }
                else
                {
                    organization.Id = model.Id;
                    _unitOfWork.Organizations.Update(organization);
                }
                return RedirectToAction("OrganizationsList");
            }
            return View(model);
        }

        public IActionResult Delete(Guid id)
        {
            var organization = _unitOfWork.Organizations.GetById(id);
            if (organization is null)
            {
                return View("NotFound");
            }
            _unitOfWork.Organizations.Delete(organization);
            return RedirectToAction("OrganizationsList");
        }

        [HttpGet("NewEmployee")]
        public async Task<IActionResult> AddEmployeeToOrganization(Guid organizationId)
        {
            var persons = _unitOfWork.Organizations.GetPersonsOutOfOrganization(organizationId);
            NewEmployeeViewModel model = new NewEmployeeViewModel() { OrganizationId = organizationId };
            foreach (var person in await persons)
            {
                var mappedModel = ModelMapHandler.BuildModelMapping(person, new PersonModel());
                mappedModel.Gender = (Models.Gender?)person.GenderId;
                model.Persons.Add(mappedModel);
            }
            return View(model);
        }

        [HttpPost("NewEmployee")]
        public IActionResult AddEmployeeToOrganization(Guid personId, Guid organizationId)
        {
            _unitOfWork.PersonToOrganizations.InsertPersonToOrganization(personId, organizationId);
            return RedirectToAction("AddEmployeeToOrganization", new { organizationId = organizationId });
        }
        public IActionResult RemoveEmployeeFromOrganization(Guid personId, Guid organizationId)
        {
            _unitOfWork.PersonToOrganizations.RemovePersonFromOrganization(personId, organizationId);
            return RedirectToAction("OrganizationDetails", new { id = organizationId });
        }
        private OrganizationModel GetOrganizationModel(Guid id)
        {
            var organization = _unitOfWork.Organizations.GetById(id);
            if (organization is not null)
            {
                var model = ModelMapHandler.BuildModelMapping(organization, new OrganizationModel());
                return model;
            }
            return null;
        }
    }
}
