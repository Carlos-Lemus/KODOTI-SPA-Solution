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
    public interface IProductService
    {
        public Task<DataCollection<ProductDTO>> GetAll(int page, int take);
        public Task<ProductDTO> GetById(int id);
        public Task<ProductDTO> Create(ProductCreateDTO model);
        public Task Update(int id, ProductCreateDTO model);
        public Task Remove(int id);
    }

    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DataCollection<ProductDTO>> GetAll(int page, int take)
        {
            return _mapper.Map<DataCollection<ProductDTO>>(
                    await _context.Products.OrderByDescending(x => x.ProductId)
                    .AsQueryable()
                    .PageAsync(page, take)
                );
        }

        public async Task<ProductDTO> GetById(int id)
        {
            return _mapper.Map<ProductDTO>(
                await _context.Products.FindAsync(id)
                );
        }

        public async Task<ProductDTO> Create(ProductCreateDTO model)
        {
            var entry = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Price = model.Price
            };

            await _context.AddAsync(entry);
            await _context.SaveChangesAsync();

            return _mapper.Map<ProductDTO>(entry);
        }

        public async Task Update(int id, ProductCreateDTO model)
        {
            var entry = await _context.Products.FindAsync(id);
            entry.Name = model.Name;
            entry.Description = model.Description;
            entry.Price = model.Price;

            await _context.SaveChangesAsync();
        }

        public async Task Remove(int id)
        {
            _context.Remove(new Product()
            {
                ProductId = id
            });

            await _context.SaveChangesAsync();
        }

    }
}
