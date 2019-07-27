using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    public class SoftwareBLL
    {
        DataService db = new DataService();

        public DataTable Software()
        {
            return db.myTable_sp("sp_Sel_Software");
        }
        public DataTable Software_Id(int id)
        {
            string sql = "SELECT * FROM SoftWare WHERE Id =" + id;
            return db.myTable(sql);
        }
        // Lấy bảng Software theo Type
        public DataTable Software_Type(int Type)
        {
            string sql = "SELECT * FROM SoftWare WHERE Type =" + Type + " ORDER BY Id DESC";
            return db.myTable(sql);
        }
        public bool Upd_ReadNumber(int id, int ReadNumber)
        {
            SqlParameter p1 = new SqlParameter("@Id", id);
            SqlParameter p2 = new SqlParameter("@ReadNumber", ReadNumber);
            return db.exe_sp("sp_Upd_Software_ReadNumber", p1, p2);
        }
        // Chuyển từ byte sang các đại lượng khác
        public string ToFileSize(double value)
        {
            string[] suffixes = { "bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
            for (int i = 0; i < suffixes.Length; i++)
            {
                if (value <= (Math.Pow(1024, i + 1)))
                {
                    return ThreeNonZeroDigits(value / Math.Pow(1024, i)) + " " + suffixes[i];
                }
            }

            return ThreeNonZeroDigits(value / Math.Pow(1024, suffixes.Length - 1)) +
                " " + suffixes[suffixes.Length - 1];
        }

        // Return the value formatted to include at most three
        // non-zero digits and at most two digits after the
        // decimal point. Examples:
        //         1
        //       123
        //        12.3
        //         1.23
        //         0.12
        private static string ThreeNonZeroDigits(double value)
        {
            if (value >= 100)
            {
                // No digits after the decimal.
                return value.ToString("0,0");
            }
            else if (value >= 10)
            {
                // One digit after the decimal.
                return value.ToString("0.0");
            }
            else
            {
                // Two digits after the decimal.
                return value.ToString("0.00");
            }
        }
        public bool Ins(string Name, string Des, String Image, String file, string PostDate, int UserId, int Type, int Size, string Version, string YeuCau, string PhatHanh)
        {

            SqlParameter p2 = new SqlParameter("@Name", Name);
            SqlParameter p3 = new SqlParameter("@Description", Des);
            SqlParameter p4 = new SqlParameter("@ImagePath", Image);
            SqlParameter p5 = new SqlParameter("@FilePath", file);
            SqlParameter p6 = new SqlParameter("@PostDate", PostDate);
            SqlParameter p7 = new SqlParameter("@UserId", UserId);
            SqlParameter p8 = new SqlParameter("@Type", Type);
            SqlParameter p9 = new SqlParameter("@Size", Size);
            SqlParameter p10 = new SqlParameter("@Version", Version);
            SqlParameter p11 = new SqlParameter("@YeuCau", YeuCau);
            SqlParameter p12 = new SqlParameter("@PhatHanh", PhatHanh);

            return db.exe_sp("[dbo].[sp_Ins_Software]", p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12);


        }
        public bool Upd(int id, string Name, string Des, String Image, String file, string LastDate, int Type, int Size, string Version, string YeuCau, string PhatHanh)
        {
            SqlParameter p0 = new SqlParameter("@Id", id);
            SqlParameter p1 = new SqlParameter("@Name", Name);
            SqlParameter p2 = new SqlParameter("@Description", Des);
            SqlParameter p3 = new SqlParameter("@ImagePath", Image);
            SqlParameter p4 = new SqlParameter("@FilePath", file);
            SqlParameter p5 = new SqlParameter("@LastDate", LastDate);
            SqlParameter p6 = new SqlParameter("@Type", Type);
            SqlParameter p7 = new SqlParameter("@Size", Size);
            SqlParameter p8 = new SqlParameter("@Version", Version);
            SqlParameter p9 = new SqlParameter("@YeuCau", YeuCau);
            SqlParameter p10 = new SqlParameter("@PhatHanh", PhatHanh);

            return db.exe_sp("sp_Upd_SoftWare", p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10);

        }
        public bool Del(int id)
        {
            SqlParameter p0 = new SqlParameter("@Id", id);
            return db.exe_sp("sp_Del_Software", p0);
        }
        public int Get_TypeSoftware_WhereId(int Id)// lấy  Id Thể loại soft theo Id soft
        {
            string sql = "select SoftWare.Type from SoftWare where Id=" + Id.ToString();
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

        // thao tác với bảng SoftwareType
        #region thao tác với bảng VideoType
        public DataTable SoftWareTypeAll()
        {
            string sql = "SELECT * FROM SoftWareType ORDER BY Id DESC";
            return db.myTable(sql);
        }
       
        public int GetUserId_FromSoftwareType(int ListArticleId)// Lấy UserId theo Id
        {
            string sql = "SELECT * FROM SoftWareType WHERE Id=" + ListArticleId.ToString();
            DataTable dt = db.myTable(sql);
            int UserId = 0;
            if (dt.Rows.Count > 0)
            {
                UserId = int.Parse(dt.Rows[0]["UserId"].ToString());
            }
            return UserId;
        }
        public bool Ins_SoftwareType(string tenDS, string Mota, int UserId)
        {
            SqlParameter p = new SqlParameter("@Name", tenDS);
            SqlParameter p1 = new SqlParameter("@Description", Mota);
            SqlParameter p2 = new SqlParameter("@UserId", UserId);
            return db.exe_sp("sp_Ins_SoftwareType", p, p1, p2);
        }
        public bool Up_SoftwareType(int Id, string Name, string Mota)
        {
            SqlParameter p1 = new SqlParameter("@Id", Id);
            SqlParameter p2 = new SqlParameter("@Name", Name);
            SqlParameter p3 = new SqlParameter("@Description", Mota);
            return db.exe_sp("sp_Und_SoftwareType", p1, p2, p3);
        }
        public bool Del_Ds(int Id)
        {
            string sql = "DELETE SoftWareType WHERE Id=" + Id;
            return db.exe(sql);
        }

        #endregion
    }
}
