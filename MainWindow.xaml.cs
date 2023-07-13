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
using System.Configuration;

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


        private void BtnAra_Click(object sender, RoutedEventArgs e)
        {

        }



        private void BtnGuncelle_Click(object sender, RoutedEventArgs e)
        {
            OleDbConnection conn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" +
            @"Data Source = C:\Users\Administrator\Documents\Kutuphane.accdb;" + "User Id=Admin;Password=;");
            conn.Open();

            OleDbCommand show = new OleDbCommand("update kitaplar set kitapad =@p1,yazar=@p2,basimtarih=@p3,konu=@p4 where kitapid = @p5", conn);

            show.Parameters.AddWithValue("@p1", (TxtAd.Text));
            show.Parameters.AddWithValue("@p2", (TxtYazar.Text));
            show.Parameters.AddWithValue("@p3", (DateSelect.SelectedDate));
            show.Parameters.AddWithValue("@p4", (TxtKonu.Text));
            show.Parameters.AddWithValue("@p5", int.Parse(TxtID.Text));

            show.ExecuteNonQuery();
            MessageBox.Show("Guncelleme islemi basarili bir sekilde gerceklesti.");

            conn.Close();
        }

        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {


            OleDbConnection conn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" +
            @"Data Source = C:\Users\Administrator\Documents\Kutuphane.accdb;" + "User Id=Admin;Password=;");
            conn.Open();

            OleDbCommand show = new OleDbCommand("insert into Kitaplar(kitapid, kitapad,yazar,basimtarih,konu) values (@p1,@p2,@p3,@p4,@p5)", conn);
            show.Parameters.AddWithValue("@p1", int.Parse(TxtID.Text));
            show.Parameters.AddWithValue("@p2", (TxtAd.Text));
            show.Parameters.AddWithValue("@p3", (TxtYazar.Text));
            show.Parameters.AddWithValue("@p4", (DateSelect.SelectedDate));
            show.Parameters.AddWithValue("@p5", (TxtKonu.Text));
            show.ExecuteNonQuery();
            conn.Close();
            MessageBox.Show("Ekleme islemi basarili bir sekilde gerceklesti.");

        }

        private void BtnSil_Click(object sender, RoutedEventArgs e)
        {
            //id ye gore silme islemi yapiyorum
            //id gir ve sil butonuna bas
            OleDbConnection conn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" +
            @"Data Source = C:\Users\Administrator\Documents\Kutuphane.accdb;" + "User Id=Admin;Password=;");
            conn.Open();

            OleDbCommand show = new OleDbCommand("Delete From kitaplar where kitapid = @p1", conn);
            show.Parameters.AddWithValue("@p1", int.Parse(TxtID.Text));

            show.ExecuteNonQuery();
            conn.Close();

            MessageBox.Show("Silme islemi basarili bir sekilde gerceklesti. ");

        }


        private void BtnListe_Click_1(object sender, RoutedEventArgs e)
        {
            //butona
            OleDbConnection conn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" +
            @"Data Source = C:\Users\Administrator\Documents\Kutuphane.accdb;" + "User Id=Admin;Password=;");
            conn.Open();


            OleDbCommand show = new OleDbCommand("Select * From Kitaplar", conn);
            dataGrid.ItemsSource = show.ExecuteReader();



        }

        private void TxtKonu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //baglanti.Open();
            //NpgsqlDataAdapter da = new NpgsqlDataAdapter("select * from kategoriler,yazarlar,yayinevleri", baglanti);
            //DataTable dt = new DataTable();
            //da.Fill(dt);
            //comboBox1.DisplayMember = "kategoriad";
            //comboBox2.DisplayMember = "yazarad";
            //comboBox3.DisplayMember = "yayineviad";
            //comboBox1.ValueMember = "kategoriid";
            //comboBox2.ValueMember = "yazarid";
            //comboBox3.ValueMember = "yayineviid";
            //comboBox1.DataSource = dt;
            //comboBox2.DataSource = dt;
            //comboBox3.DataSource = dt;
            //baglanti.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            OleDbConnection conn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" +
            @"Data Source = C:\Users\Administrator\Documents\Kutuphane.accdb;" + "User Id=Admin;Password=;");
            conn.Open();

            //using (OleDbConnection connection = new OleDbConnection(conn))
            //{
                OleDbCommand command = new OleDbCommand("SELECT DISTINCT konu FROM kitaplar", conn);
                OleDbDataReader reader = command.ExecuteReader();
                List<string> konular = new List<string>();
                while (reader.Read())
                {
                    konular.Add(reader.GetString(0));
                }
                TxtKonu.ItemsSource = konular;
            //}

        }
    }
}
