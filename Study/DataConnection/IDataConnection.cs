
using System;
using System.Collections.Generic;
using System.Text;
using Study.Models;

/// <summary>
/// Интерфейс нужный для легкой смены базы данных. Задает все действия производимые с базой данных
/// </summary>

namespace Study
{
    public interface IDataConnection
    {
        StudentModel createStudent(StudentModel model);
        TeacherModel createTeacher(TeacherModel model);
        CourseModel createCourse(CourseModel model);
        TopicModel createTopic(TopicModel model);
        TopicPortionModel createTopicPortionModel(TopicPortionModel model);
        QuestionModel createQuestionModel(QuestionModel model);
        WrongAnswerModel createWrongAnswerModel(WrongAnswerModel model);
        GroupModel createGroup(GroupModel model);
        GroupToCourseRealationModel CreateGroupToCourseRealation(GroupToCourseRealationModel model);
        List<TopicPortionModel> GetTopicPortions_bytopic(int topicid);
        List<WrongAnswerModel> GetWrongAnswerModels_byQuestionid(int QuestionID);
        List<QuestionModel> GetQuestions_byTopic(int topicid);
        List<TopicModel> GetTopicModels_All();
        List<TopicModel> GetTopicModels_byCourseID( int courseid);
        List<CourseModel> GetCourseModelsAll();
        List<CourseModel> GetCourseModelsByTeacherID(int TeacherID);
        List<GroupModel> GetGroups_All();
        CourseModel GetCourse(int id);
        String GetGroupName(int id);
        List<StudentModel> getStudents_All();
        List<StudentModel> GetStudentsByCourseid(int Courseid);
        List<GroupToCourseRealationModel> GetGroupToCourseRelationWithCourseID(int Courseid);
        List<GroupToCourseRealationModel> GetGroupToCourseRelationWithGroupid(int Groupid);
        List<GradeModel> GetGrades_byStudent(int studentid);
        List<GradeModel> GetStudentGradesforCourse(int studentid, int courseid);
        GradeModel createGrade(GradeModel model);
        bool CheckifUsernameIsFree(string userName);
        bool CheckifTeacherUsernameIsFree(string userName);
        bool CheckPasswordByUserName(string userName, string enteredPassword);
        bool CheckTeacherPasswordByUserName(string userName, string enteredPassword);
        bool checkIfThereIsaTeachersData();
        StudentModel getStudentByid(int id);
        StudentModel GetStudentByUserName(string username);
        TeacherModel GetTeacherByUserName(string username);
        TopicModel getTopicById(int id);
        String getTopicNameById(int id);
        int GetTopicOrderNumberByid(int id);
        int GetNumberofTopicsByCouriseid(int Courseid);
        void deleteQuestionWithTopic(int Topicid);
        void deleteWrongAnswerWithQuestion(int Questionid);
        void deleteTopicPortionWithTopic(int Topicid);
        void deleteGradeWithTopic(int Topicid);
        void deleteTopic(int id);
        void deleteTopicWithCourse (int Courseid);
        void deleteCourseToStudentRelationWithCourse(int Courseid);
        void deleteCourseToStudentRelation(int id);
        void deleteCourse(int Courseid);
        void deleteQuestion(int id);
        void deleteWrongAnswer(int id);
        void deleteTopicPortion(int id);
        void deleteQuestionWrongAnswerbyQuestionid(int id);
        void UpdateTopicOrder(TopicModel model);
        void updateTopicName(TopicModel model);
        void updateCourseName(CourseModel model);
        void updateTopicPortion(TopicPortionModel model);
        void updateQuestion(QuestionModel model);
    }
}
