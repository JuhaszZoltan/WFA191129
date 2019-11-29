using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace WFA191129
{
    public partial class FrmMain : Form
    {
        MySqlConnection conn;
        public FrmMain()
        {
            conn = new MySqlConnection(
                "Server=winsql.vereb.dc;" +
                "Database=diak90;" +
                "Uid=diak90;" +
                "Pwd=2JtORd;");
            InitializeComponent();
            FillDgv();
        }

        private void FillDgv()
        {
            dgvPkmn.Rows.Clear();
            conn.Open();
            var cmd = new MySqlCommand("SELECT * FROM pokemonok;", conn);
            var r = cmd.ExecuteReader();
            while (r.Read())
            {
                dgvPkmn.Rows.Add(r[0], r[1], r[2]);
            }
            conn.Close();
        }

        private void tbnUj_Click(object sender, EventArgs e)
        {
            conn.Open();

            var a = new MySqlDataAdapter()
            {
                InsertCommand = new MySqlCommand("INSERT INTO pokemonok (nev, tipus) VALUES " +
                $"('{tbNev.Text}', '{tbTipus.Text}');", conn),
            };
            a.InsertCommand.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("tábla frissítve!");
            FillDgv();
        }

        private void btnTorol_Click(object sender, EventArgs e)
        {
            conn.Open();
            var a = new MySqlDataAdapter()
            {
                DeleteCommand = new MySqlCommand("DELETE FROM pokemonok WHERE " +
                $"id = {dgvPkmn.SelectedRows[0].Cells[0].Value}", conn),
            };
            a.DeleteCommand.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("tábla frissítve!");
            FillDgv();
        }
    }
}
