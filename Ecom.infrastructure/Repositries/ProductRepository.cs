using AutoMapper;
using Ecom.Core.DTO;
using Ecom.Core.Entites.Product;
using Ecom.Core.Interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositries
{
    public class ProductRepository:GenericRepositry<Product>, IProductRepository
    {
        private readonly AppDBContext context;
        private readonly IMapper mapper;
        private readonly IImageMnagementSrvice imageMnagementSrvice;
        public ProductRepository(AppDBContext context, IMapper mapper, IImageMnagementSrvice imageMnagementSrvice) : base(context)
        {
            this.context = context;
            this.mapper = mapper;
            this.imageMnagementSrvice = imageMnagementSrvice;
        }

        public async Task<bool> AddAsync(AddProductDTO productDTO)
        {
            if (productDTO is null)
                return false;
            var product = mapper.Map<Product>(productDTO);
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();

            var ImagePath = await imageMnagementSrvice.AddImageAsync(productDTO.Photo, productDTO.Name);
            var photo = ImagePath.Select(Path => new Photo
            {
                ImageName = Path,
                ProductId = product.Id
            }).ToList();
            await context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateAsync(UpdateProductDTO updateProductDTO)
        {
            if (updateProductDTO is null)
                return false;
            var FindProduct = await context.Products.Include(m=>m.Category).Include(m=>m.Photos).FirstOrDefaultAsync(m=>m.Id==updateProductDTO.Id);
            if (FindProduct is null)
                return false;
            mapper.Map(updateProductDTO, FindProduct);
            var FindPhoto = await context.Photos.Where(m => m.ProductId == updateProductDTO.Id).ToListAsync();
            foreach (var item in FindPhoto)
                imageMnagementSrvice.DeleteImageAsync(item.ImageName);
            context.RemoveRange(FindPhoto);
            var ImagePath =await imageMnagementSrvice.AddImageAsync(updateProductDTO.Photo, updateProductDTO.Name);
            var photo = ImagePath.Select(path => new Photo
            {
                ImageName = path,
                ProductId=updateProductDTO.Id
            });
            context.Photos.AddRangeAsync(photo);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
