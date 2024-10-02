using ContactBookApplication.Data.Contract;
using ContactBookApplication.Models;
using ContactBookApplication.Services.Contract;
using Microsoft.DotNet.MSIdentity.Shared;
using System.Diagnostics.Metrics;

namespace ContactBookApplication.Services.Implementation
{
    public class ContactBookService : IContactBookService
    {
        private readonly IContactBookRepository _contactBookRepository;

        public ContactBookService(IContactBookRepository contactBookRepository)
        {
            _contactBookRepository = contactBookRepository;
        }

        public IEnumerable<ContactBook> GetContactBooks(char? character)
        {
            var contacts = _contactBookRepository.GetAll(character);
            if (contacts != null && contacts.Any())
            {
                foreach (var contact in contacts.Where(c => c.FileName == ""))
                {
                    contact.FileName = "Default.png";
                }
                return contacts;
            }

            return new List<ContactBook>();
        }
        public ContactBook? GetContact(int id)
        {
            var contact= _contactBookRepository.GetContact(id);
            if(contact.FileName == "")
            {
                contact.FileName = "Default.png";
            }
            return contact;
        }
        public int TotalContact()
        {
            return _contactBookRepository.TotalContact();
        }
        public IEnumerable<ContactBook> GetPaginatedContactBook(char? character,int page, int pageSize)
        {
            return _contactBookRepository.GetPaginatedContactBook(character,page, pageSize);
        }
        public IEnumerable<ContactBook> GetPaginatedContacts(int page, int pageSize)
        {
            return _contactBookRepository.GetPaginatedContacts(page, pageSize);
        }
        public string AddContact(ContactBook contact, IFormFile file)
        {
            if (_contactBookRepository.ContactExists(contact.FirstName,contact.LastName,contact.PhoneNumber))
            {
                return "Contact already exists.";
            }
            var fileName = string.Empty;
            if (file != null && file.Length > 0)
            {
                //process the upload file(eg. SAve to disk)
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", file.FileName);

                //save the file to storage and set path
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(stream);
                    fileName = file.FileName;
                }
                contact.FileName = fileName;
            }

            var result = _contactBookRepository.InsertContact(contact);

            return result ? "Contact saved successfully." : "Something went wrong, please try after sometime.";
        }

        public string ModifyContact(ContactBook contact)
        {
            var message = string.Empty;
            if (_contactBookRepository.ContactExists(contact.ContactId, contact.FirstName,contact.PhoneNumber))
            {
                message = "Contact already exists.";
                return message;
            }

            var existingContact = _contactBookRepository.GetContact(contact.ContactId);
            var result = false;
            if (existingContact != null)
            {
                existingContact.FirstName = contact.FirstName;
                existingContact.LastName = contact.LastName;
                existingContact.EmailId= contact.EmailId;
                existingContact.PhoneNumber=contact.PhoneNumber;
                existingContact.Address= contact.Address;
                existingContact.FileName = contact.FileName;
                result = _contactBookRepository.UpdateContact(existingContact);
            }

            message = result ? "Contact updated successfully." : "Something went wrong, please try after sometime.";
            return message;
        }

        public string RemoveContact(int id)
        {
            var result = _contactBookRepository.DeleteContact(id);
            if (result)
            {
                return "Contact deleted successfully.";
            }
            else
            {
                return "Something went wrong, please try after sometimes.";
            }
        }
    }
}
