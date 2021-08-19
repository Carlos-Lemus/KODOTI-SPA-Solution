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
    [Route("clients")]
    public class ClientController: ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService _clientService)
        {
            this._clientService = _clientService;
        }

        [HttpGet]
        public async Task<ActionResult<DataCollection<ClientDTO>>> GetAll(int page, int take)
        {
            return await _clientService.GetAll(page, take);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDTO>> GetById(int id)
        {
            return await _clientService.GetById(id);
        }

        [HttpPost]
        public async Task<ActionResult> Create(ClientCreateDTO model)
        {
            var result = await _clientService.Create(model);
            return CreatedAtAction(
                    "GetById",
                    new { id = result.ClientId },
                    result
                );
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Create(int id, ClientCreateDTO model)
        {
            await _clientService.Update(id, model);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remove(int id)
        {
            await _clientService.Remove(id);
            return NoContent();
        }

    }
}
