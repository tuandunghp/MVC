using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataService
    {
        public DataService() { }
        string connectionstring = ConfigurationManager.ConnectionStrings["WebDB"].ConnectionString;
        SqlConnection myConnection;
        void mo()
        {
            myConnection = new SqlConnection(connectionstring);
            myConnection.Open();
        }
        void dong()
        {
            myConnection.Close();
        }
        // truyền vào chuỗi sql, trả về bảng kết quả
        public DataTable myTable(string sql, params SqlParameter[] ps)
        {
            mo();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(sql, myConnection);
            cmd.Parameters.AddRange(ps);
            try
            {
                dt.Load(cmd.ExecuteReader());
            }
            finally
            {
                dong();
            }
            return dt;
        }

        // truyền vào stored procedures, trả về bảng kết quả
        public DataTable myTable_sp(string sql, params SqlParameter[] ps)
        {
            mo();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand(sql, myConnection);
            cmd.Parameters.AddRange(ps);

            cmd.CommandType = CommandType.StoredProcedure;
            try
            {
                dt.Load(cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                return dt = null;
            }
            finally
            {
                dong();
            }
            return dt;
        }

        public DataSet myDataset_sp(string sql, params SqlParameter[] ps)
        {
            mo();
            SqlCommand cmd = new SqlCommand(sql, myConnection);
            cmd.Parameters.AddRange(ps);
            cmd.CommandType = CommandType.StoredProcedure;

            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(sql, myConnection);
            da.SelectCommand = cmd;
            try
            {

                da.Fill(ds);

            }
            catch (Exception ex)
            {
                return ds = null;
            }
            finally
            {
                dong();
            }



            return ds;
        }

        // truyền vào chuỗi sql, trả về true or false
        public bool exe(string sql, params SqlParameter[] ps)
        {
            mo();
            SqlCommand cmd = new SqlCommand(sql, myConnection);
            cmd.Parameters.AddRange(ps);
            try
            {
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
            dong();
        }

        // truyền vào stored procedures, trả về true or false
        public bool exe_sp(string sql, params SqlParameter[] ps)
        {
            bool kt = false;
            mo();
            SqlCommand cmd = new SqlCommand(sql, myConnection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(ps);
            try
            {
                cmd.ExecuteNonQuery();
                kt = true;
            }
            catch (Exception ex)
            {
                kt = false;
            }
            finally
            {
                dong();
            }
            return kt;
        }
        //public void showMessage(string mess)
        //{
        //    string strBuilder = "<script language='javascript'>alert('" + mess + "')</script>";
        //    Response.Write(strBuilder);

        //}
        //  / * SELECT convert(varchar, getdate(), 100) – mon dd yyyy hh:mmAM (or PM)

        //                                                – Oct  2 2008 11:01AM          

        //        SELECT convert(varchar, getdate(), 101) – mm/dd/yyyy - 10/02/2008                  

        //        SELECT convert(varchar, getdate(), 102) – yyyy.mm.dd – 2008.10.02           

        //        SELECT convert(varchar, getdate(), 103) – dd/mm/yyyy

        //        SELECT convert(varchar, getdate(), 104) – dd.mm.yyyy

        //        SELECT convert(varchar, getdate(), 105) – dd-mm-yyyy

        //        SELECT convert(varchar, getdate(), 106) – dd mon yyyy

        //        SELECT convert(varchar, getdate(), 107) – mon dd, yyyy

        //        SELECT convert(varchar, getdate(), 108) – hh:mm:ss

        //        SELECT convert(varchar, getdate(), 109) – mon dd yyyy hh:mm:ss:mmmAM (or PM)

        //                                                – Oct  2 2008 11:02:44:013AM   

        //        SELECT convert(varchar, getdate(), 110) – mm-dd-yyyy

        //        SELECT convert(varchar, getdate(), 111) – yyyy/mm/dd

        //        SELECT convert(varchar, getdate(), 112) – yyyymmdd

        //        SELECT convert(varchar, getdate(), 113) – dd mon yyyy hh:mm:ss:mmm

        //                                                – 02 Oct 2008 11:02:07:577     

        //        SELECT convert(varchar, getdate(), 114) – hh:mm:ss:mmm(24h)

        //        SELECT convert(varchar, getdate(), 120) – yyyy-mm-dd hh:mm:ss(24h)

        //        SELECT convert(varchar, getdate(), 121) – yyyy-mm-dd hh:mm:ss.mmm

        //        SELECT convert(varchar, getdate(), 126) – yyyy-mm-ddThh:mm:ss.mmm

        //                                                – 2008-10-02T10:52:47.513

        //        – SQL create different date styles with t-sql string functions

        //        SELECT replace(convert(varchar, getdate(), 111), ‘/’, ‘ ‘) – yyyy mm dd

        //        SELECT convert(varchar(7), getdate(), 126)                 – yyyy-mm

        //        SELECT right(convert(varchar, getdate(), 106), 8)          – mon yyyy
        //*/
    }
}
