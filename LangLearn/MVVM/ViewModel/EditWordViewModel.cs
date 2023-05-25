using LangLearn.Core;
using LangLearn.MVVM.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace LangLearn.MVVM.ViewModel
{
    internal class EditWordViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Word> _words;
        public ObservableCollection<Word> Words
        {
            get { return _words; }
            set
            {
                _words = value;
                OnPropertyChanged();
            }
        }

        private Word _selectedWord;
        public Word SelectedWord
        {
            get { return _selectedWord; }
            set
            {
                _selectedWord = value;
                OnPropertyChanged();
            }
        }

        public ICommand SaveChangesCommand { get; }

        public EditWordViewModel()
        {
            LoadWordsFromDatabase();
            SaveChangesCommand = new RelayCommand(SaveChanges, CanSaveChanges);
        }

        private void LoadWordsFromDatabase()
        {
            SqlLiteDataAccess dataAccess = new SqlLiteDataAccess();
            Words = new ObservableCollection<Word>(dataAccess.GetAllWords());
        }

        private bool CanSaveChanges(object parameter)
        {
            return SelectedWord != null;
        }

        private void SaveChanges(object parameter)
        {
            if (SelectedWord != null)
            {
                SqlLiteDataAccess dataAccess = new SqlLiteDataAccess();
                dataAccess.UpdateWord(SelectedWord);
                MessageBox.Show("Zmiany zostały zapisane.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
