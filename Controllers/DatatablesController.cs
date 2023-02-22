using AspNetCoreDatatable.Data;
using AspNetCoreDatatable.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        // Searching
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

        // Custom Searching
        [HttpPost("column-searching")]
        public async Task<IActionResult> PostColumnSearchingAsync()
        {
            try
            {
                bool Searching = false;
                List<Expression<Func<Street, bool>>> expressions = new();
                System.Reflection.PropertyInfo[] props = typeof(Street).GetProperties();
                for (int i = 0; i < props.Length; i++)
                {
                    string colName = Request.Form["columns[" + i + "][name]"].FirstOrDefault();
                    string searchValue = Request.Form["columns[" + i + "][search][value]"].FirstOrDefault();
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        Expression<Func<Street, bool>> expression = s => typeof(Street).GetProperty(colName).GetValue(s).ToString().Contains(searchValue);
                        expressions.Add(expression);
                        Searching = true;
                    }
                }

                string draw = Request.Form["draw"].FirstOrDefault();
                string start = Request.Form["start"].FirstOrDefault();
                string length = Request.Form["length"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                List<Street> streets = new();
                if (Searching)
                {
                    //streets = await _context.Streets.Where(CombineWithOr(expressions.ToArray())).ToListAsync();
                    //streets = await _context.Streets.Where(s=).ToListAsync();
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
            catch (Exception ex)
            {
                return BadRequest();
                throw;
            }
        }

        // Ordering
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
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        public static Expression<Func<T, bool>> CombineWithOr<T>(params Expression<Func<T, bool>>[] filters)
        {
            Expression<Func<T, bool>> first = filters.First();
            ParameterExpression param = first.Parameters.First();
            Expression body = first.Body;

            foreach (Expression<Func<T, bool>> other in filters.Skip(1))
            {
                ReplaceParameter replacer = new ReplaceParameter
                {
                    OriginalParameter = other.Parameters.First(),
                    NewParameter = param
                };
                // We need to replace the original expression parameter with the result parameter
                body = Expression.Or(body, replacer.Visit(other.Body));
            }

            return Expression.Lambda<Func<T, bool>>(
                body,
                param
            );
        }

        private class ReplaceParameter : ExpressionVisitor
        {
            public Expression OriginalParameter { get; set; }
            public Expression NewParameter { get; set; }
            protected override Expression VisitParameter(ParameterExpression node)
            {
                return node == OriginalParameter ? NewParameter : base.VisitParameter(node);
            }
        }
    }
}
