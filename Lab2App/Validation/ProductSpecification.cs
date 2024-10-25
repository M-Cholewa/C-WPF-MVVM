using System;
using System.Text.RegularExpressions;
using Lab2App.Model;

namespace Lab2App.Validation
{
   
    public class ProductNameSpecification : ISpecification<Product>
    {
        public bool IsSatisfiedBy(Product product)
        {
            
            return !string.IsNullOrWhiteSpace(product.Name)
                   && product.Name.Length >= 3 && product.Name.Length <= 20
                   && Regex.IsMatch(product.Name, @"^[a-zA-Z0-9]+$");
        }

        public string ErrorMessage => "Nazwa produktu musi zawierać od 3 do 20 znaków, tylko litery i cyfry";
    }
    
    public class ProductPriceSpecification : ISpecification<Product>
    {
        public bool IsSatisfiedBy(Product product)
        {
            return product.Price >= 0.01;
        }

        public string ErrorMessage => "Cena produktu musi być większa od 0.01";
    }

    public class ProductStockSpecification : ISpecification<Product>
    {
        public bool IsSatisfiedBy(Product product)
        {
            return product.Stock >= 0;
        }

        public string ErrorMessage => "Ilość produktu musi być większa lub równa 0";
    }

    
    public class ProductDescriptionSpecification : ISpecification<Product>
    {
        public bool IsSatisfiedBy(Product product)
        {
            return product.Description == null || product.Description.Length <= 100; 
        }

        public string ErrorMessage => "Opis produktu musi zawierać maksymalnie 100 znaków";
    }
}
