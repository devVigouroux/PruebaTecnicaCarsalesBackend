using PruebaTecnicaCarsales.BFF.Domain;
using PruebaTecnicaCarsales.BFF.Dto;

namespace PruebaTecnicaCarsales.BFF.Services.Interfaces;
public interface IContactoService
{
    IEnumerable<Contacto> GetAll();
    Contacto? GetById(int id);
    Contacto Create(ContactoDto dto);
    bool ExistsByPhone(string telefono);
    void Delete(int id);
    Contacto Update(int id, ContactoDto dto);
}