using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
//using System.Web.UI.HtmlControls;
//using System.Web.UI;

//using BLL;


namespace BLL
{
   public class ArticleBLL
    {
       DataService db = new DataService();

       #region Hàm sử lý thẻ Meta
       //public void LoadTagMeta(string Title, string Desciption, string Url, string Type, string ImageLink, string SileName, HtmlHead Header)
       //{
           
       //    // meta mạng xã hội propery og:
       //    HtmlMeta TitleOg = new HtmlMeta();
       //    TitleOg.Attributes.Add("property", "og:title");
       //    TitleOg.Content = Title;
       //    Header.Controls.Add(TitleOg);
       //    Header.Controls.Add(new LiteralControl("\n"));// xuống dòng
       //    HtmlMeta DesciptionOg = new HtmlMeta();
       //    DesciptionOg.Attributes.Add("property", "og:description");
       //    DesciptionOg.Content = Desciption;
       //    Header.Controls.Add(DesciptionOg);
       //    Header.Controls.Add(new LiteralControl("\n"));// xuống dòng
       //    HtmlMeta UrlOg = new HtmlMeta();
       //    UrlOg.Attributes.Add("property", "og:url");
       //    UrlOg.Content = Url;
       //    Header.Controls.Add(UrlOg);
       //    Header.Controls.Add(new LiteralControl("\n"));// xuống dòng
       //    HtmlMeta TypeOg = new HtmlMeta();
       //    TypeOg.Attributes.Add("property", "og:type");
       //    TypeOg.Content = Type;
       //    Header.Controls.Add(TypeOg);
       //    Header.Controls.Add(new LiteralControl("\n"));// xuống dòng
       //    HtmlMeta ImageOg = new HtmlMeta();
       //    ImageOg.Attributes.Add("property", "og:image");
       //    ImageOg.Content = ImageLink;
       //    Header.Controls.Add(ImageOg);
       //    Header.Controls.Add(new LiteralControl("\n"));// xuống dòng
       //    HtmlMeta SileNameOg = new HtmlMeta();
       //    SileNameOg.Attributes.Add("property", "og:site_name");
       //    SileNameOg.Content = SileName;
       //    Header.Controls.Add(SileNameOg);
       //    Header.Controls.Add(new LiteralControl("\n"));// xuống dòng
       //    // Meta switter
       //    HtmlMeta SwitterTitle = new HtmlMeta();
       //    SwitterTitle.Attributes.Add("name", "twitter:title");
       //    SwitterTitle.Content = Title;
       //    Header.Controls.Add(SwitterTitle);
       //    Header.Controls.Add(new LiteralControl("\n"));// xuống dòng
       //    HtmlMeta SwitterDes = new HtmlMeta();
       //    SwitterDes.Attributes.Add("name", "twitter:description");
       //    SwitterDes.Content = Desciption;
       //    Header.Controls.Add(SwitterDes);
       //    Header.Controls.Add(new LiteralControl("\n"));// xuống dòng
       //    HtmlMeta SwitterImage = new HtmlMeta();
       //    SwitterImage.Attributes.Add("name", "twitter:image");
       //    SwitterImage.Content = ImageLink;
       //    Header.Controls.Add(SwitterImage);
       //    Header.Controls.Add(new LiteralControl("\n"));// xuống dòng
       //    // meta itemprop
       //    HtmlMeta itempropName = new HtmlMeta();
       //    itempropName.Attributes.Add("itemprop", "name");
       //    itempropName.Content = Title;
       //    Header.Controls.Add(itempropName);
       //    Header.Controls.Add(new LiteralControl("\n"));// xuống dòng
       //    HtmlMeta itempropDes = new HtmlMeta();
       //    itempropDes.Attributes.Add("itemprop", "description");
       //    itempropDes.Content = Desciption;
       //    Header.Controls.Add(itempropDes);
       //    Header.Controls.Add(new LiteralControl("\n"));// xuống dòng
       //    HtmlMeta itempropImage = new HtmlMeta();
       //    itempropImage.Attributes.Add("itemprop", "image");
       //    itempropImage.Content = ImageLink;
       //    Header.Controls.Add(itempropImage);
       //    Header.Controls.Add(new LiteralControl("\n"));// xuống dòng
       //}
       //public void LoadMetaArticle(string ChuyenMuc, string CreateDate, string ModifiedDate, string Author, HtmlHead Header)
       //{
       //    HtmlMeta ArticleSection = new HtmlMeta();
       //    ArticleSection.Attributes.Add("property", "article:section");
       //    ArticleSection.Content = ChuyenMuc;
       //    Header.Controls.Add(ArticleSection);
       //    Header.Controls.Add(new LiteralControl("\n"));// xuống dòng
       //    HtmlMeta ArticleCreateDate = new HtmlMeta();
       //    ArticleCreateDate.Attributes.Add("property", "article:published_time");
       //    ArticleCreateDate.Content = CreateDate;
       //    Header.Controls.Add(ArticleCreateDate);
       //    Header.Controls.Add(new LiteralControl("\n"));// xuống dòng
       //    HtmlMeta ArticleModifiedDate = new HtmlMeta();
       //    ArticleModifiedDate.Attributes.Add("property", "article:modified_time");
       //    ArticleModifiedDate.Content = ModifiedDate;
       //    Header.Controls.Add(ArticleModifiedDate);
       //    Header.Controls.Add(new LiteralControl("\n"));// xuống dòng
       //    HtmlMeta ArticleAuthor = new HtmlMeta();
       //    ArticleAuthor.Attributes.Add("property", "article:author");
       //    ArticleAuthor.Content = Author;
       //    Header.Controls.Add(ArticleAuthor);
       //    Header.Controls.Add(new LiteralControl("\n"));// xuống dòng
       //}

