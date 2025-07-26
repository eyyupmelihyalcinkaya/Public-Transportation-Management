using internshipproject1.Application.Features.Customer.Commands.CreateCustomer;
using internshipproject1.Application.Features.Customer.Commands.DeleteCustomer;
using internshipproject1.Application.Features.Customer.Commands.UpdateCustomer;
using internshipproject1.Application.Features.Customer.Queries.GetAllCustomers;
using internshipproject1.Application.Features.Customer.Queries.GetCustomerByEmail;
using internshipproject1.Application.Features.Customer.Queries.GetCustomerById;
using internshipproject1.Application.Features.Customer.Queries.GetInactiveCustomers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace internshipProject1.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;
    
        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("AllCustomers")]
        public async Task<ActionResult> GetAllCustomers()
        {
            var customers = await _mediator.Send(new GetAllCustomersQueryRequest());
            if (customers == null || !customers.Any())
            {
                return NotFound("No customers found.");
            }
            return Ok(customers);
        }
        [HttpGet("ActiveCustomers")]
        public async Task<ActionResult> GetAllActiveCustomers()
        { 
            var activeCustomers = await _mediator.Send(new GetAllCustomersQueryRequest());
            if (activeCustomers == null || !activeCustomers.Any())
            {
                return NotFound("No active customers found.");
            }
            return Ok(activeCustomers.Where(c => c.IsActive));
        }
        [HttpGet("Students")]
        public async Task<ActionResult> GetAllStudents()
        { 
            var students = await _mediator.Send(new GetAllCustomersQueryRequest());
            if (students == null || !students.Any())
            {
                return NotFound("No students found.");
            }
            return Ok(students.Where(c => c.IsStudent));
        }
        [HttpGet("ByEmail")]
        public async Task<ActionResult> GetCustomerByEmail([FromQuery] string email)
        { 
            var customer = await _mediator.Send(new GetCustomerByEmailQueryRequest(email));
            if (customer == null)
            { 
                return NotFound($"Customer with email {email} not found.");
            }
            return Ok(customer);
        }

        [HttpGet("ById")]
        public async Task<ActionResult> GetCustomerById([FromQuery] int id)
        { 
            var customer = await _mediator.Send(new GetCustomerByIdQueryRequest(id));
            if (customer == null)
            { 
                return NotFound($"Customer with ID {id} not found.");
            }
            return Ok(customer);
        }

        [HttpGet("ByNameAndSurname")]
        public async Task<ActionResult> GetCustomerByNameAndSurname([FromQuery] string name, [FromQuery] string surname)
        { 
            var customers = await _mediator.Send(new GetAllCustomersQueryRequest());
            if (customers == null || !customers.Any())
            {
                return NotFound("No customers found.");
            }
            var filteredCustomers = customers.Where(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && 
                                                          c.Surname.Equals(surname, StringComparison.OrdinalIgnoreCase)).ToList();
            if (!filteredCustomers.Any())
                {
                return NotFound($"No customers found with name {name} and surname {surname}.");
            }
            return Ok(filteredCustomers);
        }

        [HttpGet("Inactives")]
        public async Task<ActionResult> GetAllInactives()
        { 
            var inactiveCustomers = await _mediator.Send(new GetInactiveCustomersQueryRequest());
            if (inactiveCustomers == null || !inactiveCustomers.Any())
            {
                return NotFound("No inactive customers found.");
            }
            return Ok(inactiveCustomers);

        }



        [HttpPost]
        public async Task<ActionResult> CreateNewCustomer([FromBody] CreateCustomerCommandRequest request)
        { 
            var customer = await _mediator.Send(request);
            if (customer == null)
            {
                return BadRequest("Customer creation failed.");
            }
            return CreatedAtAction(nameof(GetAllCustomers), new { id = customer.Id }, customer);
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteCustomer([FromQuery] int id)
        { 
            var deletedCustomer = await _mediator.Send(new DeleteCustomerCommandRequest(id));
            if(deletedCustomer == null)
            {
                return NotFound($"Customer with ID {id} not found.");
            }
            return Ok($"Customer with ID {id} has been deleted successfully.");
        }

        [HttpPut]
        public async Task<ActionResult> UpdateCustomer([FromBody] UpdateCustomerCommandRequest request)
        { 
            var customer = await _mediator.Send(request);
            if (customer == null)
            {
                return NotFound($"Customer with ID {request.Id} not found.");
            }
            return Ok(customer);
        }
    }
}
