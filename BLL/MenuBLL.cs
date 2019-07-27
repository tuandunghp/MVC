using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;
namespace BLL
{
   public class MenuBLL
    {
       public MenuBLL() { }
       DataService db = new DataService();
       public DataTable Menus()
       {
           return db.myTable_sp("sp_Sel_Menu");
       }
       public DataTable Menus_Where(int Where)
       {
           SqlParameter p = new SqlParameter("@where", Where);
           return db.myTable_sp("sp_Sel_Where_Menu", p);
       }
       public DataTable menu_id(int articleId)
       {
           string sql="select * from vMenu_Article where Id="+articleId.ToString();
           return  db.myTable(sql);
       }
       // lấy menu theo id menu
       public DataTable MenuName(int articleId)
       {
           string sql = "select * from Menu where Id=" + articleId.ToString();
           return db.myTable(sql);
       }
       // select Page name
       public DataTable MenuPage(int articleId)
       {
           string sql = "select * from Pages where Id=" + articleId.ToString();

           return db.myTable(sql);
       }
       public bool Exist_position(int Where)
       {
           bool kt = false;
           SqlParameter p = new SqlParameter("@where", Where);
           if (db.myTable_sp("sp_Sel_Exist_Menu", p).Rows.Count > 0) kt = true;
           return kt;
       }
       public bool Ins(string Menu, string discription, string Link, int position, string Url)
       {
           SqlParameter p1 = new SqlParameter("@Menu", Menu);
           SqlParameter p2 = new SqlParameter("@discription", discription);
           SqlParameter p3 = new SqlParameter("@Link", Link);
           SqlParameter p4 = new SqlParameter("@position", position);
           SqlParameter p5 = new SqlParameter("@Url", Url);
           return db.exe_sp("[dbo].[sp_Ins_Menu]", p1, p2, p3, p4, p5);


       }
       public bool Upd(int id, string Menu, string discription, string Link, int position, string Url)
       {
           SqlParameter p0 = new SqlParameter("@id", id);
           SqlParameter p1 = new SqlParameter("@Menu", Menu);
           SqlParameter p2 = new SqlParameter("@Discription", discription);
           SqlParameter p3 = new SqlParameter("@Link", Link);
           SqlParameter p4 = new SqlParameter("@position", position);
           SqlParameter p5 = new SqlParameter("@Url", Url);
           return db.exe_sp("sp_Upd_Menu", p0, p1, p2, p3, p4, p5);

       }
       public bool Del(int id)
       {
           SqlParameter p0 = new SqlParameter("@id", id);
           return db.exe_sp("sp_Del_Menu", p0);
       }
       public static string loadMenu()
       {
           DataService db = new DataService();
           DataTable dt = db.myTable_sp("sp_Sel_Menu");
           string s = "";
           if (dt.Rows.Count > 0)
           {
               s += "<div class=\"menu\">";
               s += "<ul class=\"mainmenu\">";
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   s += string.Format("<li> <a href=\"{0}\">{1}</a>", dt.Rows[i]["page"].ToString(), dt.Rows[i]["Category"].ToString());
                   s += "</li>";

               }
               s += "</ul>";
               s += "</div>";
           }
           return s;

       }
       public static string loadCategory()
       {
           DataService db = new DataService();
           DataTable dt = db.myTable_sp("sp_Sel_Menu");
           string s = "";
           if (dt.Rows.Count > 0)
           {
               s += "<div class=\"chuyenmuc\">";
               s += "<ul class=\"caccm\">";
               for (int i = 1; i < dt.Rows.Count; i++)
               {
                   s += string.Format("<li> <a href=\"{0}\">{1}</a>", dt.Rows[i]["page"].ToString(), dt.Rows[i]["Category"].ToString());
                   s += "</li>";

               }
               s += "</ul>";
               s += "</div>";
           }
           return s;

       }
    }
}
