using System;
using System.Data;
using System.Windows.Forms;
using MetroFramework.Forms;
using SupplyChainManagement_S1.MainClass;
using System.Globalization;
using System.Threading;
using System.ComponentModel;

namespace SupplyChainManagement_S1.UI.Supplier
{
    public partial class Frm_Dashboard : MetroForm
    {
        private App_Data appData;
        /* ----- [ MAIN FUNCTION ] ----- */
        private void InitMaps()
        {
            string path = string.Format("{0}\\map.html", Application.StartupPath);
            WebB_GMaps.Url = new Uri(path);
        }
        private void InitGrid_SP()
        {
            Grid_SPBahanBaku.Columns.Add("NamaSupplier", "Nama Supplier");
            Grid_SPBahanBaku.Columns.Add("Kendaraan", "Kendaraan");
            Grid_SPBahanBaku.Columns.Add("Tanggal", "Tanggal");
            Grid_SPBahanBaku.Columns.Add("JumlahBayar", "Jumlah Bayar");
            Grid_SPBahanBaku.Columns.Add("JumlahDp", "Jumlah Dp");
            Grid_SPBahanBaku.Columns.Add("JumlahPesanan", "Jumlah Pesanan");
            Grid_SPBahanBaku.Columns.Add("StatusPesanan", "Status Pesanan");
            Grid_SPBahanBaku.Columns["JumlahDp"].Visible = false;
            Grid_SPBahanBaku.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            Grid_SPBahanBaku.ReadOnly = true;
            Grid_SPBahanBaku.AutoGenerateColumns = false;
            Grid_SPBahanBaku.AllowDrop = false;
            Grid_SPBahanBaku.AllowUserToAddRows = false;
            Grid_SPBahanBaku.AllowUserToDeleteRows = false;
            for (int i = 0; i < Grid_SPBahanBaku.Columns.Count - 1; i++)
                Grid_SPBahanBaku.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
        }

        private void BindGrid_SP()
        {
            Grid_SPBahanBaku.Rows.Clear();
            for (int rIndex = 0; rIndex < appData.Data_SpBahanBaku.GetLength(0); rIndex++)
            {
                Grid_SPBahanBaku.Rows.Add(
                    appData.Data_SpBahanBaku[rIndex, 2].ToString(),
                    appData.Data_SpBahanBaku[rIndex, 9].ToString(),
                    appData.Data_SpBahanBaku[rIndex, 3].ToString(),
                    appData.Data_SpBahanBaku[rIndex, 5].ToString(),
                    appData.Data_SpBahanBaku[rIndex, 6].ToString(),
                    appData.Data_SpBahanBaku[rIndex, 7].ToString(),
                    appData.Data_SpBahanBaku[rIndex, 8].ToString()
                    );
            }
        }
        /* ----- [ GENERATED SCRIPT ] ----- */
        public Frm_Dashboard()
        {
            appData = new App_Data();
            InitializeComponent();
        }

        private void Frm_Dashboard_Load(object sender, EventArgs e)
        {
            InitMaps();
            InitGrid_SP();
            BindGrid_SP();
        }
    }
}