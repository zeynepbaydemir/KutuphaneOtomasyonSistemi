using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;
using System.Windows.Markup;
using System.Drawing;

namespace KutuphaneOtomasyon
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {


        public MainWindow()
        {
            InitializeComponent();


        }

        //OleDbConnection conn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" +
        //            @"Data Source = C:\Users\Administrator\Documents\Kutuphane.accdb;" + "User Id=Admin;Password=;");

        /*
        public void BtnListe_Click(object sender, RoutedEventArgs e, DataGrid dataGrid)
        {


            //OleDbCommand command = new OleDbCommand("Select * From Student", conn);
            //BtnListele.ItemSource = command.ExecuteReader();

            //OleDbCommand show = new OleDbCommand("Select * From Kitaplar", conn);
            //string sorgu = "select * from yazarlar";
            //NpgsqlDataAdapter da = new NpgsqlDataAdapter(sorgu, bglnti);
            //DataSet ds = new DataSet();
            //da.Fill(ds);
            //dataGrid.DataSource = ds.Tables[0];

            OleDbConnection conn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" +
@"Data Source = C:\Users\Administrator\Documents\Kutuphane.accdb;" + "User Id=Admin;Password=;");
            conn.Open();

            //OleDbCommand command = new OleDbCommand("SELECT * FROM kitaplar", conn);

            //OleDbDataAdapter adapter = new OleDbDataAdapter(command);
            //System.Data.DataTable dt = new System.Data.DataTable();
            //adapter.Fill(dt);
            //dataGrid.ItemsSource = dt.DefaultView;

            OleDbCommand show = new OleDbCommand("Select * From Kitaplar", conn);
            dataGrid.ItemsSource = show.ExecuteReader();


        }*/


        private void BtnAra_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnGuncelle_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {

            //OleDbCommand cmd = conn.CreateCommand();
            //conn.Open();
            //cmd.CommandText = "Insert into kitaplar(kitapid, kitapad, yazar, basimtarih, konu) Values('" + textBox1.Text + "','" + textBox2.Text + "')";
            //cmd.Connection = conn;
            //cmd.ExecuteNonQuery();
            //MessageBox.Show("Record Submitted", "Congrats");
            //conn.Close();

            //baglanti.Open();
            //string query = "INSERT INTO kitaplar (kitapid,kitapad,yazar,basimtarih,konu) VALUES" + "(@kitapad,@yazar,@basimtarih,@konu)";
            //cmd = new OleDbCommand(query, conn);
            //cmd.Parameters.AddWithValue("@kitapad", txtKitapAd.Text);
            //cmd.Parameters.AddWithValue("@yazar", TxtYazar.Text);
            //cmd.Parameters.AddWithValue("@basimtarih", dtpBasimTarih.Value);
            //cmd.Parameters.AddWithValue("@konu",IntKonu.Int);

        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {

        }


        private void BtnListe_Click_1(object sender, RoutedEventArgs e)
        {
            OleDbConnection conn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" +
@"Data Source = C:\Users\Administrator\Documents\Kutuphane.accdb;" + "User Id=Admin;Password=;");
            conn.Open();


            OleDbCommand show = new OleDbCommand("Select * From Kitaplar", conn);
            dataGrid.ItemsSource = show.ExecuteReader();

        }
    }
}
