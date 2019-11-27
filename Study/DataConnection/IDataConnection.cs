
using System;
using System.Collections.Generic;
using System.Text;

namespace Study
{
    public interface IDataConnection
    {
        StudentModel createStudent(StudentModel model);
        TopicModel createTopic(TopicModel model);
        TopicPortionModel createTopicPortionModel(TopicPortionModel model);
        QuestionModel createQuestionModel(QuestionModel model);
        List<TopicPortionModel> GetTopicPortions_bytopic(int topicid);
        List<QuestionModel> GetQuestions_byTopic(int topicid);
        List<TopicModel> GetTopicModels_All();
        List<StudentModel> getStudents_All();
        List<GradeModel> GetGrades_byStudent(int studentid);
        GradeModel createGrade(GradeModel model);
        StudentModel getStudentByid(int id);
        TopicModel getTopicById(int id);
        void UpdateTopicOrder(TopicModel model);
        void deleteQuestionWithTopic(int Topicid);
        void deleteTopicPortionWithTopic(int Topicid);
        void deleteGradeWithTopic(int Topicid);
        void deleteTopic(int id);
        void deleteQuestion(int id);
        void deleteTopicPortion(int id);
        void updateTopicName(TopicModel model);
        void updateTopicPortion(TopicPortionModel model);
        void updateQuestion(QuestionModel model);

    }
}
