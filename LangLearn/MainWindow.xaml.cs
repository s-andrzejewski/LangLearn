using LangLearn.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace LangLearn
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel = new MainViewModel();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = _viewModel;
        }

        private void MenuOption_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                string headerText = radioButton.Content as string;
                if (!string.IsNullOrEmpty(headerText))
                {
                    Header.Text = headerText;
                }
            }
        }
    }
}
