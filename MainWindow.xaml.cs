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
using KutuphaneOtomasyon.DataSet1TableAdapters;
using System.Windows.Controls.Primitives;

namespace KutuphaneOtomasyon
{
    public partial class MainWindow : System.Windows.Window
    {


        public MainWindow()
        {
            InitializeComponent();
        }            
            private void BtnAra_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    using (OleDbConnection conn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" +
                        @"Data Source = C:\Users\Administrator\Documents\Kutuphane.accdb;" + "User Id=Admin;Password=;"))
                    {
                        conn.Open();

                    string query = "SELECT Kitaplar.kitapid, Kitaplar.kitapad, Kitaplar.yazar, Kitaplar.basimtarih, Konular.kitapkonu " +
                                   "FROM Kitaplar INNER JOIN Konular ON Kitaplar.konu = Konular.konuid " +
                                   "WHERE 1=1";



                    if (!string.IsNullOrEmpty(TxtID.Text))
                        {
                            query += " AND Kitaplar.kitapid = @kitapid";
                        }

                    if (!string.IsNullOrEmpty(TxtAd.Text))
                        {
                            query += " AND Kitaplar.kitapad LIKE @kitapad";
                        }

                        if (!string.IsNullOrEmpty(TxtYazar.Text))
                        {
                            query += " AND Kitaplar.yazar LIKE @yazar";
                        }

                        //if (DateSelect.SelectedDate != null)
                        //{
                        //    query += " AND Kitaplar.basimtarih = @basimtarih";
                        //}

                    if (DateSelect.SelectedDate != null)
                    {
                        if (TxtAralik.Text == "Öncesi" )
                        {
                            string aralikSecim = ((ComboBoxItem)TxtAralik.SelectedItem)?.Content.ToString();
                            //query += " Select Kitaplar.basimtarih from Kitaplar where Kitaplar.basimtarih < @basimtarih ";
                            query = "Select * from Kitaplar where Kitaplar.basimtarih < @basimtarih ";

                        }
                        if (TxtAralik.Text == "Sonrası")
                        {
                            string aralikSecim = ((ComboBoxItem)TxtAralik.SelectedItem)?.Content.ToString();
                            query = " Select * from Kitaplar where Kitaplar.basimtarih > @basimtarih ";
                        }
                        if(TxtAralik.Text != "Öncesi" && TxtAralik.Text != "Sonrası")
                        {
                            query += " AND Kitaplar.basimtarih = @basimtarih";
                        }
                        
                    }

                    if (TxtKonu.Text == "Edebiyat" || TxtKonu.Text == "Tarih" || TxtKonu.Text == "Araştırma" || 
                        TxtKonu.Text == "Bilim" || TxtKonu.Text == "Felsefe")
                        {
                            string konuSecim = ((ComboBoxItem)TxtKonu.SelectedItem)?.Content.ToString();
                            query += " AND Konular.kitapkonu = @kitapkonu";
                        }

                        OleDbCommand cmd = new OleDbCommand(query, conn);

                    if (!string.IsNullOrEmpty(TxtID.Text))
                        {
                            cmd.Parameters.AddWithValue("@kitapid", int.Parse(TxtID.Text));
                        }

                        if (!string.IsNullOrEmpty(TxtAd.Text))
                        {
                            cmd.Parameters.AddWithValue("@kitapad", TxtAd.Text);
                        }

                        if (!string.IsNullOrEmpty(TxtYazar.Text))
                        {
                            cmd.Parameters.AddWithValue("@yazar", "%" + TxtYazar.Text + "%");
                        }

                        if (DateSelect.SelectedDate != null)
                        {
                            cmd.Parameters.AddWithValue("@basimtarih", DateSelect.SelectedDate.Value);
                        }

                        if (TxtKonu.SelectedItem != null)
                        {
                            string konuSecim = ((ComboBoxItem)TxtKonu.SelectedItem)?.Content.ToString();
                            cmd.Parameters.AddWithValue("@kitapkonu", konuSecim);
                        }

                        OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                        System.Data.DataTable dt = new System.Data.DataTable();
                        da.Fill(dt);
                        dataGrid.ItemsSource = CollectionViewSource.GetDefaultView(dt);

                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }

        

        ////!!! Hata !!! -> Bir çalıştırışta sadece bir kez işlem gerçekleştirebiliyorsun.

        //string query = "SELECT * FROM Kitaplar WHERE 1=1";
        //if (!string.IsNullOrEmpty(TxtID.Text)) //calisiyo
        //{
        //    query += " AND kitapid = " + TxtID.Text;
        //}
        //if (!string.IsNullOrEmpty(TxtAd.Text)) //calisiyo
        //{
        //    query += " AND kitapad LIKE '%" + TxtAd.Text + "%'";
        //}
        //if (!string.IsNullOrEmpty(TxtYazar.Text))//calisiyo
        //{
        //    query += " AND yazar LIKE '%" + TxtYazar.Text + "%'";
        //}

        ////Where[Date] between "
        ////+ dateTimePicker2.Value.ToString("#yyyy/MM/dd#")
        ////if (DateSelect.SelectedDate != null) //calismiyor date kisminda hep bi hata veriyo
        ////{
        ////    query += " AND basimtarih= " + DateSelect.SelectedDate.Value.ToString("dd/MM/yyyy");

        ////}

        //if (TxtKonu.SelectedItem != null) //calisiyo
        //{
        //    query += " AND konu = '" + ((ComboBoxItem)TxtKonu.SelectedItem).Content.ToString() + "'";
        //}


        ////hoca ile yaptikkk
        ////string a = "Select * from kitaplar where basimtarih between #05/07/2023# AND #20/07/2023# ";

