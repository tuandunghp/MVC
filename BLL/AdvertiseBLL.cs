using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;
namespace BLL
{
    public class AdvertiseBLL
    {
        DataService db = new DataService();
        public DataTable myAdvertise()
        {
            return db.myTable_sp("sp_Sel_Advertise");
        }
        public DataTable myAdvertise_Image()
        {
            return db.myTable_sp("sp_Sel_Advertise_Image");
        }
        public DataTable myAdvertise_Flash()
        {
            return db.myTable_sp("sp_Sel_Advertise_Flash");
        }
        public DataTable Image_Top4()
        {
            string sql = "SELECT top 3 [Id],[Name],[Image],[Link],[UserId],[CreateDate],[Type]  FROM [DBCNTT].[dbo].[Advertise] where Type='1' order by Id Desc";
            return db.myTable(sql);
        }
        public DataTable myPages_Where(int Where)
        {
            SqlParameter p = new SqlParameter("@id", Where);
            return db.myTable_sp("sp_Sel_Where_Advertise", p);
            
        }
        public bool Ins(string Name, String Image, String Link, int UserId,int Type)
        {
            SqlParameter p1 = new SqlParameter("@Name", Name);
            SqlParameter p2 = new SqlParameter("@Image", Image);
            SqlParameter p3 = new SqlParameter("@Link", Link);
            SqlParameter p4 = new SqlParameter("@UserId", UserId);
            SqlParameter p5 = new SqlParameter("@Type", Type);
            return db.exe_sp("[dbo].[sp_Ins_Advertise]",p1, p2, p3, p4,p5);
        }
        public bool Upd(int id, string Name, String Image, String Link, int UserId,int Type)
        {
            SqlParameter p0 = new SqlParameter("@id", id);
            SqlParameter p1 = new SqlParameter("@Name", Name);
            SqlParameter p2 = new SqlParameter("@Image", Image);
            SqlParameter p3 = new SqlParameter("@Link", Link);
            SqlParameter p4 = new SqlParameter("@UserId", UserId);
            SqlParameter p5 = new SqlParameter("@Type", Type);
            return db.exe_sp("sp_Upd_Advertise", p0, p1, p2, p3, p4,p5);
        }
        public bool Del(int id)
        {
            SqlParameter p0 = new SqlParameter("@id", id);
            return db.exe_sp("sp_Del_Advertise", p0);
        }
    }
}
