using PruebaTecnicaCarsales.BFF.Domain;
using PruebaTecnicaCarsales.BFF.Dto;

namespace PruebaTecnicaCarsales.BFF.Services.Interfaces;
public interface IContactService
{
    IEnumerable<Contact> GetAll();
    Contact? GetById(int id);
    Contact Create(ContactDto dto);
    bool ExistsByNameAndPhone(string nombre, string telefono);
    bool Delete(int id);
    Contact Update(int id, ContactDto dto);
}