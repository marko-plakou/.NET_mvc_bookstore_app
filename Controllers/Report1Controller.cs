using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using bookstore_app.Models;

namespace bookstore_app.Controllers
{
    public class Report1Controller : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "The first report";
            return View();


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Index(Report1 report1)
        {
            if (ModelState.IsValid)
            {
                pubsEntities2 db = new pubsEntities2();
                string query;
                IEnumerable<authors> results;
                SqlParameter number_of_sales = new SqlParameter("number_of_sales", report1.X);
                SqlParameter first_date = new SqlParameter("first_date", report1.From_time);
                SqlParameter second_date = new SqlParameter("second_date", report1.To_time);
                if (!report1.X.HasValue) {
                    query = "SELECT authors.au_id,authors.au_fname,authors.au_lname,authors.phone,authors.address,authors.city,authors.state,authors.zip,authors.contract " +
                             "FROM authors INNER JOIN titleauthor ON authors.au_id=titleauthor.au_id INNER JOIN sales ON titleauthor.title_id=sales.title_id " +
                             "WHERE sales.ord_date >= CONVERT(datetime,@first_date,110) AND  sales.ord_date <= CONVERT(datetime,@second_date,110) GROUP BY authors.au_id,authors.au_fname,authors.au_lname,authors.phone,authors.address,authors.city,authors.state,authors.zip,authors.contract ORDER BY sum(sales.qty) DESC;";
                    results = db.Database.SqlQuery<authors>(query, parameters: new[] {first_date, second_date });
                }
                else if (!report1.From_time.HasValue)
                {
                    query = "SELECT TOP (@number_of_sales) authors.au_id,authors.au_fname,authors.au_lname,authors.phone,authors.address,authors.city,authors.state,authors.zip,authors.contract " +
                             "FROM authors INNER JOIN titleauthor ON authors.au_id=titleauthor.au_id INNER JOIN sales ON titleauthor.title_id=sales.title_id " +
                             "WHERE sales.ord_date <= CONVERT(datetime,@second_date,110) GROUP BY authors.au_id,authors.au_fname,authors.au_lname,authors.phone,authors.address,authors.city,authors.state,authors.zip,authors.contract ORDER BY sum(sales.qty) DESC;";
                    results = db.Database.SqlQuery<authors>(query, parameters: new[] { number_of_sales, second_date });
                }
                else
                {
                    query = "SELECT TOP (@number_of_sales) authors.au_id,authors.au_fname,authors.au_lname,authors.phone,authors.address,authors.city,authors.state,authors.zip,authors.contract " +
                             "FROM authors INNER JOIN titleauthor ON authors.au_id=titleauthor.au_id INNER JOIN sales ON titleauthor.title_id=sales.title_id " +
                             "WHERE sales.ord_date >= CONVERT(datetime,@first_date,110) GROUP BY authors.au_id,authors.au_fname,authors.au_lname,authors.phone,authors.address,authors.city,authors.state,authors.zip,authors.contract ORDER BY sum(sales.qty) DESC;";
                    results = db.Database.SqlQuery<authors>(query, parameters: new[] { number_of_sales, first_date});
                }

                ViewBag.results = results;
                return View("Results");
            }
            else { 
                return View(); 
            }
        }
    }
}