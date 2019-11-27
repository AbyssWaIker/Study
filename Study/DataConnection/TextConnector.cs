using System;
using System.Collections.Generic;
using System.Text;

namespace Study
{
    public class TextConnector : IDataConnection
    {
        public GradeModel createGrade(GradeModel model)
        {
            throw new NotImplementedException();
        }

        public QuestionModel createQuestionModel(QuestionModel model)
        {
            throw new NotImplementedException();
        }

        public StudentModel createStudent(StudentModel model)
        {
            throw new NotImplementedException();
        }

        public TopicModel createTopic(TopicModel model)
        {
            throw new NotImplementedException();
        }

        public TopicPortionModel createTopicPortionModel(TopicPortionModel model)
        {
            throw new NotImplementedException();
        }

        public void deleteGradeWithTopic(int Topicid)
        {
            throw new NotImplementedException();
        }

        public void deleteQuestion(int id)
        {
            throw new NotImplementedException();
        }

        public void deleteQuestionWithTopic(int Topicid)
        {
            throw new NotImplementedException();
        }

        public void deleteTopic(int id)
        {
            throw new NotImplementedException();
        }

        public void deleteTopicPortion(int id)
        {
            throw new NotImplementedException();
        }

        public void deleteTopicPortionWithTopic(int Topicid)
        {
            throw new NotImplementedException();
        }

        public List<GradeModel> GetGrades_byStudent(int studentid)
        {
            throw new NotImplementedException();
        }

        public List<QuestionModel> GetQuestions_byTopic(int topicid)
        {
            throw new NotImplementedException();
        }

        public StudentModel getStudentByid(int id)
        {
            throw new NotImplementedException();
        }

        public List<StudentModel> getStudents_All()
        {
            throw new NotImplementedException();
        }

        public TopicModel getTopicById(int id)
        {
            throw new NotImplementedException();
        }

        public List<TopicModel> GetTopicModels_All()
        {
            throw new NotImplementedException();
        }

        public List<TopicPortionModel> GetTopicPortions_bytopic(int topicid)
        {
            throw new NotImplementedException();
        }

        public void updateQuestion(QuestionModel model)
        {
            throw new NotImplementedException();
        }

        public void updateTopicName(TopicModel model)
        {
            throw new NotImplementedException();
        }

        public void UpdateTopicOrder(TopicModel model)
        {
            throw new NotImplementedException();
        }

        public void updateTopicPortion(TopicPortionModel model)
        {
            throw new NotImplementedException();
        }
    }
}
