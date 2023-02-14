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
        public List<ProductType> ProductTypes { get; set; }
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
            ProductTypes = DataAccess.GetProductTypes();
            ProductTypes.Insert(0, new ProductType { Name = "Все типы" });
            cbProductType.ItemsSource = ProductTypes;
            AllProducts = Products;
            cbProductType.SelectedIndex = 0;
            cbSort.SelectedIndex = 0;

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
        }

        private void btnNextPage_Click(object sender, RoutedEventArgs e)
        {
            if (pageIndex < maxPageIndex)
            {
                pageIndex++;
                lvPagination.SelectedIndex++;
            }
        }

        private void lvPagination_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lvPagination.SelectedItem != null)
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
            AllFilters();
        }

        private void cbProductType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            AllFilters();
        }
        private void AllFilters()
        {
            Products = AllProducts;

            if (tbxSearch.Text != "")
            {
                Products = Products.Where(a => a.Name.Contains($"{tbxSearch.Text}") || a.Description.Contains($"{tbxSearch.Text}")).ToList();
            }

            var selectedSort = cbSort.SelectedItem as TextBlock;
            if (selectedSort == tbOrderName)
                Products = Products.OrderBy(a => a.Name).ToList();
            else if (selectedSort == tbOrderDescName)
                Products = Products.OrderByDescending(a => a.Name).ToList();
            else if (selectedSort == tbOrderNum)
                Products = Products.OrderBy(a => a.Workshop.Name).ToList();
            else if (selectedSort == tbOrderDescNum)
                Products = Products.OrderByDescending(a => a.Workshop.Name).ToList();
            else if (selectedSort == tbOrderCost)
                Products = Products.OrderBy(a => a.MinPrice).ToList();
            else if (selectedSort == tbOrderDescCost)
                Products = Products.OrderByDescending(a => a.MinPrice).ToList();

            var selectedProductType = cbProductType.SelectedItem as ProductType;
            if (selectedProductType.Name != "Все типы")
                Products = Products.FindAll(a => a.ProductType == selectedProductType);

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
            maxPageIndex = (int)Math.Ceiling((double)Products.Count / 20);
            for (int i = 1; i <= maxPageIndex; i++)
            {
                Pages.Add(new Pagin { Title = Convert.ToString(i) });
            }
            lvPagination.ItemsSource = Pages;
            lvPagination.Items.Refresh();
            lvPagination.SelectedIndex = 0;
        }
    }
    public class Pagin
    {
        public string Title { get; set; }
    }

}