       #endregion


       #region Các hàm lấy Top tin
       //top 6 tin liên quan
       public DataTable LayBaiDaDang(string machuyenmuc, string mabaivet)
       {
           string sql = "select top 6 Id, Title, CreateDate, TitleUrl,Source,ReadNumber,ShowArticle from Article where (CategoryId=" + machuyenmuc + ") and (Id<>" + mabaivet + ") order by Id desc";
           return db.myTable(sql);
       }
       public DataTable Top6TinLienQuan(int PageId, int ArticleId)
       {
           SqlParameter p1 = new SqlParameter("PageId", PageId);
           SqlParameter p2 = new SqlParameter("ArticleId", ArticleId);
           return db.myTable_sp("sp_sel_ArticleTop6_LienQuan", p1, p2);
       }
       public DataTable Top8TinLienQuan(int machuyenmuc, int mabaivet)
       {
           SqlParameter p1 = new SqlParameter("PageId", machuyenmuc);
           SqlParameter p2 = new SqlParameter("ArticleId", mabaivet);
           return db.myTable_sp("sp_sel_Article_Lienquan", p1, p2);
       }

       public int ArticleTop1()
       {
           int ID = 0;
           string sql = "select top 1* from Article order by Id desc";
           DataTable dt = db.myTable(sql);
           if (dt.Rows.Count > 0)
               ID = int.Parse(dt.Rows[0]["Id"].ToString());
           return ID;

       }

       // top 6 tin được mới nhất
       public DataTable myArticle_Top5()
       {
           return db.myTable_sp("sp_Sel_Article_Top5");
       }
       // Top 6 tin mới, lấy theo PageId
       public DataTable myTop6New_WherePage(int PageId)
       {
           SqlParameter p = new SqlParameter("PageId", PageId);
           return db.myTable_sp("sp_sel_Top6New_wherePageId", p);
       }
       // Lấy Top tin mới nhất, truyền vào số lượng tin muốn lấy
       public DataTable myArticle_Top100(int Number)
       {
           SqlParameter p1 = new SqlParameter("@Number", Number);
           return db.myTable_sp("sp_sel_top100NewArticle", p1);
       }

       // top 5 tin được xem nhiều nhất gần đây
       public DataTable myArticle_xemnhieu()
       {
           return db.myTable_sp("sp_Sel_xemnhieu");
       }
       // top 6 tin xem nhiều theo MenuId
       public DataTable MyTop6_Article_WhereMenuId(int MenuId)
       {
           SqlParameter p = new SqlParameter("@MenuId", MenuId);
           return db.myTable_sp("sp_sel_Top6Article_WhereMenu", p);
       }
       // top 6 tin xem nhiều theo PageId
       public DataTable MyTop6_Article_WherePageId(int PageId)
       {
           SqlParameter p = new SqlParameter("@PageId", PageId);
           return db.myTable_sp("sp_sel_Top6_Article_WherePage", p);
       }
       public DataTable myArticle_Top3(int id)
       {
           SqlParameter p = new SqlParameter("@MenuId", id);

           return db.myTable_sp("sp_Sel_Article_MenuTop3", p);
       }
       public DataTable myArticle_Top5tin(int id)
       {
           SqlParameter p = new SqlParameter("@MenuId", id);

           return db.myTable_sp("sp_sel_article_menutop5", p);
       }

