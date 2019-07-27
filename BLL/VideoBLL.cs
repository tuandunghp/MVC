using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using DAL;
using System.Data;
namespace BLL
{
   public class VideoBLL
    {
       DataService db = new DataService();
       public VideoBLL(){}
       // lấy tất cả video
       public DataTable Videos()
       {
           return db.myTable_sp("sp_Sel_Video");
       }
       // lấy video theo Type
       public DataTable Videos_Type(int type)
       {
           SqlParameter p = new SqlParameter("@Type", type);
           return db.myTable_sp("sp_sel_video_type", p);
       }
       public DataTable Videos_XemNhieu()
       {
           return db.myTable_sp("sp_sel_top40Video");
       }
       // Lấy Top Video, truyền vào số lượng tin muốn lấy
       public DataTable myVideo_Top(int Number)
       {
           SqlParameter p1 = new SqlParameter("@Number", Number);
           return db.myTable_sp("sp_Sel_TopVideo", p1);
       }
       // lấy video theo id
       public DataTable Videos_Id(int id)
       {
           SqlParameter p = new SqlParameter("@Id", id);
           return db.myTable_sp("sp_Sel_Video_Id",p);
       }

       public int Get_TypeVideo_WhereId(int Id)// lấy  Id Thể loại Video theo Id video
       {
           string sql = "select Video.Type from Video where Id=" + Id.ToString();
           DataTable dt = db.myTable(sql);
           int TypeId = 0;
           if (dt.Rows.Count > 0)
           {
               string list = dt.Rows[0][0].ToString();
               if (list != "")
               {
                   TypeId = int.Parse(list);
               }
           }
           return TypeId;
       }
       public bool Upd_ReadVideo(int id, int xem)
       {
           SqlParameter p1 = new SqlParameter("@Id", id);
           SqlParameter p2 = new SqlParameter("@ReadNumber", xem);
           return db.exe_sp("sp_Upd_Video_ReadNumber", p1, p2);


       }
       public bool Ins(string Name, String Image, String Video, string PostDate, int UserId, int Type)
       {

           SqlParameter p1 = new SqlParameter("@Name", Name);
           SqlParameter p2 = new SqlParameter("@ImagePath", Image);
           SqlParameter p3 = new SqlParameter("@VideoPath", Video);
           SqlParameter p4 = new SqlParameter("@PostDate", PostDate);
           SqlParameter p5 = new SqlParameter("@UserId", UserId);
           SqlParameter p6 = new SqlParameter("@Type", Type);

           return db.exe_sp("[dbo].[sp_Ins_Video]", p1, p2, p3, p4, p5, p6);


       }
       public bool Upd(int id, string Name, String Image, String Video, int Type)
       {
           SqlParameter p0 = new SqlParameter("@Id", id);
           SqlParameter p1 = new SqlParameter("@Name", Name);
           SqlParameter p2 = new SqlParameter("@ImagePath", Image);
           SqlParameter p3 = new SqlParameter("@VideoPath", Video);
           SqlParameter p4 = new SqlParameter("@Type", Type);

           return db.exe_sp("sp_Upd_Video", p0, p1, p2, p3, p4);

       }
       public bool Del(int id)
       {
           SqlParameter p0 = new SqlParameter("@Id", id);
           return db.exe_sp("sp_Del_Video", p0);
       }

       // thao tác với bảng VideoType
       #region thao tác với bảng VideoType
       public DataTable VideoTypeAll()
       {
           string sql = "SELECT * FROM VideoType ORDER BY Id DESC";
           return db.myTable(sql);
       }
       
       public int GetUserId_FromVideoType(int ListArticleId)// Lấy UserId theo Id
       {
           string sql = "SELECT * FROM VideoType WHERE Id=" + ListArticleId.ToString();
           DataTable dt = db.myTable(sql);
           int UserId = 0;
           if (dt.Rows.Count > 0)
           {
               UserId = int.Parse(dt.Rows[0]["UserId"].ToString());
           }
           return UserId;
       }
       public bool Ins_VideoType(string tenDS, string Mota, int UserId)
       {
           SqlParameter p = new SqlParameter("@Name", tenDS);
           SqlParameter p1 = new SqlParameter("@Description", Mota);
           SqlParameter p2 = new SqlParameter("@UserId", UserId);
           return db.exe_sp("sp_Ins_VideoType", p, p1, p2);
       }
       public bool Up_VideoType(int Id, string Name, string Mota)
       {
           SqlParameter p1 = new SqlParameter("@Id", Id);
           SqlParameter p2 = new SqlParameter("@Name", Name);
           SqlParameter p3 = new SqlParameter("@Description", Mota);
           return db.exe_sp("sp_Und_VideoType", p1, p2, p3);
       }
       public bool Del_Ds(int Id)
       {
           string sql = "DELETE VideoType WHERE Id=" + Id;
           return db.exe(sql);
       }

       #endregion
    }
}
