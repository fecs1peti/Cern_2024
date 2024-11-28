using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cern_2024
{
    public partial class Form1 : Form
    {
        int v_akt = 12, e_db = 0;
        int vj_l, vf_t, vb_l, va_t;
        List<Label> e_all = new List<Label>();
        Timer new_e = new Timer();
        Timer move_e = new Timer();
        string a_i;
        List<int> del_all = new List<int>();

        public Form1()
        {
            InitializeComponent();
            Start();

        }
        void Start()
        {
            E_500ms();
            //New_Electron();
            E_Moving();

        }

        void E_500ms()
        {
            new_e.Interval = 500;
            new_e.Start();
            new_e.Tick += (sb, eb) =>
            {
                New_Electron();
            };
        }
        void New_Electron()
        {
            Label uj = new Label();
            uj.Width = 10;
            uj.Height = 10;
            uj.BackColor = Color.Yellow;
            uj.ForeColor = Color.Yellow;
            uj.BorderStyle = BorderStyle.FixedSingle;
            uj.Text = "01";
            uj.TabIndex = 24;
            uj.Top = pb_VJobb.Bottom - 7;
            uj.Left = pb_VJobb.Left - 3;
            this.Controls.Add(uj);
            uj.BringToFront();
            e_all.Add(uj);
            pb_Fesz_Szab.BringToFront();
            l_AktFesz.BringToFront();
            b_VUp.BringToFront();
            b_VDown.BringToFront();
        }
        void E_Moving()
        {
            move_e.Interval = 30;
            move_e.Tick += (sm, em) =>
            {
                for (int i = 0; i < e_all.Count; i++)
                {
                    if (e_all[i].Visible==true)
                    {
                        int a_x = e_all[i].Left;
                        int a_y = e_all[i].Top;
                        a_i = e_all[i].Text;
                        if (a_i == "01")
                        {
                            if (a_y > pb_VJobb.Top + e_all[i].TabIndex)
                            {
                                e_all[i].Top -= e_all[i].TabIndex;

                            }
                            else
                            {
                                e_all[i].Text = "-10";
                                e_all[i].Top = pb_VJobb.Top - 3;
                            }
                        }
                        else if (a_i == "-10")
                        {
                            if (a_x > pb_VBal.Left + e_all[i].TabIndex)
                            {
                                e_all[i].Left -= e_all[i].TabIndex;
                            }
                            else
                            {
                                e_all[i].Text = "0-1";
                                e_all[i].Left = pb_VBal.Left - 3;
                            }
                            if (e_all[i].Left > pb_Fesz_Szab.Left && e_all[i].Left < pb_Fesz_Szab.Right)
                            {
                                e_all[i].TabIndex = v_akt;
                            }
                        }
                        else if (a_i == "0-1")
                        {
                            if (a_y < pb_VJobb.Bottom - e_all[i].TabIndex)
                            {
                                e_all[i].Top += e_all[i].TabIndex;
                            }
                            else
                            {
                                e_all[i].Text = "10";
                                e_all[i].Top = pb_VJobbAlsó.Top - 3;
                            }
                        }
                        else if (a_i == "10")
                        {
                            if (a_x < pb_VBalAlsó.Right - e_all[i].TabIndex)
                            {
                                e_all[i].Left += e_all[i].TabIndex;
                            }
                            else
                            {
                                del_all.Add(i);
                                e_all[i].Hide();

                                e_db++;
                                l_e_db.Text = "Electron Passsed: " + e_db.ToString();
                            }
                        };
                    }
                    /*if (del_all.Count > 0)
                    {
                        e_all.RemoveAt(del_all[0]);
                        del_all.RemoveAt(0);
                    }*/
                }
            };
            move_e.Start();
        }

        private void b_VUp_Click(object sender, EventArgs e)
        {
            if (v_akt < 24)
            {
                v_akt++;
            }
            l_AktFesz.Text = v_akt.ToString() + "V";
        }

        private void b_VDown_Click(object sender, EventArgs e)
        {
            if (v_akt > 1)
            {
                v_akt--;
            }
            l_AktFesz.Text = v_akt.ToString() + "V";
        }
    }
}
