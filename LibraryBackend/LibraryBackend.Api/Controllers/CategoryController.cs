using LibraryBackend.Application.Dtos.Request;
using LibraryBackend.Application.Features.Commands;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBackend.Api.Controllers;

public class CategoryController : BaseController<CategoryController>
{
    [Route("add", Name = "AddNewBookCategory")]
    [HttpPost]
    public async Task<IActionResult> AddNewBookCategory(CreateCategoryDto createCategory)
    {
        var response = await _mediator.Send(new CreateCategoryCommand() { CategoryDto = createCategory});
        return ResolveActionResult(response);
    }
}