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

namespace LopushokShakirov.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProductListPage.xaml
    /// </summary>
    public partial class ProductListPage : Page
    {
        public List<Pagin> Pages = new List<Pagin>();
        public ProductListPage()
        {
            InitializeComponent();
            Pages.Add(new Pagin { Title = "1"});
            Pages.Add(new Pagin { Title = "2" });
            Pages.Add(new Pagin { Title = "3" });
            lvPagination.ItemsSource = Pages;
        }
    }
    public class Pagin
    {
        public string Title { get; set; }
    }

}
