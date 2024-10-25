using Lab2App.Model;
using System.Collections.Generic;
using System.Linq;

namespace Lab2App.Validation
{
    public class ProductValidator : ValidatorBase<Product>
    {
        public ProductValidator()
        {
            Specifications.Add(new ProductNameSpecification());
            Specifications.Add(new ProductPriceSpecification());
            Specifications.Add(new ProductStockSpecification());
        }
    }
}
