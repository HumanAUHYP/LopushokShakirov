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
using LopushokShakirov.DB;

namespace LopushokShakirov.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page
    {
        public List<Product> Products { get; set; }
        public List<Product> AllProducts { get; set; }
        public List<Pagin> Pages = new List<Pagin>();
        public int pageIndex = 1;
        public int maxPageIndex;
        MainWindow globalMainWindow;

        public ProductListPage(MainWindow mainWindow)
        {
            InitializeComponent();
            globalMainWindow = mainWindow;
            mainWindow.tbTitle.Text = "Продукты";

            Products = DataAccess.GetProducts();
            AllProducts = Products;




            Paginator();
            DisplayProductInPage();
            this.DataContext = this;
            
        }

        
        private void btnChange_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProductPage(globalMainWindow));
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLastPage_Click(object sender, RoutedEventArgs e)
        {
            if (pageIndex > 1)
            {
                pageIndex--;
                lvPagination.SelectedIndex--;
            }
            DisplayProductInPage();
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (pageIndex < maxPageIndex)
            {
                pageIndex++;
                lvPagination.SelectedIndex++;
            }
            DisplayProductInPage();
        }

        private void lvPagination_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            pageIndex = Convert.ToInt32((lvPagination.SelectedItem as Pagin).Title);
            DisplayProductInPage();
        }
        public void DisplayProductInPage()
        {
            var productsInPage = new List<Product>();
            for (int i = (pageIndex - 1) * 20; i < pageIndex * 20; i++)
            {
                try
                {
                    productsInPage.Add(Products[i]);
                }
                catch (Exception)
                {
                    break;
                }

            }
            lvProducts.ItemsSource = productsInPage;
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void AllFilters()
        {
            Products = AllProducts;

            if (tbxSearch.Text != "")
            {
                Products = Products.Where(a => a.Name.Contains($"{tbxSearch.Text}") || a.Description.Contains($"{tbxSearch.Text}")).ToList();
            }
            Paginator();
            DisplayProductInPage();
        }

        private void tbxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            AllFilters();
        }
        private void Paginator()
        {
            Pages.Clear();
            maxPageIndex = Products.Count / 20;
            for (int i = 1; i <= maxPageIndex; i++)
            {
                Pages.Add(new Pagin { Title = Convert.ToString(i) });
            }
            lvPagination.ItemsSource = Pages;
            lvPagination.Items.Refresh();
        }
    }
    public class Pagin
    {
        public string Title { get; set; }
    }

}
