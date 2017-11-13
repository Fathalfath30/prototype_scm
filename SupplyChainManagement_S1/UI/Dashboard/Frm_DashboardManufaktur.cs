using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MetroFramework.Forms;
using MySql.Data.MySqlClient;
using SupplyChainManagement_S1.MainScript;
using System.Globalization;
using SupplyChainManagement_S1.UI.Master;

namespace SupplyChainManagement_S1.UI.Dashboard
{
    public partial class Frm_DashboardManufaktur : MetroForm
    {
        private Cls_Barang CBarang;
        private Cls_DbConnection CDbConnection;
        private MySqlConnection SqlConn;

        public Frm_DashboardManufaktur(Cls_DbConnection ClsDB)
        {
            InitializeComponent();
            CDbConnection = ClsDB;
            SqlConn = ClsDB.Connection;

            CBarang = new Cls_Barang(SqlConn);
        }

        private void Frm_Manufaktur_Dashboard_Load(object sender, EventArgs e)
        {
            Tmr_Refresh_koneksi.Start();
            Tmr_RefreshDT.Start();
            Tmr_Refresh_data.Start();
        }

        private void Frm_Manufaktur_Dashboard_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void Tmr_Refresh_koneksi_Tick(object sender, EventArgs e)
        {
            if (this.SqlConn.State != ConnectionState.Open)
            {
                MLabel_Status_koneksi.Text = "Terputus";
                MLabel_Status_koneksi.ForeColor = Color.Red;
                Tmr_Refresh_koneksi.Stop();

                DialogResult Drs = MessageBox.Show(
                    this, 
                    "Koneksi dengan database terputus, Apakah anda ingin mencoba kembali ?", 
                    "Koneksi Terputus", 
                    MessageBoxButtons.RetryCancel, 
                    MessageBoxIcon.Information);

                if (Drs == DialogResult.Retry)
                {
                    CDbConnection.OpenConnection();
                    if (SqlConn.State == ConnectionState.Open)
                    {
                        SqlConn = CDbConnection.Connection;
                        Tmr_Refresh_koneksi.Start();
                    }
                }
                else
                {
                    MessageBox.Show(
                        this,
                        "Koneksi dengan database terputus, Apakah anda ingin mencoba kembali ?",
                        "Koneksi Terputus",
                        MessageBoxButtons.RetryCancel,
                        MessageBoxIcon.Information);
                    Application.Exit();
                }
            }
            else
            {
                MLabel_Status_koneksi.Text = "Terhubung";
                MLabel_Status_koneksi.ForeColor = Color.Lime;
            }
        }

        private void Tmr_RefreshDT_Tick(object sender, EventArgs e)
        {
            CultureInfo culture = new CultureInfo("id-ID");
            MLabel_Jam.Text = DateTime.Now.ToString("HH:mm:ss");
            DateTime dTime = new DateTime(
                Convert.ToInt32(DateTime.Now.ToString("yyyy")),
                Convert.ToInt32(DateTime.Now.ToString("MM")),
                Convert.ToInt32(DateTime.Now.ToString("dd")));

            MLabel_Tanggal.Text = dTime.ToString("dddd, dd MMMM yyyy", culture) ;

        }

        private void MTile_Barang_Click(object sender, EventArgs e)
        {
            Tmr_Refresh_data.Stop();
            new Frm_ManajemenBarang(CDbConnection).ShowDialog();
            Tmr_Refresh_data.Start();
        }

        private void Tmr_Refresh_data_Tick(object sender, EventArgs e)
        {
            MTile_Barang.TileCount = CBarang.rowCount();
        }
    }
}