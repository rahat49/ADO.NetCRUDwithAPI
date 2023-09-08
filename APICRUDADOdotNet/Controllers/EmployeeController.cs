using APICRUDADOdotNet.DAL;
using APICRUDADOdotNet.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICRUDADOdotNet.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeDAL _emp;
        public EmployeeController(EmployeeDAL emp)
        {
            _emp = emp;
        }
        [HttpGet("GetList")]
        public IEnumerable<Employee> GetList()
        {
            var list= _emp.GetAll();
            return list;
        }
        [HttpPost("InsertData")]
        public Employee InsertData(Employee emp)
        {
            if(ModelState.IsValid)
            {
                _emp.Insert(emp);
            }
            return emp;
               
        }
        [HttpPatch("Update")]

        public Employee Update(int id, Employee emp)
        {
            var employee = _emp.GetById(id);
            if (employee == null)
            {
                return employee;
            }
            else
            {
                _emp.Update(emp);
            }
            return emp;
        }
        [HttpDelete("Delete")]
        public bool Delete(int id)
        {
            bool a = false;
            var employee =_emp.GetById(id);
            if (employee != null)
            {
                a= true;
                _emp.Delete(id);
            }
            else
            {
                a = false;
            }
            return a;
        }
    }
}
