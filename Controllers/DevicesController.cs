﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cmpg323project2try.Models;

namespace cmpg323project2try.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevicesController : ControllerBase
    {
        private readonly databaseContext _context;

        public DevicesController(databaseContext context)
        {
            _context = context;
        }

        /*// GET: api/Devices
         [HttpGet]
         public async Task<ActionResult<IEnumerable<Device>>> GetDevice()
         {
             return await _context.Device.ToListAsync();
         }
        */
        // GET: api/Devices/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Device>> GetDevice(Guid id)
        //{
        //    var device = await _context.Device.FindAsync(id);

        //    if (device == null)
        //    {
        //        return NotFound();
        //    }

        //    return device;
        //}

        // PUT: api/Devices/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDevice(Guid id, Device device)
        {
            if (id != device.DeviceId)
            {
                return BadRequest();
            }

            _context.Entry(device).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        //// POST: api/Devices
        //// To protect from overposting attacks, enable the specific properties you want to bind to, for
        //// more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        //[HttpPost]
        //public async Task<ActionResult<Device>> PostDevice(Device device)
        //{
        //    _context.Device.Add(device);
        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateException)
        //    {
        //        if (DeviceExists(device.DeviceId))
        //        {
        //            return Conflict();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return CreatedAtAction("GetDevice", new { id = device.DeviceId }, device);
        //}

        //// DELETE: api/Devices/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Device>> DeleteDevice(Guid id)
        //{
        //    var device = await _context.Device.FindAsync(id);
        //    if (device == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Device.Remove(device);
        //    await _context.SaveChangesAsync();

        //    return device;
        //}

        //private bool DeviceExists(Guid id)
        //{
        //    return _context.Device.Any(e => e.DeviceId == id);
        //}

        [HttpGet]
        public IEnumerable<Device> Get()
        {
            using (databaseContext dbContext2 = new databaseContext())
            {
                return dbContext2.Device.ToList();
            }
        }



        [HttpGet("{id}")]
        public async Task<ActionResult<Device>> GetUsingId(Guid id)
        {

            //var catid = _context.Category.FirstOrDefaultAsync(t=>id == t.CategoryId);
            try
            {
                var catid = await _context.Device.FindAsync(id);

                if (catid == null)
                {
                    return NotFound();

                }
                else
                {
                    return catid;
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }

        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletedevice(Guid id)
        {
            var device = await _context.Device.FindAsync(id);
            if (_context.Device == null || device == null)
            {
                return NotFound();

            }
            _context.Device.Remove(device);
            await _context.SaveChangesAsync();
            return NoContent();
        }



    }
}
