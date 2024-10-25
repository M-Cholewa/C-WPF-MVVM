using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lab2App.Model;
using Lab2App.Services;
using Lab2App.Validation;

namespace Lab2App.ViewModel
{
    public class ProductAddViewModel : ObservableRecipient
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

        public ICommand AddCommand { get; }

        public ProductAddViewModel()
        {
            _productService = ProductService.Instance;
            _product = new Product(); // Inicjalizacja nowego produktu
            AddCommand = new RelayCommand(AddProduct);
        }

        private async void AddProduct()
        {
            try
            {
                new ProductValidator().Validate(Product);
                await _productService.AddProductAsync(Product);
                MessageBox.Show("Produkt został dodany.", "Informacja", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (ValidationException ex)
            {
                MessageBox.Show($"Popraw dane: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas dodawania produktu: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
