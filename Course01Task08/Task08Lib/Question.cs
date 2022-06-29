using System;

namespace Task08Lib {
    /// <summary>
    /// Класс вопроса.
    /// </summary>
    [Serializable]
    public class Question {

        #region Properties
        /// <summary>
        /// Текст вопроса.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Флаг ответа.
        /// </summary>
        public bool Answer { get; set; }
        #endregion

        #region Initializers and deinitalizers 
        public Question() { }

        public Question(string text, bool answer)
        {
            Text = text;
            Answer = answer;
        }
        #endregion
    }
}