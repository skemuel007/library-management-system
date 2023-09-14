using LibraryBackend.Application.Dtos.Request;
using LibraryBackend.Application.Features.Commands;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBackend.Api.Controllers;
public class BookController : BaseController<BookController>
{
    [Route("add", Name = "AddNewBook")]
    [HttpPost]
    public async Task<IActionResult> AddNewBook(CreateBookDto createBook)
    {
        var response = await _mediator.Send(new CreateBookCommand() { BookDto = createBook});
        return ResolveActionResult(response);
    }
}