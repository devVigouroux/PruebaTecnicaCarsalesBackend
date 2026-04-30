
/*using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaCarsales.BFF.Domain;
using PruebaTecnicaCarsales.BFF.Dto;
using PruebaTecnicaCarsales.BFF.Services.Interfaces;

namespace PruebaTecnicaCarsales.BFF.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactsController : ControllerBase
    {
        private static List<Contact>contacts= new List<Contact>();
        private readonly IContactService _contactService;
    
        public ContactsController(IContactService contactService)
        {
            _contactService=contactService;
        }
        [HttpGet("GetAllContact")]
        public IActionResult GetAll()
        {
            var contacts=_contactService.GetAll();
            if(!contacts.Any())
                
                return Ok(new {message="No existen Contactos",contactos=contacts});
            else
            {
                return Ok(contacts);
            }    
            
        }

        [HttpGet("GetContact{id}")]
        public IActionResult GetById(int id)
        {
            var contact = _contactService.GetById(id);
            if (contact == null)
                return Ok(new 
                {
                    message="No existen registros en la lista para el Id",id
                });
            return Ok(contact);
        }

        [HttpPost("AddContact")]
        public ActionResult AddContact(ContactDto dto)
        {
        
            var contact=_contactService.Create(dto);
            return CreatedAtAction(nameof(GetById),new {id = contact.Id},contact);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var deleted = _contactService.Delete(id);

            if (!deleted)
                return Ok(new 
                {
                    message="No existen contactos en la lista a eliminar para el Id",id
                });
                return Ok(new
                {
                    message = "Contacto eliminado"
                });
        }

        [HttpPut("{id}")]
        public ActionResult UpdateContact(int id, ContactDto dto)
        {
            var updatedContact = _contactService.Update(id, dto);

            return Ok(updatedContact);
        }


    }
    
    

}*/
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

        [HttpGet]
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

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var contact = _contactService.GetById(id);
            return Ok(contact);
        }

        [HttpPost]
        public IActionResult AddContact(ContactDto dto)
        {
            var contact = _contactService.Create(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = contact.Id },
                contact
            );
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _contactService.Delete(id);

            return Ok(new
            {
                message = "Contacto eliminado correctamente"
            });
        }

        [HttpPut("{id}")]
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