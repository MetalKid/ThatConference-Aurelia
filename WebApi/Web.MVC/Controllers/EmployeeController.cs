using System.Linq;
using System.Threading;
using System.Web.Http;
using ThatConference.Common.Enums;
using ThatConference.Common.Exceptions;
using ThatConference.Northwind.Common.DataTransferObjects.Northwind;
using ThatConference.Northwind.Common.Filters.Northwind;
using ThatConference.Northwind.Interfaces.Services.Northwind;
using ThatConference.Northwind.Web.Mvc.Common;

namespace ThatConference.Northwind.Web.Mvc.Controllers
{
    [RoutePrefix("employee")]
    public class EmployeeController : NorthwindApiControllerBase
    {

        private IEmployeesService _employeeService;

        public EmployeeController(IEmployeesService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        [Route("getall")]
        public IHttpActionResult Get()
        {
            var result = _employeeService.Get();
            var data = from a in result.Data
                select
                    new
                    {
                        a.EmployeeID,
                        a.FirstName,
                        a.LastName,
                        BirthDate = a.BirthDate.HasValue ? a.BirthDate.Value.ToShortDateString() : null,
                        HireDate = a.HireDate.HasValue ? a.HireDate.Value.ToShortDateString() : null,
                        a.Address,
                        a.City,
                        a.PostalCode,
                        a.Region,
                        a.Country
                    };
            return Ok(data);
        }

        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var result = _employeeService.Get(new EmployeesFilter { EmployeeID = id});
            if (result.Data.Count != 1)
            {
                return NotFound();
            }

            var record = result.Data.Single();
            var data = new 
            {
                record.EmployeeID,
                record.FirstName,
                record.LastName,
                BirthDate = record.BirthDate.HasValue ? record.BirthDate.Value.ToShortDateString() : null,
                HireDate = record.HireDate.HasValue ? record.HireDate.Value.ToShortDateString() : null,
                record.Address,
                record.City,
                record.PostalCode,
                record.Region,
                record.Country
            };
            return Ok(data);
        }

        [HttpPost]
        public IHttpActionResult Create([FromBody] EmployeesDto employee)
        {
            employee.DataState = DataStateEnum.New;
            var result = RunSave(
                () => _employeeService.Save(new[] { employee }),
                "Successfully created Employee.",
                "Failed to create Employee.",
                1);
            return Ok(result.ToJson());
        }

        [HttpPut]
        public IHttpActionResult Update([FromBody] EmployeesDto employee)
        {
            employee.DataState = DataStateEnum.Modified;
            var result = RunSave(
                () => _employeeService.Save(new[] {employee}),
                "Successfully updated Employee.",
                "Failed to update Employee.",
                1);
            return Ok(result.ToJson());
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            EmployeesDto employee = new EmployeesDto() {
                DataState = DataStateEnum.Deleted,
                EmployeeID = id
            };
            var result = RunSave(
                () => _employeeService.Save(new[] { employee }),
                "Successfully deleted Employee.",
                "Failed to deleted Employee.",
                1);
            return Ok(result.ToJson());
        }

    }
}
