

using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;

//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using DAL;
using System.Text.RegularExpressions;
namespace BLL
{
   public class basic
    {
       //public void messages(string s)
       //{
       //    string tb = "<script type=\"text/javascript\"> Function("+s+"){alert(messages);}</script>";
       //    Response.Write(tb);
       //}
       DataService db = new DataService();
       public basic() { }
       public string xaucon(string s, int so_ky_tu_max)
       {
           string xau="";

           if (s.Length > so_ky_tu_max)
           {
               xau = s.Substring(0, so_ky_tu_max);
               s = xau;
               int i = s.LastIndexOf(" ");
               xau = s.Substring(0, i);
           }
           else
           {
               xau = s;
           }
           
              return xau;

       }
       public string toString( string s)
       {
           if(s.Length>0) return s;
           else return string.Empty;
       }
       public long toLong(string s)
       {
           long l;
           try
           {
               long.TryParse(s, out l);
           }
           catch { l = 0; }
           return l;
       }
       public int toInt(string s)
       {
           int l;
           try
           {
               int.TryParse(s, out l);
           }
           catch { l = 0; }
           return l;
       }
       public float toFloat(string s)
       {
           float l;
           try
           {
               float.TryParse(s, out l);
           }
           catch { l = 0; }
           return l;
       }
       public string ConvertEditor(string text)
       {
           //for (int i = 32; i < 48; i++)
           //{
           //    text = text.Replace(((char)i).ToString(), " ");
           //}
           text = text.Replace("<", "&lt;");
           text = text.Replace(">", "&gt;");
           
           return text;
       }
       public string ConvertToUnTitle(string text)
       {
           for (int i = 32; i < 48; i++)
           {
               text = text.Replace(((char)i).ToString(), " ");
           }
           text = text.Replace(".", "-");
           text = text.Replace("?", "-");
           text = text.Replace(" ", "-");

           text = text.Replace(",", "-");
           text = text.Replace(";", "-");
           text = text.Replace("/", " ");
           text = text.Replace(":", "-");

           Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

           string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);

           return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
       }
       public string ConvertToUnSign(string text)
       {

           for (int i = 32; i < 48; i++)
           {

               text = text.Replace(((char)i).ToString(), " ");

           }
           text = text.Replace(".", "-");

           text = text.Replace(" ", " ");

           text = text.Replace(",", "-");
           text = text.Replace(";", "-");
           text = text.Replace("/", " ");
           text = text.Replace(":", "-");

           Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

           string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);

