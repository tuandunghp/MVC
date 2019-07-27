using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace BLL
{
    public class TagBLL
    {
        DataService db = new DataService();
        // load Tag
        public DataTable LoadTagArticle(int IdArticle)
        {
            SqlParameter p1 = new SqlParameter("@IdArticle", IdArticle);

            return db.myTable_sp("sp_sel_TagArticle", p1);
        }
        // load Tag Id
        public DataTable LoadTagId(int Id)
        {
            SqlParameter p1 = new SqlParameter("@Id", Id);

            return db.myTable_sp("sp_sel_TagId", p1);
        }
        // tìm kiếm
        //public DataTable Tag_search_UnSign(string TieuDe)
        //{
        //    string sql = "";
        //    sql += string.Format("select top 20 * from Article where dbo.fuToUnSign(Title) like '%{0}%'", TieuDe);
        //    return db.myTable(sql);
        //}
        // full text search với 2 cột Title và Discription
        public DataTable Tag_search_UnSign(string TieuDe)
        {
            SqlParameter p1 = new SqlParameter("@Search", TieuDe);

            return db.myTable_sp("sp_Search_FullText_Article", p1);
        }
        // full text search với 1 cột Title
        public DataTable FullTextSearch_Title(string TieuDe)
        {
            SqlParameter p1 = new SqlParameter("@Search", TieuDe);

            return db.myTable_sp("sp_Search_FullText_Article_Title", p1);
        }
        // full text search với 1 cột Discription
        public DataTable FullTextSearch_Discription(string TieuDe)
        {
            SqlParameter p1 = new SqlParameter("@Search", TieuDe);

            return db.myTable_sp("sp_Search_FullText_Article_Discription", p1);
        }
        // thêm Tag
        public bool InsTagArticle(string TagName, string TagUrl, int IdArticle)
        {

            SqlParameter p1 = new SqlParameter("@TagName", TagName);
            SqlParameter p2 = new SqlParameter("@TagUrl", TagUrl);
            SqlParameter p3 = new SqlParameter("@IdArticle", IdArticle);
            return db.exe_sp("[dbo].[sp_ins_TagArticle]", p1, p2,p3);
        }
        public bool Del_Tag(int id)
        {
            SqlParameter p1 = new SqlParameter("@id", id);
            return db.exe_sp("sp_del_tag", p1);
        }
    }
    public class GetSetTag
    {
        string stt;
        string tagName;
        string tagUrl;
        int idArticle;
        public string Stt
        {
            get { return stt; }
            set { stt = value; }
        }
        public string TagName
        {
            get { return tagName; }
            set { tagName = value; }
        }

        public string TagUrl
        {
            get { return tagUrl; }
            set { tagUrl = value; }
        }
        public int IdArticle
        {
            get { return idArticle; }
            set { idArticle = value; }
        }
    }
    public class ListTag
    {
        public List<GetSetTag> List
        {
            get;
            set;
        }
        public void AddTag(GetSetTag tag)
        {
            List.Add(tag);
        }
        public void DeleteTag(string stt)
        {
            for (int i = 0; i < List.Count; i++)
            {
                if (List[i].Stt == stt)
                    List.RemoveAt(i);
            }
        }
    }
}
