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
    [Authorize(Roles = RolesHelper.Admin + ", " + RolesHelper.Seller)]
    [ApiController]
    [Route("orders")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService _orderService)
        {
            this._orderService = _orderService;
        }

        [HttpGet]
        public async Task<ActionResult<DataCollection<OrderDTO>>> GetAll(int page, int take)
        {
            return await _orderService.GetAll(page, take);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDTO>> GetById(int id)
        {
            return await _orderService.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create(OrderCreateDTO model)
        {
            var result = await _orderService.Create(model);
            return CreatedAtAction(
                    "GetById",
                    new { id = result.OrderId },
                    result
                );
        }

    }
}
