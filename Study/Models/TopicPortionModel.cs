using System;
using System.Collections.Generic;
using System.Text;

namespace Study
{
    public class TopicPortionModel
    {
        /// <summary>
        /// представляет уникальный идентификатор раздела темы
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// представляет указатель на уникальный идентификатор темы, к которой принадлежит этот раздел
        /// </summary>
        public int TopicId { get; set; }

        /// <summary>
        /// представляет название раздела
        /// </summary>
        public string TopicPortionName { get; set; }

        /// <summary>
        /// представляет текст раздела
        /// </summary>
        public String TopicPortionText { get; set; }
    }
}
