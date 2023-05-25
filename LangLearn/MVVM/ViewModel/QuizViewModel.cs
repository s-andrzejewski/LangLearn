using LangLearn.Core;
using LangLearn.MVVM.Model;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;

namespace LangLearn.MVVM.ViewModel
{
    internal class QuizViewModel : INotifyPropertyChanged
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

        private string _userAnswer;
        public string UserAnswer
        {
            get { return _userAnswer; }
            set
            {
                _userAnswer = value;
                OnPropertyChanged();
            }
        }

        private Word _currentQuestion;
        public Word CurrentQuestion
        {
            get { return _currentQuestion; }
            set
            {
                _currentQuestion = value;
                OnPropertyChanged();
            }
        }

        private string _sentenceExample;
        public string SentenceExample
        {
            get { return _sentenceExample; }
            set
            {
                _sentenceExample = value;
                OnPropertyChanged();
            }
        }

        private string _originalWord;
        public string OriginalWord
        {
            get { return _originalWord; }
            set
            {
                _originalWord = value;
                OnPropertyChanged();
            }
        }

        private string _translatedWord;
        public string TranslatedWord
        {
            get { return _translatedWord; }
            set
            {
                _translatedWord = value;
                OnPropertyChanged();
            }
        }

        public QuizViewModel()
        {
            LoadWordsFromDatabase();
            InitializeQuiz();
        }

        private void LoadWordsFromDatabase()
        {
            SqlLiteDataAccess dataAccess = new SqlLiteDataAccess();
            Words = new ObservableCollection<Word>(dataAccess.GetAllWords());
        }

        private void InitializeQuiz()
        {
            // Przygotuj pytania
            var randomWords = Words.OrderBy(w => Guid.NewGuid()).Take(10);
            _quizQuestions = new List<Word>(randomWords);

            // Inicjalizuj licznik i wyświetl pierwsze pytanie
            _currentQuestionIndex = 0;
            DisplayQuestion();
        }

        private void ResetQuiz()
        {
            _currentQuestionIndex = 0;
            _quizScore = 0;
            LoadWordsFromDatabase();
            InitializeQuiz();
        }


        private List<Word> _quizQuestions;
        private int _currentQuestionIndex;
        private int _quizScore;

        private void DisplayQuestion()
        {
            if (_currentQuestionIndex < _quizQuestions.Count)
            {
                // Wyświetl pytanie
                CurrentQuestion = _quizQuestions[_currentQuestionIndex];
            }
            else
            {
                // Quiz zakończony
                MessageBox.Show($"Koniec quizu! Twój wynik: {_quizScore}/10");

                MessageBoxResult result = MessageBox.Show("Rozpoczynamy od nowa", "Quiz", MessageBoxButton.OK);

                // Jeśli użytkownik kliknął "OK", zresetuj quiz i rozpocznij od nowa
                if (result == MessageBoxResult.OK)
                {
                    ResetQuiz();
                }
            }
        }

        private RelayCommand _checkAnswerCommand;
        public RelayCommand CheckAnswerCommand
        {
            get
            {
                if (_checkAnswerCommand == null)
                {
                    _checkAnswerCommand = new RelayCommand(param => CheckAnswer());
                }
                return _checkAnswerCommand;
            }
        }

        private void CheckAnswer()
        {
            // Pobierz aktualne pytanie
            Word currentQuestion = _quizQuestions[_currentQuestionIndex];

            // Sprawdź odpowiedź
            if (currentQuestion.TranslatedWord.Equals(UserAnswer, StringComparison.OrdinalIgnoreCase))
            {
                // Odpowiedź poprawna
                _quizScore++;
                MessageBox.Show("Poprawna odpowiedź!");
            }
            else
            {
                // Odpowiedź niepoprawna
                MessageBox.Show("Niepoprawna odpowiedź!");
            }

            // Przejdź do kolejnego pytania
            _currentQuestionIndex++;

            // Wyświetl kolejne pytanie
            DisplayQuestion();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
