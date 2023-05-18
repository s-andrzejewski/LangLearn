using LangLearn.MVVM.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LangLearn.MVVM.ViewModel
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        public HomeViewModel HomeVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            { 
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            CurrentView = HomeVM;
        }

        private RadioButton _selectedOption;

        public RadioButton SelectedOption
        {
            get { return _selectedOption; }
            set
            {
                _selectedOption = value;
                OnPropertyChanged("SelectedOption");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void MenuOption_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;
            if (radioButton != null)
            {
                // Ustawia odpowiedni widok w zależności od zaznaczonej opcji w menu:
                Debug.WriteLine("name of radio: " + radioButton.Name);
                switch (radioButton.Name)
                {
                    case "QuizOption":
                        //QuizVM = new QuizViewModel();
                        //CurrentView = QuizVM;
                        break;
                    //case "AddWordOption":
                     //   CurrentView = AddWordVM;
                     //   break;
                    //case "EditWordOption":
                      //  CurrentView = EditWordVM;
                      //  break;
                    //case "DeleteWordOption":
                      //  CurrentView = DeleteWordVM;
                     //   break;
                    default:
                        break;
                }
            }
        }
    }
}
