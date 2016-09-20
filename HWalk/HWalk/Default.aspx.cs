using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HWalk.DAL;
using HWalk.STATIC;

namespace HWalk
{
    public partial class _Default : Page
    {
        List<LocalWalker> _localWalkers = new List<LocalWalker>();
        List<LocalMilage> _localMiles = new List<LocalMilage>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) {
                using (WalkEntities wtx = new WalkEntities())
                {
                    var _walkers = (from w in wtx.WALKERs
                                    select w).ToList();

                    foreach (var w in _walkers)
                    {
                        LocalWalker _walk = new LocalWalker();
                        _walk.Name = w.WALKER_FNAME.Trim() + " " + w.WALKER_LNAME.Trim();
                        _walk.Walker_Id = w.WALKER_ID;

                        var milage = (from m in wtx.MILEAGEs
                                      where m.WALKER_ID == w.WALKER_ID
                                      select m.MILEAGE1).ToList();

                        _walk.Mileage = (milage.Count > 0) ? double.Parse(milage.Sum().ToString()) : 0;

                        _localWalkers.Add(_walk);

                    }

                    var mile_entries = (from x in wtx.MILEAGEs
                                        select x).ToList();

                    double _total = 0;
                    foreach (var w in mile_entries)
                    {
                        _localMiles.Add(new LocalMilage { Mileage_Id = w.MILEAGE_ID, Mileage = w.MILEAGE1, Mileage_Date = w.MILEAGE_DT, Walker_Id = w.WALKER_ID });
                        _total += w.MILEAGE1;
                    }

                    rptWalker.DataSource = _localWalkers;
                    rptWalker.DataBind();

                    rptMileDetails.DataSource = _localMiles;
                    rptMileDetails.DataBind();

                    lblTeamMiles.Text = _total.ToString();
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var name = hdnCurrentId.Value.ToString();
            int _id = int.Parse(name.TrimStart('W').ToString());

            using (WalkEntities ptx = new WalkEntities())
            {
                DateTime dt = DateTime.Parse(tbDate.Text);
                double mi = double.Parse(tbNewMilage.Text);

                ptx.MILEAGEs.Add(new MILEAGE { WALKER_ID = _id, MILEAGE1 = mi, MILEAGE_DT = dt });
                ptx.SaveChanges();

                tbDate.Text = "";
                tbNewMilage.Text = "";
                Response.Redirect("Default.aspx");            }
        }

  
    }
}