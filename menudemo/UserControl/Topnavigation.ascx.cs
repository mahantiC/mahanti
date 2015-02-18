using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace menudemo.UserControl
{
    public partial class Topnavigation : System.Web.UI.UserControl
    {
        public string connectionstring =@"Data Source=ASI-INA94109YB\NEWSQL2008;Initial Catalog=Mahantidotnet;User ID=sa;Password=sql@2008";
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
                GetMenuData();
            }
            
        }

        public void GetMenuData()
        {
            if (Session["roleid"] != null)
            {
                int session = int.Parse(Session["roleid"].ToString());
                StringBuilder objstr = new StringBuilder();
                List<TopMenu> objTopmenu = new List<TopMenu>();
                List<LeftMenu> objLeftmenu = new List<LeftMenu>();
                objTopmenu = GetParantMenu();
                objLeftmenu = GetChildMenu();
                objstr.Append("<ul id=\"drop-nav\">");
                int Totalmainitem = 1;
                foreach (TopMenu _pitem in objTopmenu)
                {

                    objstr.Append("<li id=liitem" + Totalmainitem + "><a href='" + _pitem.Url + "'>" + _pitem.TopName + "</a>");
                    Totalmainitem = Totalmainitem + 1;
                    //var objLeftmenuitem = objLeftmenu.OrderBy(o => o.OrderbyID)
                    //    .Where(role=> role.RoldId == session)
                    //    .Where(m => m.ParentId == _pitem.Id).ToList();

                    var objLeftmenuitem = objLeftmenu.Where(x => x.RoldId == session && x.ParentId == _pitem.Id).OrderBy(o => o.OrderbyID).ToList();


                    int divCount = 0;
                    if (objLeftmenuitem.Count > 0)
                    {
                        StringBuilder objLeftStr = new StringBuilder();
                        //objLeftStr.Append("<ul>");
                        foreach (var _citem in objLeftmenuitem)
                        {
                            objLeftStr.Append("<li><a href='" + _citem.LeftMenuUrl + "'>" + _citem.LeftMenuName + "</a></li>");
                            divCount = divCount + 1;
                        }
                        // objLeftStr.Append("</ul>");
                        // div2.InnerHtml  = objLeftStr.ToString();
                        panel.Controls.Add(new LiteralControl("<div id=div" + divCount + ">" + objLeftStr.ToString() + " </div>"));
                    }
                    objstr.Append("</li>");
                }
                objstr.Append("</ul>");
                div1.InnerHtml = objstr.ToString();
            }
            else
            {
                Response.Redirect("/login.aspx");
            }
        }
        public List<TopMenu> GetParantMenu()
        {
            List<TopMenu> objmenu = new List<TopMenu>();
            DataTable _objdt = new DataTable();
            string querystring = "select * from tbl_Model;";
            SqlConnection _objcon = new SqlConnection(connectionstring);
            SqlDataAdapter _objda = new SqlDataAdapter(querystring, _objcon);
            _objcon.Open();
            _objda.Fill(_objdt);
            if (_objdt.Rows.Count > 0)
            {
                for (int i = 0; i < _objdt.Rows.Count; i++)
                {
                    objmenu.Add(new TopMenu { Id = (string)_objdt.Rows[i]["Id"], TopName = _objdt.Rows[i]["TopMenu"].ToString(), Url = _objdt.Rows[i]["RedirectUrl"].ToString() });
                }
            }
            return objmenu;
        }
         public List<LeftMenu> GetChildMenu()
        {
            List<LeftMenu> objmenu = new List<LeftMenu>();
            DataTable _objdt = new DataTable();
            string querystring = "select * from tbl_leftMenu;";
            SqlConnection _objcon = new SqlConnection(connectionstring);
            SqlDataAdapter _objda = new SqlDataAdapter(querystring, _objcon);
            _objcon.Open();
            _objda.Fill(_objdt);
            if (_objdt.Rows.Count > 0)
            {
                for (int i = 0; i < _objdt.Rows.Count; i++)
                {
                    objmenu.Add(new LeftMenu { ParentId = (string)_objdt.Rows[i]["TopChildId"], LeftMenuName = _objdt.Rows[i]["LeftMenu"].ToString(), LeftMenuUrl = _objdt.Rows[i]["RedirectUrl"].ToString(), OrderbyID = _objdt.Rows[i]["orderId"].ToString(), RoldId = int.Parse(_objdt.Rows[i]["Role"].ToString()) });
                }
            }
            return objmenu;
        }
    }

    }
