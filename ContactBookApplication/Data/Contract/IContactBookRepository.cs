using ContactBookApplication.Models;

namespace ContactBookApplication.Data.Contract
{
    public interface IContactBookRepository
    {
        IEnumerable<ContactBook> GetAll(char? character);

        ContactBook? GetContact(int id);
        ContactBook? GetContact(int id,char? character);

        bool ContactExists(string fname,string lname, string phoneNumber);

        bool ContactExists(int categoryId, string name, string phoneNumber);

        bool InsertContact(ContactBook contact);

        bool UpdateContact(ContactBook contact);

        bool DeleteContact(int id);
        int TotalContact();
        IEnumerable<ContactBook> GetPaginatedContactBook(char? character,int page, int pageSize);
        IEnumerable<ContactBook> GetPaginatedContacts(int page, int pageSize);
    }
}
