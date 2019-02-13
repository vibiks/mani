using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using FetchtoApp.Models;

namespace FetchtoApp.Controllers
{
    [AllowAnonymous]
    public class UserManageController : Controller
    {


        // GET: UserManage
        [HttpPost]
        [Route("api/sign_exe/{username}/{password}")]
        [ActionName("sign_exe")]
        public ActionResult sign_exe(String username, String password)
        {
            UserManageModel userobj = new UserManageModel();
            String Status = "";
            List<UserInfo> userlist = userobj.fnUserModel(username: username, password: password, action: "AUTHENTICATE");
            if (userlist.Count > 0)
            {
                Status = "Login Success";
                Session["UserInfo"] = userlist[0];
            }
            else
            {
                Status = "Login Failed";
            }
            return Json(Status, JsonRequestBehavior.DenyGet);

        }


        // GET: UserManage
        [HttpPost]
        [Route("api/Validateuseremail/{email}")]
        [ActionName("Validateuseremail")]
        public ActionResult Validateuseremail(String email)
        {
            UserManageModel userobj = new UserManageModel();
            String Status = "";
            List<UserInfo> userlist = userobj.fnUserModel(email: email, action: "VALIDATEMAIL");
            if (userlist.Count > 0)
            {
                Status = "true";
            }
            else
            {
                Status = "false";
            }
            return Json(Status, JsonRequestBehavior.DenyGet);

        }

        [HttpPost]
        [Route("api/Usesrregistration/{username}/{password}/{mobile}/{email}")]
        [ActionName("Usesrregistration")]
        public ActionResult Usesrregistration(String username,String password, String mobile,String email)
        {
            UserManageModel userobj = new UserManageModel();
            String Status = "";
            List<UserInfo> userlist = userobj.fnUserModel(username: username, password: password, email: email, phoneno: mobile, action: "INSERT");
            if (userlist.Count > 0)
            {
                Status = "true";
            }
            else
            {
                Status = "false";
            }
            return Json(Status, JsonRequestBehavior.DenyGet);
        }
    }
}
