using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;

namespace Study
{
    public class SqlConnector : IDataConnection
    {
        private string db = "Studybase";

        public GradeModel createGrade(GradeModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var q = new DynamicParameters();
                q.Add("@studentid", model.studentid);
                q.Add("@Topicid", model.Topicid);
                q.Add("@QuestionAnsweredCorrectly", model.QuestionAnsweredCorrectly);
                q.Add("@QuestionAnswered", model.QuestionAnswered);
                q.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.SpInsertGrade", q, commandType: CommandType.StoredProcedure);
                model.id = q.Get<int>("@id");
            }
            return model;
        }

        public QuestionModel createQuestionModel(QuestionModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var q = new DynamicParameters();
                q.Add("@TopicId", model.Topicid);
                q.Add("@QuestionText", model.QuestionText);
                q.Add("@CorrectAnswer", model.CorrectAnswer);
                q.Add("@WrongAnswer1", model.WrongAnswer1);
                q.Add("@WrongAnswer2", model.WrongAnswer2);
                q.Add("@timeToAnswer", model.timeToAnswer);
                q.Add("@id", 0,dbType:DbType.Int32, direction:ParameterDirection.Output);
                connection.Execute("dbo.spInsertQuestion", q, commandType: CommandType.StoredProcedure);
                model.id = q.Get<int>("@id");
            }
            return model;
        }

        public StudentModel createStudent(StudentModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var s = new DynamicParameters();
                s.Add("@name", model.Name);
                s.Add("@group", model.Group);
                s.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spInsertStudent", s, commandType: CommandType.StoredProcedure);
                model.id = s.Get<int>("@id");
            }
            return model;
        }

        public TopicModel createTopic(TopicModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var t = new DynamicParameters();
                t.Add("@topicName", model.topicName);
                t.Add("@TopicOrderNumber", model.TopicOrderNumber);
                t.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spInsertTopic", t, commandType: CommandType.StoredProcedure);
                model.id = t.Get<int>("@id");
            }
            return model;
        }

        public TopicPortionModel createTopicPortionModel(TopicPortionModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var tp = new DynamicParameters();
                tp.Add("@TopicId", model.TopicId);
                tp.Add("@topicPortionName", model.TopicPortionName);
                tp.Add("@TopicPortionText", model.TopicPortionText);
                tp.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spInsertTopicPortion", tp, commandType: CommandType.StoredProcedure);
                model.id = tp.Get<int>("@id");
            }
            return model;
        }

        public void deleteGradeWithTopic(int Topicid)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var t = new DynamicParameters();
                t.Add("@Topicid", Topicid);
                connection.Execute("dbo.SpDeleteGradeWithTopic", t, commandType: CommandType.StoredProcedure);
            }
        }

        public void deleteQuestion(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var t = new DynamicParameters();
                t.Add("@id", id);
                connection.Execute("dbo.spDeleteQuestion", t, commandType: CommandType.StoredProcedure);
            }
        }

        public void deleteQuestionWithTopic(int Topicid)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var t = new DynamicParameters();
                t.Add("@Topicid", Topicid);
                connection.Execute("dbo.spDeleteQuestionWithTopic", t, commandType: CommandType.StoredProcedure);
            }
        }

        public void deleteTopic(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var t = new DynamicParameters();
                t.Add("@id", id);
                connection.Execute("dbo.spDeleteTopic", t, commandType: CommandType.StoredProcedure);
            }
        }

        public void deleteTopicPortion(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var t = new DynamicParameters();
                t.Add("@id", id);
                connection.Execute("dbo.spDeleteTopicPortion", t, commandType: CommandType.StoredProcedure);
            }
        }

        public void deleteTopicPortionWithTopic(int Topicid)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var t = new DynamicParameters();
                t.Add("@Topicid", Topicid);
                connection.Execute("dbo.spDeleteTopicPortionWithTopic", t, commandType: CommandType.StoredProcedure);
            }
        }

        public List<GradeModel> GetGrades_byStudent(int studentid)
        {
            List<GradeModel> output = new List<GradeModel>();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var q = new DynamicParameters();
                q.Add("@studentid", studentid);
                connection.Query<GradeModel>("dbo.SpInsertGrade", q, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<QuestionModel> GetQuestions_byTopic(int topicid)
        {
            List<QuestionModel> output = new List<QuestionModel>();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var qm = new DynamicParameters();
                qm.Add("@Topicid", topicid);
                output = connection.Query<QuestionModel>("dbo.spGetQuestions_byTopic",qm, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public StudentModel getStudentByid(int id)
        {
            StudentModel st = new StudentModel();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var sm = new DynamicParameters();
                sm.Add("@id", id);
                st = connection.QuerySingle<StudentModel>("dbo.spGetStudentByid", sm, commandType: CommandType.StoredProcedure);
            }
            return st;
        }

        public List<StudentModel> getStudents_All()
        {
            List<StudentModel> output = new List<StudentModel>();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<StudentModel>("dbo.spGetStudent_All", commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public TopicModel getTopicById(int id)
        {
            TopicModel tm = new TopicModel();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var sm = new DynamicParameters();
                sm.Add("@id", id);
                tm = connection.QuerySingle<TopicModel>("dbo.spGetTopicByid", sm, commandType: CommandType.StoredProcedure);
            }
            return tm;
        }

        public List<TopicModel> GetTopicModels_All()
        {
            List<TopicModel> output = new List<TopicModel>();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<TopicModel>("dbo.spGetTopicsAll", commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<TopicPortionModel> GetTopicPortions_bytopic(int topicid)
        {
            List<TopicPortionModel> output = new List<TopicPortionModel>();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var tpm = new DynamicParameters();
                tpm.Add("@Topicid", topicid);
                output = connection.Query<TopicPortionModel>("dbo.spGetTopicPortions_ByTopic", tpm, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public void updateQuestion(QuestionModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var q = new DynamicParameters();
                q.Add("@id", model.id);
                q.Add("@QuestionText", model.QuestionText);
                q.Add("@CorrectAnswer", model.CorrectAnswer);
                q.Add("@WrongAnswer1", model.WrongAnswer1);
                q.Add("@WrongAnswer2", model.WrongAnswer2);
                q.Add("@timeToAnswer", model.timeToAnswer);
                connection.Execute("dbo.spUpdateQuestion", q, commandType: CommandType.StoredProcedure);
            }
        }

        public void updateTopicName(TopicModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var t = new DynamicParameters();
                t.Add("@id", model.id);
                t.Add("@topicName", model.topicName);
                connection.Execute("dbo.spUpdateTopicName", t, commandType: CommandType.StoredProcedure);
            }
        }

        public void UpdateTopicOrder(TopicModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var t = new DynamicParameters();
                t.Add("@TopicOrderNumber", model.TopicOrderNumber);
                t.Add("@id",model.id);
                connection.Execute("dbo.SpUpdateTopicOrderNumber", t, commandType: CommandType.StoredProcedure);
            }
        }

        public void updateTopicPortion(TopicPortionModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var tp = new DynamicParameters();
                tp.Add("@id", model.id);
                tp.Add("@topicPortionName", model.TopicPortionName);
                tp.Add("@TopicPortionText", model.TopicPortionText);
                connection.Execute("dbo.spUpdateTopicPortion", tp, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
