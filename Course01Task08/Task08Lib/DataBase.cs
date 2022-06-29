#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Xml.Serialization;

namespace Task08Lib {
    /// <summary>
    /// База данных вопросов.
    /// </summary>
    public class DataBase {
        
        #region Variables and constants
        /// <summary>
        /// Строка подключения к базе данных.
        /// </summary>
        private readonly string fileName;
        /// <summary>
        /// Внутренняя коллекция вопросов.
        /// </summary>
        private List<Question> questions;
        #endregion

        #region Properties
        /// <summary>
        /// Количество вопросов в коллекции. 
        /// </summary>
        public int Count => questions.Count;
        /// <summary>
        /// Доступ к вопросу по индексу.
        /// </summary>
        /// <param name="index"></param>
        public Question this[int index] => questions[index];
        /// <summary>
        /// Паттерн проверки текста вопроса
        /// </summary>
        public const string QuestionTextPattern = @"\W{1,150}";

        #endregion

        #region Initializers and deinitalizers 
        public DataBase(string fileName)
        {
            this.fileName = fileName;
            questions = new List<Question>(100);
            Load();
        }
        #endregion

        #region Methods
        /// <summary>
        /// Добавляет вопрос в базу данных.
        /// </summary>
        /// <param name="text">Текст вопроса.</param>
        /// <param name="answer">Ответ на вопрос.</param>
        public bool Add(string text, bool answer)
        {
            questions.Add(new Question(text, answer));
            return Save();
        }

        /// <summary>
        /// Удаляет вопрос из коллекции по его индексу.
        /// </summary>
        /// <param name="index">Индекс вопроса.</param>
        public bool Remove(int index)
        {
            if (index >= 0 && index < questions.Count)
            {
                questions.RemoveAt(index);
                Save();
                return true;
            }
            return false;
        }
        
        /// <summary>
        /// Загружает (десериализует) коллекцию вопросов из xml-файла.
        /// </summary>
        public void Load()
        {
            var serializer = new XmlSerializer(typeof(List<Question>));
            using (var fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                if(!File.Exists(fileName)) return;
                try
                {
                    if (serializer.Deserialize(fs) is List<Question> existingQuestions)
                        questions = existingQuestions;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
        
        public bool Save()
        {
            var serializer = new XmlSerializer(typeof(List<Question>));
            using (var fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(fs, questions);
                return true;
            }
        }
        /// <summary>
        /// Возвращает все вопросы игры
        /// </summary>
        /// <returns></returns>
        public List<Question> GetQuestions()
        {
            return questions;
        }
        #endregion
    }
}