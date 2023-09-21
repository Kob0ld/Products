﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static ProductAPI.DTOs;

namespace ProductAPI.Controllers
{
    [Route("product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static readonly List<ProductDTO> products = new()
        {
            new ProductDTO(Guid.NewGuid(),"Termék1",2500,DateTimeOffset.UtcNow, DateTimeOffset.UtcNow),
            new ProductDTO(Guid.NewGuid(),"Termék2",5500,DateTimeOffset.UtcNow, DateTimeOffset.UtcNow),
            new ProductDTO(Guid.NewGuid(),"Termék3",12500,DateTimeOffset.UtcNow, DateTimeOffset.UtcNow),
        };

        [HttpGet]
        public IEnumerable<ProductDTO> GetAll()
        { return products; }

        [HttpGet("{id}")]
        public ActionResult<ProductDTO> GetById(Guid id)
        {
            var product = products.Where(x => x.Id == id).FirstOrDefault();
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public ActionResult<ProductDTO> PostProduct(CreateProductDTO createProduct)
        {
            var product = new ProductDTO
                (
                    Guid.NewGuid(),
                    createProduct.ProductName,
                    createProduct.ProductPrice,
                    DateTimeOffset.UtcNow,
                    DateTimeOffset.UtcNow
                );

            products.Add(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
        }

        [HttpPut]
        public ProductDTO PullProduct(Guid id, UpdateProductDTO updateProduct)
        {
            var existingProduct = products.Where(x => x.Id == id).FirstOrDefault();
            var product = existingProduct with
            {
                ProductName = updateProduct.ProductName,
                ProductPrice = updateProduct.ProductPrice,
                ModifiedTime = DateTimeOffset.UtcNow,
            };

            var index = products.FindIndex(x => x.Id == id);
            products[index] = product;
            return products[index];
        }

        [HttpDelete("{id})")]
        public ActionResult<string> DeleteProduct(Guid id) 
        {
            var index = products.FindIndex(x => x.Id == id);
            products.RemoveAt(index);
            if (index == 0)
            {
                return NotFound();
            }
            return StatusCode(205, "Törölt");
        }
    }
}
