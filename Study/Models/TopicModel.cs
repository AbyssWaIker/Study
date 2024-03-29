﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Study
{
    public class TopicModel
    {
        /// <summary>
        /// представляет уникальный идентификатор темы
        /// </summary>
        public int id { get; set; }

        public int Courseid { get; set; }

        public int TopicOrderNumber { get; set; }

        /// <summary>
        /// представляет название темы
        /// </summary>
        public string topicName { get; set; }

        /// <summary>
        /// представляет список разделов темы
        /// </summary>
        public List<TopicPortionModel> TopicPortions { get; set; } = new List<TopicPortionModel>();

        /// <summary>
        /// представляет список вопросов к теме
        /// </summary>
        public List<QuestionModel> Questions { get; set; } = new List<QuestionModel>();

        public String OrderAndName
        {
            get
            {
                return $"{TopicOrderNumber} {topicName}";
            }
        }

        public TopicModel()
        {

        }

        public TopicModel(int Id, string name, List<TopicPortionModel> Portions, List<QuestionModel> QuestionsList)
        {
            id = Id;
            topicName = name;
            TopicPortions = Portions;
            Questions = QuestionsList;
        }
    }
}
