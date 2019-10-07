using System;
using System.AddIn.Pipeline;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RibbonMAFSpaceIssue
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAddins();
            LoadRibbonView();
            LoadContentView();
        }

        private void LoadContentView()
        {
            var addInContent = new AddinContentUserControl();// { Content="Addin Module"};
            var addinElement = ConvertControlToContractThenBackToFrameworkElement(addInContent);
            Grid.SetRow(addinElement, 1);
            Grid.SetColumn(addinElement, 2);
            mainGrid.Children.Add(addinElement);
        }

        private void LoadRibbonView()
        {
            var menuControl = new MenuUserControl();
            //pass through Addin pipeline
            var convertedRibbon = ConvertControlToContractThenBackToFrameworkElement(menuControl);

            Grid.SetColumn(convertedRibbon, 0);
            Grid.SetRow(convertedRibbon, 0);
            Grid.SetColumnSpan(convertedRibbon, 2);

            mainGrid.Children.Add(convertedRibbon);
        }

        private void LoadAddins()
        {
            var listAddins = new [] {"Marketing", "HRM", "Payroll", "Orders", "Etc" };

            addInsList.ItemsSource = listAddins;
        }

        /// <summary>
        /// Helper function to mimic Managed Addin Framework (MAF) Pipeline 
        /// </summary>
        /// <param name="elemnt"></param>
        /// <returns></returns>
        private FrameworkElement ConvertControlToContractThenBackToFrameworkElement(FrameworkElement elemnt)
        {
            var contract = FrameworkElementAdapters.ViewToContractAdapter(elemnt);
            var convertedElement = FrameworkElementAdapters.ContractToViewAdapter(contract);

            return convertedElement;
        }
    }
}
