using Dapper;
using Study.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Study.DataConnection
{
    public class SqliteConnector : IDataConnection
    {
        private String db = "SqliteStudybase";
        public bool CheckifTeacherUsernameIsFree(string userName)
        {
            bool free = false;
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                try
                {
                    TeacherModel get = connection.QuerySingle<TeacherModel>(@"select * from Teachers where userName = '" + userName + "'");
                }
                catch (InvalidOperationException)
                {
                    free = true;
                    return free;
                }
            }
            return free;
        }

        public bool checkIfThereIsaTeachersData()
        {
            bool isThereAData = true;

            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                int TeachersNumber = connection.QuerySingle<int>("select count(id) from Teachers", new int());
                if (TeachersNumber == 0)
                {
                    isThereAData = false;
                }
            }
            return isThereAData;
        }

        public bool CheckifUsernameIsFree(string userName)
        {
            bool free = false;
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                StudentModel get = new StudentModel();

                try
                {
                    get = connection.QuerySingle<StudentModel>("select * from Students where userName = '" + userName + "'");
                }
                catch(InvalidOperationException)
                {
                    free = true;
                }
                
            }
            return free;
        }

        public bool CheckPasswordByUserName(string userName, string enteredPassword)
        {
            bool match = false;
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                String password = connection.QuerySingle<string>("SELECT StudentPassword from Students where userName = '" + userName + "' LIMIT 1");
                if (password == enteredPassword)
                {
                    match = true;
                }
            }
            return match;
        }

        public bool CheckTeacherPasswordByUserName(string userName, string enteredPassword)
        {
            bool match = false;
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                //SELECT Password from Teachers where userName=@userName LIMIT 1
                String password = connection.QuerySingle<string>("SELECT Password from Teachers where userName = '"+userName+"' LIMIT 1");
                if (password == enteredPassword)
                {
                    match = true;
                }
            }
            return match;
        }

        public CourseModel createCourse(CourseModel model)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                String Query = @"insert into Courses(TeacherId, Name)
                                    values(@TeacherId, @Name); ";
                connection.Execute(Query, model);
                //Как бы я не плясал с бубном, SELECT last_insert_rowid() с какого-то хрена, каждый раз выдает 0, так что id получаем этим костылем
                int id = connection.QuerySingle<int>("select id from Courses where Name=@Name AND TeacherId=@TeacherId LIMIT 1", model);
                model.id =id;
            }
            return model;
        }

        public GradeModel createGrade(GradeModel model)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                String Query = @"insert into Grades(studentid, Topicid, QuestionAnsweredCorrectly, QuestionAnswered)
                                    values(@studentid, @Topicid, @QuestionAnsweredCorrectly, @QuestionAnswered);";
                connection.Execute(Query, model);
                //Как бы я не плясал с бубном, SELECT last_insert_rowid() с какого-то хрена, каждый раз выдает 0, так что id получаем этим костылем
                int id = connection.QuerySingle<int>("select id from Grades where studentid=@studentid AND Topicid=@Topicid AND QuestionAnsweredCorrectly=@QuestionAnsweredCorrectly AND QuestionAnswered=@QuestionAnswered LIMIT 1", model);
                model.setID(id);
            }
            return model;
        }

        public GroupModel createGroup(GroupModel model)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                String Query = @"insert into Groups(GroupName)
                                    values(@GroupName);";
                connection.Execute(Query, model);
                int id = connection.QuerySingle<int>("select id from Groups where GroupName=@GroupName", model);
                model.id = id;
            }
            return model;
        }

        public GroupToCourseRealationModel CreateGroupToCourseRealation(GroupToCourseRealationModel model)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                String Query = @"insert into StudentToCourse(Courseid, Groupid)
                                    values(@Courseid, @Groupid); ";
                connection.Execute(Query, model);
                //Как бы я не плясал с бубном, SELECT last_insert_rowid() с какого-то хрена, каждый раз выдает 0, так что id получаем этим костылем
                int id = connection.QuerySingle<int>("select id from StudentToCourse Where Courseid=@Courseid AND Groupid=@Groupid LIMIT 1;", model);
                model.id = id;
            }
            return model;
        }

        public QuestionModel createQuestionModel(QuestionModel model)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                String Query = @"insert into Questions(TopicId, QuestionText, CorrectAnswer, timetoanswer)
                                    values(@TopicId, @QuestionText, @CorrectAnswer, @timetoanswer); ";
                connection.Execute(Query, model);
                int id = connection.QuerySingle<int>("select id from Questions where TopicId=@TopicId AND QuestionText=@QuestionText AND CorrectAnswer=@CorrectAnswer AND timetoanswer=@timetoanswer LIMIT 1;", model);
                model.id = id;
            }
            return model;
        }

        public StudentModel createStudent(StudentModel model)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                String Query = @"insert into Students(StudentName, StudentGroupid, userName, StudentPassword)
                                values(@StudentName, @StudentGroupid, @userName, @StudentPassword); ";
                connection.Execute(Query, model);
                int id = connection.QuerySingle<int>("select id from Students where userName=@userName LIMIT 1", model);
                model.id = id;
            }
            return model;
        }

        public TeacherModel createTeacher(TeacherModel model)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                String Query = @"insert into Teachers(Name,UserName,Password,Position)
                                    values(@name, @UserName, @Password, @Position); ";
                connection.Execute(Query, model);
                int id = connection.QuerySingle<int>("select id from Teachers where username=@username LIMIT 1", model);
                model.id = id;
            }
            return model;
        }

        public TopicModel createTopic(TopicModel model)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                String Query = @"insert into Topics(topicName, TopicOrderNumber, Courseid)
                                    values(@TopicName, @TopicOrderNumber, @Courseid); ";
                connection.Execute(Query, model);
                int id = connection.QuerySingle<int>("select id from Topics where topicName=@topicName AND TopicOrderNumber=@TopicOrderNumber AND Courseid=@Courseid LIMIT 1;", model);
                model.id = id;
            }
            return model;
        }

        public TopicPortionModel createTopicPortionModel(TopicPortionModel model)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                String Query = @"insert into TopicPortions(Topicid, TopicPortionName, TopicPortionText)
                                values(@TopicId, @TopicPortionName, @TopicPortionText); ";
                connection.Execute(Query, model);
                int id = connection.QuerySingle<int>("select id from TopicPortions where Topicid=@Topicid AND TopicPortionName=@TopicPortionName AND TopicPortionText=@TopicPortionText LIMIT 1;", model);
                model.id = id;
            }
            return model;
        }

        public WrongAnswerModel createWrongAnswerModel(WrongAnswerModel model)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                String Query = @"insert into WrongAnswers(QuestionId,WrongAnswerText)
	                                values(@QuestionId,@WrongAnswerText);";
                connection.Execute(Query, model);
                int id = connection.QuerySingle<int>("select id from WrongAnswers where QuestionId=@QuestionId AND WrongAnswerText=@WrongAnswerText LIMIT 1;", model);
                model.id = id;
            }
            return model;
        }

        public void deleteCourse(int Courseid)
        {

            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute("delete from Courses where id = '"+Courseid + "'");
            }
        }

        public void deleteCourseToStudentRelation(int id)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute("delete from StudentToCourse where id = '" + id + "'");
            }
        }

        public void deleteCourseToStudentRelationWithCourse(int Courseid)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute("delete from StudentToCourse where Courseid = '" + Courseid + "'");
            }
        }

        public void deleteGradeWithTopic(int Topicid)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute("delete from Grades where Topicid = '" + Topicid + "'");
            }
        }

        public void deleteQuestion(int id)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute("delete from Questions where id = '" + id + "'");
            }
        }

        public void deleteQuestionWithTopic(int Topicid)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute("delete from Questions where Topicid = '" + Topicid + "'");
            }
        }

        public void deleteQuestionWrongAnswerbyQuestionid(int id)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute("delete from WrongAnswers where QuestionId = '" + id + "'");
            }
        }

        public void deleteTopic(int id)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute("delete from Topics where id = '" + id + "'");
            }
        }

        public void deleteTopicPortion(int id)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute("delete from TopicPortions where id '=" + id + "'");
            }
        }

        public void deleteTopicPortionWithTopic(int Topicid)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute("delete from TopicPortions where Topicid = '" + Topicid + "'");
            }
        }

        public void deleteTopicWithCourse(int Courseid)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute("delete from Topics where Courseid = '" + Courseid + "'");
            }
        }

        public void deleteWrongAnswer(int id)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute("delete from WrongAnswers where id = '" + id + "'");
            }
        }

        public void deleteWrongAnswerWithQuestion(int Questionid)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute("delete from WrongAnswers where QuestionId = '" + Questionid + "'");
            }
        }

        public CourseModel GetCourse(int id)
        {
            CourseModel output = new CourseModel();
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.QuerySingle<CourseModel>("select * from Courses where id = '" + id + "'");
            }
            return output;
        }

        public int getCourseidByTopicid(int topicid)
        {
            int Courseid;

            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                Courseid = connection.QuerySingle<int>(@"SELECT Courses.id  from Courses  
	                                                        inner join Topics on Topics.Courseid = Courses.id 
	                                                        where Topics.Id ='" + topicid +"' limit 1");
            }
            return Courseid;
        }

        public List<CourseModel> GetCourseModelsAll()
        {
            List<CourseModel> output = new List<CourseModel>();
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<CourseModel>("select * from Courses").ToList();
            }
            return output;
        }

        public List<CourseModel> GetCourseModelsByTeacherID(int TeacherID)
        {
            List<CourseModel> output = new List<CourseModel>();
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<CourseModel>("select * from Courses where TeacherId= '"+TeacherID + "'").ToList();
            }
            return output;
        }

        public string getCourseNamebyId(int id)
        {
            String output;
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.QuerySingle<String>(@"SELECT Name from Courses
	                                                        where Id ='" + id + "' Limit 1");
            }
            return output;
        }

        public string GetCourseNameByTopicid(int topicid)
        {
            String output;
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.QuerySingle<String>(@"SELECT Name from Courses  
	                                                        inner join Topics on Topics.Courseid = Courses.id 
	                                                        where Topics.Id ='"+topicid+ "' Limit 1");
            }
            return output;
        }

        public List<GradeModel> GetGrades_byStudent(int studentid)
        {
            List<GradeModel> output = new List<GradeModel>();
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<GradeModel>("select * from Grades where studentid = '" + studentid + "'").ToList();
            }
            return output;
        }

        public string GetGroupName(int id)
        {
            String output;
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.QuerySingle<String>("select GroupName from Groups where id = '" + id + "'");
            }
            return output;
        }

        public List<GroupModel> GetGroups_All()
        {
            List<GroupModel> output = new List<GroupModel>();
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<GroupModel>("select * from Groups").ToList();
            }
            return output;
        }

        public List<GroupToCourseRealationModel> GetGroupToCourseRelationWithCourseID(int Courseid)
        {
            List<GroupToCourseRealationModel> output = new List<GroupToCourseRealationModel>();
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<GroupToCourseRealationModel>("select * from StudentToCourse where Courseid = '"+Courseid + "'").ToList();
            }
            return output;
        }

        public List<GroupToCourseRealationModel> GetGroupToCourseRelationWithGroupid(int Groupid)
        {
            List<GroupToCourseRealationModel> output = new List<GroupToCourseRealationModel>();
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<GroupToCourseRealationModel>("select * from StudentToCourse where Groupid = '" + Groupid + "'").ToList();
            }
            return output;
        }

        public int GetNumberofTopicsByCouriseid(int Courseid)
        {
            int output;
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.QuerySingle<int>("select count(*) from Topics where Courseid = '" + Courseid + "'");
            }
            return output;
        }

        public List<QuestionModel> GetQuestions_byTopic(int topicid)
        {
            List<QuestionModel> output = new List<QuestionModel>();
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<QuestionModel>("SELECT * from Questions where Topicid = '" + topicid + "'").ToList();
            }
            return output;
        }

        public StudentModel getStudentByid(int id)
        {
            StudentModel output = new StudentModel();
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.QuerySingle<StudentModel>("select Id, StudentName, StudentGroupid from Students where id= '" + id+ "' LIMIT 1 ");
            }
            return output;
        }

        public StudentModel GetStudentByUserName(string username)
        {
            StudentModel output = new StudentModel();
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.QuerySingle<StudentModel>("select * from Students where username = '" + username + "' LIMIT 1 ");
            }
            return output;
        }

        public List<GradeModel> GetStudentGradesforCourse(int studentid, int courseid)
        {
            List<GradeModel> output = new List<GradeModel>();
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<GradeModel>(@"select * from Grades where studentid = '" + studentid+"' and topicid IN(select Id from Topics where Courseid ="+courseid+")").ToList();
            }
            return output;
        }

        public List<StudentModel> GetStudentsByCourseid(int Courseid)
        {
            List <StudentModel> output = new List<StudentModel>();
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<StudentModel>(@"select Students.Id, StudentName, StudentGroupid from Students
                                                          inner JOIN StudentToCourse on StudentToCourse.Groupid=Students.StudentGroupid
                                                          where StudentToCourse.Courseid= '" + Courseid + "' and StudentToCourse.Access = '1' ").ToList();
            }
            return output;
        }

        public List<StudentModel> getStudents_All()
        {
            List<StudentModel> output = new List<StudentModel>();
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<StudentModel>(@"select * from Students").ToList();
            }
            return output;
        }

        public TeacherModel GetTeacherByUserName(string username)
        {
            TeacherModel output = new TeacherModel();
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.QuerySingle<TeacherModel>("select * from Teachers where userName = '" + username + "' LIMIT 1 ");
            }
            return output;
        }

        public TopicModel getTopicById(int id)
        {
            TopicModel output = new TopicModel();
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.QuerySingle<TopicModel>("select * from Topics where id = '" + id + "' LIMIT 1 ");
            }
            return output;
        }

        public List<TopicModel> GetTopicModels_All()
        {
            List<TopicModel> output = new List<TopicModel>();
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<TopicModel>(@"select * from Topics").ToList();
            }
            return output;
        }

        public List<TopicModel> GetTopicModels_byCourseID(int courseid)
        {
            List<TopicModel> output = new List<TopicModel>();
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<TopicModel>(@"select * from Topics where courseid = '"+ courseid + "'").ToList();
            }
            return output;
        }

        public string getTopicNameById(int id)
        {
            String output;
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.QuerySingle<String>(@"select topicName from Topics where id = '" + id + "' LIMIT 1");
            }
            return output;
        }

        public int GetTopicOrderNumberByid(int id)
        {
            int output;
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.QuerySingle<int>(@"select TopicOrderNumber from Topics where id = '" + id + "' LIMIT 1");
            }
            return output;
        }

        public List<TopicPortionModel> GetTopicPortions_bytopic(int topicid)
        {
            List<TopicPortionModel> output = new List<TopicPortionModel>();
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<TopicPortionModel>(@"select * from TopicPortions where topicid = '" + topicid + "'").ToList();
            }
            return output;
        }

        public List<WrongAnswerModel> GetWrongAnswerModels_byQuestionid(int QuestionID)
        {
            List<WrongAnswerModel> output = new List<WrongAnswerModel>();
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                output = connection.Query<WrongAnswerModel>(@"select * from WrongAnswers where QuestionID = '" + QuestionID + "'").ToList();
            }
            return output;
        }

        public void updateCourseName(CourseModel model)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute(@"update Courses 
                                     set Name = @Name
                                     where id = @id;", model);
            }
        }

        public void updateGroupToCourseRelation(GroupToCourseRealationModel relation)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute(@"update StudentToCourse
                                     set Access = @Access
	                                 where id = @id;", relation);
            }
        }

        public void updateQuestion(QuestionModel model)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute(@"update Questions  
	                                 set QuestionText=@QuestionText, CorrectAnswer=@CorrectAnswer, timetoanswer=@timetoanswer
	                                 where id=@id ", model);
            }
        }

        public void updateTopicName(TopicModel model)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute(@"update Topics 
	                                 set topicName = @topicName
	                                 where id = @id;", model);
            }
        }

        public void UpdateTopicOrder(TopicModel model)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute(@"update Topics 
	                                 set TopicOrderNumber = @TopicOrderNumber 
	                                 where id = @id;", model);
            }
        }

        public void updateTopicPortion(TopicPortionModel model)
        {
            using (IDbConnection connection = new SQLiteConnection(GlobalConfig.CnnString(db)))
            {
                connection.Execute(@"update TopicPortions
	                                 set TopicPortionName = @TopicPortionName, TopicPortionText=@TopicPortionText
	                                 where id = @id;;", model);
            }
        }
    }
}
