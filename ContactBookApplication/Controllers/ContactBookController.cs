using ContactBookApplication.Data;
using ContactBookApplication.Models;
using ContactBookApplication.Services.Contract;
using ContactBookApplication.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;
using System.Diagnostics.Metrics;

namespace ContactBookApplication.Controllers
{
    public class ContactBookController : Controller
    {
        private AppDbContext _context;
        private readonly IContactBookService _contactBookService;
        public ContactBookController(AppDbContext _appDbcontext, IContactBookService contactBookService)
        {
            _context = _appDbcontext;
            _contactBookService = contactBookService;
        }


        public IActionResult Index(char? character, int page = 1, int pageSize = 2)
        {
            ViewBag.CurrentPage = page; // Pass the current page number to the ViewBag
                                        // Get total count of categories
            var totalCount = _contactBookService.TotalContact();
            // Calculate total number of pages
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            // Get paginated categories
            //var contacts = _contactBookService.GetPaginatedContactBook(character,page, pageSize);
            var contacts = character == null
            ? _contactBookService.GetPaginatedContacts(page, pageSize)
            : _contactBookService.GetPaginatedContactBook(character, page, pageSize);
            // Set ViewBag properties
            ViewBag.TotalPages = totalPages;
            ViewBag.PageSize = pageSize;
            ViewBag.Character = character;
            return View(contacts);
        }



        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var contact = _contactBookService.GetContact(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }
        [HttpPost]
        public IActionResult Edit(ContactBook contactBook)
        {
            var message = _contactBookService.ModifyContact(contactBook);

            if ((message == "Contact already exists." || message == "Something went wrong, please try after sometime."))
            {
                TempData["ErrorMessage"] = message;
            }
            else
            {
                TempData["SuccessMessage"] = message;
                return RedirectToAction("Index");
            }
            return View(contactBook);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(ContactViewModel contactbook)
        {
            if (ModelState.IsValid)
            {
                ContactBook contact = new ContactBook()
                {
                    ContactId = contactbook.ContactId,
                    FirstName = contactbook.FirstName,
                    LastName = contactbook.LastName,
                    PhoneNumber = contactbook.PhoneNumber,
                    EmailId = contactbook.EmailId,
                    Address = contactbook.Address,
                };
                var result = _contactBookService.AddContact(contact,contactbook.File);
                if (result == "Contact already exists." || result == "Something went wrong, please try after sometime.")
                {
                    TempData["ErrorMessage"] = result;
                }
                else if (result == "Contact saved successfully.")
                {
                    TempData["SuccessMessage"] = result;
                    return RedirectToAction("Index");
                }
            }
            return View(contactbook);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var contact = _contactBookService.GetContact(id);

            if (contact == null)
            {
                return NotFound();
            }
            return View(contact);
        }


        [Authorize]
        public IActionResult Delete(int id)
        {
            var contact = _contactBookService.GetContact(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);


        }
        [HttpPost]
        public IActionResult DeleteConfirm(int contactId)
        {
            var result = _contactBookService.RemoveContact(contactId);

            if (result == "Contact deleted successfully.")
            {
                TempData["SuccessMessage"] = result;
            }
            else
            {
                TempData["ErrorMessage"] = result;
            }

            return RedirectToAction("Index");

        }
    }
}
