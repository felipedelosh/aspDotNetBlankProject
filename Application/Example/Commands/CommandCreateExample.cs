using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Example.Commands
{
    public record CommandCreateExample
    (
      [Required] string Title,
      [Required] string Description,
      [Required] string Information,
      bool? IsDeleted 
    ) : IRequest<int>;
}
