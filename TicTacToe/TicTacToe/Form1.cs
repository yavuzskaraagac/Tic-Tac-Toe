using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TicTacToe
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            yenile(); // Oyunu başlatırken butonları yenile
        }

        // X ve O oyuncularını temsil eden enum
        public enum oyuncular
        {
            X, O
        }

        oyuncular oyuncuX; // Mevcut oyuncuyu takip etmek için değişken (X veya O)
        int oyuncu = 0;    // Oyuncunun skorunu tutan sayaç
        int bilgisayar = 0; // Bilgisayarın skorunu tutan sayaç
        Random random = new Random(); // Bilgisayarın hamleleri için rastgele nesnesi
        List<Button> butonlar; // Butonları (oyun tahtasını) tutan liste

        // Bilgisayarın hamle yapması için event
        private void OyuncuSure(object sender, EventArgs e)
        {
            if (butonlar.Count > 0) // Boşta buton kaldıysa
            {
                int index = random.Next(butonlar.Count); // Rastgele bir buton seç
                butonlar[index].Enabled = false; // Seçilen butonu devre dışı bırak
                oyuncuX = oyuncular.O; // Bilgisayarın sembolünü 'O' olarak ayarla
                butonlar[index].Text = oyuncuX.ToString(); // Butonda 'O' göster
                butonlar[index].BackColor = Color.Yellow; // Bilgisayarın hamlesini göstermek için rengi değiştir
                butonlar.RemoveAt(index); // Butonu kullanılabilir butonlardan kaldır
                pcZaman.Stop(); // Bilgisayar hamlesinden sonra zamanlayıcıyı durdur
                OyunKontrol(); // Kazanan olup olmadığını kontrol et
            }
        }

        // Yenile butonuna tıklanınca oyunu sıfırlama işlemi
        private void yenileButon(object sender, EventArgs e)
        {
            yenile();
        }

        // Oyuncunun hamle yapması için event
        private void OyuncuTikla(object sender, EventArgs e)
        {
            var button = (Button)sender; // Tıklanan butonu al
            oyuncuX = oyuncular.X; // Oyuncunun sembolünü 'X' olarak ayarla
            button.Text = oyuncuX.ToString(); // Butona 'X' göster
            button.Enabled = false; // Hamle sonrası butonu devre dışı bırak
            button.BackColor = Color.Green; // Oyuncunun hamlesini göstermek için rengi değiştir
            butonlar.Remove(button); // Butonu kullanılabilir butonlardan çıkar
            pcZaman.Start(); // Bilgisayarın hamlesi için zamanlayıcıyı başlat
            OyunKontrol(); // Kazanan olup olmadığını kontrol et
        }

        // Oyun tahtasını yenilemek ve sıfırlamak için metod
        private void yenile()
        {
            // Tüm butonları (Tic Tac Toe ızgarasını) içeren listeyi başlat
            butonlar = new List<Button> { button1, button2, button3, button4, button5, button6, button7, button8, button9 };

            // Tahtadaki her butonu sıfırla
            foreach (Button x in butonlar)
            {
                x.Enabled = true; // Butonu etkinleştir
                x.Text = "?"; // Varsayılan metni "?" olarak ayarla
                x.BackColor = DefaultBackColor; // Varsayılan arka plan rengine sıfırla
            }
        }

        // Kazanma durumunu kontrol eden metod
        private void OyunKontrol()
        {
            // Oyuncunun kazanıp kazanmadığını kontrol et (X sembolü için tüm kazanma kombinasyonları)
            if ((button1.Text == "X" && button2.Text == "X" && button3.Text == "X") ||
                (button4.Text == "X" && button5.Text == "X" && button6.Text == "X") ||
                (button7.Text == "X" && button8.Text == "X" && button9.Text == "X") ||
                (button1.Text == "X" && button4.Text == "X" && button7.Text == "X") ||
                (button2.Text == "X" && button5.Text == "X" && button8.Text == "X") ||
                (button3.Text == "X" && button6.Text == "X" && button9.Text == "X") ||
                (button1.Text == "X" && button5.Text == "X" && button9.Text == "X") ||
                (button3.Text == "X" && button5.Text == "X" && button7.Text == "X"))
            {
                pcZaman.Stop(); // Kazanma durumunda zamanlayıcıyı durdur
                MessageBox.Show("Kazanan Oyuncu"); // Kazanan mesajı göster
                oyuncu++; // Oyuncu skorunu artır
                label1.Text = "Oyuncu: " + oyuncu; // Oyuncu skorunu etikette güncelle
                yenile(); // Tahtayı sıfırla
            }
            // Bilgisayarın kazanıp kazanmadığını kontrol et (O sembolü için tüm kazanma kombinasyonları)
            else if ((button1.Text == "O" && button2.Text == "O" && button3.Text == "O") ||
                     (button4.Text == "O" && button5.Text == "O" && button6.Text == "O") ||
                     (button7.Text == "O" && button8.Text == "O" && button9.Text == "O") ||
                     (button1.Text == "O" && button4.Text == "O" && button7.Text == "O") ||
                     (button2.Text == "O" && button5.Text == "O" && button8.Text == "O") ||
                     (button3.Text == "O" && button6.Text == "O" && button9.Text == "O") ||
                     (button1.Text == "O" && button5.Text == "O" && button9.Text == "O") ||
                     (button3.Text == "O" && button5.Text == "O" && button7.Text == "O"))
            {
                pcZaman.Stop(); // Kazanma durumunda zamanlayıcıyı durdur
                MessageBox.Show("Kazanan Bilgisayar"); // Bilgisayarın kazandığını göster
                bilgisayar++; // Bilgisayarın skorunu artır
                label2.Text = "Bilgisayar: " + bilgisayar; // Bilgisayar skorunu etikette güncelle
                yenile(); // Tahtayı sıfırla
            }
        }
    }
}