using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;
using System.Text.RegularExpressions;

namespace BLL
{
    public class SettingComment
    {
        DataService db = new DataService();
        basic b = new basic();
        public bool Check_HideShowComment()
        {
            bool check = false;
            DataTable dt = db.myTable("select * from SettingsComment");
            if (dt.Rows.Count > 0) check = bool.Parse(dt.Rows[0]["HideShow"].ToString());
            return check;
        }
        public bool Check_SendEmailComment()
        {
            bool check = false;
            DataTable dt = db.myTable("select * from SettingsComment");
            if (dt.Rows.Count > 0) check = bool.Parse(dt.Rows[0]["SendEmailComment"].ToString());
            return check;
        }
        public bool Check_SendEmailChoDuyet()
        {
            bool check = false;
            DataTable dt = db.myTable("select * from SettingsComment");
            if (dt.Rows.Count > 0) check = bool.Parse(dt.Rows[0]["SendEmailChoDuyet"].ToString());
            return check;
        }
        public bool Check_Login()
        {
            bool check = false;
            DataTable dt = db.myTable("select * from SettingsComment");
            if (dt.Rows.Count > 0) check = bool.Parse(dt.Rows[0]["Login"].ToString());
            return check;
        }
        public bool Check_ActiveAllComment()
        {
            bool check = false;
            DataTable dt = db.myTable("select * from SettingsComment");
            if (dt.Rows.Count > 0) check = bool.Parse(dt.Rows[0]["ActiveAllComment"].ToString());
            return check;
        }
        public bool Check_FacebookComment()
        {
            bool check = false;
            DataTable dt = db.myTable("select * from SettingsComment");
            if (dt.Rows.Count > 0) check = bool.Parse(dt.Rows[0]["FacebookComment"].ToString());
            return check;
        }
        public int Check_NumberShow()
        {
            int check = 5;
            DataTable dt = db.myTable("select * from SettingsComment");
            if (dt.Rows.Count > 0) check = int.Parse(dt.Rows[0]["NumberShow"].ToString());
            return check;
        }
        public int Check_NumberShowRep()
        {
            int check = 5;
            DataTable dt = db.myTable("select * from SettingsComment");
            if (dt.Rows.Count > 0) check = int.Parse(dt.Rows[0]["NumberShowRep"].ToString());
            return check;
        }
        public int Check_StyleAvatar()
        {
            int check = 0;
            DataTable dt = db.myTable("select * from SettingsComment");
            if (dt.Rows.Count > 0) check = int.Parse(dt.Rows[0]["AvatarStyle"].ToString());
            return check;
        }
        public int Check_UserEmail()
        {
            int check = 0;
            DataTable dt = db.myTable("select * from SettingsComment");
            if (dt.Rows.Count > 0) check = int.Parse(dt.Rows[0]["UserEmail"].ToString());
            return check;
        }
        public int Check_ServerEmail()
        {
            int check = 0;
            DataTable dt = db.myTable("select * from SettingsComment");
            if (dt.Rows.Count > 0) check = int.Parse(dt.Rows[0]["ServerEmail"].ToString());
            return check;
        }
        public string Check_StringSpam()
        {
            string check = "";
            DataTable dt = db.myTable("select * from SettingsComment");
            if (dt.Rows.Count > 0) check = dt.Rows[0]["StringSpam"].ToString();
            return check;
        }
        public string Check_EmailSpam()
        {
            string check = "";
            DataTable dt = db.myTable("select * from SettingsComment");
            if (dt.Rows.Count > 0) check = dt.Rows[0]["EmailSpam"].ToString();
            return check;
        }
        public bool KiemTra_StringSpam(string Chuoi)
        {
            string StringSpam = Check_StringSpam(); // lấy chuỗi Spam trong CSDL
            string[] MangSpam = StringSpam.Split(' '); // tách chuỗi thành từng mảng
            bool KtSpam = false;
            foreach (string s in MangSpam)
            {
                KtSpam = Regex.IsMatch(Chuoi, s, RegexOptions.IgnoreCase);// kiểm tra từng mảng trong CSDL trong chuỗi người dùng nhập vào
                if (KtSpam) // nếu có thì dừng lặp
                    break;
            }
            return KtSpam;
        }
        public bool KiemTra_EmailSpam(string Chuoi)
        {
            string StringSpam = Check_EmailSpam(); // lấy chuỗi Spam trong CSDL
            string[] MangSpam = StringSpam.Split(' '); // tách chuỗi thành từng mảng
            bool KtSpam = false;
            foreach (string s in MangSpam)
            {
                KtSpam = String.Compare(Chuoi,s,true)==0;// kiểm tra từng mảng trong CSDL với Email nhập vào xem có giống nhau không
                if (KtSpam) // nếu có thì dừng lặp
                    break;
            }
            return KtSpam;
        }
        // Lấy bảng settingcomment
        public DataTable GetSettingComment()
        {
            string sql = "SELECT * FROM SettingsComment";
            DataTable dt = db.myTable(sql);
            return dt;
        }
        // lấy bảng SettingSendEmail
        public DataTable GetSettingSendEmail(int Server)
        {
            string sql = "SELECT * FROM SettingSendEmail WHERE Server=" + Server.ToString();
            DataTable dt = db.myTable(sql);
            return dt;
        }
        // cập nhật settingcomment
        public bool Upd_Setting_Comments(bool HideShow, bool FacebookComment, int NumberShow, int NumberShowRep,
            bool Login, bool ActiveAllComment, bool SendEmailComment, bool SendEmailDaDuyet, int UserEmail, string StringSpam,
            string EmailSpam, int Avatar)
        {
            SqlParameter p1 = new SqlParameter("@HideShow", HideShow);
            SqlParameter p2 = new SqlParameter("@FacebookComment", FacebookComment);
            SqlParameter p3 = new SqlParameter("@NumberShow", NumberShow);
            SqlParameter p4 = new SqlParameter("@NumberShowRep", NumberShowRep);
            SqlParameter p5 = new SqlParameter("@Login", Login);
            SqlParameter p6 = new SqlParameter("@ActiveAllComment", ActiveAllComment);
            SqlParameter p7 = new SqlParameter("@SendEmailComment", SendEmailComment);
            SqlParameter p8 = new SqlParameter("@SendEmailDaDuyet", SendEmailDaDuyet);
            SqlParameter p9 = new SqlParameter("@UserEmail", UserEmail);
            SqlParameter p10 = new SqlParameter("@StringSpam", StringSpam);
            SqlParameter p11 = new SqlParameter("@EmailSpam", EmailSpam);
            SqlParameter p12 = new SqlParameter("@Avatar", Avatar);
            return db.exe_sp("sp_Upd_SettingComment", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);
        }

