using MediatR;
using Domain.Services;

namespace Application.Example.Commands
{
    public class CommandCreateExampleHandler : IRequestHandler<CommandCreateExample, int>
    {
        private readonly ExampleService _service;
        public CommandCreateExampleHandler(ExampleService service)
        {
            _service = service;
        }

        public async Task<int> Handle(CommandCreateExample request, CancellationToken cancellationToken)
        {
            var entity = new Domain.Entities.Example
            {
                Title = request.Title,
                Description = request.Description,
                Information = request.Information,
                IsDeleted = false,
            };


            Domain.Entities.Example example = await _service.Create(entity);

            return example.Id;
        }
    }
}
