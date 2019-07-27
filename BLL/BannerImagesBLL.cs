using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BLL
{
    public class BannerImagesBLL
    {
        DataService db = new DataService();
        basic b = new basic();
        public BannerImagesBLL()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public DataTable mybanner_Top(int NumberTop, int Style)//ok
        {
            SqlParameter p1 = new SqlParameter("@Number", NumberTop);
            SqlParameter p2 = new SqlParameter("Style", Style);
            return db.myTable_sp("sp_sel_Banner_top8",p1,p2);
        }
        public DataTable myShowBanner()// show table bannerimages
        {
            return db.myTable_sp("sp_sel_myshowBanner");
        }
        public DataTable myBannerImage_Where(int Where)
        {
            SqlParameter p = new SqlParameter("@Id", Where);
            return db.myTable_sp("sel_BannerImage_whereId", p);

        }
        public bool Ins(string PKName, string Picture, string Link, int Status,string Mota, string Author, string Animate, int Position)
        {
            SqlParameter p1 = new SqlParameter("@BannerName", PKName);
            SqlParameter p2 = new SqlParameter("@Picture", Picture);
            SqlParameter p3 = new SqlParameter("@Link", Link);
            SqlParameter p4 = new SqlParameter("@Status", Status);
            SqlParameter p5 = new SqlParameter("@Discription", Mota);
            SqlParameter p6 = new SqlParameter("@Author", Author);
            SqlParameter p7 = new SqlParameter("@Animate", Animate);
            SqlParameter p8 = new SqlParameter("@Position", Position);
            return db.exe_sp("sp_ins_BannerImages", p1, p2, p3, p4,p5, p6, p7, p8);

        }
        public bool Upd(int id, string PKName, string Picture, string Link,string Mota, int Status, string Author, string Animate)
        {
            SqlParameter p0 = new SqlParameter("@Id", id);
            SqlParameter p1 = new SqlParameter("@BannerName", PKName);
            SqlParameter p2 = new SqlParameter("@Picture", Picture);
            SqlParameter p3 = new SqlParameter("@Link", Link);
            SqlParameter p4 = new SqlParameter("@Status", Status);
            SqlParameter p5 = new SqlParameter("@Discription", Mota);
            SqlParameter p6 = new SqlParameter("@Author", Author);
            SqlParameter p7 = new SqlParameter("@Animate", Animate);
            return db.exe_sp("sp_Ups_BannerImages", p0, p1, p2, p3, p5, p4, p6, p7);
        }
        public bool Del(int id)
        {
            SqlParameter p = new SqlParameter("@id", id);
            return db.exe_sp("sp_Del_BannerImages", p);
        }
        // Cập nhật Position của id truyền vào là 1 và tăng tất cả Position khác lên 1 đơn vị
        public bool Upd_All_Position(int id)
        {
            bool kt = true;
            string sql_Select = "SELECT Position,Id FROM BannerImage ORDER BY Position";
            DataTable dt = db.myTable(sql_Select);
            // bắt đầu với Position = 2
            int Posi = 2;
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    // Id
                    int IdPosition = int.Parse(dt.Rows[i]["Id"].ToString());
                    // Nếu id đưa vào bằng với id cũ thì update position của id đó bằng 1
                    if (IdPosition == id)
                    {
                        string sql = "UPDATE BannerImage SET Position=1 WHERE Id=" + id;
                        db.myTable(sql);
                        continue;
                    }
                    // Cập nhật lại tất cả Position bắt đầu từ 2
                    string sql_Update = "UPDATE BannerImage SET Position=" + Posi + " WHERE Id=" + IdPosition;
                    db.myTable(sql_Update);
                    Posi++;
                }
            }
            return kt;
                 
        }
        // lấy Id lớn nhất trong bảng bannerimage
        public int MaxId()
        {
            int ReInt = 0;
            string sql = "SELECT Id FROM BannerImage ORDER BY Id DESC";
            DataTable dt = db.myTable(sql);
            if (dt.Rows.Count > 0)
                ReInt = int.Parse(dt.Rows[0][0].ToString());
            return ReInt;
        }
        public int SettingsBanner()
        {
            string sql = "SELECT * FROM Settings";
            int Style = 0;
            DataTable dt = db.myTable(sql);
            if (dt.Rows.Count > 0)
                Style = b.toInt(dt.Rows[0]["StyleBannerImage"].ToString());
            return Style;
        }
        public bool UpStyleBanner(string Style)
        {
            string sql = "UPDATE Settings SET StyleBannerImage=" + Style;
            return db.exe(sql);
        }
        public bool UpNumberMaxBanner(string Number)
        {
            string sql = "UPDATE Settings SET NumberMaxTop=" + Number;
            return db.exe(sql);
        }
        // lấy số NumberMax
        public int GetNumberMax()
        {
            string sql = "SELECT Settings.NumberMaxTop FROM Settings";
            DataTable dt = db.myTable(sql);
            int Number = b.toInt(dt.Rows[0][0].ToString());
            return Number;
        }
    }
    
}
