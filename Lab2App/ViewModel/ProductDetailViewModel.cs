using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab2App.Model;
using Lab2App.Services;
using Lab2App.Validation;

namespace Lab2App.ViewModel
{
    public class ProductDetailViewModel : ObservableRecipient
    {
        private readonly ProductService _productService;
        private Product _product;

        public Product Product
        {
            get => _product;
            private set
            {
                _product = value;
                OnPropertyChanged(nameof(Product));
            }
        }

        public ICommand UpdateCommand { get; }
        public ICommand DeleteCommand { get; }

        public ProductDetailViewModel(ProductSimplified productSimplified)
        {
            _productService = ProductService.Instance;
            _product = new Product();
            UpdateCommand = new RelayCommand(UpdateProduct);
            DeleteCommand = new RelayCommand(DeleteProduct);


            LoadProductDataAsync(productSimplified.Id);
        }

        private async void LoadProductDataAsync(Guid productId)
        {
            try
            {
                Product = await _productService.GetProductByIdAsync(productId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas ładowania danych produktu: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteProduct()
        {
            try
            {
                await _productService.DeleteProductAsync(Product.Id);
                MessageBox.Show("Produkt został usunięty.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas usuwania produktu: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void UpdateProduct()
        {
            try
            {
                new ProductValidator().Validate(Product);
                await _productService.UpdateProductAsync(Product);
                MessageBox.Show("Zmiany zostały zapisane.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ValidationException ex)
            {
                MessageBox.Show($"Popraw dane: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas zapisu zmian: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
