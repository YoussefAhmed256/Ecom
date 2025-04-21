using Ecom.Core.Entites.Product;
using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositries
{
    public class CategoryRepository : GenericRepositry<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDBContext context) : base(context)
        {

        }
    }
}
