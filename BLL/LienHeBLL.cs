using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data.SqlClient;
using System.Data;

namespace BLL
{
    public class LienHeBLL
    {
        DataService db = new DataService();
        // ins Lien he
        public bool Ins_LienHe(string Title, string fullname, string Email, string ad, string phone, string Body, string CreateDate)
        {
            SqlParameter p1 = new SqlParameter("@Title", Title);
            SqlParameter p2 = new SqlParameter("@Name", fullname);
            SqlParameter p3 = new SqlParameter("@Email", Email);
            SqlParameter p4 = new SqlParameter("@Address", ad);
            SqlParameter p5 = new SqlParameter("@Phone", phone);
            SqlParameter p6 = new SqlParameter("@Body", Body);
            SqlParameter p7 = new SqlParameter("@CreateDate", CreateDate);
            return db.exe_sp("sp_ins_LienHe", p1, p2, p3, p4, p5, p6, p7);
        }
        // Lấy bảng liên hệ
        public DataTable GetLienHe()
        {
            string sql = "SELECT * FROM LienHe ORDER BY Id DESC";
            DataTable dt = db.myTable(sql);
            return dt;
        }
        // Lấy bảng liên hệ theo id
        public DataTable GetLienHe_Id(int Id)
        {
            string sql = "SELECT * FROM LienHe WHERE Id=" + Id;
            DataTable dt = db.myTable(sql);
            return dt;
        }
        // Lấy bảng RepLienHe theo idLienHe
        public DataTable GetRepLienHe_Id(int Id)
        {
            string sql = "SELECT * FROM RepLienHe WHERE LienHeId=" + Id;
            DataTable dt = db.myTable(sql);
            return dt;
        }
        // Lấy liên hệ chưa trả  lời
        public DataTable GetLienHe_NoRep()
        {
            string sql = "SELECT * FROM LienHe WHERE Reply='False' or SendEmail='False' ORDER BY Id DESC";
            DataTable dt = db.myTable(sql);
            return dt;
        }
        // Thêm trả lời cho liên hệ
        public bool ins_RepLienHe(string FullName, string LoginName, string Rep, string Date, int IdLienHe)
        {
            SqlParameter p1 = new SqlParameter("FullName", FullName);
            SqlParameter p2 = new SqlParameter("UserName", LoginName);
            SqlParameter p3 = new SqlParameter("Rep", Rep);
            SqlParameter p4 = new SqlParameter("CreateDate", Date);
            SqlParameter p5 = new SqlParameter("LienHeId", IdLienHe);
            return db.exe_sp("ins_RepLienHe", p1, p2, p3, p4, p5);
        }
        // đánh dấu đã trả lời liên hệ
        public bool UpReply(int IdLienhe)
        {
            string sql = "UPDATE LienHe SET Reply='TRUE' WHERE Id=" + IdLienhe.ToString();
            return db.exe(sql);
        }
        // đánh dấu đã gửi email thành công
        public bool UpRepEmail(int IdLienhe)
        {
            string sql = "UPDATE LienHe SET SendEmail='TRUE' WHERE Id=" + IdLienhe.ToString();
            return db.exe(sql);
        }
        // thêm tình trạng gửi email
        public bool UpErorEmail(int IdLienhe, string ErrorEmail)
        {
            string sql = "UPDATE LienHe SET ErrorEmail='" + ErrorEmail + "' WHERE Id=" + IdLienhe.ToString();
            return db.exe(sql);
        }
        // xóa liên hệ
        public bool Del_LienHe(int id)
        {
            string sql = "DELETE FROM LienHe WHERE Id=" + id;
            return db.exe(sql);
        }
        // đếm liên hệ chưa trả lời
        public int SoLienHe_NoRep()
        {
            string sql = "SELECT * FROM LienHe WHERE Reply='False' or SendEmail='False'";
            int Dem = db.myTable(sql).Rows.Count;
            return Dem;
        }
    }
}
