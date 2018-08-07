using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyWebMVC.Models
{
    public class BaseUser
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string RealName { get; set; }
        public string Title { get; set; }
        public string Email { get; set; }
        public string Lang { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string QICQ { get; set; }
        public string HomeAddress { get; set; }
    }
}