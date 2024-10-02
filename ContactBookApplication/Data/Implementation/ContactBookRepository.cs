using ContactBookApplication.Data.Contract;
using ContactBookApplication.Models;
using System.Diagnostics.Metrics;

namespace ContactBookApplication.Data.Implementation
{
    public class ContactBookRepository : IContactBookRepository
    {
        private readonly AppDbContext _appDbContext;

        public ContactBookRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<ContactBook> GetAll(char? character)
        {
            List<ContactBook> contacts = _appDbContext.ContactBook.Where(c => c.FirstName.StartsWith(character.ToString().ToLower())).ToList();
            return contacts;
        }

        public ContactBook? GetContact(int id)
        {
            var contact = _appDbContext.ContactBook.FirstOrDefault(c => c.ContactId == id);
            return contact;
        }
        public ContactBook? GetContact(int id,char? character)
        {
            var contact = _appDbContext.ContactBook.FirstOrDefault(c => c.ContactId == id);
            if (contact.FileName == "")
            {
                contact.FileName = "DefaultImage.png";
            }
            return contact;
        }
        public int TotalContact()
        {
            return _appDbContext.ContactBook.Count();
        }

        public IEnumerable<ContactBook> GetPaginatedContactBook(char? character,int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;
            return _appDbContext.ContactBook
                .Where(c => c.FirstName.StartsWith(character.ToString().ToLower()))
                .OrderBy(c => c.ContactId)
                .Skip(skip)
                .Take(pageSize)
                .ToList();
        }
        public IEnumerable<ContactBook> GetPaginatedContacts(int page, int pageSize)
        {
            int skip = (page - 1) * pageSize;

            return _appDbContext.ContactBook
                .OrderBy(c => c.ContactId)
                .Skip(skip)
                .Take(pageSize)
                .ToList();
        }

        public bool InsertContact(ContactBook contact)
        {
            var result = false;
            if (contact != null)
            {
                _appDbContext.ContactBook.Add(contact);
                _appDbContext.SaveChanges();
                result = true;
            }

            return result;
        }

        public bool UpdateContact(ContactBook contact)
        {
            var result = false;
            if (contact != null)
            {
                _appDbContext.ContactBook.Update(contact);
                //_appDbContext.Entry(contact).State = EntityState.Modified;
                _appDbContext.SaveChanges();
                result = true;
            }
            return result;
        }

        public bool DeleteContact(int id)
        {
            var result = false;
            var contact = _appDbContext.ContactBook.Find(id);
            if (contact != null)
            {
                _appDbContext.ContactBook.Remove(contact);
                _appDbContext.SaveChanges();
                result = true;
            }

            return result;
        }

        public bool ContactExists(string fname,string lname, string phoneNumber)
        {
            var contact = _appDbContext.ContactBook.FirstOrDefault(c => c.FirstName == fname && c.LastName == lname || c.PhoneNumber ==phoneNumber);
            if (contact != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ContactExists(int categoryId, string name, string phoneNumber)
        {
            var contact = _appDbContext.ContactBook.FirstOrDefault(c => c.ContactId != categoryId && c.FirstName == name || c.PhoneNumber==phoneNumber);
            if (contact != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        
    }
}
