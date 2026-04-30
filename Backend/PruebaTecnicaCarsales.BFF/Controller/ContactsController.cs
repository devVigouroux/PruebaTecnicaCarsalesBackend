using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaCarsales.BFF.Domain;
using PruebaTecnicaCarsales.BFF.Dto;
using PruebaTecnicaCarsales.BFF.Services.Interfaces;

namespace PruebaTecnicaCarsales.BFF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;

        public ContactsController(IContactService contactService)
        {
            _contactService = contactService;
        }

        [HttpGet("GetAllContacts")]
        public IActionResult GetAll()
        {
            var contacts = _contactService.GetAll();

            return Ok(new
            {
                message = contacts.Any()
                    ? "Contactos encontrados"
                    : "No existen contactos",
                contactos = contacts
            });
        }

        [HttpGet("GetContact{id}")]
        public IActionResult GetById(int id)
        {
            var contact = _contactService.GetById(id);
            return Ok(contact);
        }

        [HttpPost("AddContact")]
        public IActionResult AddContact(ContactDto dto)
        {
            var contact = _contactService.Create(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = contact.Id },
                contact
            );
        }

        [HttpDelete("DeleteContact{id}")]
        public IActionResult Delete(int id)
        {
            _contactService.Delete(id);

            return Ok(new
            {
                message = "Contacto eliminado correctamente"
            });
        }

        [HttpPut("SetContact{id}")]
        public IActionResult UpdateContact(int id, ContactDto dto)
        {
            var updatedContact = _contactService.Update(id, dto);
            //return Ok(updatedContact);
            return Ok(new {
                message="Contacto actualizado correctamente",
                contacto=updatedContact
            });
        }
    }
}