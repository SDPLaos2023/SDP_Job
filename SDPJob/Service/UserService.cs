using System.Data;
using System.Data.SqlClient;
using SDPJob.Configuration;
using SDPJob.Models;
namespace SDPJob.Service
{
    public class UserService
    {
        public IEnumerable<UserModel> GetLookup(string filter)
        {
            List<UserModel> listUser = new List<UserModel>();
            using (SqlConnection con = new SqlConnection(Appsetting.GetStringConnection()))
            {
                SqlCommand cmd = new SqlCommand("sp_User_GetLookup", con);
                cmd.Parameters.AddWithValue("@filter", filter);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    UserModel userModel = new UserModel();
                    userModel.UserID = Convert.ToInt32(rdr["UserID"]);
                    userModel.Email = rdr["Email"].ToString();
                    userModel.NickName = rdr["NickName"].ToString();
                    listUser.Add(userModel);
                }
                con.Close();
            }
            return listUser;
        }
    }
}
