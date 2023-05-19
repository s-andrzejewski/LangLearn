using System.Collections.Generic;
using System.Data.SQLite;

namespace LangLearn.MVVM.Model
{
    public class SqlLiteDataAccess
    {
        private const string DatabasePath = "MVVM/Model/LangLearnDB.db";

        public List<Word> GetAllWords()
        {
            using (var connection = new SQLiteConnection($"Data Source={DatabasePath};Version=3;"))
            {
                connection.Open();

                var command = new SQLiteCommand("SELECT * FROM Words", connection);
                var reader = command.ExecuteReader();

                var words = new List<Word>();

                while (reader.Read())
                {
                    var word = new Word
                    {
                        ID = reader.GetInt32(0),
                        OriginalWord = reader.GetString(1),
                        TranslatedWord = reader.GetString(2),
                        SentenceExample = reader.GetString(3)
                    };

                    words.Add(word);
                }

                return words;
            }
        }

        public void AddWord(Word word)
        {
            using (var connection = new SQLiteConnection($"Data Source={DatabasePath};Version=3;"))
            {
                connection.Open();

                var command = new SQLiteCommand("INSERT INTO Words (word_original, word_translate, sentence_example) VALUES (@original, @translate, @example)", connection);
                command.Parameters.AddWithValue("@original", word.OriginalWord);
                command.Parameters.AddWithValue("@translate", word.TranslatedWord);
                command.Parameters.AddWithValue("@example", word.SentenceExample);

                command.ExecuteNonQuery();
            }
        }

        public void UpdateWord(Word word)
        {
            using (var connection = new SQLiteConnection($"Data Source={DatabasePath};Version=3;"))
            {
                connection.Open();

                var command = new SQLiteCommand("UPDATE Words SET word_original = @original, word_translate = @translate, sentence_example = @example WHERE ID = @id", connection);
                command.Parameters.AddWithValue("@original", word.OriginalWord);
                command.Parameters.AddWithValue("@translate", word.TranslatedWord);
                command.Parameters.AddWithValue("@example", word.SentenceExample);
                command.Parameters.AddWithValue("@id", word.ID);

                command.ExecuteNonQuery();
            }
        }

        public void DeleteWord(int wordID)
        {
            using (var connection = new SQLiteConnection($"Data Source={DatabasePath};Version=3;"))
            {
                connection.Open();

                var command = new SQLiteCommand("DELETE FROM Words WHERE ID = @id", connection);
                command.Parameters.AddWithValue("@id", wordID);

                command.ExecuteNonQuery();
            }
        }
    }
}
