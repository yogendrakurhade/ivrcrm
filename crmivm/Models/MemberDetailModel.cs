using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;

namespace crmivm.Models
{
    public class MemberDetailModel

    {
        public int login_id { get; set; }
        public int claim_id { get; set; }
        public int case_id { get; set; }
        public string url_no { get; set; }
        public string loc_no { get; set; }
        public string claim_type { get; set; }

        public string emp_name { get; set; }
        public string patient_name { get; set; }
        public int emp_no { get; set; }
        public string claimed_amt { get; set; }
        public string status { get; set; }
        public DateTime doa { get; set; }
        public DateTime dod { get; set; }
        public string grp_code { get; set; }
        public string approved_amt { get; set; }
        public string varGrpCode { get; set; }
        public int intEmpNo { get; set; }
        
        private string connectionstring = ConfigurationManager.AppSettings["CLAIMS"].ToString();
        public List<MemberDetailModel> GetMemberDetails(int intEmpNo,string varGrpCode)
        {
            List<MemberDetailModel> AddData = new List<MemberDetailModel>();
            using (SqlConnection myConnection = new SqlConnection(connectionstring))
            {
                int login = 0;
                using (SqlCommand cmd = new SqlCommand())
                {
                    myConnection.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "spGetClaimDetailsTC";
                    cmd.Connection = myConnection;
                    cmd.Parameters.Add("@varGrpCode", SqlDbType.VarChar).Value = varGrpCode;
                    cmd.Parameters.Add("@intEmpNo", SqlDbType.Int).Value = intEmpNo;
                    cmd.Parameters.Add("@login_id", SqlDbType.Int).Value = login;
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                AddData.Add(new MemberDetailModel
                                {

                                    grp_code = dr["grp_code"].ToString(),
                                    emp_no = Convert.ToInt32(dr["emp_no"].ToString()),
                                    claim_id = Convert.ToInt32(dr["claim_id"].ToString()),
                                    case_id = Convert.ToInt32(dr["case_id"].ToString()),
                                    url_no = dr["url_no"].ToString(),
                                    loc_no = dr["loc_no"].ToString(),
                                    claim_type = dr["claim_type"].ToString(),
                                    emp_name = dr["emp_name"].ToString(),
                                    patient_name = dr["patient_name"].ToString(),
                                    claimed_amt = dr["claimed_amt"].ToString(),
                                    status = dr["status"].ToString(),
                                    doa = Convert.ToDateTime(dr["doa"].ToString()),
                                    dod = Convert.ToDateTime(dr["dod"].ToString()),
                                    approved_amt = dr["approved_amt"].ToString()
                                });
                            }

                        }
                    }
                }
            }
            return AddData;
        }
    }
}