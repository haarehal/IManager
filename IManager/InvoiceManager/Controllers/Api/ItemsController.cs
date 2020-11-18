using AutoMapper;
using InvoiceManager.Dtos;
using InvoiceManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace InvoiceManager.Controllers.Api
{
    public class ItemsController : ApiController
    {
        private ApplicationDbContext _context;

        public ItemsController()
        {
            _context = new ApplicationDbContext();
        }

        // GET /api/items
        public IHttpActionResult GetItems(string query = null)
        {
            var itemsQuery = _context.Items.Include(i => i.Invoice);

            if (!String.IsNullOrWhiteSpace(query)) itemsQuery = itemsQuery.Where(i => i.Description.Contains(query));

            var itemDtos = itemsQuery.ToList().Select(Mapper.Map<Item, ItemDto>);

            return Ok(itemDtos);
        }

        // GET /api/items/{id}
        public IHttpActionResult GetItem(int id)
        {
            var item = _context.Items.SingleOrDefault(i => i.Id == id);

            if (item == null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<Item, ItemDto>(item));
        }

        // POST /api/items
        [HttpPost]
        public IHttpActionResult CreateItem(ItemDto itemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var item = Mapper.Map<ItemDto, Item>(itemDto);

            _context.Items.Add(item);
            _context.SaveChanges();

            itemDto.Id = item.Id;

            return Created(new Uri(Request.RequestUri + "/" + item.Id), itemDto);
        }

        // PUT /api/items/{id}
        [HttpPut]
        public IHttpActionResult UpdateItem(int id, ItemDto itemDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var ItemInDb = _context.Items.SingleOrDefault(i => i.Id == id);

            if (ItemInDb == null)
            {
                return NotFound();
            }

            Mapper.Map(itemDto, ItemInDb);

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/items/{id}
        [HttpDelete]
        public IHttpActionResult DeleteItem(int id)
        {
            var ItemInDb = _context.Items.SingleOrDefault(i => i.Id == id);

            if (ItemInDb == null)
            {
                return NotFound();
            }

            _context.Items.Remove(ItemInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
