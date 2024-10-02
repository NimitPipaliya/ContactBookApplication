using ContactBookApplication.Models;

namespace ContactBookApplication.Services.Contract
{
    public interface IContactBookService
    {
        IEnumerable<ContactBook> GetContactBooks(char? character);

        ContactBook? GetContact(int id);

        string AddContact(ContactBook contact, IFormFile file);


        string RemoveContact(int id);

        string ModifyContact(ContactBook contact);

        int TotalContact();
        IEnumerable<ContactBook> GetPaginatedContactBook(char? character,int page, int pageSize);
        IEnumerable<ContactBook> GetPaginatedContacts(int page, int pageSize);
    }
}
