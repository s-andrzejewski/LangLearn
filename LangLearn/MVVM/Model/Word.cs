using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LangLearn.MVVM.Model
{
    public class Word
    {
        public int ID { get; set; }
        public string OriginalWord { get; set; }
        public string TranslatedWord { get; set; }
        public string SentenceExample { get; set; }
    }
}

