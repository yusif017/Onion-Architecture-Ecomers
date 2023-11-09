
using Application.Repositories.ProductImageFileRepositories;
using Ecomerce.Application.Abstraction.Storage;
using Ecomerce.Application.Repositories;
using Ecomerce.Application.Repositories.FileRepositories;
using Ecomerce.Application.Repositories.ProductImageFileRepositories;
using Ecomerce.Application.Repositories.Products;
using Ecomerce.Application.RequestParameters;
using Ecomerce.Application.ViewModels.Products;
using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;

namespace Ecommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    #region interface
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IStorageService _storageService;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IProductImageFileReadRepository _productImageFileReadRepository;
    public ProductController(IProductWriteRepository productWriteRepository, IProductReadRepository productReadRepository, IStorageService storageService, IProductImageFileWriteRepository productImageFileWriteRepository, IProductImageFileReadRepository productImageFileReadRepository)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
        _storageService = storageService;
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _productImageFileReadRepository = productImageFileReadRepository;
    }

    #endregion



    [HttpGet]
    public IActionResult Get([FromQuery] Pagination pagination)
    {
        var totalCount = _productReadRepository.GetAll(tracking: false).Count();

        var products = _productReadRepository.GetAll(tracking: false)
            .Skip(pagination.Page * pagination.Size)
            .Take(pagination.Size)
            .Select(z => new
            {
                z.Name,
                z.CreatedDate,
                z.UpdateDate,
                z.Price,
                z.Id,
                z.Stock
            });

        return Ok(new
        {
            totalCount,
            products
        });
    }

    [HttpGet("{id}")]
    public IActionResult Get(string id)
    {
        return Ok(_productReadRepository.GetById(id, false));
    }


    [HttpPost]
    public async Task<IActionResult> Post([FromBody] VM_Create_Product createProduct)
    {
        await _productWriteRepository.AddAsync(new()
        {
            Name = createProduct.Name,
            Price = createProduct.Price,
            Stock = createProduct.Stock
        });
        await _productWriteRepository.SaveChangesAsync();
        return StatusCode((int)HttpStatusCode.Created);
    }

    [HttpPut]
    public async Task<IActionResult> Put(VM_Update_Product model)
    {
        if (ModelState.IsValid)
        {
            Console.WriteLine();
        }

        Product product = await _productReadRepository.GetByIdAsync(model.Id, tracking: true);
        product.Stock = model.Stock;
        product.Price = model.Price;
        product.Name = model.Name;
        await _productWriteRepository.SaveChangesAsync();
        return Ok();
    }

    [HttpDelete]
    public IActionResult Delete(string id)
    {
        var x = _productWriteRepository.Remove(id);
        var y = _productWriteRepository.SaveChanges();
        return Ok(new
        {
            message = "Delete Success"
        });
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Upload(IFormFileCollection formFiles)
    {
        var datas = await _storageService.UploadAsync("sastolkkk", formFiles);
        await _productImageFileWriteRepository.AddRangeAsync(datas.Select(x => new ProductImageFile
        {
            FileName = x.fileName,
            Path = x.pathOrContainerName,
            Storage = _storageService.StorageName

        }).ToList());
        await _productImageFileWriteRepository.SaveChangesAsync();
        return Ok();
    }

}