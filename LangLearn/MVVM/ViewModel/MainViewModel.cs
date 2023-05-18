using LangLearn.Core;
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
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand QuizViewCommand { get; set; }
        public RelayCommand AddViewCommand { get; set; }
        public RelayCommand EditViewCommand { get; set; }
        public RelayCommand DelViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public QuizViewModel QuizVM { get; set; }
        public AddWordViewModel AddVM { get; set; }
        public EditWordViewModel EditVM { get; set; }
        public DeleteWordViewModel DelVM { get; set; }

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
            QuizVM = new QuizViewModel();
            AddVM = new AddWordViewModel();
            EditVM = new EditWordViewModel();
            DelVM = new DeleteWordViewModel();

            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(obj =>
            {
                CurrentView = HomeVM;
            });

            QuizViewCommand = new RelayCommand(obj =>
            {
                CurrentView = QuizVM;
            });

            AddViewCommand = new RelayCommand(obj =>
            {
                CurrentView = AddVM;
            });

            EditViewCommand = new RelayCommand(obj =>
            {
                CurrentView = EditVM;
            });

            DelViewCommand = new RelayCommand(obj =>
            {
                CurrentView = DelVM;
            });
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
    }
}