       // top 1 tin mới
       public DataTable myArticle_Top1tin(int id)
       {
           SqlParameter p = new SqlParameter("@MenuId", id);
           return db.myTable_sp("sp_sel_top1_Article", p);
       }
       // top 5 tin mới
       public DataTable myArticle_Top5(int id)
       {
           SqlParameter p = new SqlParameter("@MenuId", id);
           return db.myTable_sp("sp_sel_top5_ArticleNo1", p);
       }

       #endregion

       #region Các hàm khác
       // Lấy tất cả bài viết
       public DataTable myArticle()
       {
           return db.myTable_sp("sp_Sel_Article");
       }
       public DataTable myArticle_TieuDiem()
       {
           return db.myTable_sp("sp_Sel_TieuDiem");
       }
       public DataTable sel_thongbao()
       {
           return db.myTable_sp("sp_sel_thongbao");
       }

       public int GetPageID(int articleId)// lấy  pageId theo articleid
       {
           string sql = "select vMenu_Article.CategoryId from vMenu_Article where Id=" + articleId.ToString();
           DataTable dt = db.myTable(sql);
           int PageId = 0;
           if (dt.Rows.Count > 0)
           {
               PageId = int.Parse(dt.Rows[0][0].ToString());
           }
           return PageId;
       }
      

       // đếm số tổng số bài viết
       public int SoBaiViet()
       {
           string sql = "select Id from Article ";
           return db.myTable(sql).Rows.Count;

       }
       
       public DataTable LayTheoTieuDe(int machuyenmuc, string tieude)
       {
          // string sql =string.Format("SELECT [Id]    ,[CategoryId]  ,[UserId]    ,[Title]  FROM [Article] where (CategoryId={0})and (Title=N'{1}')",machuyenmuc,tieude);
           string sq = "SELECT [Id]    ,[CategoryId]  ,[UserId]    ,[Title]  FROM [Article] where (CategoryId=@MaCM)and (Title=@td)";
           SqlParameter[] p = new SqlParameter[2];
           p[0] = new SqlParameter("@MaCM", machuyenmuc);
           p[1] = new SqlParameter("@td", tieude);

           return db.myTable(sq,p);
       }
       public DataTable myArticle_MostReadNumber()
       {
           return db.myTable_sp("sp_Sel_Article_MostRead");
       }

       // lấy bài viết theo Author
       public DataTable sel_Article_Author(int Id)
       {
           SqlParameter p = new SqlParameter("@Id", Id);
           return db.myTable_sp("sp_sel_Article_AuthorId", p);
       }
       public DataTable aticle_Category(int id)
       {
           SqlParameter p= new SqlParameter("@CategoryId",id);
           return db.myTable_sp("[dbo].[sp_Sel_Category_Article]",p);
       }
       
        public DataTable aticle_Where(int CategoryID)
       {
           SqlParameter p= new SqlParameter("@CategoryId",CategoryID);
           return db.myTable_sp("[dbo].[sp_Sel_Article_CategoryID]", p);
       }
       // lấy bảng article
        public DataTable aticle_Where_Id(int Id)
        {
            SqlParameter p = new SqlParameter("@Id", Id);
            return db.myTable_sp("[dbo].[sp_Sel_Article_Id]", p);
        }
       // lấy bảng article, Category - Page bảng Pages, Id - LoginName - FullName bảng Users
        public DataTable Select_aticle_Where_Id(int Id)
        {
            SqlParameter p = new SqlParameter("@Id", Id);
            return db.myTable_sp("[dbo].[sp_select_Article_Id]", p);
        }
        public DataTable aticle_Where_Menu(int menuid)
        {
            SqlParameter p = new SqlParameter("@MenuId",menuid);
            return db.myTable_sp("[dbo].[sp_Sel_Article_Menu]", p);
        }
       // select top 6 menu Id
        public DataTable Top_Article_menu(int menuid)
        {
            SqlParameter p = new SqlParameter("@MenuId", menuid);
            return db.myTable_sp("[dbo].[sp_selTop_MunuId]", p);
        }
        public DataTable aticle_Where_Pages(int PagesId)
        {
            SqlParameter p = new SqlParameter("@PagesId", PagesId);
            return db.myTable_sp("[dbo].[sp_Sel_Article_Pages]", p);
        }

