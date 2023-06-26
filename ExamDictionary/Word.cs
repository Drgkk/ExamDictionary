using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ExamDictionary
{
    [DataContract]
    [Serializable]
    internal class Word
    {
        [DataMember]
        public string MainWord { get; set; }
        [DataMember]
        public List<string> Translations;
        public Word()
        {
            Translations = new List<string>();
        }

        public Word(string n)
        {
            MainWord = n;
            Translations = new List<string>();
        }

    }
}
