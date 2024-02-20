using Microsoft.VisualBasic;
using SDPJob.Configuration;
using SDPJob.Models;
using System.Data.SqlClient;
using System.Data;

namespace SDPJob.Service
{
    public class TaskService
    {

        public IEnumerable<TaskModel> GetNewRequest()
        {
            List<TaskModel> listTask = new List<TaskModel>();
            using (SqlConnection con = new SqlConnection(Appsetting.GetStringConnection()))
            {
                SqlCommand cmd = new SqlCommand("sp_Task_GetTaskByTypeID", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TypeID", 1);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    TaskModel taskModel = new TaskModel();
                    taskModel.TaskID = Convert.ToInt32(rdr["TaskID"]);
                    taskModel.DueDate = Convert.ToDateTime(rdr["DueDate"]);
                    taskModel.ProjectID = Convert.ToInt32(rdr["ProjectID"]);
                    taskModel.Assignee = Convert.ToInt32(rdr["Assignee"]);
                    taskModel.Description = rdr["Description"].ToString();
                    listTask.Add(taskModel);
                }
                con.Close();
            }
            return listTask;
        }
        public IEnumerable<TypeModel> GetType(string filter)
        {
            List<TypeModel> listTask = new List<TypeModel>();
            using (SqlConnection con = new SqlConnection(Appsetting.GetStringConnection()))
            {
                SqlCommand cmd = new SqlCommand("sp_Task_GetType", con);
                cmd.Parameters.AddWithValue("@filter", filter);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    TypeModel typeModel = new TypeModel();
                    typeModel.TypeID = Convert.ToInt32(rdr["TypeID"]);
                    typeModel.TypeName = rdr["TypeName"].ToString();
                    listTask.Add(typeModel);
                }
                con.Close();
            }
            return listTask;
        }
        public IEnumerable<TaskModel> GetTask(string filter)
        {
            List<TaskModel> listTask = new List<TaskModel>();
            using (SqlConnection con = new SqlConnection(Appsetting.GetStringConnection()))
            {
                SqlCommand cmd = new SqlCommand("sp_Task_GetTask", con);
                cmd.Parameters.AddWithValue("@filter", filter);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    TaskModel taskModel = new TaskModel();
                    taskModel.TaskID = Convert.ToInt32(rdr["TaskID"]);
                    taskModel.DueDate = Convert.ToDateTime(rdr["DueDate"]);
                    taskModel.ProjectID = Convert.ToInt32(rdr["ProjectID"]);
                    taskModel.Assignee = Convert.ToInt32(rdr["Assignee"]);
                    taskModel.Description = rdr["Description"].ToString();
                    listTask.Add(taskModel);
                }
                con.Close();
            }
            return listTask;
        }
        public void AddTask(TaskModel taskModel)
        {
            using (SqlConnection con = new SqlConnection(Appsetting.GetStringConnection()))
            {
                SqlCommand cmd = new SqlCommand("sp_Task_AddTask", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@DueDate", taskModel.DueDate.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@Type", taskModel.Type);
                cmd.Parameters.AddWithValue("@ProjectID", taskModel.ProjectID);
                cmd.Parameters.AddWithValue("@Assignee", taskModel.Assignee);
                cmd.Parameters.AddWithValue("@Description", taskModel.Description);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
