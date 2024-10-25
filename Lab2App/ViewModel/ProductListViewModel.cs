using Lab2App.Model;
using Lab2App.Services;
using Lab2App.Views;
using System.Windows.Input;
using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Lab2App.ViewModel
{
    public class ProductListViewModel : ObservableRecipient
    {
        //public ObservableCollection<ProductSimplified> Products { get; set; }

        private List<ProductSimplified> _products;

        public List<ProductSimplified> Products
        {
            get => _products;
            private set
            {
                _products = value;
                OnPropertyChanged(nameof(Products));
            }
        }

        public ProductSimplified SelectedProduct { get; set; }
        private readonly ProductService _productService;

        // Komendy
        public ICommand AddProductCommand { get; }
        public ICommand OpenProductDetailCommand { get; }
        public ICommand RefreshCommand { get; }

        public ProductListViewModel()
        {
            _products = [];

            // Inicjalizacja komend
            AddProductCommand = new RelayCommand(OpenAddProductView);
            OpenProductDetailCommand = new RelayCommand<ProductSimplified?>(OpenProductDetailView);
            RefreshCommand = new RelayCommand(RefreshProducts);

            _productService = ProductService.Instance;
            SelectedProduct = new ProductSimplified();

            LoadProducts();
        }

        private void OpenAddProductView()
        {
            // Logika otwierająca ProductEditView do dodania nowego produktu
            var addProductView = new ProductAddView
            {
                DataContext = new ProductAddViewModel()
            };
            addProductView.ShowDialog();
        }

        private void OpenProductDetailView(ProductSimplified? selectedProduct)
        {

            if (selectedProduct == null)
            {
                MessageBox.Show("Nie wybrano produktu.", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Logika otwierająca ProductDetailView dla wybranego produktu
            var detailView = new ProductDetailView
            {
                DataContext = new ProductDetailViewModel(selectedProduct)
            };

            detailView.ShowDialog();
        }

        public void RefreshProducts()
        {
            LoadProducts();
        }

        public async void LoadProducts()
        {

            try
            {
                Products = await _productService.GetAllProductsAsync();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas ładowania danych produktów: {ex.Message}", "Błąd", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}
