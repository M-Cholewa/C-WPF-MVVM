using Lab2App.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Lab2App.Views
{
    /// <summary>
    /// Interaction logic for ProductListView.xaml
    /// </summary>
    public partial class ProductListView : UserControl
    {
        public ProductListView()
        {
            InitializeComponent();
        }



        private void OnProductDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is ProductListViewModel viewModel && viewModel.SelectedProduct != null)
            {
                viewModel.OpenProductDetailCommand.Execute(viewModel.SelectedProduct);
            }
        }
    }
}
