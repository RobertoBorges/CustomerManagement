using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerManagement.Application.Features.Customers.Commands;
using CustomerManagement.Application.Features.Customers.Queries;
using CustomerManagement.Shared.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomerManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Customers
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<CustomerDto>>> GetCustomers()
        {
            var customers = await _mediator.Send(new GetCustomers.Query());
            return Ok(customers);
        }

        // GET: api/Customers/CM001
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CustomerDto>> GetCustomer(string id)
        {
            var customer = await _mediator.Send(new GetCustomerById.Query { Id = id });
            
            if (customer == null)
            {
                return NotFound();
            }

            return Ok(customer);
        }

        // POST: api/Customers
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CustomerDto>> CreateCustomer([FromBody] CreateUpdateCustomerDto customerDto)
        {
            var customer = await _mediator.Send(new CreateCustomer.Command { CustomerDto = customerDto });
            
            return CreatedAtAction(nameof(GetCustomer), new { id = customer.Id }, customer);
        }

        // PUT: api/Customers/CM001
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCustomer(string id, [FromBody] CreateUpdateCustomerDto customerDto)
        {
            var result = await _mediator.Send(new UpdateCustomer.Command { Id = id, CustomerDto = customerDto });
            
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }

        // DELETE: api/Customers/CM001
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCustomer(string id)
        {
            var result = await _mediator.Send(new DeleteCustomer.Command { Id = id });
            
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
