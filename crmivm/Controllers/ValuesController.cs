using crmivm.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace crmivm.Controllers
{
    //[Authorize]
    public class ValuesController : ApiController
    {
        MemberDetailModel MDM = new MemberDetailModel();
        private string connectionstring = ConfigurationManager.AppSettings["EPConnectionString"].ToString();
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(string mob_no)
        {
            mob_no = dbCALL(mob_no);
            return  mob_no;
        }


        // GET api/values/5
        public IHttpActionResult Get_mob_no(string mob_no)
        {
            mob_no = dbCALL(mob_no);
            return Json(mob_no);
        }

        [HttpGet]
        [ActionName("memberdetails")]
        public IHttpActionResult memberdetails(int intEmpNo, string varGrpCode)
        {
            #region
            List<MemberDetailModel> ReturnDATA = new List<MemberDetailModel>();
            ReturnDATA = MDM.GetMemberDetails(intEmpNo, varGrpCode);
            return Json(ReturnDATA);
            #endregion
        }

        [System.Web.Http.NonAction]
        public string dbCALL(string mob_no)
        {
            //List<string> mess = new List<string>();
            string mess1 = string.Empty;
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "TrueCoverInsertData";
                    cmd.Connection = con;
                    cmd.Parameters.Add("@mem_add_mob_no", SqlDbType.VarChar).Value = mob_no;
                    cmd.Parameters.Add("@message", SqlDbType.VarChar, 100);
                    cmd.Parameters["@message"].Direction = ParameterDirection.Output;
                    con.Open();
                    cmd.ExecuteReader();
                    mess1 = cmd.Parameters["@message"].Value.ToString();
                }
                //mess.Add(mess1);
            }
            return mess1;
        }

    }
}
