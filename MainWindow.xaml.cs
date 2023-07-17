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
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using System.Collections;

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
            OleDbConnection conn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" +
               @"Data Source = C:\Users\Administrator\Documents\Kutuphane.accdb;" + "User Id=Admin;Password=;");
            conn.Open();

            //dataGrid.Items.Clear();


            //!!! Hata !!! -> Bir çalıştırışta sadece bir kez işlem gerçekleştirebiliyorsun.

            string query = "SELECT * FROM Kitaplar WHERE 1=1";
            if (!string.IsNullOrEmpty(TxtID.Text)) //calisiyo
            {
                query += " AND kitapid = " + TxtID.Text;
            }
            if (!string.IsNullOrEmpty(TxtAd.Text)) //calisiyo
            {
                query += " AND kitapad LIKE '%" + TxtAd.Text + "%'";
            }
            if (!string.IsNullOrEmpty(TxtYazar.Text))//calisiyo
            {
                query += " AND yazar LIKE '%" + TxtYazar.Text + "%'";
            }

            //Where[Date] between "
            //+ dateTimePicker2.Value.ToString("#yyyy/MM/dd#")
            //if (DateSelect.SelectedDate != null) //calismiyor date kisminda hep bi hata veriyo
            //{
            //    query += " AND basimtarih= " + DateSelect.SelectedDate.Value.ToString("dd/MM/yyyy");
               
            //}

            //if (TxtKonu.SelectedItem != null) //calisiyo
            //{
            //    query += " AND konu = '" + ((ComboBoxItem)TxtKonu.SelectedItem).Content.ToString() + "'";
            //}

            //ClearInputs();

            //hoca ile yaptikkk
            //string a = "Select * from kitaplar where basimtarih between #05/07/2023# AND #20/07/2023# ";

            OleDbCommand cmd = new OleDbCommand(query, conn);//query yerine a yazarak aralıklı tarih araması yapabilirsin
            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            dataGrid.ItemsSource = CollectionViewSource.GetDefaultView(dt);

            conn.Close();





            //yeni bi deneme
            //DataTable tablo = new DataTable();
            //conn.Open();
            //SqlDataAdapter adtr = new SqlDataAdapter("select *from kitaplar where tc like %"+txt);


            //OleDbCommand show = new OleDbCommand("select * from kitaplar where 1=1");
            //if (!string.IsNullOrEmpty(TxtAd.Text))
            //{
            //    show += "AND kitapad = @p1" 
            //}


            //OleDbCommand show = new OleDbCommand("select * from kitaplar where kitapad = @kitapad", conn);
            //show.Parameters.AddWithValue("kitapad", TxtAd.Text);
            ////OleDbDataReader oku = show.ExecuteReader();
            //dataGrid.ItemsSource = show.ExecuteReader();




            ////Bu calisiyo
            //OleDbCommand show = new OleDbCommand("select * from kitaplar where kitapad = @p1 or yazar = @p2 or konu = @p4 or kitapid = @p5", conn);
            ////show.Parameters.AddWithValue("kitapad", TxtAd.Text);//Bu yorum satırı


            //show.Parameters.AddWithValue("@p1", TxtAd.Text);
            //show.Parameters.AddWithValue("@p2", (TxtYazar.Text));
            ////show.Parameters.AddWithValue("@p3", (DateSelect.SelectedDate));//yorum satırı
            //show.Parameters.AddWithValue("@p4", (TxtKonu.Text));
            //show.Parameters.AddWithValue("@p5", int.Parse(TxtID.Text));
            //dataGrid.ItemsSource = show.ExecuteReader();//bu yorum sat degil.

            ////OleDbDataReader oku = show.ExecuteReader();//yorum satırı



            //while (oku.Read())
            //{
            //    //TxtID.Text = oku["Id"].ToString();
            //    //TxtAd.Text = oku["Kitap Adı"].ToString();
            //    //TxtYazar.Text = oku["Yazar"].ToString();
            //    ////DateSelect.SelectedDate = oku["Basım Tarih"].ToString();
            //    ////txtParola.Text = oku["Parola"].ToString();
            //    //TxtKonu.Text = oku["Konu"].ToString() ;

            //    ListViewItem ekle = new ListViewItem();
            //    ekle.Content = oku["kitapad"].ToString();
            //    //ekle.SubItems.Add(oku["yazar"].ToString());
            //    ////ekle.SubItems.Add(oku["Ad"].ToString());
            //    //ekle.SubItems.Add(oku["basimtarih"].ToString());
            //    //ekle.SubItems.Add(oku["konu"].ToString());

            //    dataGrid.Items.Add(ekle);
            //}
            //conn.Close();
        }

        //private void ClearInputs()
        //{
        //    TxtID.Text = string.Empty;
        //    TxtAd.Text = string.Empty;
        //    TxtYazar.Text = string.Empty;
        //    TxtKonu.SelectedItem = null;
        //    DateSelect.SelectedDate = null;
        //}

        private void BtnGuncelle_Click(object sender, RoutedEventArgs e)
        {
            OleDbConnection conn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" +
            @"Data Source = C:\Users\Administrator\Documents\Kutuphane.accdb;" + "User Id=Admin;Password=;");
            conn.Open();

            //ad ve yazar bossa mesaj dondurur
            if (TxtAd.Text == "" && TxtYazar.Text == "")
            {
                MessageBox.Show("Doldurulması gereken alanları doldurma");
                return;
            }

            //Id kısmı bos ve diger seyler doluysa mesaj bastırır ekrana
            if (TxtAd.Text != "" && TxtYazar.Text != "" && (DateSelect.SelectedDate) != null)
            {
                MessageBox.Show("Lütfen ID kısmını doldurunuz");
                return;
            }

            OleDbCommand show = new OleDbCommand("update kitaplar set kitapad =@p1,yazar=@p2,basimtarih=@p3,konu=@p4 where kitapid = @p5", conn);

            show.Parameters.AddWithValue("@p1", (TxtAd.Text));
            show.Parameters.AddWithValue("@p2", (TxtYazar.Text));
            show.Parameters.AddWithValue("@p3", (DateSelect.SelectedDate));
            show.Parameters.AddWithValue("@p4", (int)TxtKonu.SelectedItem);
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
            show.Parameters.AddWithValue("@p5", (Konular)Enum.Parse(typeof(Konular), TxtKonu.SelectedItem.ToString()));

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

            //if (Konular k = (Konular)2)
            //{
                
            //}

            OleDbCommand show = new OleDbCommand("Select * From Kitaplar", conn);

            dataGrid.ItemsSource = show.ExecuteReader();

            //sayi = 5;
            //for (int i = 0; i < sayi.Length; i++)
            //{

            //}

        }

        private void TxtKonu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }


        public enum Konular
        {
            Seciniz,
            Araştırma,
            Bilim,
            Edebiyat,
            Felsefe,
            Tarih
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Console.WriteLine(Konular.Felsefe);
            int konu = (int)Konular.Felsefe;
            Console.WriteLine(konu);

            var wd = (Konular)1;
            Console.WriteLine(wd);


            //Hoca ile yazdık
            Konular k = (Konular)2;
            MessageBox.Show(k.ToString());
            Konular.Bilim.ToString();

            //************************************************


            //base.Window_Loaded(e);

            //List<string> konuListesi = Enum.GetNames(typeof(Konular)).ToList();
            //TxtKonu.ItemsSource = konuListesi;
            //TxtKonu.SelectedIndex = 0;

            //List<string> konuListesi = Enum.GetNames(typeof(Konular)).ToList();

            TxtKonu.ItemsSource = Enum.GetValues(typeof(Konular));
            TxtKonu.SelectedIndex = 0;

            //List<string> konuListesi = Enum.GetNames(typeof(Konular)).ToList();

            // ComboBox'a enum değerlerini string olarak ata
            //TxtKonu.ItemsSource = konuListesi;

            // Default olarak ilk öğeyi seçin
            //TxtKonu.SelectedIndex = 0;

            //OleDbConnection conn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" +
            //@"Data Source = C:\Users\Administrator\Documents\Kutuphane.accdb;" + "User Id=Admin;Password=;");
            //conn.Open();

            ////using (OleDbConnection connection = new OleDbConnection(conn))
            ////{
            //OleDbCommand command = new OleDbCommand("SELECT DISTINCT konu FROM kitaplar", conn);
            //OleDbDataReader reader = command.ExecuteReader();
            //List<string> konular = new List<string>();
            //while (reader.Read())
            //{
            //    konular.Add(reader.GetString(0));
            //}
            ////TxtKonu.ItemsSource = konular;
            ////}
            ///





        

        }
        public class Kitap
        {
            public int konu { get; set; }
        }

        public class sanalDatabase 
        {
            ArrayList sanalDB = new ArrayList();
            public kitapReturnValue KonularSecim(Konular K) 
            {
                sanalDB.Add(K);
                return kitapReturnValue.Felsefe;
            }
        }

        class Program 
        {
            static void Main(string[] args) 
            {
                Kitap K1 = new Kitap();
                K1.konu = 1;

                sanalDatabase SB = new sanalDatabase();
                kitapReturnValue kitapSecimSonuc = SB.KonularSecim(K1);

                //if (kitapSecimSonuc == kitapReturnValue.Felsefe)
                //{

                //}

                int enumInt = (int)kitapReturnValue.Tarih;
                Console.WriteLine(enumInt);

            }
        }

        public enum kitapReturnValue 
        {
           
            Araştırma = 1,
            Bilim = 2,
            Edebiyat = 3,
            Felsefe = 4,
            Tarih = 5
        }
    }
}
