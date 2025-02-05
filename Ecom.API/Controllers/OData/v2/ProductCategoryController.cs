﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ECom.Contracts.Data.Repositories;
using System.Linq;
using ECom.Core.Entities;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using ECom.API.Controllers.OData.Login;
using System.Threading.Tasks;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Formatter;

namespace ECom.API.Controllers.OData.v2
{
    public class ProductCategoryController : BaseODataController
    {
        private readonly IUnitOfWork _uow;
        public ProductCategoryController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        /// <summary>
        /// Gets all Product
        /// Use the GET http verb
        /// Request for v2/ProductCategory
        /// </summary>
        /// <returns>List of ProductCategory</returns>
        /// <response code="200">Returns IEnumerable of ProductCategory </response>
        /// <response code="401">If the user is not authorize or JWT token expired</response>   
        [EnableQuery]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProductCategory>))]
        [ProducesResponseType(404)]
        [HttpGet("v2/ProductCategory")]
        public IActionResult Get()
        {
            var items = _uow.ProductCategory.GetAll().AsQueryable();
            return Ok(items);
        }
        /// <summary>
        /// Gets single Product
        /// Use the GET http verb
        /// Request for v2/ProductCategory(3)
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Single ProductCategory</returns>
        /// <response code="200">Returns a single Product that matches the Id </response>
        /// <response code="404">Returns a 404 NotFound if the product category does not exist </response>
        /// <response code="401">If the user is not authorize or JWT token expired</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(200, Type = typeof(ProductCategory))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("v2/ProductCategory({id})")]
        [HttpGet("v2/ProductCategory/{id}")]
        public IActionResult Get(int id)
        {
            var entity = _uow.ProductCategory.Get(id);
            if (entity == null)
            {
                return NotFound();
            }
            return Ok(entity);
        }
        /// <summary>
        /// Creates a ProductCategory.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /ProductCategory
        ///     {
        ///        "name": "ProductCategory 1 ",
        ///        
        ///     }
        ///
        /// </remarks>
        /// <param name="item"></param>
        /// <returns>A newly created ProductCategory</returns>
        /// <response code="201">Returns the newly created ProductCategory</response>
        /// <response code="400">If the item is null</response>            
        /// <response code="401">If the user is not authorize or JWT token expired</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("v2/ProductCategory")]
        public async Task<IActionResult> Create(ProductCategory item)
        {
            _uow.ProductCategory.Add(item);
            await _uow.Commit();
            return Ok(item);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [HttpPatch("v2/ProductCategory({id})")]
        [HttpPatch("v2/ProductCategory/{id}")]
        public IActionResult Patch(int id, Delta<ProductCategory> item)
        {
            if (item != null)
            {
                var original = _uow.ProductCategory.Get(id);
                if (original == null)
                {
                    return NotFound($"Not found ProductCategory with id = {id}");
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                item.Patch(original);
                _uow.Commit();
                return Updated(original);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        /// <summary>
        /// Update a specific ProductCategory.
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /ProductCategory/1
        ///     {
        ///        "id": 1,
        ///        "name": "ProductCategory Update",
        ///        
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        /// <response code="401">If the user is not authorize or JWT token expired</response>
        /// <response code="400">Returns a 404 NotFound if the product category does not exist </response>
        /// <response code="404">Returns a 400 BadRequest if the product category parameter is null or param id is not matched with id in the ProductCategory </response>
        /// <response code="204">Returns a 204 NoContent if the request is successfuly completed. </response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [HttpPut("v2/ProductCategory")]
        public async Task<IActionResult> Update(int id, ProductCategory item)
        {
            if (item == null || item.Id != id)
            {
                return BadRequest();
            }
            var productType = _uow.ProductCategory.Get(id);
            if (productType == null)
            {
                return NotFound();
            }
            productType.Name = item.Name;
            _uow.ProductCategory.Update(productType);
            await _uow.Commit();
            return NoContent();
        }
        /// <summary>
        /// Use the DELETE http verb
        /// Request for v2/productcategory(5) or v2/productcategory/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <summary>
        /// Deletes a specific ProductCategory.
        /// </summary>      
        /// <response code="401">If the user is not authorize or JWT token expired</response>
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        [HttpDelete("v2/ProductCategory({id})")]
        [HttpDelete("v2/ProductCategory/{id}")]
        public async Task<IActionResult> Delete([FromODataUri] int id)
        {
            var item = _uow.ProductCategory.Get(id);
            if (item == null)
            {
                return NotFound();
            }
            _uow.ProductCategory.Delete(item.Id);
            await _uow.Commit();
            return NoContent();
        }
    }
}