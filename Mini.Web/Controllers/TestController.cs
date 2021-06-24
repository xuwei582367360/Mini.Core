
using Microsoft.AspNetCore.Mvc;
using Mini.IServives;
using Mini.Model.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mini.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly ICustomerServices _customerServices;

        public TestController(ICustomerServices customerServices)
        {
            _customerServices = customerServices;
        }

        public IActionResult Index()
        {           
            return View();
        }

        public async Task<List<Customer>> GetAll()
        {
            List<Customer> cusList = await _customerServices.Query();
            return cusList;
        }
    }
}
