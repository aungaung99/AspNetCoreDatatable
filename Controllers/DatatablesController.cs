using AspNetCoreDatatable.Data;
using AspNetCoreDatatable.Entities;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
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
            return Ok(new { data = await _context.UserInfos.ToListAsync() });
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

                IQueryable<UserInfo> userInfo = (from dbuserinfo in _context.UserInfos select dbuserinfo);
                if (!string.IsNullOrEmpty(searchValue))
                {
                    userInfo = userInfo.Where(m => m.Name.Contains(searchValue)
                                                || m.Gender.Contains(searchValue)
                                                || m.EyeColor.Contains(searchValue)
                                                || m.Email.Contains(searchValue)
                                                || m.Phone.Contains(searchValue)
                                                || m.Company.Contains(searchValue));
                }

                recordsTotal = userInfo.Count();
                List<UserInfo> data = pageSize < 0 ? await userInfo.ToListAsync() : await userInfo.Skip(skip).Take(pageSize).ToListAsync();
                return Ok(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

                IQueryable<UserInfo> userInfo = (from dbuserinfo in _context.UserInfos select dbuserinfo);
                if (!string.IsNullOrEmpty(searchValue))
                {
                    userInfo = userInfo.Where(m => m.Name.Contains(searchValue)
                                                || m.Gender.Contains(searchValue)
                                                || m.EyeColor.Contains(searchValue)
                                                || m.Email.Contains(searchValue)
                                                || m.Phone.Contains(searchValue)
                                                || m.Company.Contains(searchValue));
                }

                recordsTotal = userInfo.Count();
                List<UserInfo> data = pageSize < 0 ? await userInfo.ToListAsync() : await userInfo.Skip(skip).Take(pageSize).ToListAsync();
                return Ok(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });
            }
            catch (Exception)
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
                // Create a config object
                ParsingConfig config = new ParsingConfig
                {
                    UseParameterizedNamesInDynamicQuery = true
                };

                string draw = Request.Form["draw"].FirstOrDefault();
                string start = Request.Form["start"].FirstOrDefault();
                string length = Request.Form["length"].FirstOrDefault();
                string sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                string sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                string searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                IQueryable<UserInfo> userInfo = (from dbUserInfo in _context.UserInfos select dbUserInfo);
                recordsTotal = userInfo.Count();

                if (!string.IsNullOrEmpty(searchValue))
                {
                    userInfo = userInfo.Where(m => m.Name.Contains(searchValue)
                                                || m.Gender.Contains(searchValue)
                                                || m.EyeColor.Contains(searchValue)
                                                || m.Email.Contains(searchValue)
                                                || m.Phone.Contains(searchValue)
                                                || m.Company.Contains(searchValue));
                }

                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    userInfo = userInfo.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                List<UserInfo> data = pageSize < 0 ? await userInfo.ToListAsync() : await userInfo.Skip(skip).Take(pageSize).ToListAsync();
                var jsonData = new { draw, recordsFiltered = recordsTotal, recordsTotal, data };
                return Ok(jsonData);

            }
            catch (Exception)
            {
                return BadRequest();
                throw;
            }
        }

        // All IN One
        [HttpPost("AIO")]
        public async Task<IActionResult> PostAIOAsync()
        {
            try
            {
                // Create a config object
                ParsingConfig config = new ParsingConfig
                {
                    UseParameterizedNamesInDynamicQuery = true
                };

                string draw = Request.Form["draw"].FirstOrDefault();
                string start = Request.Form["start"].FirstOrDefault();
                string length = Request.Form["length"].FirstOrDefault();
                string sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                string sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                string searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;

                IQueryable<UserInfo> userInfo = (from dbUserInfo in _context.UserInfos select dbUserInfo);
                recordsTotal = userInfo.Count();

                StringBuilder @condition = new StringBuilder();
                @condition.Append("u => ");
                PropertyInfo[] props = typeof(UserInfo).GetProperties();
                for (int i = 0; i < props.Length; i++)
                {
                    if (!string.IsNullOrEmpty(searchValue) && (props[i].Name == "Name" || props[i].Name == "Age" || props[i].Name == "IsActive"))
                    {
                        if (props[i].PropertyType != typeof(string))
                        {
                            @condition.Append("u.");
                            @condition.Append(props[i].Name);
                            @condition.Append(".Value.ToString().Contains(\"" + searchValue + "\") || ");
                        }
                        else
                        {
                            @condition.Append("u.");
                            @condition.Append(props[i].Name);
                            @condition.Append(".Contains(\"" + searchValue + "\") || ");
                        }
                    }
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    var whereStr = @condition.ToString().Substring(0, @condition.Length - 4);
                    userInfo = userInfo.Where(whereStr);
                }




                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    userInfo = userInfo.OrderBy(sortColumn + " " + sortColumnDirection);
                }

                List<UserInfo> data = pageSize < 0 ? await userInfo.ToListAsync() : await userInfo.Skip(skip).Take(pageSize).ToListAsync();
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
