using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Dapper;
using Study.Models;

namespace Study
{
    public class SqlConnector : IDataConnection
    {
        private string db = "SQlStudybase";

        public bool CheckifUsernameIsFree(string userName)
        {
            bool free = false;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                int get;
                var u = new DynamicParameters();
                u.Add("@userName", userName);
                u.Add("@get", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spCheckifUsernameIsFree", u, commandType: CommandType.StoredProcedure);
                get = u.Get<int>("@get");
                free = Convert.ToBoolean(get);
            } 
            return free;
        }

        public bool CheckifTeacherUsernameIsFree(string userName)
        {
            bool free = false;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                int get;
                var u = new DynamicParameters();
                u.Add("@userName", userName);
                u.Add("@get", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spCheckifTeacherUsernameIsFree", u, commandType: CommandType.StoredProcedure);
                get = u.Get<int>("@get");
                free = Convert.ToBoolean(get);
            }
            return free;
        }

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
                int id = q.Get<int>("@id");
                model.setID(id);
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
                q.Add("@timeToAnswer", model.timeToAnswer);
                q.Add("@id", 0,dbType:DbType.Int32, direction:ParameterDirection.Output);
                connection.Execute("dbo.spInsertQuestion", q, commandType: CommandType.StoredProcedure);
                model.id = q.Get<int>("@id");
            }
            return model;
        }



        public WrongAnswerModel createWrongAnswerModel(WrongAnswerModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var q = new DynamicParameters();
                q.Add("@QuestionId", model.QuestionId);
                q.Add("@WrongAnswerText", model.WrongAnswerText);
                q.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spInsertWrongAnswerToQuestion", q, commandType: CommandType.StoredProcedure);
                model.id = q.Get<int>("@id");
            }
            return model;
        }

        public StudentModel createStudent(StudentModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var s = new DynamicParameters();
                s.Add("@name", model.StudentName);
                s.Add("@groupid", model.StudentGroupid);
                s.Add("@userName", model.getUserName() );
                s.Add("@StudentPassword", model.getPassword());
                s.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spInsertStudent", s, commandType: CommandType.StoredProcedure);
                model.id = s.Get<int>("@id");
            }
            return model;
        }

        public GroupModel createGroup(GroupModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var s = new DynamicParameters();
                s.Add("@name", model.GroupName);
                s.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spInsertGroup", s, commandType: CommandType.StoredProcedure);
                model.id = s.Get<int>("@id");
            }
            return model;
        }

        public TeacherModel createTeacher(TeacherModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var s = new DynamicParameters();
                s.Add("@name", model.Name);
                s.Add("@userName", model.userName);
                s.Add("@Password", model.Password);
                s.Add("@Position", model.Position);
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
                t.Add("@Courseid", model.Courseid);
                t.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spInsertTopic", t, commandType: CommandType.StoredProcedure);
                int TopicID = t.Get<int>("@id");
                model.id = TopicID;
            }
            return model;
        }

        public CourseModel createCourse(CourseModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var c = new DynamicParameters();
                c.Add("@Name", model.Name);
                c.Add("@Teacherid", model.Teacherid);
                c.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spInsertCourse", c, commandType: CommandType.StoredProcedure);
                int courseID = c.Get<int>("@id");
                model.id = courseID;
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


        public GroupToCourseRealationModel CreateGroupToCourseRealation(GroupToCourseRealationModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var StC_R = new DynamicParameters();
                StC_R.Add("@Courseid", model.Courseid);
                StC_R.Add("@Groupid", model.Groupid);
                StC_R.Add("@id", 0, dbType: DbType.Int32, direction: ParameterDirection.Output);
                connection.Execute("dbo.spInsertGroupToCourseRelation", StC_R, commandType: CommandType.StoredProcedure);
                model.id = StC_R.Get<int>("@id");
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

        public void deleteWrongAnswer(int id)
        {
            using(IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var i = new DynamicParameters();
                i.Add("@QuestionId", id);
                connection.Execute("dbo.spDeleteWrongQuestionAnswerWithQuestionID", i, commandType: CommandType.StoredProcedure);
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

        public void deleteQuestionWrongAnswerbyQuestionid(int QuestionId)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var i = new DynamicParameters();
                i.Add("@QuestionId", QuestionId);
                connection.Execute("dbo.spDeleteWrongQuestionAnswerWithQuestionID", i, commandType: CommandType.StoredProcedure);
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
                output = connection.Query<GradeModel>("dbo.spGetGradesByStudent", q, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<GradeModel> GetStudentGradesforCourse(int studentid, int courseid)
        {

            List<GradeModel> output = new List<GradeModel>();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var q = new DynamicParameters();
                q.Add("@studentid", studentid);
                q.Add("@courseid", courseid);
                output = connection.Query<GradeModel>("dbo.spGetCourseGradesByStudent", q, commandType: CommandType.StoredProcedure).ToList();
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

        public List<GroupToCourseRealationModel> GetGroupToCourseRelationWithCourseID(int Courseid)
        {
            List<GroupToCourseRealationModel> output;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var sm = new DynamicParameters();
                sm.Add("@Courseid", Courseid);
                output = connection.Query<GroupToCourseRealationModel>("dbo.spGetGroupToCourseRelationWithCourseID", sm, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<GroupToCourseRealationModel> GetGroupToCourseRelationWithGroupid(int Groupid)
        {
            List<GroupToCourseRealationModel> output;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var sm = new DynamicParameters();
                sm.Add("@Groupid", Groupid);
                output = connection.Query<GroupToCourseRealationModel>("dbo.spGetGroupToCourseRelationWithGroupid", sm, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public StudentModel GetStudentByUserName(string username)
        {
            StudentModel st = new StudentModel();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var sm = new DynamicParameters();
                sm.Add("@username", username);
                st = connection.QuerySingle<StudentModel>("dbo.spGetStudentByUserName", sm, commandType: CommandType.StoredProcedure);
                st.grades = GetGrades_byStudent(st.id);
            }
            return st;
        }

        public string GetGroupName(int id)
        {
            String GroupName;
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var g = new DynamicParameters();
                g.Add("@id", id);
                GroupName = connection.QuerySingle<String>("dbo.spGetGroupName", g, commandType: CommandType.StoredProcedure);
            }
            return GroupName;

        }

        public TeacherModel GetTeacherByUserName(string username)
        {
            TeacherModel t = new TeacherModel();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var t1 = new DynamicParameters();
                t1.Add("@username", username);
                t = connection.QuerySingle<TeacherModel>("dbo.spGetTeacherByUserName", t1, commandType: CommandType.StoredProcedure);
            }
            return t;
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

        public List<GroupModel> GetGroups_All()
        {
            List<GroupModel> output = new List<GroupModel>();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<GroupModel>("dbo.spGetGroups_All", commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<StudentModel> GetStudentsByCourseid(int Courseid)
        {
            List<StudentModel> output = new List<StudentModel>();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var sm = new DynamicParameters();
                sm.Add("@Courseid", Courseid);
                output = connection.Query<StudentModel>("dbo.spGetStudentByCourseid", sm, commandType: CommandType.StoredProcedure).ToList();
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


        public string getTopicNameById(int id)
        {
            String topicname;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var sm = new DynamicParameters();
                sm.Add("@id", id);
                topicname = connection.QuerySingle<String>("dbo.spGetTopicNameByid", sm, commandType: CommandType.StoredProcedure);
            }
            return topicname;
        }

        public string GetCourseNameByTopicid(int topicid)
        {
            string CourseName;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var sm = new DynamicParameters();
                sm.Add("@topicid", topicid);
                CourseName = connection.QuerySingle<String>("dbo.spGetCourseName_byTopic", sm, commandType: CommandType.StoredProcedure);
            }
            return CourseName;
        }

        public string getCourseNamebyId(int id)
        {
            string CourseName;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var sm = new DynamicParameters();
                sm.Add("@id", id);
                CourseName = connection.QuerySingle<String>("dbo.spGetCourseName_byid", sm, commandType: CommandType.StoredProcedure);
            }
            return CourseName;
        }

        public int getCourseidByTopicid(int topicid)
        {
            int Courseid;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var sm = new DynamicParameters();
                sm.Add("@topicid", topicid);
                Courseid = connection.QuerySingle<int>("dbo.spGetCourseId_byTopic", sm, commandType: CommandType.StoredProcedure);
            }
            return Courseid;
        }

        public int GetTopicOrderNumberByid(int id)
        {
            int TopicOrderNumber;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var sm = new DynamicParameters();
                sm.Add("@id", id);
                TopicOrderNumber = connection.QuerySingle<int>("dbo.spGetTopicOrderNumberByid", sm, commandType: CommandType.StoredProcedure);
            }
            return TopicOrderNumber;
        }

        public int GetNumberofTopicsByCouriseid(int Courseid)
        {
            int TopicsNumber;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var sm = new DynamicParameters();
                sm.Add("@Courseid", Courseid);
                TopicsNumber = connection.QuerySingle<int>("dbo.spGetTopicsNumberByCourseid", sm, commandType: CommandType.StoredProcedure);
            }
            return TopicsNumber;
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

        public List<TopicModel> GetTopicModels_byCourseID(int Courseid)
        {
            List<TopicModel> output = new List<TopicModel>();

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {

                var tm = new DynamicParameters();
                tm.Add("@Courseid", Courseid);
                output = connection.Query<TopicModel>("dbo.spGetTopicsByCourse", tm, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<WrongAnswerModel> GetWrongAnswerModels_byQuestionid(int QuestionID)
        {
            List<WrongAnswerModel> output = new List<WrongAnswerModel>();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {

                var wa = new DynamicParameters();
                wa.Add("@QuestionID", QuestionID);
                output = connection.Query<WrongAnswerModel>("dbo.spGetWrongAnswers_byQuestionID", wa, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<CourseModel> GetCourseModelsAll()
        {
            List<CourseModel> output = new List<CourseModel>();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<CourseModel>("dbo.spGetCoursesAll", commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }

        public List<CourseModel> GetCourseModelsByTeacherID(int Teacherid)
        {
            List<CourseModel> output = new List<CourseModel>();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var c = new DynamicParameters();
                c.Add("@Teacherid", Teacherid);
                output = connection.Query<CourseModel>("dbo.spGetCoursesByteacher", c, commandType: CommandType.StoredProcedure).ToList();
            }
            return output;
        }


        public CourseModel GetCourse(int id)
        {
            CourseModel output = new CourseModel();
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var c = new DynamicParameters();
                c.Add("@id", id);
                output = connection.QuerySingle<CourseModel>("dbo.spGetCourse", c, commandType: CommandType.StoredProcedure);
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

        public bool CheckPasswordByUserName(string userName, string enteredPassword)
        {
            bool match = false;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                
                var u = new DynamicParameters();
                u.Add("@userName", userName);
                u.Add("@StudentPassword", "", dbType: DbType.String, direction: ParameterDirection.Output);
                string output=connection.QuerySingle<string>("dbo.spGetPasswordByUserName", u, commandType: CommandType.StoredProcedure);
                if(output==enteredPassword)
                {
                    match = true;
                }
            }
            return match;
        }

        public bool CheckTeacherPasswordByUserName(string userName, string enteredPassword)
        {
            bool match = false;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {

                var u = new DynamicParameters();
                u.Add("@userName", userName);
                u.Add("@Password", "", dbType: DbType.String, direction: ParameterDirection.Output);
                string output = connection.QuerySingle<string>("dbo.spGetTeacherPasswordByUserName", u, commandType: CommandType.StoredProcedure);
                if (output == enteredPassword)
                {
                    match = true;
                }
            }
            return match;
        }

        public bool checkIfThereIsaTeachersData()
        {
            bool isThereAData = false;

            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                int TeachersNumber = connection.QuerySingle<int>("dbo.spGetTeachersNumber", commandType: CommandType.StoredProcedure);
                if (TeachersNumber != 0)
                {
                    isThereAData = true;
                }
            }
            return isThereAData;
        }

        public void updateQuestion(QuestionModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var q = new DynamicParameters();
                q.Add("@id", model.id);
                q.Add("@QuestionText", model.QuestionText);
                q.Add("@CorrectAnswer", model.CorrectAnswer);
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

        public void updateCourseName(CourseModel model)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var c = new DynamicParameters();
                c.Add("@id", model.id);
                c.Add("@Name", model.Name);
                connection.Execute("dbo.spUpdateCourseName", c, commandType: CommandType.StoredProcedure);
            }
        }

        public void updateGroupToCourseRelation(GroupToCourseRealationModel relation)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var c = new DynamicParameters();
                c.Add("@id", relation.id);
                c.Add("@Access", relation.Access);
                connection.Execute("dbo.spUpdateCourseToGroupRealtion", c, commandType: CommandType.StoredProcedure);
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

        public void deleteWrongAnswerWithQuestion(int Questionid)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var q = new DynamicParameters();
                q.Add("@QuestionId", Questionid);
                connection.Execute("dbo.spDeleteWrongQuestionAnswerWithQuestionID", q, commandType: CommandType.StoredProcedure);
            }
        }

        public void deleteTopicWithCourse(int Courseid)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var q = new DynamicParameters();
                q.Add("@Courseid", Courseid);
                connection.Execute("dbo.spDeleteTopicWithCourseID", q, commandType: CommandType.StoredProcedure);
            }
        }

        public void deleteCourseToStudentRelationWithCourse(int Courseid)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var q = new DynamicParameters();
                q.Add("@Courseid", Courseid);
                connection.Execute("dbo.spDeleteStudentToCourseRelationWithCourseID", q, commandType: CommandType.StoredProcedure);
            }
        }

        public void deleteCourseToStudentRelation(int id)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var q = new DynamicParameters();
                q.Add("@id", id);
                connection.Execute("dbo.spDeleteStudentToCourseRelation", q, commandType: CommandType.StoredProcedure);
            }
        }

        public void deleteCourse(int Courseid)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(GlobalConfig.CnnString(db)))
            {
                var q = new DynamicParameters();
                q.Add("@id", Courseid);
                connection.Execute("dbo.spDeleteCourse", q, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
