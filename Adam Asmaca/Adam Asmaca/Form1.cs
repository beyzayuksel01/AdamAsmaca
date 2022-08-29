using System;
using System.Collections.Generic; 
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Adam_Asmaca
{
    public partial class Form1 : Form
    {
        public Form1() 
        {
            InitializeComponent(); 
        }                //burada harflerimizi tanımlıyoruz ve harfler değişkenimize atıyoruz
        string[] harfler = { "a", "b", "c", "ç", "d", "e", "f", "g", "ğ", "h", "ı", "i", "j", "k", "l", "m", "n", "o", "ö", "p", "r", "s", "ş", "t", "u", "ü", "v", "y", "z" };
        Adam_Asmaca_islemleri cl_adam_asmaca;
        string kelime;  //kelime değişkeni oluşturuyoruz
        string[] secilen_harfler; // boş bir dize oluşturduk
        int can_hakki = 7; //can hakkını 7 olarak belirleyip değişkenimize atıyoruz
        int kalan_hakkimiz; //kalan hakkimiz , genişliş ve yükseklik değişkenleri oluşturuyoruz
        int width, height;
        private void Form1_Load(object sender, EventArgs e)
        {
            konum_ayarla();   //konumu ayarlaması için yükseklik ve genişlik tanımını ayarlıyoruz
            width = pcb_adam.Width;
            height = pcb_adam.Height;
            kalan_hakkimiz = can_hakki; //verdiğimiz can hakkını kalan hakkımıza atıyoruz
            secilen_harfler = new string[0]; //Boyut belirtmeden boş bir dizi oluşturuyoruz
            cl_adam_asmaca = new Adam_Asmaca_islemleri(); 
        }
        private void kelimeyi_Sec() //burada kelimeyi seç metodumuzu oluşturduk
        { 
           Adam_Asmaca_islemleri.Yeni_Kelime kelimemiz_ =    cl_adam_asmaca.kelime_al(); // atama işlemi yapıyoruz
           lbl_kategori.Text ="Bu bir " + kelimemiz_.kategori; //label a yazdıracağımız şeyi giriyoruz
           kelime = kelimemiz_.kelime;
           foreach (char harf in kelime.ToCharArray()) //foreach döngüsü kullanarak, kelime değişkenimizi tochararray kullanarak string ifadesini karakterlerine ayırarak dizi şeklinde döndürüp harf değişkenimizi içinde aratıyoruz
            {
               lbl_kelime.Text = lbl_kelime.Text + "_ "; // label a alt tire ekliyoruz ki yazdığımız harflerin yeri belli olsun?
           }
           button1.Visible = false; //burada buttonun görünmesini önlüyoruz
        }

        private void button1_Click(object sender, EventArgs e) //button 1 e basılınca olacak şeyleri ayarlıyoruz
        {
            pcb_adam.Visible = true; //adam figürünün görünürlüğünü true yapıyoruz
            textBox1.Visible = true; //textboxın da görünürlüğünü true yapıyoruz
            lbl_soylenenler.Visible = true; 
            lbl_kelime.Visible = true; //labellarımızın da görünebilirliğini true yapıyoruz
            lbl_kategori.Visible = true;
            button2.Visible = true;
            kelimeyi_Sec();
        }

        private void button2_Click(object sender, EventArgs e) //button2 ye basılınca yapılması gereken işlemleri giriyoruz
        {
            string veri = textBox1.Text; //girilen veriyi textbox1 e yazmasını sağlıyoruz
            bool buldu = false; //buldu değişkeni oluşturup false olarak atıyoruz
            bool soylendi = false; //soylendi değişkeni oluşturup false değeri döndürüyoruz
            foreach (var item in secilen_harfler) //seçilen harfler e baktırıp eğer aynı harf girilmişse söylendi değişkenimizi true yapıyoruz ve message boxta çıktı yazdırıyoruz
            {
                if (veri.ToLower() == item.ToLower()) //string ifadesi içerisinde bulunan tüm büyük harfleri küçük harfe dönüştürüyoruz
                {
                    soylendi = true;
                    MessageBox.Show("Bu harfi daha önce söylediniz");
                }
            }
            if (!soylendi) //eğer girilen harf önceden söylenmemişse aşağıdaki işlemleri yaptırıyoruz
            {
                for (int i = 0; i < harfler.Length; i++) // 0 dan başlayıp harflerin uzunluğuna kadar birer arttırarak teker teker kontrol ediyoruz
                {
                    if (veri.ToLower() == harfler[i]) //girilen veriyi küçük harfe dönüştürüp harfler değişkenimizde aratıyoruz 
                    {
                        buldu = true; //eğer harfler değişkenimizde varsa buldu yu true yapıyoruz
                        kelimede_bul(veri); //girilen veriyi kelimede bul 
                        break;
                    }
                }
                if (!buldu) //eğer girilen veri yanlışsa message boxta böyle bir çıktı yazdırıyoruz
                {
                    MessageBox.Show("29 harften birini yazınız");
                }
                guncelle();
            }
            int index = lbl_kelime.Text.IndexOf("_"); //IndexOf( ) metodu, kendisini çağıran string tipli değişkenin içinde kendisine parametre olarak verilen karakter veya stringi arar
            if (index == -1) //IndexOf( ) metodu, aradığını bulamazsa geriye -1 değerini döndürür
            {
                sonraki_bolum();
            }

        }
        private void kelimede_bul(string harf) //girilen harfi kelime içinde bulmaya çalışacağız
        {
            bool dogru_mu = false; //dogru mu diye boolean değişken oluşturup false döndürüyoruz
            char[] dizi = kelime.ToCharArray(); //girilen kelimeyi karakterlerine ayırarak dizi şekline döüştürüyoruz ve dizi değişkenine atıyoruz char olarak
            for (int a=0; a < dizi.Length;a++ ) //for döngüsü kullanarak dizi nin içindekileri kontrol ediyoruz
            {
                if (dizi[a].ToString() != "") 
                {
                    if (harf == dizi[a].ToString().ToLower()) //döndürülen dizeyi küçük harflere çeviriyoruz
                    {
                        dogru_mu = true; //doğruysa true dönmesini istiyoruz
                        lbl_kelime.Text = lbl_kelime.Text.Remove(a * 2, 1); //Geçerli dizeden belirtilen sayıda karakter silinmekte olan yeni bir dize döndürüyoruz
                        lbl_kelime.Text = lbl_kelime.Text.Insert(a * 2, harf).ToUpper(); //string ifade içerisinde belirtilen index numarası alanından itibaren eklenmesi istenen ifadeyi ekliyoruz ve büyük harfe çeviriyoruz

                    }
                }
            }
            if (!dogru_mu) //eğer doğru değil ise kalan can hakkımızı 1 azaltıyoruz
            {
                kalan_hakkimiz--;
                pcb_adam.Invalidate(); //burda da adam asma için formumuza çizim ekliyoruz
            }
            ekle_harf(harf); 
      
        }
        private void guncelle() //söylediğimiz harflerin listelenmesi için güncelle komut(metot)’unu oluşturuyoruz.
        {
            lbl_soylenenler.Text = ""; //Daha önce söylediğimiz bütün harfleri yazmak için lbl_soylenenler oluşturuyoruz.
            foreach (string  item in secilen_harfler) //Foreach döngüsü ile label_soylenenler.Text’e sırasıyla secilen_harfller’i yazmamızı sağlıyoruz.
            {
                lbl_soylenenler.Text = lbl_soylenenler.Text + item + " "; //İlk olarak label_soylenenler textini boş olarak tutuyotuz.
            } //Bir kez söylenenler bir daha yazıldığında zaten hata mesajımızı getiren bir koşulumuz olduğu için sadece soylendi=false durumundaki yere bu oluşturduğumuz güncelle metodumuzu koymamız gerekmektedir.
        }
        private void ekle_harf(string harf) 
        {
            string[] a = new string[secilen_harfler.Length + 1]; // yeni bir dize oluşturduk 
            for (int i = 0; i < secilen_harfler.Length; i++) // for döngümüzde de seçilen harflerin sayısından küçükse 1 arttırarak döngüye sokuyoruz
            {
                a[i] = secilen_harfler[i]; //secilen harflerdeki i.elemanları a değişkenimize atıyoruz
            }
            a[a.Length - 1] = harf; //a nın içindeki elemanların sayısından 1 azaltılıyor ve a ya harf değişkenini atıyoruz
            secilen_harfler = a; // burada da secilen harfler değişkenimize a değişkenini atıyoruz
        }

        private void pcb_adam_Paint(object sender, PaintEventArgs e) // burda adam asmacadaki çizimlerin özelliklerini tanımlıyoruz
        {
            Pen kalem = new Pen(Color.Black, 12); //burada şeklin çizimi için siyah rengi ve 12 fontunu seçiyoruz
            if (kalan_hakkimiz < can_hakki) //kalan can hakkımızı kontrol ediyoruz eğer toplam can hakkımızdan küçükse ilk çizgimizi yazdırıyoruz
            {
                e.Graphics.DrawLine(kalem, width / 10, height / 15, width / 10, height / 15 * 14); // çizimin özelliklerini, yükseklik ve genişliğini tanımlıyoruz
                e.Graphics.DrawLine(kalem, width / 10, height / 15, width / 2, height / 15);
            }
            if (kalan_hakkimiz < can_hakki - 1)//baş ve ipin özelliklerini tanımlıyoruz
            {
                e.Graphics.DrawLine(kalem, width / 2, height / 15, width / 2, height / 15 * 3);
                e.Graphics.DrawEllipse(kalem, width / 2-width/10, height / 5, width / 5, height / 10);
            }
            if (kalan_hakkimiz < can_hakki - 2)//gövdenin özelliklerini tanımlıyoruz
            {
                e.Graphics.DrawLine(kalem, width / 2, height / 10 * 3, width / 2, height / 10 * 6);
            }
            if (kalan_hakkimiz < can_hakki - 3)//sağ kolun özelliklerini tanımlıyoruz
            {
                e.Graphics.DrawLine(kalem, width / 2, height / 10 * 3, width / 2 + width / 10, height / 10 * 3 + height / 5);
            }
            if (kalan_hakkimiz < can_hakki - 4)//sol kolun özelliklerini tanımlıyoruz
            {
                e.Graphics.DrawLine(kalem, width / 2, height / 10 * 3, width / 2 - width / 10, height / 10 * 3 + height / 5);
            }
            if (kalan_hakkimiz < can_hakki - 5)//sağ ayak 
            {
                e.Graphics.DrawLine(kalem, width / 2, height / 10 * 6, width / 2 + width / 10, height / 10 * 6 + height / 10);
            }
            if (kalan_hakkimiz < can_hakki - 6)//sol ayak
            {
                e.Graphics.DrawLine(kalem, width / 2, height / 10 * 6, width / 2 - width / 10, height / 10 * 6 + height / 10);
                oyun_bitti(); 
            }
        }
        private void sonraki_bolum() // oyun bittikten sonra ekranı temizlemesi ve yeniden oynanabilmesi için method tanımlıyoruz
        {
            temizle();
            kelimeyi_Sec();
        }
        private void oyun_bitti() // oyun bitti metodu oluşturup içine işlemlerimizi giriyoruz
        {
            MessageBox.Show("Oyun Bitti.Doğru Kelime: "+kelime); //çktımızı alıyoruz
            button1.Visible = true; //button1 in görünürlüğünü true yapıyoruz
            textBox1.Visible = false; //textbox1 in görünürlüğünü false yapıyoruz yani görünmesini istemiyoruz
            button2.Visible = false; 
            temizle();
        }
        private void temizle() // temizle metodu oluşturup her şeyin temizlenmesini sağlıyoruz oyun başlamadan önce formumuz nasılsa o hale dönüyor
        {
            secilen_harfler = new string[0];
            kalan_hakkimiz = can_hakki;
            pcb_adam.Invalidate();
            lbl_kategori.Text = "";
            lbl_kelime.Text = "";
            lbl_soylenenler.Text = "";
            textBox1.Text = "";
        }

        private void Form1_SizeChanged(object sender, EventArgs e) //formumuzun boyutlarıı ayarlamak için yeni bir metod oluşturuyoruz
        {
            konum_ayarla();
        }
        public void konum_ayarla() //burada formumuzun içindeki konum düzenini ayarlıyoruz 
        {
            int width, height;
            width = this.Width;
            height = this.Height;
            panel1.Width = width;
            panel1.Height = height;
            panel1.Location = new Point(0, 0);
            pcb_adam.Width = width / 9* 4;
            pcb_adam.Height = height / 4 * 3;
            pcb_adam.Location = new Point(width - pcb_adam.Width - 10,10);
            lbl_kategori.Location = new Point(10, 10);
            lbl_kelime.Location = new Point(10, 50);
            lbl_soylenenler.Location = new Point(10, 120);
            button1.Size = new System.Drawing.Size(width / 3, height / 10);
            textBox1.Location = new Point(10, 90);
            button1.Location = new Point(width / 2-button1.Width/2, height / 2-button1.Height/2);
            button2.Location = new Point(50, 90);
            lbl_soylenenler.Location = new Point(10, height / 2);
        }
      
    }
    class Adam_Asmaca_islemleri
    {
        private List<Yeni_Kelime> yeni_kelime_listesi;//yeni kelime listesi
        private List<Yeni_Kelime> kelime_liste;
        public Adam_Asmaca_islemleri()
        {
            data_dosyasini_al();
        }
        public struct Yeni_Kelime
        {
             public string kelime; //kelimenin adı
             public string kategori; //kelimenin hangi katagoride olduğu
        }
        public Yeni_Kelime kelime_al() //yeni kelime üreticez
        {
            if (kelime_liste.Count == 0) 
            {
                kelime_liste = yeni_kelime_listesi; //kelime listesi yeni kelime listesine eşit olana kadar döngü devam ediyor
            }
            Random rastg = new Random(); //rastgele değer oluşturuyoruz
            int a = rastg.Next(0, kelime_liste.Count); // oluşturduğumuz rastg değişkenine rastgele bir tamsayı döndürmesi için next kullanıyoruz ve 0 dan başlayıp kelime_liste dizisinin eleman sayısına kadar olan kısmını a değişkeine atıyoruz
            Yeni_Kelime kelimemiz = kelime_liste[a];
            return kelimemiz;
        }
        private Yeni_Kelime Yeni_kelime_olustur(string kelime, string kategori)
        {
            Yeni_Kelime kelimemiz = new Yeni_Kelime(); //yeni kelime dizesi oluşturuyoruz 
            kelimemiz.kelime = kelime;
            kelimemiz.kategori = kategori;
            return kelimemiz;
        }
        private void data_dosyasini_al()
        {
            yeni_kelime_listesi = new List<Yeni_Kelime>();
            StreamReader oku = new StreamReader(@"data.txt",Encoding.Default); //dosyadan bir metin okumak için stream reader kullanıyoruz
            string veri;
            while ((veri = oku.ReadLine()) != null)
            {
                if (veri != "")
                {
                    yeni_kelime_listesi.Add(Yeni_kelime_olustur(veri.Split(',')[0], veri.Split(',')[1]));
                }
            }
            kelime_liste = yeni_kelime_listesi;
        }
    }
}
