using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private static List<Contact>contacts= new List<Contact>();
        private readonly IContactService _contactService;
    
        public ContactsController(IContactService contactService)
        {
            _contactService=contactService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var contacts=_contactService.GetAll();
            if(!contacts.Any())
                return NotFound("No existen Contactos");
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var contact = _contactService.GetById(id);
            if (contact == null)
                return NotFound("No existen registros en la lista para ese Id");
                

            return Ok(contact);
        }

        [HttpPost]
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
                return NotFound("No existen registros");

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateContact(int id, ContactDto dto)
        {
            var updatedContact = _contactService.Update(id, dto);

            return Ok(updatedContact);
        }


    }
    
    

}