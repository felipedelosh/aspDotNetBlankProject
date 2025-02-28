using MediatR;
using System.ComponentModel.DataAnnotations;

namespace Application.Example.Commands
{
    public record CommandCreateExample
    (
      [Required] int Id,
      [Required] string Title,
      [Required] string Description,
      [Required] string Information,
      bool? IsDeleted 
    ) : IRequest<int>;
}