        public bool Upd_ReadNumber(int id, int ReadNumber)
        {
            SqlParameter p1 = new SqlParameter("@id", id);
            SqlParameter p2 = new SqlParameter("@ReadNumber", ReadNumber);
            return db.exe_sp("sp_Upd_Article_ReadNumber", p1, p2);
        }
        public DataTable AddressHome()
        {
            return db.myTable_sp("sp_sel_IpAdressHome");
        }

        // xóa ip truy cập thep ngày
        public bool DelDate(string Date)
        {
            SqlParameter p1 = new SqlParameter("@CreateDate", Date);
            return db.exe_sp("sp_Del_IpAddress", p1);
        }
        public DataTable sel_ucControl()
        {
            return db.myTable_sp("sp_sel_UcControl");
        }

       #endregion

        #region Sử lý phân trang
        public DataSet myDataSet_Menu(int TrangHienTai, int SoDongCuaMotTRang, int SoTrangCanPhan, int MenuId)
        {
            DataSet dts = new DataSet();
            SqlParameter[] arrParam = {
                                        new SqlParameter("@currPage", SqlDbType.Int),
                                        new SqlParameter("@recodperpage", SqlDbType.Int),
                                        new SqlParameter("@Pagesize", SqlDbType.Int),
                                        new SqlParameter("@MenuId", SqlDbType.Int)
                                        };
            arrParam[0].Value = TrangHienTai;
            arrParam[1].Value = SoDongCuaMotTRang;
            arrParam[2].Value = SoTrangCanPhan;
            arrParam[3].Value = MenuId;

            return db.myDataset_sp("spPhanTrang_Article_Menu", arrParam);
        }
        public DataSet myDataSet_Pages(int TrangHienTai, int SoDongCuaMotTRang, int SoTrangCanPhan, int PagesId)
        {
            DataSet dts = new DataSet();
            SqlParameter[] arrParam = {
                                        new SqlParameter("@currPage", SqlDbType.Int),
                                        new SqlParameter("@recodperpage", SqlDbType.Int),
                                        new SqlParameter("@Pagesize", SqlDbType.Int),
                                        new SqlParameter("@PagesId", SqlDbType.Int)
                                        };
            arrParam[0].Value = TrangHienTai;
            arrParam[1].Value = SoDongCuaMotTRang;
            arrParam[2].Value = SoTrangCanPhan;
            arrParam[3].Value = PagesId;

            return db.myDataset_sp("spPhanTrang_Article_Pages", arrParam);
        }
        public DataSet myDataSet(int TrangHienTai, int SoDongCuaMotTRang, int SoTrangCanPhan)
        {
            DataSet dts = new DataSet();
            SqlParameter[] arrParam = {
                                        new SqlParameter("@currPage", SqlDbType.Int),
                                        new SqlParameter("@recodperpage", SqlDbType.Int),
                                        new SqlParameter("@Pagesize", SqlDbType.Int)
                                        };
            arrParam[0].Value = TrangHienTai;
            arrParam[1].Value = SoDongCuaMotTRang;
            arrParam[2].Value = SoTrangCanPhan;
            // arrParam[3].Value = MenuId;

            return db.myDataset_sp("spPhanTrang_Article", arrParam);
        }
        #endregion      

        #region Hàm tìm kiếm bài viết
        //public DataTable article_search_Sign(string TieuDe)
        //{
        //    string sql = "";
        //    sql += string.Format("select top 12 * from Article where Title like '%{0}%'", TieuDe);
        //    return db.myTable(sql);
        //}
        //public DataTable article_search_UnSign(string TieuDe)
        //{
        //    string sql = "";
        //    sql += string.Format("select top 12 * from Article where dbo.fuToUnSign(Title) like '%{0}%'", TieuDe);
        //    return db.myTable(sql);
        //}
       // search download
       
