using ContactApi.Models;

namespace ContactApi.Services;

public class ContactService
{
    private readonly List<Contact> _contacts = new();
    private int _nextId = 1;

    public List<Contact> GetAll() => _contacts.ToList();

    public Contact? GetById(int id) => _contacts.FirstOrDefault(c => c.Id == id);

    public Contact Add(Contact contact)
    {
        contact.Id = _nextId++;
        _contacts.Add(contact);
        return contact;
    }

    public bool Delete(int id)
    {
        var contact = GetById(id);
        if (contact is null) return false;
        return _contacts.Remove(contact);
    }

    public Contact? Update(int id, Contact updated)
    {
        var contact = GetById(id);
        if (contact is null) return null;

        contact.FirstName = updated.FirstName;
        contact.LastName = updated.LastName;
        contact.Address = updated.Address;
        contact.PhoneNumber = updated.PhoneNumber;
        contact.Age = updated.Age;
        contact.PostCode = updated.PostCode;
        return contact;
    }
}
