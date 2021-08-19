using AutoMapper;
using Microsoft.EntityFrameworkCore;
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
    public interface IOrderService
    {
        public Task<DataCollection<OrderDTO>> GetAll(int page, int take);
        public Task<OrderDTO> GetById(int id);
        public Task<OrderDTO> Create(OrderCreateDTO model);
        /*public Task Update(int id, OrderCreateDTO model);
        public Task Remove(int id);*/
    }

    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private decimal IvaRate = 0.13m;

        public OrderService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DataCollection<OrderDTO>> GetAll(int page, int take)
        {
            return _mapper.Map<DataCollection<OrderDTO>>(
                    await _context.Orders.OrderByDescending(x => x.OrderId)
                    .Include(x => x.Cliente)
                    .Include(x => x.Items)
                    .ThenInclude(x => x.Product)
                    .AsQueryable()
                    .PageAsync(page, take)
                );
        }

        public async Task<OrderDTO> GetById(int id)
        {
            return _mapper.Map<OrderDTO>(
                await _context.Orders
                .Include(x => x.Cliente)
                .Include(x => x.Items)
                .ThenInclude(x => x.Product)
                .SingleAsync(x => x.OrderId == id)
                );
        }

        public async Task<OrderDTO> Create(OrderCreateDTO model)
        {
            var entry = _mapper.Map<Order>(model);

            PrepareDetail(entry.Items);
            HeaderDetail(entry);

            await _context.AddAsync(entry);
            await _context.SaveChangesAsync();

            return _mapper.Map<OrderDTO>(
                await GetById(entry.OrderId)
                );
        }

        private void PrepareDetail(IEnumerable<OrderDetail> items)
        {
            foreach(var item in items)
            {
                item.Total = item.UnitPrice * item.Quantity;
                item.Iva = item.Total * IvaRate;
                item.SubTotal = item.Total - item.Iva;
            }
        }

        private void HeaderDetail(Order order)
        {
            order.Total = order.Items.Sum(x => x.Total);
            order.Iva = order.Items.Sum(x => x.Iva);
            order.SubTotal = order.Items.Sum(x => x.SubTotal);
        }

        /*public async Task Update(int id, OrdercreateDTO model)
        {
            var entry = await _context.Orders.FindAsync(id);

            await _context.SaveChangesAsync();
        }*/

        /*public async Task Remove(int id)
        {
            _context.Remove(new OrderDTO()
            {
                OrderId = id
            });

            await _context.SaveChangesAsync();
        }*/

    }
}
