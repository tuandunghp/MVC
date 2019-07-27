using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

using DAL;
namespace BLL
{
    public class PagesBLL
    {
        DataService db = new DataService();
        public DataTable myPages()
        {
            return db.myTable_sp("sp_Sel_Pages");
        }
        public DataTable myPages_Where(int Where)
        {
            SqlParameter p = new SqlParameter("@where", Where);
            return db.myTable_sp("sp_Sel_Where_Pages", p);
        }
        public DataTable Page_id(int Where)
        {
            SqlParameter p = new SqlParameter("@where", Where);
            return db.myTable_sp("sp_Sel_Pages_Id", p);
        }
        public bool Exist_position(int Where,int Menu)
        {
            bool kt = false;
            SqlParameter p = new SqlParameter("@where", Where);
            SqlParameter p1 = new SqlParameter("@Menu", Menu);

            if( db.myTable_sp("sp_Sel_Exist_Pages", p,p1).Rows.Count>0) kt=true;
            return kt;
        }
        public bool Ins(int menuid,string category, string discription, string page, int position, string LinkUrl)
        {
            SqlParameter p0 = new SqlParameter("@Menu", menuid);

            SqlParameter p1 = new SqlParameter("@category", category);
            SqlParameter p2 = new SqlParameter("@discription", discription);
            SqlParameter p3 = new SqlParameter("@page", page);
            SqlParameter p4 = new SqlParameter("@position", position);
            SqlParameter p5 = new SqlParameter("@LinkUrl", LinkUrl);
           return  db.exe_sp("[dbo].[sp_Ins_Pages]",p0, p1, p2, p3, p4, p5);

           
        }
        public bool Upd(int id,int menuid, string category, string discription, string page, int position, string LinkUrl)
        {
            SqlParameter p0 = new SqlParameter("@id", id);
            SqlParameter p01 = new SqlParameter("@Menu", menuid);
            SqlParameter p1 = new SqlParameter("@Category", category);
            SqlParameter p2 = new SqlParameter("@Discription", discription);
            SqlParameter p3 = new SqlParameter("@Page", page);
            SqlParameter p4 = new SqlParameter("@position", position);
            SqlParameter p5 = new SqlParameter("@LinkUrl", LinkUrl);
            return db.exe_sp("sp_Upd_Pages", p0,p01, p1, p2, p3, p4, p5);

        }
        public bool Del(int id)
        {
            SqlParameter p0 = new SqlParameter("@id", id);
            return db.exe_sp("sp_Del_Pages", p0);
        }
        public static string loadMenu()
        {
            DataService db = new DataService();
            DataTable dt = db.myTable_sp("sp_Sel_Menu");
            string s = "";
            if(dt.Rows.Count>0)
            {
                 s+="<div class=\"menu\">";
                 s += "<ul class=\"mainmenu\">";
                 for (int i = 0; i < dt.Rows.Count; i++)
                 {
                     if (i <2)
                     {
                         s += string.Format("<li> <a href=\"{0}\">{1}</a>", dt.Rows[i]["Link"].ToString(), dt.Rows[i]["Menu"].ToString());

                     }
                     else
                     ////if (i == 1)
                     ////    {
                     ////        VideoBLL vd = new VideoBLL();
                     ////        DataTable dtVideo = vd.Videos();
                     ////        s += string.Format("<li> <a href=\"{0}?vd={2}\">{1}</a>", dt.Rows[i]["Link"].ToString(), dt.Rows[i]["Menu"].ToString(),dtVideo.Rows[0]["Id"].ToString());

                     ////    }
                     //else
                         {
                             s += string.Format("<li> <a href=\"{0}?m={2}&c=0&page=1\">{1}</a>", dt.Rows[i]["Link"].ToString(), dt.Rows[i]["Menu"].ToString(), dt.Rows[i]["Id"].ToString());

                         }
                     s += "</li>";

                 }
                 s += "</ul>";
                 s += "</div>";
            }
           return s;

        }
        public static string loadMenuAll()
        {
            DataService db = new DataService();
            basic b = new basic();
            DataTable dt = db.myTable_sp("sp_Sel_Menu");
           // DataTable dt1 = db.myTable_sp();
            string s = "";
            if (dt.Rows.Count > 0)
            {
                s += "<div class=\"menuTop\">";
                s += "<ul id=\"nav\" class=\"mainmenuTop\">";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (i < 2)
                    {
                        s += string.Format("<li> <a href=\"{0}\">{1}</a>", dt.Rows[i]["Link"].ToString(), dt.Rows[i]["Menu"].ToString());

                    }
                    else
                    {
                        s += string.Format("<li> <a href=\"{0}?m={2}&c=0&page=1\">{1}</a>", dt.Rows[i]["Link"].ToString(), dt.Rows[i]["Menu"].ToString(), dt.Rows[i]["Id"].ToString());

                    }
                    SqlParameter p = new SqlParameter("@where",b.toInt(dt.Rows[i]["Id"].ToString()));
                    DataTable dt1= db.myTable_sp("sp_Sel_Where_Pages", p);
                    if (dt1.Rows.Count > 0)
                    {
                            s += "<ul id=\"nav1\" class=\"submenuTop\">";
                            for(int j=0; j<dt1.Rows.Count;j++)
                            {
                                s += string.Format("<li> <a href=\"{0}?m={2}&c={3}&page=1\">{1}</a>", dt1.Rows[j]["Page"].ToString(), dt1.Rows[j]["Category"].ToString(), dt.Rows[i]["Id"].ToString(), dt1.Rows[j]["Id"].ToString());
                                  s += "</li>";
                            }
                            s += "</ul>";
                    }
                    s += "</li>";

                }
                s += "</ul>";
                s += "</div>";
                
            }
            return s;

        }
        //public static string loadCategory()
        //{
        //    DataService db = new DataService();
        //    DataTable dt = db.myTable_sp("sp_Sel_Pages");
        //    string s = "";
        //    if (dt.Rows.Count > 0)
        //    {
        //        s += "<div class=\"chuyenmuc\">";
        //        s += "<ul class=\"caccm\">";
        //        for (int i =1; i < dt.Rows.Count; i++)
        //        {
        //            s += string.Format("<li> <a href=\"{0}\">{1}</a>", dt.Rows[i]["page"].ToString(), dt.Rows[i]["Category"].ToString());
        //            s += "</li>";

        //        }
        //        s += "</ul>";
        //        s += "</div>";
        //    }
        //    return s;

        //}
    }
}
