using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Novacode;
using System.IO;

namespace HWalk
{
    public partial class Print : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                using (HWalk.DAL.WalkEntities wtx = new DAL.WalkEntities())
                {
                    var _walkers = (from w in wtx.WALKERs
                                    select w).ToList();


                    var _path = System.Configuration.ConfigurationManager.AppSettings["StandardFileLocation"].Trim();
                    string filename = _path+ DateTime.Now.Second + "_walkers.docx";


                    using (DocX document = DocX.Create(filename))
                    {
                        foreach (var w in _walkers)
                        {
                            Novacode.Table walk_table_header = document.InsertTable(2, 2);
                            walk_table_header.AutoFit = AutoFit.Window;

                            walk_table_header.Alignment = Alignment.center;

                            //Set Table border
                            //Novacode.Border border = new Border();
                            //border.Tcbs = Novacode.BorderStyle.Tcbs_single;
                            //walk_table_header.SetBorder(TableBorderType.Right, border);
                            //walk_table_header.SetBorder(TableBorderType.Left, border);
                            //walk_table_header.SetBorder(TableBorderType.Bottom, border);
                            //walk_table_header.SetBorder(TableBorderType.Top, border);
                            //walk_table_header.SetBorder(TableBorderType.InsideH, border);
                            //walk_table_header.SetBorder(TableBorderType.InsideV, border);

                            // Insert Logo
                            walk_table_header.Rows[0].Cells[0].Width = 75;
                            walk_table_header.Rows[0].Cells[0].VerticalAlignment = VerticalAlignment.Top;
                            using (MemoryStream ms = new MemoryStream())
                            {
                                System.Drawing.Image myImg = System.Drawing.Image.FromFile(System.Configuration.ConfigurationManager.AppSettings["logoPath"].ToString() + @"Logo-Sm-300.png");

                                myImg.Save(ms, myImg.RawFormat);  // Save your picture in a memory stream.
                                ms.Seek(0, SeekOrigin.Begin);

                                Novacode.Image img = document.AddImage(ms); // Create image.

                                walk_table_header.Rows[0].Cells[0].RemoveParagraphAt(0);
                                Paragraph logo_para = walk_table_header.Rows[0].Cells[0].InsertParagraph();

                                Novacode.Picture pic1 = img.CreatePicture();
                                pic1.SetPictureShape(BasicShapes.cube);
                                pic1.Width = 75;
                                pic1.Height = 75;

                                logo_para.InsertPicture(pic1, 0);
                            }

                            //Insert Walker Name
                            walk_table_header.Rows[0].Cells[1].RemoveParagraphAt(0);
                            walk_table_header.Rows[0].Cells[1].VerticalAlignment = VerticalAlignment.Top;
                            walk_table_header.Rows[0].Cells[1].Width = 400;
                            Paragraph wlk_name = walk_table_header.Rows[0].Cells[1].InsertParagraph(w.WALKER_FNAME.ToString().Trim() + " " + w.WALKER_LNAME.ToString().Trim()).FontSize(18).Bold();
                            wlk_name.Alignment = Alignment.center;

                            //Create walk table
                            double total = 0;
                            var _walks = (from k in w.MILEAGEs
                                          where k.WALKER_ID == w.WALKER_ID
                                          select k).ToList();

                            //create table
                            Novacode.Table walk_steps = walk_table_header.Rows[1].Cells[0].InsertTable(22, 3);
                            //standard_steps.AutoFit = AutoFit.Contents;
                            walk_steps.Rows[0].Cells[2].Width = 1;
                            walk_steps.Rows[0].Cells[0].InsertParagraph("Date").Bold();
                            walk_steps.Rows[0].Cells[1].Width = 400;
                            walk_steps.Rows[0].Cells[1].InsertParagraph("Distance").Bold();
                            walk_steps.Rows[0].Cells[2].Width = 400;
                            walk_steps.Rows[0].Cells[2].InsertParagraph("Miles So Far").Bold();

                            //Set Table border
                            Novacode.Border border = new Border();
                            border.Tcbs = Novacode.BorderStyle.Tcbs_single;
                            walk_steps.SetBorder(TableBorderType.Right, border);
                            walk_steps.SetBorder(TableBorderType.Left, border);
                            walk_steps.SetBorder(TableBorderType.Bottom, border);
                            walk_steps.SetBorder(TableBorderType.Top, border);
                            walk_steps.SetBorder(TableBorderType.InsideH, border);
                            walk_steps.SetBorder(TableBorderType.InsideV, border);

                            Novacode.Table walk_steps2 = walk_table_header.Rows[1].Cells[1].InsertTable(22,3);
                            //standard_steps.AutoFit = AutoFit.Contents;
                            walk_steps2.Rows[0].Cells[2].Width = 1;
                            walk_steps2.Rows[0].Cells[0].InsertParagraph("Date").Bold();
                            walk_steps2.Rows[0].Cells[1].Width = 400;
                            walk_steps2.Rows[0].Cells[1].InsertParagraph("Distance").Bold();
                            walk_steps2.Rows[0].Cells[2].Width = 400;
                            walk_steps2.Rows[0].Cells[2].InsertParagraph("Miles So Far").Bold();

                            walk_steps2.SetBorder(TableBorderType.Right, border);
                            walk_steps2.SetBorder(TableBorderType.Left, border);
                            walk_steps2.SetBorder(TableBorderType.Bottom, border);
                            walk_steps2.SetBorder(TableBorderType.Top, border);
                            walk_steps2.SetBorder(TableBorderType.InsideH, border);
                            walk_steps2.SetBorder(TableBorderType.InsideV, border);

                            //loop list
                            //42
                            int _rownum = 0;
                            foreach (var mile in _walks)
                            {
                                if (_rownum < 21)
                                {
                                    walk_steps.InsertRow();
                                    _rownum++;
                                    walk_steps.Rows[_rownum].Cells[0].InsertParagraph(mile.MILEAGE_DT.ToShortDateString());
                                    walk_steps.Rows[_rownum].Cells[1].InsertParagraph(mile.MILEAGE1.ToString().Trim());
                                    total += mile.MILEAGE1;
                                    walk_steps.Rows[_rownum].Cells[2].InsertParagraph(total.ToString());
                                }
                                else
                                {
                                    walk_steps2.InsertRow();
                                    _rownum++;
                                    walk_steps2.Rows[_rownum - 21].Cells[0].InsertParagraph(mile.MILEAGE_DT.ToShortDateString());
                                    walk_steps2.Rows[_rownum-21].Cells[1].InsertParagraph(mile.MILEAGE1.ToString().Trim());
                                    total += mile.MILEAGE1;
                                    walk_steps2.Rows[_rownum-21].Cells[2].InsertParagraph(total.ToString());
                                }
                            }

                            walk_steps2.InsertRow();
                            walk_steps2.Rows[22].Cells[2].InsertParagraph("Total: " + total).Bold();


                            document.InsertParagraph().InsertPageBreakAfterSelf();
                        }
                        document.Save();
                    }
                }
            }

        }
    }
}