using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FetchtoApp.Models;

namespace FetchtoApp.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["UserInfo"] != null)
            {
                //ViewBag.UserInfo = (UserInfo)Session["UserInfo"];
                return View();
            }
            else
            {
                return RedirectToAction("Index","Home");
            }
        }
        public ActionResult Signout()
        {
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ProfileInfo()
        {
            ProfileModel objProfileModel = new ProfileModel();
            UserInfo objUserInfo = (UserInfo)Session["UserInfo"];
            objProfileModel.companyname = objUserInfo.Companyname;
            objProfileModel.gst = objUserInfo.GST;
            objProfileModel.accname = objUserInfo.BankAccountname;
            objProfileModel.accno = objUserInfo.BankAccountno;
            objProfileModel.ifsc = objUserInfo.IFSC;
            objProfileModel.branch = objUserInfo.Branch;
            objProfileModel.logo = objUserInfo.Logo;
            objProfileModel.warehousename = objUserInfo.Warehousename;
            objProfileModel.address = objUserInfo.Address;
            objProfileModel.landmark = objUserInfo.Landmark;
            objProfileModel.pincode = objUserInfo.Pincode;
            objProfileModel.city = objUserInfo.City;
            objProfileModel.state = objUserInfo.State;
            objProfileModel.contactno = objUserInfo.Contactno;

            string path = Server.MapPath("~/images/");
            string filepath = path + objUserInfo.profilepic;
            if (System.IO.File.Exists(Server.MapPath("~/images/" + objUserInfo.profilepic)))
            {
                byte[] bytes = System.IO.File.ReadAllBytes(filepath);
                objProfileModel.profileimagefile = (HttpPostedFileBase)new MemoryPostedFile(bytes, filepath, objUserInfo.profilepic);
            }

            return View(objProfileModel);
        }
        [HttpPost]
        public ActionResult UploadFiles()
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                try
                {
                    //  Get all files from Request object  
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  
                        //string filename = Path.GetFileName(Request.Files[i].FileName);  

                        HttpPostedFileBase file = files[i];
                        string fname;

                        // Checking for Internet Explorer  
                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            fname = testfiles[testfiles.Length - 1];
                        }
                        else
                        {
                            fname = file.FileName;
                        }
                        string path = Server.MapPath("~/images/");
                        if (!Directory.Exists(path))
                            Directory.CreateDirectory(path);

                        // Get the complete folder path and store the file inside it.  
                        fname = Path.Combine(Server.MapPath("~/images/"), fname);
                        file.SaveAs(fname);
                    }
                    // Returns message that successfully uploaded  
                    return Json("File Uploaded Successfully!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else
            {
                return Json("No files selected.");
            }
        }
        [HttpPost]
        [ActionName("saveprofile")]
        public ActionResult ProfileInfo1(ProfileModel objProfileModel, string submit)
        {
            switch (submit)
            {
                case "upload":
                    string path = Server.MapPath("~/images/");
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);

                    if (objProfileModel.imagefile != null)
                    {
                        string filename = Path.GetFileName(objProfileModel.imagefile.FileName);
                        objProfileModel.imagefile.SaveAs(path + filename);
                        ViewBag.Message = "file uploaded successfully";
                    }
                    break;
                case "save":
                    int rowsaffected = -1;
                    try
                    {
                        string strConnectionString = ConfigurationManager.ConnectionStrings["fetchtoconnection"].ConnectionString;
                        using (SqlConnection sqlConn = new SqlConnection(strConnectionString))
                        {
                            sqlConn.Open();
                            SqlCommand cmd = new SqlCommand("[techcertuser].[loginModification]", sqlConn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@id", SqlDbType.Int).Value = objProfileModel.userid;
                            cmd.Parameters.Add("@email", SqlDbType.NVarChar, 80).Value = objProfileModel.emailid;
                            cmd.Parameters.Add("@phone", SqlDbType.NVarChar, 15).Value = objProfileModel.phoneno;
                            cmd.Parameters.Add("@username", SqlDbType.NVarChar, 80).Value = objProfileModel.username;
                            cmd.Parameters.Add("@password", SqlDbType.NVarChar, 100).Value = objProfileModel.password;
                            cmd.Parameters.Add("@action", SqlDbType.NVarChar, 100).Value = "UPDATE";
                            cmd.Parameters.Add("@status", SqlDbType.NVarChar, 50).Value = "Active";
                            if (objProfileModel.profileimagefile != null)
                                cmd.Parameters.Add("@profilepic", SqlDbType.VarChar, 4000).Value = objProfileModel.profileimagefile.FileName;

                            cmd.Parameters.Add("@Companyname", SqlDbType.NVarChar, 50).Value = objProfileModel.companyname;
                            cmd.Parameters.Add("@GST", SqlDbType.NVarChar, 50).Value = objProfileModel.gst;
                            cmd.Parameters.Add("@BankAccountname", SqlDbType.NVarChar, 50).Value = objProfileModel.accname;
                            cmd.Parameters.Add("@BankAccountno", SqlDbType.NVarChar, 50).Value = objProfileModel.accno;
                            cmd.Parameters.Add("@IFSC", SqlDbType.NVarChar, 50).Value = objProfileModel.ifsc;
                            cmd.Parameters.Add("@Branch", SqlDbType.NVarChar, 50).Value = objProfileModel.branch;
                            if(objProfileModel.imagefile != null)
                                cmd.Parameters.Add("@Logo", SqlDbType.VarChar, 50).Value = objProfileModel.imagefile.FileName;

                            cmd.Parameters.Add("@Warehousename", SqlDbType.NVarChar, 200).Value = objProfileModel.warehousename;
                            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 4000).Value = objProfileModel.address;
                            cmd.Parameters.Add("@Landmark", SqlDbType.NVarChar, 200).Value = objProfileModel.landmark;
                            cmd.Parameters.Add("@Pincode", SqlDbType.NVarChar, 10).Value = objProfileModel.pincode;
                            cmd.Parameters.Add("@City", SqlDbType.NVarChar, 25).Value = objProfileModel.city;
                            cmd.Parameters.Add("@State", SqlDbType.VarChar, 25).Value = objProfileModel.state;
                            cmd.Parameters.Add("@Contactno", SqlDbType.VarChar, 15).Value = objProfileModel.contactno;

                            rowsaffected = cmd.ExecuteNonQuery();
                            sqlConn.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        ViewBag.Message = "Error occurred";
                    }
                    break;
            }
            return View("ProfileInfo");
        }
    }
}