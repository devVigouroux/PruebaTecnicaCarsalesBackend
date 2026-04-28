using PruebaTecnicaCarsales.BFF.Controllers;
using PruebaTecnicaCarsales.BFF.Domain;
using PruebaTecnicaCarsales.BFF.Dto;
using PruebaTecnicaCarsales.BFF.Services.Interfaces;
using Microsoft.Extensions.Logging;
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
    public bool ExistsByNameAndPhone(string nombre, string telefono)
    {
        lock (lockObject)
        {
            return contacts.Any(c =>c.Nombre.Trim().ToLower()==nombre.Trim().ToLower() 
            && c.Telefono == telefono.Trim());
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
            if (ExistsByNameAndPhone(dto.Nombre, dto.Telefono))
            {
                _logger.LogWarning(
                "Intento de duplicado: {Nombre} - {Telefono}",
                dto.Nombre,
                dto.Telefono
               );
               throw new InvalidOperationException("Ya existe el contacto en la lista con el mismo nombre y telefono");

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

            throw new InvalidOperationException(
                "No existe el contacto");
        }

        // Validar duplicado (pero no contra sí mismo)
        var exists = contacts.Any(c =>
            c.Id != id &&
            c.Nombre.Trim().ToLower() == dto.Nombre.Trim().ToLower()
            && c.Telefono == dto.Telefono.Trim());

        if (exists)
        {
            _logger.LogWarning(
                "Intento de actualización duplicada: {Nombre} - {Telefono}",
                dto.Nombre,
                dto.Telefono);

            throw new InvalidOperationException(
                "Ya existe otro contacto con el mismo nombre y teléfono");
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

}