using PruebaTecnicaCarsales.BFF.Domain;
using PruebaTecnicaCarsales.BFF.Dto;

namespace PruebaTecnicaCarsales.BFF.Services.Interfaces;
public interface IContactService
{
    IEnumerable<Contact> GetAll();
    Contact? GetById(int id);
    Contact Create(ContactDto dto);
    bool ExistsByPhone(string telefono);
    void Delete(int id);
    Contact Update(int id, ContactDto dto);
}