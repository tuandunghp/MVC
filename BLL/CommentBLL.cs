using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;
namespace BLL
{
    public class CommentBLL
    {
        DataService db = new DataService();
        public CommentBLL() { }
        // tất cả comment trừ comment spam
        public DataTable Comment_All()
        {
            return db.myTable_sp("sp_Sel_Comment_All");
        }
        // tất cả Repcomment trừ Repcomment spam
        public DataTable RepComment_All()
        {
            return db.myTable_sp("sp_Sel_RepCommemt_All");
        }
        public DataTable Comment_True(int IdArticle)
        {
            SqlParameter p1 = new SqlParameter("@IdArticle", IdArticle);
            return db.myTable_sp("sp_Sel_Comment",p1);
        }
        public DataTable LoadComment_Article(int IdArticle, int NumberLoad)
        {
            SqlParameter p1 = new SqlParameter("@IdArticle", IdArticle);
            SqlParameter p2 = new SqlParameter("@NumberLoad", NumberLoad);
            return db.myTable_sp("sp_Sel_Comment_Numberload", p1,p2);
        }
        // comment mới nhất
        public DataTable Comment_True_Desc(int IdArticle, int NumberLoad)
        {
            SqlParameter p1 = new SqlParameter("@IdArticle", IdArticle);
            SqlParameter p2 = new SqlParameter("@NumberLoad", NumberLoad);
            return db.myTable_sp("sp_Sel_Comment_Desc", p1,p2);
        }
        // load RepComment
        public DataTable RepCOmmnet(int IdCommnet, int NumberLoad)
        {
            SqlParameter p1 = new SqlParameter("@IdComment", IdCommnet);
            SqlParameter p2 = new SqlParameter("@NumberLoad", NumberLoad);
            return db.myTable_sp("sp_sel_RepComment", p1,p2);
        }
        // load comment theo bài viết
        public DataTable Comment_IdArticle(int IdArticle)
        {
            SqlParameter p1 = new SqlParameter("@IdArticle", IdArticle);
            return db.myTable_sp("sp_Sel_Comment_IdArticle", p1);
        }
        // load Repcomment theo bài viết
        public DataTable RepComment_IdArticle(int IdArticle)
        {
            SqlParameter p1 = new SqlParameter("@IdArticle", IdArticle);
            return db.myTable_sp("sp_Sel_RepComment_IdArticle", p1);
        }
        // load comment chưa xử lý
        public DataTable Comment_ChuaDuyet(int Duyet)
        {
            SqlParameter p0 = new SqlParameter("@DaDuyet", Duyet);
            return db.myTable_sp("sp_sel_Comment_DaDuyet",p0);
        }
        // load Repcomment chưa xử lý
        public DataTable RepComment_ChuaDuyet(int Duyet)
        {
            SqlParameter p0 = new SqlParameter("@DaDuyet", Duyet);
            return db.myTable_sp("sp_sel_RepComment_DaDuyet",p0);
        }
        //load spam bảng comments
        public DataTable LoadSpamComments()
        {
            return db.myTable_sp("sp_Sel_LoadSpam_Comments");
        }
        //load spam bảng RepComments
        public DataTable LoadSpamRepComments()
        {
            return db.myTable_sp("sp_sel_LoadSpam_RepComments");
        }
        public bool Ins(string FullName, string Email, string Comment, string postDate, int IdArticle,int Duyet,string WebSite,string Image)
        {

            SqlParameter p1 = new SqlParameter("@FullName", FullName);
            SqlParameter p2 = new SqlParameter("@Email", Email);
            SqlParameter p3 = new SqlParameter("@Comment", Comment);
            SqlParameter p4 = new SqlParameter("@PostDate", postDate);
            SqlParameter p5 = new SqlParameter("@IdArticle", IdArticle);
            SqlParameter p6 = new SqlParameter("@Daduyet", Duyet); // tham số 0 là chưa duyệt, 1 đã duyệt, 2 là chờ duyệt
            //SqlParameter p7 = new SqlParameter("@IpComment", IpComment);
            SqlParameter p8 = new SqlParameter("@WebSite", WebSite);
            SqlParameter p9 = new SqlParameter("@Images", Image);
            return db.exe_sp("[dbo].[sp_Ins_Comment_Article]", p1, p2, p3, p4, p5,p6,p8,p9);
        }
        // thêm RepComment
        public bool InsRepCm(string FullName, string Email, string RepComment, string postDate, int IdArticle,int Duyet, string IdComment,string Web,string Images)
        {

            SqlParameter p1 = new SqlParameter("@FullName", FullName);
            SqlParameter p2 = new SqlParameter("@Email", Email);
            SqlParameter p3 = new SqlParameter("@RepComment", RepComment);
            SqlParameter p4 = new SqlParameter("@PostDate", postDate);
            SqlParameter p5 = new SqlParameter("@IdArticle", IdArticle);
            SqlParameter p6 = new SqlParameter("@Daduyet", Duyet); // tham số 0 là chưa duyệt, 1 đã duyệt, 2 là chờ duyệt
            SqlParameter p7 = new SqlParameter("@IdComment", IdComment);
            SqlParameter p8 = new SqlParameter("@WebSite", Web);
            SqlParameter p9 = new SqlParameter("@Images", Images);
            return db.exe_sp("[dbo].[sp_ins_RepCommnent]", p1, p2, p3, p4, p5,p6, p7,p8,p9);
        }
        // Comment chưa trả lời và không bị spam
        public int SoComment_NoReply()
        {
            string sql = "select Id from Comment where Spam != 1 AND (DaDuyet = 0 or DaDuyet is NULL)";
            int Dem = db.myTable(sql).Rows.Count;
            return Dem;

        }
        // RepComment chưa trả lời và không bị spam
        public int SoRepComment_NoReply()
        {
            string sql = "select Id from RepComment where Spam != 1 AND (DaDuyet = 0 or DaDuyet is NULL)";
            int Dem = db.myTable(sql).Rows.Count;
            return Dem;
        }
        // lấy số Rep-Comments đã trả lời cho 1 bình luận chính
        public DataTable Rep_Commets_WhereIdComments(int IdComments)
        {
            string sql = "SELECT * FROM RepComment WHERE IdComment=" + IdComments.ToString();
            DataTable dt = db.myTable(sql);
            return dt;
        }
        // đếm số comment chưa kiểm duyệt
        public int SoComment()
        {
            string sql = "select Id from Comment where DaDuyet = 0 or DaDuyet is NULL";
            string sql1 = "select Id from RepComment where DaDuyet = 0 or DaDuyet is NULL";
            int Cm = db.myTable(sql).Rows.Count;
            int RepCm = db.myTable(sql1).Rows.Count;
            int tong = Cm + RepCm;
            return tong;

        }
        // đếm số comment theo Id bài viết
        public int CountComment(int IdArticle)
        {
            SqlParameter p1 = new SqlParameter("@IdArticle", IdArticle);
            SqlParameter p2 = new SqlParameter("@IdArticle", IdArticle);
            int Cm = db.myTable_sp("sp_Sel_Comment_IdArticle", p1).Rows.Count;
            int RepCm = db.myTable_sp("sp_Sel_RepComment_IdArticle", p2).Rows.Count;
            int tong = Cm + RepCm;
            return tong;
        }
        // trả về string nếu cm dài
        public string ReturnNumberCm(float Comment)
        {
            string SoCM = "";
            try
            {
                if (Comment <= 1000)
                    SoCM = Comment.ToString();
                else if (Comment > 100000000)
                {
                    string k = (Comment / 1000000).ToString(); // lấy phần dư
                    string d = (Comment % 1000000).ToString();
                    if (d == "0") // nếu dư =0 thì lấy số đó luôn
                        SoCM = k + "Tr";
                    else // nếu có dư thì lấy 5 kí tự đầu
                        SoCM = k.Substring(0, 5) + "Tr";
                }
                else if (Comment > 1000000)
                {
                    string k = (Comment / 1000000).ToString();
                    string d = (Comment % 1000000).ToString();
                    if (d == "0")
                        SoCM = k + "Tr";
                    else
                        SoCM = k.Substring(0, 4) + "Tr";
                }
                else if (Comment > 100000)
                {
                    string d = (Comment % 1000).ToString();
                    string k = (Comment / 1000).ToString();
                    if (d == "0")
                        SoCM = k + "K";
                    else
                        SoCM = k.Substring(0, 5) + "K";
                }
                else if (Comment > 10000)
                {
                    string d = (Comment % 1000).ToString();
                    string k = (Comment / 1000).ToString();
                    if (d == "0")
                        SoCM = k + "K";
                    else
                        SoCM = k.Substring(0, 4) + "K";
                }
                else if (Comment > 1000)
                {
                    string k = (Comment / 1000).ToString();
                    string d = (Comment % 1000).ToString();
                    if (d == "0")
                        SoCM = k + "K";
                    else
                        SoCM = k.Substring(0, 3) + "K";
                }
                else
                    SoCM = Comment.ToString();
            }
            catch (ArgumentOutOfRangeException)
            {
                SoCM = Comment.ToString();
            }
            return SoCM;
        }
        // đếm số comment chính theo Id bài viết
        public int CountCommentChinh(int IdArticle)
        {
            SqlParameter p1 = new SqlParameter("@IdArticle", IdArticle);
            int Cm = db.myTable_sp("sp_Sel_Comment_IdArticle", p1).Rows.Count;
            return Cm;
        }
        // Đếm số RepComment theo Id Comment chính
        public int CountRepComment_WhereIdComment(int IdComment)
        {
            string sql = "SELECT * FROM RepComment WHERE IdComment=" + IdComment.ToString();
            int SoCm = db.myTable(sql).Rows.Count;
            return SoCm;
        }
        public bool Upd(int id, bool IsShow)
        {
            SqlParameter p0 = new SqlParameter("@id", id);
            SqlParameter p1 = new SqlParameter("@IsShow",IsShow);
            return db.exe_sp("sp_Upd_Comment", p0, p1);
        }
        // xóa các bình luận theo bài viết IdArticle
        public bool Del_Comment_IdArticle(int id)
        {
            SqlParameter p1 = new SqlParameter("@IdArticle", id);
            return db.exe_sp("sp_Del_Comment_IdArticle", p1);
        }
        // xóa các trả lời bình luận RepComment theo bài viết IdArticle
        public bool Del_RepComment_IdArticle(int id)
        {
            SqlParameter p1 = new SqlParameter("@IdArticle", id);
            return db.exe_sp("sp_Del_RepComment_IdArticle", p1);
        }
        public bool Del_Comment(int id)
        {
            SqlParameter p1 = new SqlParameter("@id", id);
            return db.exe_sp("sp_del_Comment", p1);
        }
        // xóa RepComment theo IdComment
        public bool Del_MultiRepComment(int id)
        {
            SqlParameter p1 = new SqlParameter("@IdComment", id);
            return db.exe_sp("sp_Del_MultiRepComment", p1);
        }
        // del RepComment
        public bool Del_RepComment(int id)
        {
            SqlParameter p1 = new SqlParameter("@id", id);
            return db.exe_sp("sp_del_RepComment", p1);
        }
        // duyet comment 
        public bool Duyet_Comment()
        {
            return db.exe_sp("sp_Up_Comment_daduyet");
        }
        // duyet comment theo id
        public bool Duyet_Comment_id(int id, int Duyet)
        {
            SqlParameter p0 = new SqlParameter("@Id", id);
            SqlParameter p1 = new SqlParameter("@Daduyet", Duyet);
            return db.exe_sp("sp_up_CmDaduyet_Id",p0,p1);
        }
        // duyet Rep-comment theo id
        public bool Duyet_Rep_Comment_id(int id, int Duyet)
        {
            SqlParameter p0 = new SqlParameter("@Id", id);
            SqlParameter p1 = new SqlParameter("@Daduyet", Duyet);
            return db.exe_sp("sp_Upd_DaDuyet_RepComment", p0,p1);
        }
        // duyet comment theo ActicleID
        public bool Duyet_Comment_ActicleId(int ActicleId)
        {
            SqlParameter p0 = new SqlParameter("@IdArticle", ActicleId);
            return db.exe_sp("sp_up_Cm_IdArticle",p0);
        }
        // duyet Repcomment theo ActicleID
        public bool Duyet_RepComment_ActicleId(int ActicleId)
        {
            SqlParameter p0 = new SqlParameter("@IdArticle", ActicleId);
            return db.exe_sp("sp_up_RepCm_IdArticle",p0);
        }
        // Cập nhật thông tin Người bình luận bảng comment
        public bool Upd_User_Comments(int Id, string FullName, string Comments, string Web, string Email)
        {
            SqlParameter p1 = new SqlParameter("@Id", Id);
            SqlParameter p2 = new SqlParameter("@FullName", FullName);
            SqlParameter p3 = new SqlParameter("@Comment", Comments);
            SqlParameter p4 = new SqlParameter("@Web", Web);
            SqlParameter p5 = new SqlParameter("@Email", Email);
            return db.exe_sp("sp_Upd_EditComments", p1, p2, p3,p4,p5);
        }
        // Cập nhật thông tin Người bình luận bảng Rep-Commet
        public bool Upd_User_RepComments(int Id, string FullName, string Comments, string Web, string Email)
        {
            SqlParameter p1 = new SqlParameter("@Id", Id);
            SqlParameter p2 = new SqlParameter("@FullName", FullName);
            SqlParameter p3 = new SqlParameter("@RepComment", Comments);
            SqlParameter p4 = new SqlParameter("@Web", Web);
            SqlParameter p5 = new SqlParameter("@Email", Email);
            return db.exe_sp("sp_Upd_Edit_RepComments", p1, p2, p3, p4, p5);
        }
        // đánh dấu comment là spam bảng Comment
        public bool Upd_Spam_Comments(int Id, int Spam)
        {
            SqlParameter p1 = new SqlParameter("@Id", Id);
            SqlParameter p2 = new SqlParameter("@Spam", Spam);
            return db.exe_sp("sp_Upd_Spam_Comments", p1, p2);
        }
        // đánh dấu comment là spam bảng Rep-Comment
        public bool Upd_Spam_RepComments(int Id, int Spam)
        {
            SqlParameter p1 = new SqlParameter("@Id", Id);
            SqlParameter p2 = new SqlParameter("@Spam", Spam);
            return db.exe_sp("sp_Upd_Spam_RepComments", p1, p2);
        }
    }
}
