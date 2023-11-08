using Ecomerce.Application.Abstractions.Storage;
using Ecomerce.Application.Repositories;
using Ecomerce.Application.Repositories.Products;
using Ecomerce.Application.RequestParameters;
using Ecomerce.Application.ViewModels.Products;
using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Ecommerce.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    #region interface
    private readonly IProductWriteRepository _productWriteRepository;
    private readonly IProductReadRepository _productReadRepository;
    private readonly IWebHostEnvironment _hostEnvironment;
    private readonly IFileWriteRepository _fileWriteRepository;
    private readonly IFileReadRepsoitory _fileReadRepsoitory;
    private readonly IProductImageFileReadRepsoitory _productImageFileReadRepsoitory;
    private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;
    private readonly IInvoiceFileReadRepsoitory _invoiceFileReadRepsoitory;
    private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
    private readonly IStorageService _storageService;

    public ProductController(
        IProductWriteRepository productWriteRepository,
        IProductReadRepository productReadRepository,
        IWebHostEnvironment hostEnvironment,

        IFileWriteRepository fileWriteRepository,
        IFileReadRepsoitory fileReadRepsoitory,
        IProductImageFileReadRepsoitory productImageFileReadRepsoitory,
        IProductImageFileWriteRepository productImageFileWriteRepository,
        IInvoiceFileReadRepsoitory invoiceFileReadRepsoitory,
        IInvoiceFileWriteRepository invoiceFileWriteRepository,
        IStorageService storageService)
    {
        _productWriteRepository = productWriteRepository;
        _productReadRepository = productReadRepository;
        _hostEnvironment = hostEnvironment;
        _fileWriteRepository = fileWriteRepository;
        _fileReadRepsoitory = fileReadRepsoitory;
        _productImageFileReadRepsoitory = productImageFileReadRepsoitory;
        _productImageFileWriteRepository = productImageFileWriteRepository;
        _invoiceFileReadRepsoitory = invoiceFileReadRepsoitory;
        _invoiceFileWriteRepository = invoiceFileWriteRepository;
        _storageService = storageService;
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
    public async Task<IActionResult> Upload()
    {

        var datas = await _storageService.UploadAsync("resorce/files", Request.Form.Files);
        await _invoiceFileWriteRepository.AddRangeAsync(datas.Select(x => new InvoiceFile()
        {
            FileName = x.fileName,
            Path = x.pathOrContainerName,
            Storage = _storageService.StorageName
        }).ToList());
        await _invoiceFileWriteRepository.SaveChangesAsync();
        return Ok();
    }
}