using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    public class EditorBLL
    {
        DataService db = new DataService();
        public DataTable LoadEditor_Id(int Id)
        {
            SqlParameter p0 = new SqlParameter("Id", Id);
            return db.myTable_sp("sp_sel_CodeEditor", p0);
        }
        public bool Upd_ReadNumber(int id, int ReadNumber)
        {
            string sql = "UPDATE CodeEditor SET ReadNumber=" + ReadNumber + "WHERE Id=" + id;
            return db.exe(sql);
        }
        public bool ins_Editor(string title, string Css, string Html, string Js, string Full, string createDate, string UserName, bool cbDisplay)
        {
            SqlParameter p1 = new SqlParameter("@Title", title);
            SqlParameter p2 = new SqlParameter("@Css", Css);
            SqlParameter p3 = new SqlParameter("@Html", Html);
            SqlParameter p4 = new SqlParameter("@Js", Js);
            SqlParameter p5 = new SqlParameter("@FullCode", Full);
            SqlParameter p6 = new SqlParameter("@CreateDate", createDate);
            SqlParameter p7 = new SqlParameter("@UserName", UserName);
            SqlParameter p8 = new SqlParameter("@DisplayEditor", cbDisplay);
            return db.exe_sp("ins_CodeEditor", p1, p2, p3, p4, p5, p6, p7, p8);
        }
        public bool Up_Editor(int Id, string title, string Css, string Html, string Js, string Full, string createDate, bool cbDisplay)
        {
            SqlParameter p0 = new SqlParameter("@Id", Id);
            SqlParameter p1 = new SqlParameter("@Title", title);
            SqlParameter p2 = new SqlParameter("@Css", Css);
            SqlParameter p3 = new SqlParameter("@Html", Html);
            SqlParameter p4 = new SqlParameter("@Js", Js);
            SqlParameter p5 = new SqlParameter("@FullCode", Full);
            SqlParameter p6 = new SqlParameter("@CreateDate", createDate);
            SqlParameter p7 = new SqlParameter("@DisplayEditor", cbDisplay);
            return db.exe_sp("sp_Up_Editor",p0, p1, p2, p3, p4, p5, p6, p7);
        }
        public DataTable LoadEditor()
        {
            string sql = "SELECT * FROM CodeEditor ORDER BY Id DESC";
            DataTable dt = db.myTable(sql);
            return dt;
        }
        public bool Del_Editor(int Id)
        {
            string sql = "DELETE FROM CodeEditor WHERE Id=" + Id;
            return db.exe(sql);
        }
    }
}
