using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MyWebService
{
    /// <summary>
    /// Summary description for MyService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class MyService : System.Web.Services.WebService
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["conn"].ConnectionString);
        SqlDataAdapter da;
        DataSet ds;
        SqlCommand cmd;

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }

        [WebMethod]
        public DataSet GetAllStates()
        {
            da = new SqlDataAdapter("select * from StateTbl",con);
            ds = new DataSet();
            da.Fill(ds, "StateTbl");
            return ds;
        }

        [WebMethod]
        public DataSet GetCitiesByState(int StateID)
        {
            da = new SqlDataAdapter("select * from CityTbl where FkStateID=@SID",con);
            da.SelectCommand.Parameters.AddWithValue("@SID", StateID);
            ds = new DataSet();
            da.Fill(ds, "CityTbl");
            return ds;
        }

        [WebMethod]
        public string PostNewUser(string FirstName,string LastName,int StateID,int CityID)
        {
            string Data = "";
            cmd = new SqlCommand("insert into UserInfo values(@Fname,@Lname,@SID,@CID)",con);
            cmd.Parameters.AddWithValue("@Fname", FirstName);
            cmd.Parameters.AddWithValue("@Lname", LastName);
            cmd.Parameters.AddWithValue("@SID", StateID);
            cmd.Parameters.AddWithValue("@CID", CityID);
            con.Open();
            Data = cmd.ExecuteNonQuery().ToString();
            con.Close();
            if(Data!="0")
            {
                return "Inserted Successfully";
            }
            else
            {
                return "There should be problem please try again latter";
            }
        }
    }
}
