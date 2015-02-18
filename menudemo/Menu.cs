using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace menudemo
{
    public class TopMenu
    {
        public string Id { get; set; }
        public string TopName { get; set; }
        public string Url { get; set; }
    }
    public class LeftMenu
    {
        public string ParentId { get; set; }
        public string LeftMenuName { get; set; }
        public string LeftMenuUrl { get; set; }
        public string OrderbyID { get; set; }
        public int RoldId {get;set; }
    }
   
}