        //public DataTable download_search_UnSign(string Name)
        //{
        //    string sql = "";
        //    sql += string.Format("select top 12 * from Download where dbo.fuToUnSign(Name) like '%{0}%'", Name);
        //    return db.myTable(sql);
        //}
        //// search trang admin title
        //public DataTable download_search_admin_Article(string TieuDe)
        //{
        //    string sql = "";
        //    sql += string.Format("select top 12 * from Article where dbo.fuToUnSign(Title) like '%{0}%'", TieuDe);
        //    return db.myTable(sql);
        //}
        // search trang admin video
        //public DataTable search_admin_Video(string Name)
        //{
        //    string sql = "";
        //    sql += string.Format("select top 12 * from video where dbo.fuToUnSign(Name) like '%{0}%'", Name);
        //    return db.myTable(sql);
        //}
        #endregion

        #region Thêm - sửa - xóa tin
        public bool Ins(int CategoryID, int UserId, string Title, string Discription, string body, string Source, string CreateDate, Boolean Show, Boolean isHot, string Imge, string video, string file, int VideoID, string khongdau, string ListArticleId)
       {
           SqlParameter p1= new SqlParameter("@Category",CategoryID);
           SqlParameter p2= new SqlParameter("@UserId",UserId);
           SqlParameter p3= new SqlParameter("@Title",Title);
           SqlParameter p4= new SqlParameter("@Discription",Discription);
           SqlParameter p5= new SqlParameter("@body",body);
           SqlParameter p6= new SqlParameter("@Source",Source);
           SqlParameter p7 = new SqlParameter("@CreateDate", CreateDate);
           SqlParameter p8= new SqlParameter("@Show",Show);
           SqlParameter p9= new SqlParameter("@isHot",isHot);
           SqlParameter p10= new SqlParameter("@ImagePath",Imge);
           SqlParameter p11= new SqlParameter("@VideoPath",video);
           SqlParameter p12= new SqlParameter("@FilePath",file);
           SqlParameter p13 = new SqlParameter("@VideoId", VideoID);
           SqlParameter p14 = new SqlParameter("@TitleUrl", khongdau);
           SqlParameter p15 = new SqlParameter("@ListArticleId", ListArticleId);
           return db.exe_sp("sp_Ins_Article", p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11,p12,p13,p14,p15);
       }
       public bool Del(int id)
       {
           SqlParameter p1 = new SqlParameter("@id", id);
           return db.exe_sp("sp_Del_Article", p1); 
       }
       
       public bool Upd(int id,int CategoryID, int UserId, string Title,string Discription,  string body, string Source,string LastModified,Boolean Show, Boolean isHot,string Imge, string video,string file,int VideoId,string khongdau,string Dsbai)
       {
            SqlParameter p0= new SqlParameter("@id",id);
            SqlParameter p1= new SqlParameter("@UserId",UserId);
           SqlParameter p2= new SqlParameter("@CategoryId",CategoryID);
           SqlParameter p3= new SqlParameter("@Title",Title);
           SqlParameter p4= new SqlParameter("@Disciption",Discription);
           SqlParameter p5= new SqlParameter("@body",body);
           SqlParameter p6= new SqlParameter("@Source",Source);
           SqlParameter p7 = new SqlParameter("@LastModified", LastModified);
           SqlParameter p8= new SqlParameter("@show",Show);
           SqlParameter p9= new SqlParameter("@isHot",isHot);
           SqlParameter p10= new SqlParameter("@ImagePath",Imge);
           SqlParameter p11= new SqlParameter("@VideoPath",video);
           SqlParameter p12= new SqlParameter("@filePath",file);
           SqlParameter p13 = new SqlParameter("@VideoId", VideoId);
           SqlParameter p14 = new SqlParameter("@TitleUrl", khongdau);
           SqlParameter p15 = new SqlParameter("@Dsbai", Dsbai);
           return db.exe_sp("sp_Upd_Article", p0, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11,p12,p13,p14,p15);
           
       }

        #endregion

       //public bool InsGetIP(String IpAddress, int SoTruyCap)
       //{
       //    SqlParameter p1 = new SqlParameter("@IpAddress", IpAddress);
       //    SqlParameter p2 = new SqlParameter("@SoTruyCap", SoTruyCap);
       //    return db.exe_sp("ins_GetIP", p1, p2);
       //}
       //kiem tra IP
       //public DataTable KTGetIP(String IpAddress)
       //{
       //    SqlParameter p = new SqlParameter("@IpAddress", IpAddress);
       //    return db.myTable_sp("[dbo].[sp_sel_IpAddress]", p);
       //}
       // UP
       //public bool Upd_IpAddress(String IpAddress, int SoTruyCap)
       //{
       //    SqlParameter p1 = new SqlParameter("@IpAddress", IpAddress);
       //    SqlParameter p2 = new SqlParameter("@SoTruyCap", SoTruyCap);
       //    return db.exe_sp("sp_Up_IpAddress", p1, p2);
       //}
       // hiện thị số truy cập hôm nay
     

