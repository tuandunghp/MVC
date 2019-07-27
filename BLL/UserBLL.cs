using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using DAL;
namespace BLL
{
  public  class UserBLL
    {
      DataService db = new DataService();
      basic b = new basic();
      DecryptBLL de = new DecryptBLL();
      public bool exist(string name, string pass)
      { 
          bool kt=false;
          SqlParameter n = new SqlParameter("@LoginName",name);
          SqlParameter p = new SqlParameter("Password",pass);
          string mahoa = de.Encrypt(pass, true);//mã hóa mật khâu nhập vào
          SqlParameter pas = new SqlParameter("Password", mahoa);
          DataTable dt = db.myTable_sp("[dbo].[sp_Sel_Where_Users]", n, pas);
          if (dt.Rows.Count > 0) kt = true;
          return kt;
      }
      // truyền vào login name trả về true hoặc false
      public bool exist_name(string name)
      {
          bool kt = false;
          SqlParameter n = new SqlParameter("@LoginName", name);
          DataTable dt = db.myTable_sp("[dbo].[sp_Sel_WhereName_Users]", n);
          if (dt.Rows.Count > 0) kt = true;
          return kt;
      }
      // truyền vào login name trả về Id
      public int Id(string name)
      {
         int i=0;
          SqlParameter n = new SqlParameter("@LoginName", name);
          DataTable dt = db.myTable_sp("[dbo].[sp_Sel_WhereName_Users]", n);
          if (dt.Rows.Count > 0)
              i =b.toInt(dt.Rows[0][0].ToString());
          return i;
      }
      // truyền vào login name trả về Role - quyền
      public int Role(string name)
      {
          int i = 0;
          SqlParameter n = new SqlParameter("@LoginName", name);
          DataTable dt = db.myTable_sp("[dbo].[sp_Sel_WhereName_Users]", n);
          if (dt.Rows.Count > 0)
              i = b.toInt(dt.Rows[0]["UserRole"].ToString());
          return i;
      }
      // truyền vào login name trả về FullName
      public string FullName(string LoginName)
      {
          string ten = "";
          
          DataTable dt = db.myTable("Select * from users where LoginName='"+LoginName+"'");
          if (dt.Rows.Count > 0)
              ten =(dt.Rows[0]["FullName"].ToString());
          return ten;
      }
      // lấy bảng user theo email
      public DataTable myUsers_Where_Email(string Email)
      {
          SqlParameter p = new SqlParameter("@Email", Email);
          return db.myTable_sp("sp_sel_User_where_Email", p);
      }
      // lấy bảng user
      public DataTable myUsers()
      {
          return db.myTable_sp("sp_Sel_Users");
      }
      // lấy user theo role
      public DataTable myUser_Role(int role)
      {
          string sql = "";
          sql += string.Format("select * from Users where UserRole="+ role.ToString());
          return db.myTable(sql);
      }
      public DataTable myUser_Role_1_2()
      {
          string sql = "";
          sql += string.Format("select * from Users where UserRole=1 or UserRole=2");
          return db.myTable(sql);
      }
      // lấy những User mà trong bảng Setting User có EmailComment = 1
      public DataTable mySettingUserEmail()
      {
          string sql = "";
          sql += string.Format("select Users.*, SettingsUser.EmailComment as EmailComment from Users INNER JOIN SettingsUser on Users.UserId = SettingsUser.UserId where EmailComment=1");
          return db.myTable(sql);
      }
      public DataTable myUser_NotRole1000()
      {
          string sql = "";
          sql += string.Format("select * from Users where UserRole !='1000'");
          return db.myTable(sql);
      }
      // truyền vào login name trả về thông tin user name đó
      public DataTable myUsersName(string name)
      {
          string sql = "";
          sql += string.Format("select * from Users where LoginName='" + name + "'");
          return db.myTable(sql);
      }
      // truyền vào Id  trả về thông tin Id đó
      public DataTable user(int id)
      {
          SqlParameter p = new SqlParameter("@id",id);
          return db.myTable_sp("sp_Sel_WhereID_Users",p);
      }
      // Stop Account
      public bool StopAccount(int Id, int Value)
      {
          SqlParameter p1 = new SqlParameter("Id", Id);
          SqlParameter p2 = new SqlParameter("Value", Value);
          return db.exe_sp("StopAccount", p1, p2);
      }
      // trả về Id mới nhất vừa được thêm vào
      public int IdNew()
      {
          int UserId = 0;

          DataTable dt = db.myTable("select top 1* from Users order by UserId desc");
          if (dt.Rows.Count > 0)
              UserId = b.toInt(dt.Rows[0]["UserId"].ToString());
          return UserId;
      }
      public bool Del(int id)
      {
          SqlParameter p = new SqlParameter("@id", id);
          return db.exe_sp("sp_Del_Users",p);
      }
      //Setting bảng
      // kiểm tra xem User này đã có trong bảng Setting chưa
      public bool KTUser_Settings(int UserId)
      {
          bool kt = false;
          DataTable dt = db.myTable("select * from SettingsUser where UserId='" + UserId + "'");
          if (dt.Rows.Count > 0)
              kt = true;
          return kt;
      }
      // lấy bảng UserSetting theo UserId
      public DataTable Get_SettingUser_byUser(int UserId)
      {
          string sql = "";
          sql += string.Format("select * from SettingsUser where UserId='" + UserId + "'");
          return db.myTable(sql);
      }
      // thêm bảng settings
      public bool Ins_Settings(int cbBaiViet, int cbSuaBaiViet, int cbXoaBaiViet, int cbKhongXoaBV, int UserId, int cbQuangCao, int cbBinhLuan, int cbBannerImage, int cbMenu, int cbXoaMenu, int cbPage, int cbXoaPage, int cbUser, int cbXoaUser, int cbEmailComment, int cbQLSettings, int cbThongBao, int cbVideos, int cbSoft, int cbLienHe)
      {
          SqlParameter p1 = new SqlParameter("@QlArticle", cbBaiViet);
          SqlParameter p2 = new SqlParameter("@QLQuangCao", cbQuangCao);
          SqlParameter p3 = new SqlParameter("@QLComment", cbBinhLuan);
          SqlParameter p4 = new SqlParameter("@QLBannerImage", cbBannerImage);
          SqlParameter p5 = new SqlParameter("@QLMenu", cbMenu);
          SqlParameter p6 = new SqlParameter("@QLPage", cbPage);
          SqlParameter p7 = new SqlParameter("@QLUser", cbUser);
          SqlParameter p8 = new SqlParameter("@UserId", UserId);
          SqlParameter p9 = new SqlParameter("@EditArticleMe", cbSuaBaiViet);
          SqlParameter p10 = new SqlParameter("@DelArticleMe", cbXoaBaiViet);
          SqlParameter p11 = new SqlParameter("@NotDelArticle", cbKhongXoaBV);
          SqlParameter p12 = new SqlParameter("@NotDelMenu", cbXoaMenu);
          SqlParameter p13 = new SqlParameter("@NotDelPage", cbXoaPage);
          SqlParameter p14 = new SqlParameter("@NotDelUser", cbXoaUser);
          SqlParameter p15 = new SqlParameter("@EmailComment", cbEmailComment);
          SqlParameter p16 = new SqlParameter("@QLSettings", cbQLSettings);
          SqlParameter p17 = new SqlParameter("@QLThongBao", cbThongBao);
          SqlParameter p18 = new SqlParameter("@QLVideos", cbVideos);
          SqlParameter p19 = new SqlParameter("@QLSoftware", cbSoft);
          SqlParameter p20 = new SqlParameter("@QLLienHe", cbLienHe);
          return db.exe_sp("sp_ins_SettingsUser", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20);

      }
      public bool Ins(string fullname, string loginName, string password, string ad, string phone, string email, int role, string Image)
      {
            SqlParameter p1= new SqlParameter("@FullName",fullname);
          SqlParameter p2= new SqlParameter("@LoginName",loginName);
          SqlParameter p3= new SqlParameter("@Password",password);
          SqlParameter p4= new SqlParameter("@Address",ad);
          SqlParameter p5= new SqlParameter("@Phone",phone);
          SqlParameter p6= new SqlParameter("@Email",email);
          SqlParameter p7= new SqlParameter("@UserRole",role);
          SqlParameter p8 = new SqlParameter("@Images", Image);
          return db.exe_sp("sp_Ins_Users",p1,p2,p3,p4,p5,p6,p7,p8);

      }
      public bool Upd(int id,string fullname, string loginName, string password, string ad, string phone, string email,int role,string Image)
      {
          SqlParameter p0=new SqlParameter("@id",id);
           SqlParameter p1 = new SqlParameter("@FullName", fullname);
          SqlParameter p2 = new SqlParameter("@LoginName", loginName);
          SqlParameter p3 = new SqlParameter("@Pass", password);
          SqlParameter p4 = new SqlParameter("@ad", ad);
          SqlParameter p5 = new SqlParameter("@Phone", phone);
          SqlParameter p6 = new SqlParameter("@Email", email);
          //SqlParameter p7 = new SqlParameter("@date", date);
          SqlParameter p8 = new SqlParameter("@role", role);
          SqlParameter p9 = new SqlParameter("@Images", Image);
         return db.exe_sp("sp_Upd_Users",p0, p1, p2, p3, p4, p5, p6,p8,p9);
      }
      public bool Up_Settings(int cbBaiViet, int cbSuaBaiViet, int cbXoaBaiViet, int cbKhongXoaBV, int UserId, int cbQuangCao, int cbBinhLuan, int cbBannerImage, int cbMenu, int cbXoaMenu, int cbPage, int cbXoaPage, int cbUser, int cbXoaUser, int cbEmailComment, int cbQLSettings, int cbThongBao, int cbVideos, int cbSoft, int cbLienHe)
      {
          SqlParameter p1 = new SqlParameter("@QlArticle", cbBaiViet);
          SqlParameter p2 = new SqlParameter("@QLQuangCao", cbQuangCao);
          SqlParameter p3 = new SqlParameter("@QLComment", cbBinhLuan);
          SqlParameter p4 = new SqlParameter("@QLBannerImage", cbBannerImage);
          SqlParameter p5 = new SqlParameter("@QLMenu", cbMenu);
          SqlParameter p6 = new SqlParameter("@QLPage", cbPage);
          SqlParameter p7 = new SqlParameter("@QLUser", cbUser);
          SqlParameter p8 = new SqlParameter("@UserId", UserId);
          SqlParameter p9 = new SqlParameter("@EditArticleMe", cbSuaBaiViet);
          SqlParameter p10 = new SqlParameter("@DelArticleMe", cbXoaBaiViet);
          SqlParameter p11 = new SqlParameter("@NotDelArticle", cbKhongXoaBV);
          SqlParameter p12 = new SqlParameter("@NotDelMenu", cbXoaMenu);
          SqlParameter p13 = new SqlParameter("@NotDelPage", cbXoaPage);
          SqlParameter p14 = new SqlParameter("@NotDelUser", cbXoaUser);
          SqlParameter p15 = new SqlParameter("@EmailComment", cbEmailComment);
          SqlParameter p16 = new SqlParameter("@QLSettings", cbQLSettings);
          SqlParameter p17 = new SqlParameter("@QLThongBao", cbThongBao);
          SqlParameter p18 = new SqlParameter("@QLVideos", cbVideos);
          SqlParameter p19 = new SqlParameter("@QLSoftware", cbSoft);
          SqlParameter p20 = new SqlParameter("@QLLienHe", cbLienHe);
          return db.exe_sp("sp_Up_SettingsUser", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13, p14, p15, p16, p17, p18, p19, p20);

      }
      // Kiểm tra User Name
      public bool Check_Article(int UserId)
      {
          int check = 0;
          DataTable dt = db.myTable("select * from SettingsUser where UserId='" + UserId + "'");
          if (dt.Rows.Count > 0) check = b.toInt(dt.Rows[0]["QLArticle"].ToString());
          if (check == 0) return false;
          else return true;
      }
      public bool Check_EditArticleMe(int UserId)
      {
          int check = 0;
          DataTable dt = db.myTable("select * from SettingsUser where UserId='" + UserId + "'");
          if (dt.Rows.Count > 0) check = b.toInt(dt.Rows[0]["EditArticleMe"].ToString());
          if (check == 0) return false;
          else return true;
      }
      public bool Check_DelArticleMe(int UserId)
      {
          int check = 0;
          DataTable dt = db.myTable("select * from SettingsUser where UserId='" + UserId + "'");
          if (dt.Rows.Count > 0) check = b.toInt(dt.Rows[0]["DelArticleMe"].ToString());
          if (check == 0) return false;
          else return true;
      }
      public bool Check_NotDelArticle(int UserId)
      {
          int check = 0;
          DataTable dt = db.myTable("select * from SettingsUser where UserId='" + UserId + "'");
          if (dt.Rows.Count > 0) check = b.toInt(dt.Rows[0]["NotDelArticle"].ToString());
          if (check == 0) return false;
          else return true;
      }
      public bool Check_QuangCao(int UserId)
      {
          int check = 0;
          DataTable dt = db.myTable("select * from SettingsUser where UserId='" + UserId + "'");
          if (dt.Rows.Count > 0) check = b.toInt(dt.Rows[0]["QLQuangCao"].ToString());
          if (check == 0) return false;
          else return true;
      }
      public bool Check_Comment(int UserId)
      {
          int check = 0;
          DataTable dt = db.myTable("select * from SettingsUser where UserId='" + UserId + "'");
          if (dt.Rows.Count > 0) check = b.toInt(dt.Rows[0]["QLComment"].ToString());
          if (check == 0) return false;
          else return true;
      }
      public bool Check_BannerImages(int UserId)
      {
          int check = 0;
          DataTable dt = db.myTable("select * from SettingsUser where UserId='" + UserId + "'");
          if (dt.Rows.Count > 0) check = b.toInt(dt.Rows[0]["QLBannerImage"].ToString());
          if (check == 0) return false;
          else return true;
      }
      public bool Check_QLMenu(int UserId)
      {
          int check = 0;
          DataTable dt = db.myTable("select * from SettingsUser where UserId='" + UserId + "'");
          if (dt.Rows.Count > 0) check = b.toInt(dt.Rows[0]["QLMenu"].ToString());
          if (check == 0) return false;
          else return true;
      }
      public bool Check_NotDelMenu(int UserId)
      {
          int check = 0;
          DataTable dt = db.myTable("select * from SettingsUser where UserId='" + UserId + "'");
          if (dt.Rows.Count > 0) check = b.toInt(dt.Rows[0]["NotDelMenu"].ToString());
          if (check == 0) return false;
          else return true;
      }
      public bool Check_QLPage(int UserId)
      {
          int check = 0;
          DataTable dt = db.myTable("select * from SettingsUser where UserId='" + UserId + "'");
          if (dt.Rows.Count > 0) check = b.toInt(dt.Rows[0]["QLPage"].ToString());
          if (check == 0) return false;
          else return true;
      }
      public bool Check_NotDelPage(int UserId)
      {
          int check = 0;
          DataTable dt = db.myTable("select * from SettingsUser where UserId='" + UserId + "'");
          if (dt.Rows.Count > 0) check = b.toInt(dt.Rows[0]["NotDelPage"].ToString());
          if (check == 0) return false;
          else return true;
      }
      public bool Check_QLUser(int UserId)
      {
          int check = 0;
          DataTable dt = db.myTable("select * from SettingsUser where UserId='" + UserId + "'");
          if (dt.Rows.Count > 0) check = b.toInt(dt.Rows[0]["QLUser"].ToString());
          if (check == 0) return false;
          else return true;
      }
      public bool Check_NotDelUser(int UserId)
      {
          int check = 0;
          DataTable dt = db.myTable("select * from SettingsUser where UserId='" + UserId + "'");
          if (dt.Rows.Count > 0) check = b.toInt(dt.Rows[0]["NotDelUser"].ToString());
          if (check == 0) return false;
          else return true;
      }
      public bool Check_Settings(int UserId)
      {
          int check = 0;
          DataTable dt = db.myTable("select * from SettingsUser where UserId='" + UserId + "'");
          if (dt.Rows.Count > 0) check = b.toInt(dt.Rows[0]["QLSettings"].ToString());
          if (check == 0) return false;
          else return true;
      }

      public bool Check_ALL(int UserId, string Column)
      {
          int check = 0;
          DataTable dt = db.myTable("select * from SettingsUser where UserId='" + UserId + "'");
          if (dt.Rows.Count > 0) check = b.toInt(dt.Rows[0][Column].ToString());
          if (check == 0) return false;
          else return true;
      }
    }
}