        //OleDbCommand cmd = new OleDbCommand(query, conn);//query yerine a yazarak aralıklı tarih araması yapabilirsin
        //OleDbDataAdapter da = new OleDbDataAdapter(cmd);
        //System.Data.DataTable dt = new System.Data.DataTable();
        //da.Fill(dt);
        //dataGrid.ItemsSource = CollectionViewSource.GetDefaultView(dt);

        //conn.Close();

        //*******************************************************************************************************



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




        //////Bu calisiyo
        //OleDbCommand show = new OleDbCommand("Select Kitaplar.kitapid or Kitaplar.kitapad or Kitaplar.yazar or Kitaplar.basimtarih or konular.kitapkonu from kitaplar, konular where kitaplar.konu=konular.konuid ", conn);
        ////show.Parameters.AddWithValue("kitapad", TxtAd.Text);//Bu yorum satırı


        //show.Parameters.AddWithValue("kitapad", TxtAd.Text);
        //show.Parameters.AddWithValue("yazar", (TxtYazar.Text));
        //show.Parameters.AddWithValue("basimtarih", (DateSelect.SelectedDate));//yorum satırı
        //show.Parameters.AddWithValue("kitapkonu", (TxtKonu.Text));
        //show.Parameters.AddWithValue("kitapid", int.Parse(TxtID.Text));
        //dataGrid.ItemsSource = show.ExecuteReader();//bu yorum sat degil.

        ////OleDbDataReader oku = show.ExecuteReader();//yorum satı





        private void BtnGuncelle_Click(object sender, RoutedEventArgs e)
            {
            //chatgpt den destek alindi.
                try
                {
                    using (OleDbConnection conn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" +
                        @"Data Source = C:\Users\Administrator\Documents\Kutuphane.accdb;" + "User Id=Admin;Password=;"))
                    {
                        conn.Open();

                        // Kitaplar tablosunda güncelleme yapma
                        OleDbCommand guncelleCommand = new OleDbCommand("UPDATE Kitaplar " +
                            "SET kitapad = @p1, yazar = @p2, basimtarih = @p3, konu = @p4 " +
                            "WHERE kitapid = @p5", conn);
                        guncelleCommand.Parameters.AddWithValue("@p1", TxtAd.Text);
                        guncelleCommand.Parameters.AddWithValue("@p2", TxtYazar.Text);
                        guncelleCommand.Parameters.AddWithValue("@p3", DateSelect.SelectedDate);
                        guncelleCommand.Parameters.AddWithValue("@p4", GetKonuID(((ComboBoxItem)TxtKonu.SelectedItem)?.Content.ToString()));
                        guncelleCommand.Parameters.AddWithValue("@p5", int.Parse(TxtID.Text));

                        // Güncelleme işlemini gerçekleştirme
                        int affectedRows = guncelleCommand.ExecuteNonQuery();

                        if (affectedRows > 0)
                            MessageBox.Show("Güncelleme işlemi başarıyla gerçekleşti.");
                        else
                            MessageBox.Show("Güncelleme işlemi başarısız oldu. Belirtilen ID'ye ait bir kayıt bulunamadı.");

                        conn.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Bir hata oluştu: " + ex.Message);
                }
            }



        private void BtnEkle_Click(object sender, RoutedEventArgs e)
        {
            try //chatgpt den yardım alindi.
            {
                using (OleDbConnection conn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" +
                    @"Data Source = C:\Users\Administrator\Documents\Kutuphane.accdb;" + "User Id=Admin;Password=;"))
                {
                    conn.Open();

                    // Önce konuID'yi almak için Konular tablosundan konuSecim değerine göre sorgu yapalım
                    string konuSecim = ((ComboBoxItem)TxtKonu.SelectedItem)?.Content.ToString();
                    int konuID = GetKonuID(konuSecim);

                    // Kitaplar tablosuna veri ekleme
                    OleDbCommand ekleCommand = new OleDbCommand("INSERT INTO Kitaplar (kitapid, kitapad, yazar, basimtarih, konu) " +
                        "VALUES (@p1, @p2, @p3, @p4, @p5)", conn);
                    ekleCommand.Parameters.AddWithValue("@p1", int.Parse(TxtID.Text));
                    ekleCommand.Parameters.AddWithValue("@p2", TxtAd.Text);
                    ekleCommand.Parameters.AddWithValue("@p3", TxtYazar.Text);
                    ekleCommand.Parameters.AddWithValue("@p4", DateSelect.SelectedDate);
                    ekleCommand.Parameters.AddWithValue("@p5", konuID);

                    // Veri ekleme işlemini gerçekleştirme
                    ekleCommand.ExecuteNonQuery();

                    MessageBox.Show("Ekleme işlemi başarıyla gerçekleşti.");

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Bir hata oluştu: " + ex.Message);
            }
        }

        //konuyu buradan atıyorum
        private int GetKonuID(string konuSecim)
        {
            switch (konuSecim)
            {
                case "Araştırma":
                    return 1;
                case "Bilim":
                    return 2;
                case "Edebiyat":
                    return 3;
                case "Felsefe":
                    return 4;
                case "Tarih":
                    return 5;
                default:
                    return 0; // Varsayılan değer
            }
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
            //butona basınca listeler
            OleDbConnection conn = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" +
            @"Data Source = C:\Users\Administrator\Documents\Kutuphane.accdb;" + "User Id=Admin;Password=;");
            conn.Open();


            OleDbCommand show = new OleDbCommand("Select Kitaplar.kitapid, Kitaplar.kitapad, Kitaplar.yazar, Kitaplar.basimtarih, konular.kitapkonu from kitaplar, konular where kitaplar.konu=konular.konuid ", conn);

            dataGrid.ItemsSource = show.ExecuteReader();


        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void TxtKonu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
