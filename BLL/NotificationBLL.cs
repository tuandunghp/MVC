using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    public class NotificationBLL
    {
        DataService db = new DataService();
        // Lấy Notification theo Id
        public DataTable Get_Noti_Where_Id(int Id)
        {
            SqlParameter p1 = new SqlParameter("@Id", Id);
            //string sql = "SELECT * FROM Notification WHERE Id=" + Id.ToString();
            return db.myTable_sp("sp_Sel_Notifacation", p1);
        }
        // Lấy bảng Notification
        public DataTable Load_All_Notification()
        {
            string sql = "SELECT * FROM Notification ORDER BY Id DESC";
            return db.myTable(sql);
        }
        // Lấy bảng Notification, Left Join để lấy cả FullName
        public DataTable Load_All_Notifi_FullName()
        {
            return db.myTable("sp_Sel_Notification_All");
        }
        //Select top Notification mới nhất, chuyền vào số lượng muốn lấy
        public DataTable Select_Top_Notifi(int Number)
        {
            string sql = "SELECT TOP " + Number + " * FROM Notification ORDER BY Id DESC";
            return db.myTable(sql);
        }
        public bool Upd_ReadNumber(int id, int ReadNumber)
        {
            SqlParameter p1 = new SqlParameter("@Id", id);
            SqlParameter p2 = new SqlParameter("@ReadNumber", ReadNumber);
            return db.exe_sp("sp_Upd_Notifi_ReadNumber", p1, p2);
        }
        public bool Ins(string Title, string Body, string Images, string CreateDate, string TitleUrl, int UserId)
        {
            SqlParameter p0 = new SqlParameter("@Title", Title);
            SqlParameter p1 = new SqlParameter("@Body", Body);
            SqlParameter p2 = new SqlParameter("@Images", Images);
            SqlParameter p3 = new SqlParameter("@CreateDate", CreateDate);
            SqlParameter p4 = new SqlParameter("@TitleUrl", TitleUrl);
            SqlParameter p5 = new SqlParameter("@UserId", UserId);
            return db.exe_sp("sp_Ins_Notification", p0, p1, p2, p3, p4, p5);
        }
        public bool Upd(int Id, string Title, string Body, string Images, string LastModified, string TitleUrl)
        {
            SqlParameter p0 = new SqlParameter("@Id", Id);
            SqlParameter p1 = new SqlParameter("@Title", Title);
            SqlParameter p2 = new SqlParameter("@Body", Body);
            SqlParameter p3 = new SqlParameter("@Images", Images);
            SqlParameter p4 = new SqlParameter("@LastModified", LastModified);
            SqlParameter p5 = new SqlParameter("@TitleUrl", TitleUrl);
            return db.exe_sp("sp_Upd_Notification", p0, p1, p2, p3, p4, p5);
        }
        public bool Del(int Id)
        {
            SqlParameter p1 = new SqlParameter("@Id", Id);
            return db.exe_sp("sp_Del_Notification", p1);
        }
    }
}
