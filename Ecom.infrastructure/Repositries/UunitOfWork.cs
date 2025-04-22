using AutoMapper;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositries
{
    public class UunitOfWork : IUnitOfWork
    {
        private readonly AppDBContext _context;
        private readonly IMapper _mapper;
        private readonly IImageMnagementSrvice _imageMnagementSrvice;
        public IProductRepository ProductRepository {get;}

        public ICategoryRepository CategoryRepository { get; }

        public IPhotoRepository PhotoRepository { get; }

        public UunitOfWork(AppDBContext context, IImageMnagementSrvice imageMnagementSrvice, IMapper mapper)
        {
            _context = context;
            _imageMnagementSrvice = imageMnagementSrvice;
            _mapper = mapper;

            ProductRepository = new ProductRepository(_context, _mapper, _imageMnagementSrvice);
            CategoryRepository = new CategoryRepository(_context);
            PhotoRepository = new PhotoRepository(_context);
        }
    }
}
