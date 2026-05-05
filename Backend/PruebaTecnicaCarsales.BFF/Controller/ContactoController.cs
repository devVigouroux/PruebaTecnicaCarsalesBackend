using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaCarsales.BFF.Domain;
using PruebaTecnicaCarsales.BFF.Dto;
using PruebaTecnicaCarsales.BFF.Services.Interfaces;

namespace PruebaTecnicaCarsales.BFF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactosController : ControllerBase
    {
        private readonly IContactoService _contactoService;

        public ContactosController(IContactoService contactoService)
        {
            _contactoService = contactoService;
        }

        [HttpGet()]
        public IActionResult GetAll()
        {
            var contacts = _contactoService.GetAll();

            return Ok(new
            {
                message = contacts.Any()
                    ? "Contactos encontrados"
                    : "No existen contactos",
                contactos = contacts
            });
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var contact = _contactoService.GetById(id);
            return Ok(contact);
        }

        [HttpPost()]
        public IActionResult AddContact(ContactoDto dto)
        {
            var contact = _contactoService.Create(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = contact.Id },
                contact
            );
        }

        [HttpDelete("DeleteContact{id}")]
        public IActionResult Delete(int id)
        {
            _contactoService.Delete(id);

            return Ok(new
            {
                message = "Contacto eliminado correctamente"
            });
        }

        [HttpPut("SetContact{id}")]
        public IActionResult UpdateContact(int id, ContactoDto dto)
        {
            var updatedContact = _contactoService.Update(id, dto);
            //return Ok(updatedContact);
            return Ok(new {
                message="Contacto actualizado correctamente",
                contacto=updatedContact
            });
        }
    }
}