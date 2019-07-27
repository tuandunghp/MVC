using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace BLL
{
    public class WebBLL
    {
        DataService db = new DataService();
        basic b = new basic();
        public string GetTime()
        {
            string NgayThangNam = DateTime.Now.ToString("dd/MM/yyyy");
            string GioPhut = DateTime.Now.ToShortTimeString();
            string Time = NgayThangNam + ", " + GioPhut; // có dạng: 25/11/2018, 4:02 PM
            return Time;
        }
        public string ThoiGian(string Time)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB"); // chuyển định dạng datetime thành dd/MM/yy trong phiên này
            string KhoangTime = "";
            // Thời điểm hiện tại dưới dạng: 11/25/2018, 4:02 PM
            //string Date1 = DateTime.Now.ToShortDateString() + ", " + DateTime.Now.ToShortTimeString();
            //Thời điểm hiện tại dưới dạng: 25/11/2018, 4:02 PM
            string Date = GetTime();
            DateTime DateNow = DateTime.Parse(Date);
            // Mốc thời gian
            DateTime DateOld = new DateTime();
            try
            {
                DateOld = DateTime.Parse(Time);
                //DateOld = DateTime.ParseExact(Time, "dd/MM/yyyy, hh:mm tt", CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                // nếu lỗi vì ngày có dạng mm/dd/yyy. thì tao DateNew có dạng mm/dd/yyy , h:mm PM
                string DateNew = Time + ", 00:00 AM";
                //string DateNew = "26/12/2018, 10:33 AM";
                DateOld = DateTime.ParseExact(DateNew, "dd/MM/yyyy, hh:mm tt", CultureInfo.InvariantCulture);
            }
            // Khoảng thời gian.
            TimeSpan interval = DateNow.Subtract(DateOld);
            
            int day = interval.Days;
            int gio = interval.Hours;
            int phut = interval.Minutes;
            if (day > 365)
                KhoangTime = (day/365) + " Năm trước";
            else if(day >= 30)
            {
                int thang = day/30;
                KhoangTime = thang + " Tháng trước";
                
            }
            else if (day > 0)
            {
                int tuan = day / 7;
                if (tuan > 0)
                    KhoangTime = tuan + " Tuần trước";
                else
                    KhoangTime = day + " Ngày " + gio + " Giờ trước";
            }
            else
            {
                if(gio > 0)
                    KhoangTime = gio + " Giờ " + phut + " Phút trước";
                else
                    KhoangTime = phut + " Phút trước";
            }
            //int giay = interval.Seconds;
            return KhoangTime;
        }
        public DataTable Get_Uc_Menu()
        {
            string sql = "";
            sql += string.Format("SELECT * FROM UcControl");
            return db.myTable(sql);
        }
        public bool Upd_UcMenu(int UcMenu1, int UcMenu2, int UcMenu3, string Name1, string Name2, string Name3)
        {
            SqlParameter p1 = new SqlParameter("@UcMenu1", UcMenu1);
            SqlParameter p2 = new SqlParameter("@UcMenu2", UcMenu2);
            SqlParameter p3 = new SqlParameter("@UcMenu3", UcMenu3);
            SqlParameter p4 = new SqlParameter("@NameUc1", Name1);
            SqlParameter p5 = new SqlParameter("@NameUc2", Name2);
            SqlParameter p6 = new SqlParameter("@NameUc3", Name3);
            return db.exe_sp("Upd_UcMenu", p1, p2, p3, p4, p5, p6);
        }
        
        
    }
}
