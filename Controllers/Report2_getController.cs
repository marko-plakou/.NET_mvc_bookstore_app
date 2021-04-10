using bookstore_app.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace bookstore_app.Controllers
{
    public class Report2_getController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Results_rep2";
            return View();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Index(Report2_get report2)
        {
            if (ModelState.IsValid)
            {
                pubsEntities2 db = new pubsEntities2();
                string query;
                IEnumerable<Report2_post> rep2;


                
                SqlParameter first_date = new SqlParameter("first_date", report2.From_time);
                SqlParameter second_date = new SqlParameter("second_date", report2.To_time);
                if (!report2.From_time.HasValue)
                {
                    query = "SELECT ord_date,ord_num,stor_name,title " +
                    "FROM sales INNER JOIN stores on sales.stor_id=stores.stor_id INNER JOIN titles on titles.title_id=sales.title_id " +
                    "WHERE sales.ord_date >= '1/1/1900' AND sales.ord_date <= CONVERT(datetime,@second_date,110) " +
                    "AND stor_name LIKE '" + report2.starts + "%" + report2.ends + "' ORDER BY ord_date DESC;";
                    rep2 = db.Database.SqlQuery<Report2_post>(query, parameters: new[] { second_date });
                }
                else if (!report2.To_time.HasValue)
                {
                    query = "SELECT ord_date,ord_num,stor_name,title " +
                        "FROM sales INNER JOIN stores on sales.stor_id=stores.stor_id INNER JOIN titles on titles.title_id=sales.title_id " +
                        "WHERE sales.ord_date >= CONVERT(datetime,@first_date,110) AND sales.ord_date <= '1/1/2222' " +
                        "AND stor_name LIKE '" + report2.starts + "%" + report2.ends + "' ORDER BY ord_date DESC;";
                    rep2 = db.Database.SqlQuery<Report2_post>(query, parameters: new[] { first_date });
                }
                else if (string.IsNullOrWhiteSpace(report2.starts)&&string.IsNullOrWhiteSpace(report2.ends)) {
                    query = "SELECT ord_date,ord_num,stor_name,title " +
                           "FROM sales INNER JOIN stores on sales.stor_id=stores.stor_id INNER JOIN titles on titles.title_id=sales.title_id " +
                           "WHERE sales.ord_date >= CONVERT(datetime,@first_date,110) AND sales.ord_date <= CONVERT(datetime,@second_date,110) " +
                           "ORDER BY ord_date DESC;";
                    rep2 = db.Database.SqlQuery<Report2_post>(query, parameters: new[] { first_date, second_date });
                }

                 else if (string.IsNullOrWhiteSpace(report2.starts))
                {

                    query = "SELECT ord_date,ord_num,stor_name,title " +
                        "FROM sales INNER JOIN stores on sales.stor_id=stores.stor_id INNER JOIN titles on titles.title_id=sales.title_id " +
                        "WHERE sales.ord_date >= CONVERT(datetime,@first_date,110) AND sales.ord_date <= CONVERT(datetime,@second_date,110) " +
                        "AND stor_name LIKE '%" + report2.ends + "' ORDER BY ord_date DESC;";
                    rep2 = db.Database.SqlQuery<Report2_post>(query, parameters: new[] { first_date, second_date });
                }

                else if (string.IsNullOrWhiteSpace(report2.ends))
                {
                    query = "SELECT ord_date,ord_num,stor_name,title " +
                    "FROM sales INNER JOIN stores on sales.stor_id=stores.stor_id INNER JOIN titles on titles.title_id=sales.title_id " +
                    "WHERE sales.ord_date >= CONVERT(datetime,@first_date,110) AND sales.ord_date <= CONVERT(datetime,@second_date,110) " +
                    "AND stor_name LIKE '" + report2.starts + "%' ORDER BY ord_date DESC;";
                    rep2 = db.Database.SqlQuery<Report2_post>(query, parameters: new[] { first_date, second_date });
                }
                
               
                
                else
                {
                    query = "SELECT ord_date,ord_num,stor_name,title " +
                 "FROM sales INNER JOIN stores on sales.stor_id=stores.stor_id INNER JOIN titles on titles.title_id=sales.title_id " +
                 "WHERE sales.ord_date >= CONVERT(datetime,@first_date,110) AND sales.ord_date <= CONVERT(datetime,@second_date,110) " +
                 "AND stor_name LIKE '" + report2.starts + "%" + report2.ends + "' ORDER BY ord_date DESC;";
                    rep2 = db.Database.SqlQuery<Report2_post>(query, parameters: new[] { first_date, second_date });
                }
                
                Console.WriteLine(rep2);
                ViewBag.Resultset2 = rep2;
                return View("Results2");
            }
            else { return View(); }
        }
    }
}