        // Load SettingEmail
        public DataTable GetSettingEmail_Server(int Server)
        {
            string sql = "SELECT * FROM SettingSendEmail WHERE Server=" + Server.ToString();
            DataTable dt = db.myTable(sql);
            return dt;
        }
        public bool Upd_SettingEmail(string Host, string Email, string Pass, int Port, string BodyFooter, int Server,int Id)
        {
            SqlParameter p1 = new SqlParameter("@Host", Host);
            SqlParameter p2 = new SqlParameter("@EmailSend", Email);
            SqlParameter p3 = new SqlParameter("@Password", Pass);
            SqlParameter p4 = new SqlParameter("@Port", Port);
            SqlParameter p5 = new SqlParameter("@FooterBody", BodyFooter);
            SqlParameter p6 = new SqlParameter("@Server", Server);
            SqlParameter p7 = new SqlParameter("@Id", Id);
            return db.exe_sp("sp_Upd_SettingSendEmail", p1, p2, p3, p4, p5, p6, p7);
        }
        public bool Upd_ServerEmail(int Server)
        {
             SqlParameter p1 = new SqlParameter("@ServerEmail", Server);
             return db.exe_sp("sp_Upd_ServerEmail", p1);
        }
        public DataTable GetSettings()
        {
            string sql = "SELECT * FROM Settings";
            DataTable dt = db.myTable(sql);
            return dt;
        }
        // Update Settings
        public bool Upd_Settings(string Meta, string HTMLCode, string HTMLComment, string HTMLFanPage, bool FanPageFooter, bool FanPageRight)
        {
            SqlParameter p1 = new SqlParameter("@Meta", Meta);
            SqlParameter p2 = new SqlParameter("@HTMLCode", HTMLCode);
            SqlParameter p3 = new SqlParameter("@HTMLComment", HTMLComment);
            SqlParameter p4 = new SqlParameter("@HTMLFanPage", HTMLFanPage);
            SqlParameter p5 = new SqlParameter("@FanPageFooter", FanPageFooter);
            SqlParameter p6 = new SqlParameter("@FanPageRight", FanPageRight);
            return db.exe_sp("sp_Upd_Settings", p1, p2, p3, p4, p5, p6);
        }
        // Check Settings
        public bool Check_FanPageFooter()
        {
            bool check = false;
            DataTable dt = db.myTable("select * from Settings");
            if (dt.Rows.Count > 0) check = bool.Parse(dt.Rows[0]["FanPageFooter"].ToString());
            return check;
        }
        public bool Check_FanPageRight()
        {
            bool check = false;
            DataTable dt = db.myTable("select * from Settings");
            if (dt.Rows.Count > 0) check = bool.Parse(dt.Rows[0]["FanPageRight"].ToString());
            return check;
        }
        // check Fultextsearch đã được cài đặt chưa
        public int CheckFullText(string sql)
        {
            //string sql = "SELECT SERVERPROPERTY('IsFullTextInstalled')";
            DataTable dt = db.myTable(sql);
            return int.Parse(dt.Rows[0][0].ToString());
        }
        // chạy lệnh sql nhập vào
        public bool RunSql(string sql)
        {
            bool kt = false;
            kt = db.exe(sql);
            if (kt)
                kt = true;
            return kt;
        }
    }
}
