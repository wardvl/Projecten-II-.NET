﻿using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls.Expressions;
using DidactischeLeermiddelen.Models.Domain.Products;

namespace DidactischeLeermiddelen.Models.Domain.Interfaces
{
    public interface IProductGroupRepository
    {
        IQueryable<ProductGroup> FindAll();
        ProductGroup FindById(int id);
        void Add(ProductGroup productGroup);
        void Delete(ProductGroup productGroup);
        void SaveChanges();
        List<ProductGroup> Search(string query);


    }
}