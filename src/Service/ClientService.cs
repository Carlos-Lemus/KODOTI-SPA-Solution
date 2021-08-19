using AutoMapper;
using Model;
using Model.DTOs;
using Persistence.Database;
using Service.commons;
using Service.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{

    public interface IClientService
    {
        public Task<DataCollection<ClientDTO>> GetAll(int page, int take);
        public Task<ClientDTO> GetById(int id);
        public Task<ClientDTO> Create(ClientCreateDTO model);
        public Task Update(int id, ClientCreateDTO model);
        public Task Remove(int id);
    }

    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ClientService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DataCollection<ClientDTO>> GetAll(int page, int take = 20)
        {
            return _mapper.Map<DataCollection<ClientDTO>>(
                    await _context.Clients.OrderByDescending(x => x.ClientId)
                    .AsQueryable()
                    .PageAsync(page, take)
                );
        }

        public async Task<ClientDTO> GetById(int id)
        {
            return _mapper.Map<ClientDTO>(
                await _context.Clients.FindAsync(id)
                );
        }

        public async Task<ClientDTO> Create(ClientCreateDTO model)
        {
            var entry = new Client()
            {
                Name = model.Name
            };

            await _context.AddAsync(entry);
            await _context.SaveChangesAsync();

            return _mapper.Map<ClientDTO>(entry);
        }

        public async Task Update(int id, ClientCreateDTO model)
        {
            var entry = await _context.Clients.FindAsync(id);
            entry.Name = model.Name;

            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            _context.Remove(new Client()
            {
                ClientId = id
            });

            await _context.SaveChangesAsync();
        }

    }
}
