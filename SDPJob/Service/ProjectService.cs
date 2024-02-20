using SDPJob.Configuration;
using SDPJob.Models;
using System.Data.SqlClient;
using System.Data;

namespace SDPJob.Service
{
    public class ProjectService
    {
        public IEnumerable<ProjectModel> GetProject(string filter)
        {
            List<ProjectModel> listProject = new List<ProjectModel>();
            using (SqlConnection con = new SqlConnection(Appsetting.GetStringConnection()))
            {
                SqlCommand cmd = new SqlCommand("sp_User_GetProject", con);
                cmd.Parameters.AddWithValue("@filter", filter);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    ProjectModel projectModel = new ProjectModel();
                    projectModel.ProjectID = Convert.ToInt32(rdr["ProjectID"]);
                    projectModel.ProjectName = rdr["ProjectName"].ToString();
                    listProject.Add(projectModel);
                }
                con.Close();
            }
            return listProject;
        }
    }
}
