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
}
