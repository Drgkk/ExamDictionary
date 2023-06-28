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

        public override string ToString()
        {
            return $"Word: {MainWord}\nTranslations: {string.Join(", ", Translations)}";
        }

        public void ExportToATxtFile()
        {
            using (FileStream fs = File.Create($"{MainWord}.txt"))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine($"Word: {MainWord}\nTranslations: {string.Join(", ", Translations)}");
                }
            }
        }

    }
}