       #region Hàm sử lý Danh sách bài học - ListArticle
       public int GetListArticleID(int articleId)// lấy  Id danh sách bài học theo articleid
       {
           string sql = "select Article.ListArticleId from Article where Id=" + articleId.ToString();
           DataTable dt = db.myTable(sql);
           int ListId = -1;
           if (dt.Rows.Count > 0)
           {
               string list = dt.Rows[0][0].ToString();
               if (list != "")
               {
                   ListId = int.Parse(list);
               }
           }
           return ListId;
       }
       public DataTable GetDanhSachBaiHoc(int ListId) // lấy các bài viết theo listArticleId
       {
           string sql = "SELECT Article.Title, Article.Id, Article.TitleUrl FROM  Article WHERE ListArticleId=" + ListId.ToString();
           return db.myTable(sql);
       }
       public DataTable LayBangListArticle()// lấy bảng ListArticle
       {
           return db.myTable_sp("sp_sel_ListArticle");
       }
       public int GetUserId_FromListArticle(int ListArticleId)// Lấy UserId theo ListId
       {
           string sql = "SELECT * FROM ListArticle WHERE Id=" + ListArticleId.ToString();
           DataTable dt = db.myTable(sql);
           int UserId = 0;
           if (dt.Rows.Count > 0)
           {
               UserId = int.Parse(dt.Rows[0]["UserId"].ToString());
           }
           return UserId;
       }
       public string GetName_FromListArticle(int ListArticleId)// Lấy ListName theo ListId
       {
           string sql = "select ListArticle.ListName from ListArticle where Id=" + ListArticleId.ToString();
           DataTable dt = db.myTable(sql);
           string Name = "";
           if (dt.Rows.Count > 0)
           {
               Name = dt.Rows[0][0].ToString();
           }
           return Name;
       }
       public bool Ins_DsBaiHoc(string tenDS, string Mota, int UserId)
       {
           SqlParameter p = new SqlParameter("TenDS", tenDS);
           SqlParameter p1 = new SqlParameter("Mota", Mota);
           SqlParameter p2 = new SqlParameter("UserId", UserId);
           return db.exe_sp("sp_ins_ListArticle", p, p1, p2);
       }
       public bool Up_DsBaiHoc(int Id, string Name, string Mota)
       {
           SqlParameter p1 = new SqlParameter("Id", Id);
           SqlParameter p2 = new SqlParameter("NameList", Name);
           SqlParameter p3 = new SqlParameter("Mota", Mota);
           return db.exe_sp("sp_Up_DsBaiViet", p1, p2, p3);
       }
       public bool Del_Ds(int Id)
       {
           SqlParameter p = new SqlParameter("Id", Id);
           return db.exe_sp("sp_Del_Ds", p);
       }

       #endregion

       #region Hàm sử lý thống kê truy cập Traffic
       // Update Traffic
       public bool Up_Traffic()
       {
           bool kt = false;
           string CreDate = DateTime.Now.ToString("dd/MM/yyyy");
           string check = "SELECT  * FROM Traffic WHERE [Date]='" + CreDate + "'";
           DataTable dt = db.myTable(check);
           if(dt.Rows.Count == 0){
               string ins = "INSERT INTO Traffic (Traffic, [Date]) VALUES ("+1+", '"+CreDate+"')";
               kt = db.exe(ins);
           }else{
               int Traffic = int.Parse(dt.Rows[0]["Traffic"].ToString());
               string Upd = "UPDATE Traffic SET Traffic=" + (Traffic + 1) + " WHERE [Date]='" + CreDate + "'";
               kt = db.exe(Upd);
           }
           return kt;
       }
       // hiển thị Traffic theo ngày
       public DataTable Show_Traffic(string Day)
       {
           string sql = "SELECT * FROM Traffic WHERE [Date]='" + Day + "'";
           DataTable dt = db.myTable(sql);
           return dt;
       }

       //hiện thị truy cập theo ngày
       public DataTable AddressDay(string CreateDate)
       {
           SqlParameter p = new SqlParameter("@CreateDate", CreateDate);
           return db.myTable_sp("[dbo].[sp_sel_AddressDay]", p);
       }

