using CQRS_Test.CQRS.Commands;
using CQRS_Test.CQRS.Notifications;
using CQRS_Test.CQRS.Queries;
using CQRS_Test.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS_Test.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMediator _mediator;

    public ProductController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    public async Task<ActionResult> GetProducts()
    {
        var products = await _mediator.Send(new GetProductsQuery());
        return Ok(products);
    }
    
    [HttpGet("{id:int}", Name = "GetProductById")]
    public async Task<ActionResult> GetProducts(int id)
    {
        var product = await _mediator.Send(new GetProductByIdQuery(id));
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult> AddProduct([FromBody] Product product)
    {
        var productToReturn = await _mediator.Send(new AddProductCommand(product));

        await _mediator.Publish(new ProductAddedNotification(productToReturn));
        
        return CreatedAtRoute("GetProductById", new { id = productToReturn.Id }, productToReturn);
    }
}