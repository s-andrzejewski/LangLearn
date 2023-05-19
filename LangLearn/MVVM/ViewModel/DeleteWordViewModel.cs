using LangLearn.Core;
using LangLearn.MVVM.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace LangLearn.MVVM.ViewModel
{
    internal class DeleteWordViewModel : INotifyPropertyChanged
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
                OnPropertyChanged(nameof(SelectedWord));
            }
        }


        public DeleteWordViewModel()
        {
            LoadWordsFromDatabase();
        }

        private void LoadWordsFromDatabase()
        {
            SqlLiteDataAccess dataAccess = new SqlLiteDataAccess();
            Words = new ObservableCollection<Word>(dataAccess.GetAllWords());
        }

        private RelayCommand _deleteWordCommand;
        public RelayCommand DeleteWordCommand
        {
            get
            {
                if (_deleteWordCommand == null)
                {
                    _deleteWordCommand = new RelayCommand(DeleteWord, CanDeleteWord);
                }
                return _deleteWordCommand;
            }
        }

        private bool CanDeleteWord(object parameter)
        {
            return SelectedWord != null;
        }

        private void DeleteWord(object parameter)
        {
            if (SelectedWord != null)
            {
                SqlLiteDataAccess dataAccess = new SqlLiteDataAccess();

                int selectedWordId = SelectedWord.ID;
                dataAccess.DeleteWord(selectedWordId);
                Words.Remove(SelectedWord);
                SelectedWord = null;

                MessageBox.Show("Słówko zostało usunięte z bazy danych.");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
