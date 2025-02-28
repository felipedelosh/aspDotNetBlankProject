
using Domain.Services;
using Domain.Entities;
using Application.Example.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace aspDotNetBlankProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExampleController : ControllerBase
    {

        private readonly ExampleService _service;
        private readonly IMediator _mediator;

        public ExampleController(ExampleService service, IMediator mediator)
        {
            _service = service;
            _mediator = mediator;
        }
        // GET: api/<PersonController>
        [HttpGet]
        public async Task<List<Example>> Get()
        {
            return await _service.Get();
        }

        // GET api/<PersonController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Example>> Get(int id)
        {
            Example person = await _service.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            return person;
        }

        // POST api/<PersonController>
        [HttpPost]
        public async Task<ActionResult<int>> Post([FromBody] CommandCreateExample request)
        {
            return await _mediator.Send(new CommandCreateExample(request.Title, request.Description, request.Information, request.IsDeleted));
        }

        // PUT api/<PersonController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult<Example>> Put([FromBody] Example example)
        {
            return await _service.Edit(example);
        }

        // DELETE api/<PersonController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _service.Delete(id);
            return NoContent();
        }
    }
}
