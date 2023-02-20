using AspNetCoreDatatable.Data;
using AspNetCoreDatatable.Entities;

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
                string draw = Request.Form["draw"].FirstOrDefault();
                string start = Request.Form["start"].FirstOrDefault();
                string length = Request.Form["length"].FirstOrDefault();
                string searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                List<Street> streets = new List<Street>();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    streets = await _context.Streets.Where(m => m.StreetId.ToString().Contains(searchValue)
                                                || m.StreetName.Contains(searchValue)
                                                || m.StreetNameMm.Contains(searchValue)
                                                || m.Lat.Value.ToString().Contains(searchValue)
                                                || m.Long.Value.ToString().Contains(searchValue)).ToListAsync();
                }
                else
                {
                    streets = await _context.Streets.ToListAsync();
                }
                recordsTotal = streets.Count();
                List<Street> data = pageSize < 0 ? streets : streets.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
                return Ok(jsonData);
            }
            catch (Exception)
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
                string draw = Request.Form["draw"].FirstOrDefault();
                string start = Request.Form["start"].FirstOrDefault();
                string length = Request.Form["length"].FirstOrDefault();
                string searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                List<Street> streets = new List<Street>();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    streets = await _context.Streets.Where(m => m.StreetId.ToString().Contains(searchValue)
                                                || m.StreetName.Contains(searchValue)
                                                || m.StreetNameMm.Contains(searchValue)
                                                || m.Lat.Value.ToString().Contains(searchValue)
                                                || m.Long.Value.ToString().Contains(searchValue)).ToListAsync();
                }
                else
                {
                    streets = await _context.Streets.ToListAsync();
                }
                recordsTotal = streets.Count();
                List<Street> data = pageSize < 0 ? streets : streets.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
                return Ok(jsonData);
            }
            catch (Exception)
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
                string draw = Request.Form["draw"].FirstOrDefault();
                string start = Request.Form["start"].FirstOrDefault();
                string length = Request.Form["length"].FirstOrDefault();
                string sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                string sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                string searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;


                List<Street> streets = new();
                if (!string.IsNullOrEmpty(searchValue))
                {
                    streets = await _context.Streets.Where(m => m.StreetId.ToString().Contains(searchValue)
                                                || m.StreetName.Contains(searchValue)
                                                || m.StreetNameMm.Contains(searchValue)
                                                || m.Lat.Value.ToString().Contains(searchValue)
                                                || m.Long.Value.ToString().Contains(searchValue)).ToListAsync();
                }
                else
                {
                    streets = await _context.Streets.ToListAsync();
                }

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    if (sortColumnDirection == "asc")
                        streets = streets.OrderBy(s => typeof(Street).GetProperty(sortColumn).GetValue(s)).ToList();
                    else if (sortColumnDirection == "desc")
                        streets = streets.OrderByDescending(s => typeof(Street).GetProperty(sortColumn).GetValue(s)).ToList();
                }

                recordsTotal = streets.Count;
                List<Street> data = pageSize < 0 ? streets : streets.Skip(skip).Take(pageSize).ToList();
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
