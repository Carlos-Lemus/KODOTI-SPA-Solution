using Core_Api.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Model.DTOs;
using Service;
using Service.commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core_Api.Controllers
{
    [Authorize(Roles = RolesHelper.Admin)]
    [ApiController]
    [Route("products")]
    public class ProductController : ControllerBase
    {

        private readonly IProductService _productService;

        public ProductController(IProductService _productService)
        {
            this._productService = _productService;
        }

        [HttpGet]
        public async Task<ActionResult<DataCollection<ProductDTO>>> GetAll(int page, int take)
        {
            return await _productService.GetAll(page, take);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetById(int id)
        {
            return await _productService.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ProductCreateDTO model)
        {
            var result = await _productService.Create(model);
            return CreatedAtAction(
                    "GetById",
                    new { id = result.ProductId },
                    result
                );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Create(int id, ProductCreateDTO model)
        {
            await _productService.Update(id, model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await _productService.Remove(id);
            return NoContent();
        }

    }
}
