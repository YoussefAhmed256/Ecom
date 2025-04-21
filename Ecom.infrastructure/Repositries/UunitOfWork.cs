using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositries
{
    public class UunitOfWork : IUunitOfWork
    {
        private readonly AppDBContext _context;
        public IProductRepository ProductRepository {get;}

        public ICategoryRepository CategoryRepository { get; }

        public IPhotoRepository PhotoRepository { get; }

        public UunitOfWork(AppDBContext context)
        {
            _context = context;
            ProductRepository = new ProductRepository(_context);
            CategoryRepository = new CategoryRepository(_context);
            PhotoRepository = new PhotoRepository(_context);
        }
    }
}
