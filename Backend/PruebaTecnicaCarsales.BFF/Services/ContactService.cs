
/**using PruebaTecnicaCarsales.BFF.Domain;
using PruebaTecnicaCarsales.BFF.Dto;
using PruebaTecnicaCarsales.BFF.Services.Interfaces;
using Microsoft.Extensions.Logging;
using PruebaTecnicaCarsales.BFF.Exceptions;
namespace PruebaTecnicaCarsales.BFF.Services;

public class ContactService : IContactService
{
    
    private static readonly List<Contact>contacts= new ();
    private static readonly object lockObject= new(); 
    private static int nextId=1;
    private readonly ILogger<ContactService> _logger;


    public ContactService(ILogger<ContactService> ilogger)
    {
        _logger=ilogger;
    }

    public IEnumerable<Contact> GetAll()
    {
        lock (lockObject)
        {
            return contacts.ToList();
        }
    }
    public Contact? GetById(int id)
    {
        lock (lockObject)
        {
            return contacts.FirstOrDefault(c =>c.Id==id);
        }
    }
    public bool ExistsByPhone(string telefono)
    {
        lock (lockObject)
        {
            return contacts.Any(c =>c.Telefono == telefono.Trim());
        }
       
    }
    
    public Contact Create(ContactDto dto)
    {
        lock (lockObject)
        {
            _logger.LogInformation(
               "Intentando crear contacto: {Nombre} - {Telefono}",
                dto.Nombre,
                dto.Telefono 
            );
            if (ExistsByPhone(dto.Telefono))
            {
                _logger.LogWarning(
                "Intento de duplicado: {Telefono}",
                dto.Telefono
               );
               throw new ConflictException("Ya existe el contacto en la lista con el mismo telefono");

            }
            
           var newId = contacts.Any()
            ? contacts.Max(c => c.Id) + 1
            : 1;

           var contact= new Contact
           {
              Id =nextId++,
              Nombre = dto.Nombre.Trim(),
              Telefono= dto.Telefono.Trim(),
           };
           contacts.Add(contact);
           _logger.LogInformation("Contacto agregado con Id {Id}",contact.Id);
           return contact;

        }

    }

    public bool Delete(int id)
    {
        lock (lockObject)
        {
            var contact =contacts.FirstOrDefault(c=> c.Id == id);

            if (contact == null)
                return false;

            contacts.Remove(contact);
            return true;
        }
        
    }

    public Contact Update(int id, ContactDto dto)
{
    lock (lockObject)
    {
        _logger.LogInformation(
            "Intentando actualizar contacto Id: {Id}",
            id);

        var contact = contacts.FirstOrDefault(c => c.Id == id);

        if (contact == null)
        {
            _logger.LogWarning(
                "Contacto no encontrado para actualizar: {Id}",
                id);

            throw new NotFoundException(
                "No existe el contacto a actualizar");
        }

        // Validar duplicado (pero no contra sí mismo)
        var exists = contacts.Any(c =>c.Telefono == dto.Telefono.Trim());

        if (exists)
        {
            _logger.LogWarning(
                "Intento de actualización duplicada: {Telefono}",
                dto.Telefono);

            throw new ConflictException(
                "Ya existe otro contacto con el mismo teléfono");
        }

        // Actualizar datos
        contact.Nombre = dto.Nombre.Trim();
        contact.Telefono = dto.Telefono.Trim();

        _logger.LogInformation(
            "Contacto actualizado correctamente: {Id}",
            id);

        return contact;
    }
}

}*/

using Microsoft.Extensions.Logging;
using PruebaTecnicaCarsales.BFF.Domain;
using PruebaTecnicaCarsales.BFF.Dto;
using PruebaTecnicaCarsales.BFF.Exceptions;
using PruebaTecnicaCarsales.BFF.Services.Interfaces;

namespace PruebaTecnicaCarsales.BFF.Services;

public class ContactService : IContactService
{
    private static readonly List<Contact> contacts = new();
    private static readonly object lockObject = new();
    private static int nextId = 1;

    private readonly ILogger<ContactService> _logger;

    public ContactService(ILogger<ContactService> logger)
    {
        _logger = logger;
    }

    public IEnumerable<Contact> GetAll()
    {
        lock (lockObject)
        {
            return contacts.ToList();
        }
    }

    public Contact GetById(int id)
    {
        lock (lockObject)
        {
            var contact = contacts.FirstOrDefault(c => c.Id == id);

            if (contact == null)
                throw new NotFoundException("No existe un contacto con el id indicado");

            return contact;
        }
    }

    public bool ExistsByPhone(string telefono)
    {
        lock (lockObject)
        {
            return contacts.Any(c => c.Telefono == telefono.Trim());
        }
    }

    public Contact Create(ContactDto dto)
    {
        lock (lockObject)
        {
            _logger.LogInformation(
                "Intentando crear contacto: {Nombre} - {Telefono}",
                dto.Nombre,
                dto.Telefono
            );

            var telefono = dto.Telefono.Trim();

            if (contacts.Any(c => c.Telefono == telefono))
            {
                _logger.LogWarning(
                    "Intento de duplicado: {Telefono}",
                    telefono
                );

                throw new ConflictException(
                    "Ya existe un contacto con el mismo teléfono"
                );
            }

            var contact = new Contact
            {
                Id = nextId++,
                Nombre = dto.Nombre.Trim(),
                Telefono = telefono
            };

            contacts.Add(contact);

            _logger.LogInformation(
                "Contacto agregado con Id {Id}",
                contact.Id
            );

            return contact;
        }
    }

    public void Delete(int id)
    {
        lock (lockObject)
        {
            var contact = contacts.FirstOrDefault(c => c.Id == id);

            if (contact == null)
                throw new NotFoundException("No existe el contacto a eliminar");

            contacts.Remove(contact);

            _logger.LogInformation(
                "Contacto eliminado correctamente: {Id}",
                id
            );
        }
    }

    public Contact Update(int id, ContactDto dto)
    {
        lock (lockObject)
        {
            _logger.LogInformation(
                "Intentando actualizar contacto Id: {Id}",
                id
            );

            var contact = contacts.FirstOrDefault(c => c.Id == id);

            if (contact == null)
                throw new NotFoundException("No existe el contacto a actualizar");

            var telefono = dto.Telefono.Trim();

            var exists = contacts.Any(c =>
                c.Id != id &&
                c.Telefono == telefono
            );

            if (exists)
                throw new ConflictException(
                    "Ya existe otro contacto con el mismo teléfono"
                );

            contact.Nombre = dto.Nombre.Trim();
            contact.Telefono = telefono;

            _logger.LogInformation(
                "Contacto actualizado correctamente: {Id}",
                id
            );

            return contact;
        }
    }
}