       #endregion      

       #region Hàm sử lý gửi Email
       // Gửi Email đến các user
       public void SendEmailMultiUser(string Title, string Name, string Body, string Path)
       {
           UserBLL u = new UserBLL();
           SettingComment scm = new SettingComment();
            DataTable dt = new DataTable();
           // kiểm tra xem những User nào sẽ nhận được email
           int account = scm.Check_UserEmail(); // 1 là các admin, 2 là các manage, 3 Manage và Admin, 4 là tùy chỉnh
            // lấy bảng user theo role
           if (account == 1 || account == 2)
               dt = u.myUser_Role(account);
           else if (account == 3)
               dt = u.myUser_Role_1_2();
           else if (account == 4)
               dt = u.mySettingUserEmail();
           string title = "[Vnfee.Net] "+Title;
           string body = "<b>" + Name + "</b>" + " đã bình luận trong bài viết " + "<a href='" + Path + "' target='_black'>" + Path + "</a>" + "<br />";
           body += Body;
           if (dt.Rows.Count > 0)
           {
               for (int i = 0; i < dt.Rows.Count; i++)
               {
                   SendEmail(title, body, dt.Rows[i]["Email"].ToString());
               }
           }
       }
       // Gửi Email
       public string SendEmail(string Title, string Body, string Email)
       {
           DecryptBLL de = new DecryptBLL();
           string flag = "Server Email Null";
           // lấy settingSendEmail
           SettingComment scm = new SettingComment();
           int ServerEmail = scm.Check_ServerEmail(); // kiểm tra cài đặt chọn server nào
           if (ServerEmail == 1)
           {
               // server 1
               DataTable dt = scm.GetSettingSendEmail(1);
               SmtpClient smtp = new SmtpClient();
               try
               {
                   // giải mã mật khẩu
                   string Pass = dt.Rows[0]["Password"].ToString();
                   string Password = de.Decrypt(Pass, true);
                   //ĐỊA CHỈ SMTP Server
                   smtp.Host = dt.Rows[0]["Host"].ToString();
                   //Cổng SMTP
                   smtp.Port = int.Parse(dt.Rows[0]["Port"].ToString());
                   //SMTP yêu cầu mã hóa dữ liệu theo SSL
                   smtp.EnableSsl = true;
                   //UserName và Password của mail
                   smtp.Credentials = new NetworkCredential(dt.Rows[0]["EmailSend"].ToString(), Password);
                   MailMessage mailMessage = new MailMessage(dt.Rows[0]["EmailSend"].ToString(), Email);
                   mailMessage.Subject = Title;
                   mailMessage.IsBodyHtml = true;
                   mailMessage.Body += Body;
                   mailMessage.Body += "<br />";
                   mailMessage.Body += dt.Rows[0]["FooterBody"].ToString();

                   //Tham số lần lượt là địa chỉ người gửi, người nhận, tiêu đề và nội dung thư
                   smtp.Send(mailMessage);
                   flag = "ok";
               }
               catch (Exception ex)
               {
                   flag = ex.Message;
               }
           }
           else
           {
               // server 2 dự phòng
               DataTable dt = scm.GetSettingSendEmail(2);
               // giải mã mật khẩu
               string Pass = dt.Rows[0]["Password"].ToString();
               string Password = de.Decrypt(Pass, true);
               SmtpClient smtpClient = new SmtpClient(dt.Rows[0]["Host"].ToString(), int.Parse(dt.Rows[0]["Port"].ToString()));
               //SMTP yêu cầu mã hóa dữ liệu theo SSL
               smtpClient.EnableSsl = true;
               smtpClient.Credentials = new NetworkCredential(dt.Rows[0]["EmailSend"].ToString(), Password);
               smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
               MailMessage mailMessage = new MailMessage(dt.Rows[0]["EmailSend"].ToString(), Email);
               mailMessage.Subject = Title;
               mailMessage.IsBodyHtml = true;
               mailMessage.Body += Body;
               mailMessage.Body += "<br />";
               mailMessage.Body += dt.Rows[0]["FooterBody"].ToString();
               try
               {
                   smtpClient.Send(mailMessage);
                   flag = "ok";
               }
               catch (Exception ex)
               {
                   flag = ex.Message;
               }
           }
           return flag;
       }
       #endregion
    }
}