           return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
       }
       public string ConvertToUnSign1(string text)
       {

           for (int i = 32; i < 48; i++)
           {
               text = text.Replace(((char)i).ToString(), " ");
           }
           text = text.Replace(".", "-");
           text = text.Replace("?", "-");
           text = text.Replace(" ", "-");

           text = text.Replace(",", "-");
           text = text.Replace(";", "-");
           text = text.Replace("/", "-");
           text = text.Replace(":", "-");
           text = text.Replace("!", "-");
           text = text.Replace("#", "-");
           text = text.Replace("$", "-");
           text = text.Replace("^", "-");
           text = text.Replace("&", "-");
           text = text.Replace("*", "-");
           Regex regex = new Regex(@"\p{IsCombiningDiacriticalMarks}+");

           string strFormD = text.Normalize(System.Text.NormalizationForm.FormD);

           return regex.Replace(strFormD, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
       }
       
      //public void loaddrop(DropDownList dr, string textField,string valueField, string sql)
      // {
      //      dr.DataSource = db.myTable(sql);
      //      dr.DataTextField =textField;
      //      dr.DataValueField = valueField;
      //      dr.DataBind();
      // }

      public string LoadAllMenu()
      {
          MenuBLL mnu = new MenuBLL();
          PagesBLL p = new PagesBLL();
          ArticleBLL at = new ArticleBLL();
          DataTable menu = mnu.Menus();
          string html = "";
          for (int i = 0; i < menu.Rows.Count; i++)
          {
              // Id menu
              string ID = menu.Rows[i]["Id"].ToString();
              // tên menu
              string MenuName = menu.Rows[i]["Menu"].ToString();
              // Tên Menu không dấu
              string Menu_khongdau = ConvertToUnSign1(MenuName);
              
              // Lấy subMenu ( page ) theo menu Id
              DataTable subMenu = p.myPages_Where(int.Parse(ID));
              // Lấy top 5 tin theo Menu Id
              DataTable article = at.myArticle_Top5tin(int.Parse(ID));

              html += "<div class='row Border-Top'>";
              html += string.Format("<div class='w-100 Menu-top'><a class='Menu-link' href=\"../{0}/{3}-{2}-0\"><span>{1}</span></a>", menu.Rows[i]["Link"].ToString(), MenuName, ID, Menu_khongdau);

              // tab-more
              html += "<a class='sub-menu-link tabs_more' style='display:none;'><span> More <i class='fa fa-angle-down' aria-hidden='true'></i></span></a>";
              html += "<div class='sub_tab_more'></div>";
              
              // Tải subMenu
              //html += "<div class='w-75 Menu-top'>";
              for (int j = 0; j < subMenu.Rows.Count; j++)
              {
                  string link = subMenu.Rows[j]["LinkUrl"].ToString();
                  // Tên page
                  string PageName = subMenu.Rows[j]["Category"].ToString();
                  // tên page không dấu
                  string Pname_Khongdau = ConvertToUnSign1(PageName);
                  // nếu link rỗng thì gán url mặc định
                  if (link == "")
                  {
                      html += string.Format("<a class=\"sub-menu-link\" href=\"../{0}/{4}-{2}-{3}\">{1}</a>", subMenu.Rows[j]["Page"].ToString(), PageName, menu.Rows[i]["Id"].ToString(), subMenu.Rows[j]["Id"].ToString(), Pname_Khongdau);
                      
                  }
                  // không thì gán theo link
                  else
                  {
                      html += string.Format("<a class=\"sub-menu-link\" href=\"../{0}\">{1}</a>", link, PageName);
                  }
              }
             


              html += "</div>";
             
              // Tải TOP tin của Menu thứ i
              if(article.Rows.Count > 0)
              {
                  // tin đầu tiên cho hiển thị bên trái
                  html += "<div class='col-lg-6 col-xs-12'>";

                  html += string.Format("<a href='../New/{0}-{1}'><img class='card-img-top' src='../{2}' alt=''></a>", article.Rows[0]["TitleUrl"].ToString(), article.Rows[0]["Id"].ToString(), article.Rows[0]["Source"].ToString());
                  html += string.Format("<a class='news-link-left' href='../New/{0}-{1}'><h5 class='card-title mt-2'>{2}</h5></a>", article.Rows[0]["TitleUrl"].ToString(), article.Rows[0]["Id"].ToString(), article.Rows[0]["Title"].ToString());
                  html += string.Format("<p class='card-text'>{0}</p>", article.Rows[0]["Discription"].ToString());

                  html += "</div>";

                  html += "<div class='col-lg-6 col-xs-12'>";

                  // bắt đầu từ tin thứ 2 hiển thị bên phải
                  for (int k = 1; k < article.Rows.Count; k++)
                  {

                      html += string.Format("<a href='../News/{0}-{1}' title='{2}'>", article.Rows[k]["TitleUrl"].ToString(), article.Rows[k]["Id"].ToString(), article.Rows[k]["Title"].ToString());
                      html += "<div class='divView'>";
                      html += "<ul class='px-0'>";
                      html += "<li>";
                      html += string.Format("<img src='../{0}' />", article.Rows[k]["Source"].ToString());
                      html += string.Format("<p><span>{0}</span></br><i class='fa fa-clock-o' aria-hidden='true'></i>&nbsp;&nbsp;<span>{1}</span>", article.Rows[k]["Title"].ToString(), article.Rows[k]["CreateDate"].ToString());


                      html += "</p>";
                      html += "</li>";
                      html += "</ul>";
                      html += "</div>";
                      html += "</a>";     
                  }
                  html += "</div>";
              }


              html += "</div>";
          }

          return html;
      }

      public static string load_TwoGroup()
      {
          MenuBLL mnu = new MenuBLL();
          PagesBLL p = new PagesBLL();
          ArticleBLL at = new ArticleBLL();
          string xau = "";
          DataTable menu = mnu.Menus();
          xau+="<Table>";
          for (int i = 2; i < menu.Rows.Count; i+=2)
          {

              xau += "<tr>";
              xau += "<td>";
              #region lay nhom 1
              xau += " <div class='news_listbox'>";
              xau += "<a class='home_news_rss' href='/ketqua.tintuc?hq=xahoi&amp;mode=rss'></a>";
              xau += " <div class='news_list_title'>";
              xau += string.Format("<a href='articleGroup.aspx?m={0}&c=0' class='main_cat_tab'> <span>{1}</span> </a>", menu.Rows[i]["Id"].ToString(), menu.Rows[i]["Menu"].ToString());
              xau+= " <div class='sub_main_cat'>";
              DataTable subMenu = p.myPages_Where(int.Parse(menu.Rows[i]["Id"].ToString()));
              DataTable aticle = at.myArticle_Top3(int.Parse(menu.Rows[i]["Id"].ToString()));
              if ((subMenu.Rows.Count > 0))
              {
                  for (int j = 0; j < subMenu.Rows.Count; j++)
                  {
                      if (j < 3)
                      xau += string.Format("<a class='menucon' href='articleGroup.aspx?m={0}&c={1}'>{2}</a>  <span class='sub_main_sperator'>|</span>", menu.Rows[i]["Id"].ToString(), (subMenu.Rows[j]["Id"].ToString()), subMenu.Rows[j]["Category"].ToString());
                  }
              }

              xau += " </div>";
              xau += "<div style='clear: both;'></div>";
              xau += "</div>";
              #region tai cac tin trong chuyen muc
              if ((subMenu.Rows.Count > 0))
              {
                  xau += "<div class='news_list_content'>";
                  xau += "<div>";
                  xau += string.Format(" <a class='img_box' href=''><img src='../Uploaded/{0}' class='lazyload'", aticle.Rows[0]["Source"].ToString());
                  xau += " alt='' style='background: url(images/inline_photo.png&quot;) no-repeat scroll center center rgb(255, 255, 255);'></a>";
                  xau += string.Format(" <h3><a class='new_list_link' title='' href='ArticleDetail.aspx?at={0}'>{1} </a></h3>", aticle.Rows[0]["Id"].ToString(), aticle.Rows[0]["Title"].ToString());
                  xau += "<p>" + aticle.Rows[0]["Discription"].ToString() + "</p> </div>";
                  xau += "<div>";
                  xau += "<ul>";
                  if ((subMenu.Rows.Count > 1))
                  {
                      xau += string.Format("<li><a title='' href='ArticleDetail.aspx?at={0}'>{1}</a></li>", aticle.Rows[1]["Id"].ToString(), aticle.Rows[1]["Title"].ToString());
                  }
                  if ((subMenu.Rows.Count > 2))
                  {

                      xau += string.Format("<li><a title='' href='ArticleDetail.aspx?at={0}'>{1}</a></li>", aticle.Rows[2]["Id"].ToString(), aticle.Rows[2]["Title"].ToString());
                  }
                  xau += "</ul></div> </div> </div>";
              }
              #endregion
              #endregion
              xau += "</td>";
              #region tai nhom 2
              if (i+1 < menu.Rows.Count)
              {
                  xau += "<td>";
                  #region lay nhom tieu de nhom 2
                  xau += " <div class='news_listbox'>";
                  xau += "<a class='home_news_rss' href='/ketqua.tintuc?hq=xahoi&amp;mode=rss'></a>";
                  xau += " <div class='news_list_title'>";
                  xau += string.Format("<a href='articleGroup.aspx?m={0}&c=0' class='main_cat_tab'> <span>{1}</span> </a>", menu.Rows[i+1]["Id"].ToString(), menu.Rows[i+1]["Menu"].ToString());
                  xau += " <div class='sub_main_cat'>";
                  DataTable subMenu1 = p.myPages_Where(int.Parse(menu.Rows[i+1]["Id"].ToString()));
                  DataTable aticle1 = at.myArticle_Top3(int.Parse(menu.Rows[i+1]["Id"].ToString()));
                  if ((subMenu1.Rows.Count > 0))
                  {
                      for (int j = 0; j < subMenu1.Rows.Count; j++)
                      {
                          if(j<3)
                          xau += string.Format("<a class='menucon' href='articleGroup.aspx?m={0}&c={1}'>{2}</a>  <span class='sub_main_sperator'>|</span>", menu.Rows[i+1]["Id"].ToString(), (subMenu1.Rows[j]["Id"].ToString()), subMenu1.Rows[j]["Category"].ToString());
                      }
                  }

                  xau += " </div>";
                  xau += "<div style='clear: both;'></div>";
                  xau += "</div>";
                  #region tai cac tin trong chuyen muc nhom 2
                  if ((subMenu1.Rows.Count > 0))
                  {
                      xau += "<div class='news_list_content'>";
                      xau += "<div>";
                      xau += string.Format(" <a class='img_box' href=''><img src='../Uploaded/{0}' class='lazyload'", aticle1.Rows[0]["Source"].ToString());
                      xau += " alt='' style='background: url(images/inline_photo.png&quot;) no-repeat scroll center center rgb(255, 255, 255);'></a>";
                      xau += string.Format(" <h3><a class='new_list_link' title='' href='ArticleDetail.aspx?at={0}'>{1} </a></h3>", aticle1.Rows[0]["Id"].ToString(), aticle1.Rows[0]["Title"].ToString());
                      xau += "<p>" + aticle.Rows[0]["Discription"].ToString() + "</p> </div>";
                      xau += "<div>";
                      xau += "<ul>";
                      if ((subMenu1.Rows.Count > 1))
                      {
                          xau += string.Format("<li><a title='' href='ArticleDetail.aspx?at={0}'>{1}</a></li>", aticle1.Rows[1]["Id"].ToString(), aticle1.Rows[1]["Title"].ToString());
                      }
                      if ((subMenu1.Rows.Count > 2))
                      {

                          xau += string.Format("<li><a title='' href='ArticleDetail.aspx?at={0}'>{1}</a></li>", aticle.Rows[2]["Id"].ToString(), aticle.Rows[2]["Title"].ToString());
                      }
                      xau += "</ul></div> </div> </div>";
                  }
                  #endregion
                  #endregion
                  xau += "</td>";
              }
              #endregion
              xau += "</tr>";
              
          }
          xau += "</Table>";
          return xau;

      }
      public static string load_OneGroup()
      {
          MenuBLL mnu = new MenuBLL();
          PagesBLL p = new PagesBLL();
          ArticleBLL at = new ArticleBLL();
          basic b = new basic();
          string xau = "";
          DataTable menu = mnu.Menus();

          for (int i = 2; i < 7; i++)
          {
              xau += " <div class='news_listbox'>" +
                 "<a class='home_news_rss' href='/ketqua.tintuc?hq=xahoi&amp;mode=rss'></a>" +
                " <div class='news_list_title'>" +
                  string.Format("<a href='articleGroup.aspx?m={0}&c=0&page=1' class='main_cat_tab'> <span>{1}</span> </a>", menu.Rows[i]["Id"].ToString(), menu.Rows[i]["Menu"].ToString()) +
                    " <div class='sub_main_cat'>";
              DataTable subMenu = p.myPages_Where(int.Parse(menu.Rows[i]["Id"].ToString()));
              DataTable aticle = at.myArticle_Top5tin(int.Parse(menu.Rows[i]["Id"].ToString()));
              if ((subMenu.Rows.Count > 0))
              {
                  for (int j = 0; j < subMenu.Rows.Count; j++)
                  {
                      xau += string.Format("<a class='menucon' href='articleGroup.aspx?m={0}&c={1}&page=1'>{2}</a>  <span class='sub_main_sperator'>|</span>", menu.Rows[i]["Id"].ToString(), (subMenu.Rows[j]["Id"].ToString()), subMenu.Rows[j]["Category"].ToString());
                  }
              }

              xau += " </div>";
              xau += "<div style='clear: both;'></div>";
              xau += "</div>";
              if ((aticle.Rows.Count > 0))
              {

                  string title1 = aticle.Rows[0]["Title"].ToString();
                  string khongdau1 = b.ConvertToUnTitle(title1.ToLower());

                  xau += "<div class='news_list_content'>";
                  xau += "<div>";
                  xau += string.Format(" <a class='img_box' href=''><img src='../Uploaded/{0}' class='lazyload'", aticle.Rows[0]["Source"].ToString());
                  xau += " alt='' style='background: url(images/inline_photo.png&quot;) no-repeat scroll center center rgb(255, 255, 255);'></a>";
                  xau += string.Format(" <h3><a class='new_list_link' title='' href='../ArticleDetail/{2}-{0}'>{1} </a></h3>", aticle.Rows[0]["Id"].ToString(), aticle.Rows[0]["Title"].ToString(), khongdau1);
                  xau += "<p>" + aticle.Rows[0]["Discription"].ToString() + "</p> </div>";
                  xau += "<div>";
                  xau += "<ul class='haicon'>";
                  if ((aticle.Rows.Count > 1))
                  {
                      string title = aticle.Rows[1]["Title"].ToString();
                      string khongdau = b.ConvertToUnTitle(title.ToLower());
                      //xau += string.Format("<li><a title='' href='ArticleDetail.aspx?at={0}'>{1}</a></li>", aticle.Rows[1]["Id"].ToString(), aticle.Rows[1]["Title"].ToString());
                      xau += string.Format("<li><a title='' href='../ArticleDetail/{2}-{0}'>{1}</a></li>", aticle.Rows[1]["Id"].ToString(), aticle.Rows[1]["Title"].ToString(), khongdau);
                  }
                  if ((aticle.Rows.Count > 2))
                  {
                      string title = aticle.Rows[2]["Title"].ToString();
                      string khongdau = b.ConvertToUnTitle(title.ToLower());
                      xau += string.Format("<li><a title='' href='../ArticleDetail/{2}-{0}'>{1}</a></li>", aticle.Rows[2]["Id"].ToString(), aticle.Rows[2]["Title"].ToString(), khongdau);

                  }
                  if ((aticle.Rows.Count > 3))
                  {
                      string title = aticle.Rows[3]["Title"].ToString();
                      string khongdau = b.ConvertToUnTitle(title.ToLower());
                      xau += string.Format("<li><a title='' href='../ArticleDetail/{2}-{0}'>{1}</a></li>", aticle.Rows[3]["Id"].ToString(), aticle.Rows[3]["Title"].ToString(), khongdau);
                  }
                  if ((aticle.Rows.Count > 4))
                  {
                      string title = aticle.Rows[4]["Title"].ToString();
                      string khongdau = b.ConvertToUnTitle(title.ToLower());
                      xau += string.Format("<li><a title='' href='../ArticleDetail/{2}-{0}'>{1}</a></li>", aticle.Rows[4]["Id"].ToString(), aticle.Rows[4]["Title"].ToString(), khongdau);
                  }
                  xau += "</ul></div> </div> </div>";
              }
          }
          return xau;

      }   
      
    }
}
