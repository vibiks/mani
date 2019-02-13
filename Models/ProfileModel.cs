using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FetchtoApp.Models
{
    public class ProfileModel
    {
        public string userid { get; set; }
        public string emailid { get; set; }
        public string phoneno { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string status { get; set; }
        public string role { get; set; }
        public string profilepic { get; set; }
        public string companyname { get; set; }
        public string phone { get; set; }
        public string gst { get; set; }
        public string accname { get; set; }
        public string accno { get; set; }
        public string ifsc { get; set; }
        public string branch { get; set; }
        public string logo { get; set; }
        public string action { get; set; }
        public string warehousename { get; set; }
        public string address { get; set; }
        public string landmark { get; set; }
        public string pincode { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string contactno { get; set; }
        public HttpPostedFileBase imagefile { get; set; }
        public HttpPostedFileBase profileimagefile { get; set; }
    }
}