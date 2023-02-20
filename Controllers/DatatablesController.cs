using AspNetCoreDatatable.Data;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreDatatable.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatatablesController : ControllerBase
    {
        private readonly AspNetCoreDatatableContext _context;
        public DatatablesController(AspNetCoreDatatableContext context)
        {
            _context = context;
        }

        // Basic Datatable
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            return Ok(new { data = await _context.Streets.ToListAsync() });
        }

        // Pagination
        [HttpPost("pagination")]
        public async Task<IActionResult> PostPaginationAsync()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var userData = (from tempuser in _context.Streets select tempuser);
                if (!string.IsNullOrEmpty(searchValue))
                {
                    userData = userData.Where(m => m.StreetId.ToString().Contains(searchValue)
                                                || m.StreetName.Contains(searchValue)
                                                || m.StreetNameMm.Contains(searchValue)
                                                || m.Lat.Value.ToString().Contains(searchValue)
                                                || m.Long.Value.ToString().Contains(searchValue));
                }
                recordsTotal = userData.Count();
                var data = pageSize < 0 ? await userData.ToListAsync() : await userData.Skip(skip).Take(pageSize).ToListAsync();
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return BadRequest();
                throw;
            }
        }

        [HttpPost("searching")]
        public async Task<IActionResult> PostSearchingAsync()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var userData = (from tempuser in _context.Streets select tempuser);
                if (!string.IsNullOrEmpty(searchValue))
                {
                    userData = userData.Where(m => m.StreetId.ToString().Contains(searchValue)
                                                || m.StreetName.Contains(searchValue)
                                                || m.StreetNameMm.Contains(searchValue)
                                                || m.Lat.Value.ToString().Contains(searchValue)
                                                || m.Long.Value.ToString().Contains(searchValue));
                }
                recordsTotal = userData.Count();
                var data = pageSize < 0 ? await userData.ToListAsync() : await userData.Skip(skip).Take(pageSize).ToListAsync();
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return BadRequest();
                throw;
            }
        }

        [HttpPost("ordering")]
        public async Task<IActionResult> PostOrderingAsync()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                var userData = (from tempuser in _context.Streets select tempuser);
                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    userData = userData.OrderBy(s => sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    userData = userData.Where(m => m.StreetId.ToString().Contains(searchValue)
                                                || m.StreetName.Contains(searchValue)
                                                || m.StreetNameMm.Contains(searchValue)
                                                || m.Lat.Value.ToString().Contains(searchValue)
                                                || m.Long.Value.ToString().Contains(searchValue));
                }
                recordsTotal = userData.Count();
                var data = await userData.Skip(skip).Take(pageSize).ToListAsync();
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
                return Ok(jsonData);
            }
            catch (Exception ex)
            {
                return BadRequest();
                throw;
            }
        }
    }
}
