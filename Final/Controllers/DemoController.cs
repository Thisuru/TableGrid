using Final.Models;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using Dapper;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;

namespace Final.Controllers
{
    public class DemoController : Controller
    {
        private object connection;

        // GET: Demo  
        public ActionResult ShowGrid()
        {
            return View();
        }

        public ActionResult LoadData()
        {

            List<CheckassistantCombined> DataList = new List<CheckassistantCombined>();

            using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
            {
                try
                {

                    DataList = connection.Query<CheckassistantCombined>("Select * from CheckassistantCombined where tag IS NULL").ToList();

                    var draw = Request.Form.GetValues("draw").FirstOrDefault();
                    var start = Request.Form.GetValues("start").FirstOrDefault();
                    var length = Request.Form.GetValues("length").FirstOrDefault();
                    var sortColumn = Request.Form.GetValues("columns[" + Request.Form.GetValues("order[0][column]").FirstOrDefault() + "][name]").FirstOrDefault();
                    var sortColumnDir = Request.Form.GetValues("order[0][dir]").FirstOrDefault();
                    var searchValue = Request.Form.GetValues("search[value]").FirstOrDefault();


                    //Paging Size (10,20,50,100)    
                    int pageSize = length != null ? Convert.ToInt32(length) : 0;
                    int skip = start != null ? Convert.ToInt32(start) : 0;
                    int recordsTotal = 0;

                    // Getting all Customer data    
                    //var customerData = (from tempcustomer in _context.Customers  
                    //                    select tempcustomer);  

                    //Sorting    
                    if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
                    {
                        //customerData = customerData.OrderBy(sortColumn + " " + sortColumnDir);  
                    }
                    //Search    
                    if (!string.IsNullOrEmpty(searchValue))
                    {
                        //customerData = customerData.Where(m => m.CompanyName == searchValue);  
                    }

                    //total number of rows count     
                    recordsTotal = DataList.Count();
                    //Paging     
                    var data = DataList.Skip(skip).Take(pageSize).ToList();
                    //Returning Json Data    
                    return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
                }
                catch (Exception)
                {
                    throw;
                }
            }

        }

        [HttpGet]
        public ActionResult Edit(string ID)
        {
            CheckassistantCombined DataEdit = new CheckassistantCombined();
            try
            {
                using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    //var Customer = (from customer in _context.Customers
                    //                where customer.CustomerID == ID
                    //                select customer).FirstOrDefault();

                    DataEdit = connection.QueryFirst<CheckassistantCombined>("Select * from CheckassistantCombined where userTimestamp = @ID", new { ID = ID });

                    return View((object)DataEdit);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public ActionResult Show(string ID)
        {
            CheckassistantCombined DataShow = new CheckassistantCombined();
            try
            {
                using (IDbConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString))
                {
                    //var Customer = (from customer in _context.Customers
                    //                where customer.CustomerID == ID
                    //                select customer).FirstOrDefault();

                    DataShow = connection.QueryFirst<CheckassistantCombined>("Select * from CheckassistantCombined where userTimestamp = @ID", new { ID = ID });

                    return View((object)DataShow);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}