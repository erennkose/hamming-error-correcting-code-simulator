using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace mimariProje
{
    public partial class Form1 : Form
    {
        string a; // kullanicidan alinan datayi tutmak icin
        string h; // hata yapilacak konumu almak icin
        int hNumara; // h stringinin integer hali
        int parselayici = 0; //int.Parse islemlerini yapmak icin degisken
        string hammingliInput = ""; //hamming kodu eklenmis data
        string tmp2 = ""; // char dizisinden integera donusum icin kullandigim ara degisken
        int ekleme = 0; // toplam bitlere eklenecek check bitlerinin sayisi
        int hamming = 0; // hamming kod hesaplamak icin degisken
        int hataliHamming = 0; // hata yapildiktan sonra hamming kod icin degisken
        int hammingYerlestirme = 0; // hamming codeu girilen inputun arasina yerlestirmek icin
        string binaryHamming = "HATA"; // ilk hammingimizin binary sekilde yazmak icin degisken

        Label hataLabel = new Label();
        Label hataliDataLabel = new Label();
        Button hataButonu = new Button();
        TextBox hataYapilacakKonumBox = new TextBox();

        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) //kullanicidan alinacak 4, 8, 16 bitlik deger
        {
            a = this.textBox1.Text;
        }

        private void button1_Click(object sender, EventArgs e) //islemleri yapacak buton
        {
            /* check bitinin kac bit olacagina karar vermek icin kontroller */
            if (a == null)
            {
                MessageBox.Show("Lutfen 4, 8, 16 bitlik binary girdi girin.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                System.Windows.Forms.Application.Restart();
            }
                
            else if (a.Length == 4)
                ekleme = 3;
            else if (a.Length == 8)
                ekleme = 4;
            else if (a.Length == 16)
                ekleme = 5;


            char[] addedA = new char[a.Length + ekleme]; /* input olarak alinan deger icin hamming kod eklenebilecek kadar alana sahip olacak char dizisi */
            if (a.Length == 4 || a.Length == 8 || a.Length == 16)
            {
                int[] xorDizisi = new int[a.Length + ekleme]; /* degerleri xorlamak icin ara dizi */
                int j = 0;
                if (a.Length == 4) //sadece 4 haneliler icin
                {
                    for (int i = 0; i < a.Length + ekleme; i++)
                    {/* data bitlerinin yanlarina 0 olarak check bitleri ekledim ve addedA adli diziye atadim */
                        if (i == 3 || i == 5 || i == 6)
                            addedA[i] = '0';
                        else
                        {
                            addedA[i] = a[j];
                            j++;
                        }
                    }
                    Array.Reverse(addedA); /* kagida yazildigi gibi okumak istedigimden diziyi ters cevirdim */
                    for (int i = a.Length + ekleme - 1; 0 <= i; i--)
                    {
                        if (i == 3 || i == 5 || i == 6) /* data bitlerini xor dizisine aldim */
                            xorDizisi[i] = 0;
                        else if (addedA[i] == '1')
                            xorDizisi[i] = i + 1;
                        else
                            xorDizisi[i] = 0;
                    }
                    for (int i = a.Length + ekleme - 1; 0 <= i; i--) /* hamming hesaplama */
                    {
                        if (i == 0 || i == 1 || i == 3)
                        {;}
                        else if (addedA[i] == '1')
                            hamming = hamming ^ (i + 1);
                        else
                            hamming = hamming ^ 0;
                    }
                    binaryHamming = Convert.ToString(hamming, 2).PadLeft(ekleme, '0'); /* eksik bit durumu oldugunda ekleme degiskeni kadar binaryHamming degiskenini tamamlar */
                    for (int i = a.Length + ekleme - 1; 0 <= i; i--)
                    {
                        if ((i == 0 || i == 1 || i == 3) && (hammingYerlestirme <= ekleme - 1))
                        {
                            addedA[i] = binaryHamming[hammingYerlestirme]; /* addedA dizisinde check bitleri icin acilmis alanlari doldurdum */
                            hammingYerlestirme++;
                        }
                    }
                }
                else if (a.Length == 8)//sadece 8 haneliler icin
                {
                    for (int i = 0; i < a.Length + ekleme; i++)
                    { /* data bitlerinin yanlarina 0 olarak check bitleri ekledim ve addedA adli diziye atadim */
                        if (i == 4 || i == 8 || i == 10 || i == 11)
                            addedA[i] = '0';
                        else
                        {
                            addedA[i] = a[j];
                            j++;
                        }
                    }
                    Array.Reverse(addedA); /* kagida yazildigi gibi okumak istedigimden diziyi ters cevirdim */
                    for (int i = a.Length + ekleme - 1; 0 <= i; i--)
                    {
                        if (i == 4 || i == 8 || i == 10 || i == 11) /* data bitlerini xor dizisine aldim */
                            xorDizisi[i] = 0;
                        else if (addedA[i] == '1')
                            xorDizisi[i] = i + 1;
                        else
                            xorDizisi[i] = 0;
                    }
                    for (int i = a.Length + ekleme - 1; 0 <= i; i--) /* hamming hesaplama */
                    {
                        if (i == 0 || i == 1 || i == 3 || i == 7)
                        {;}
                        else if (addedA[i] == '1')
                            hamming = hamming ^ (i + 1);
                        else
                            hamming = hamming ^ 0;
                    }
                    binaryHamming = Convert.ToString(hamming, 2).PadLeft(ekleme, '0'); /* eksik bit durumu oldugunda ekleme degiskeni kadar binaryHamming degiskenini tamamlar */
                    for (int i = a.Length + ekleme - 1; 0 <= i; i--)
                    {
                        if ((i == 0 || i == 1 || i == 3 || i == 7) && (hammingYerlestirme <= ekleme - 1))
                        {
                            addedA[i] = binaryHamming[hammingYerlestirme]; /* addedA dizisinde check bitleri icin acilmis alanlari doldurdum */
                            hammingYerlestirme++;
                        }
                    }
                }
                else if (a.Length == 16) //sadece 16 haneliler icin
                {
                    for (int i = 0; i < a.Length + ekleme; i++)
                    { /* data bitlerinin yanlarina 0 olarak check bitleri ekledim ve addedA adli diziye atadim */
                        if (i == 5 || i == 13 || i == 17 || i == 19 || i == 20)
                            addedA[i] = '0';
                        else
                        {
                            addedA[i] = a[j];
                            j++;
                        }
                    }
                    Array.Reverse(addedA); /* kagida yazildigi gibi okumak istedigimden diziyi ters cevirdim */
                    for (int i = a.Length + ekleme - 1; 0 <= i; i--)
                    {
                        if (i == 5 || i == 13 || i == 17 || i == 19 || i == 20) /* data bitlerini xor dizisine aldim */
                            xorDizisi[i] = 0;
                        else if (addedA[i] == '1')
                            xorDizisi[i] = i + 1;
                        else
                            xorDizisi[i] = 0;
                    }
                    for (int i = a.Length + ekleme - 1; 0 <= i; i--) /* hamming hesaplama */
                    {
                        if (i == 0 || i == 1 || i == 3 || i == 7 || i == 15) 
                        {;}
                        else if (addedA[i] == '1')
                            hamming = hamming ^ (i + 1);
                        else
                            hamming = hamming ^ 0;
                    }
                    binaryHamming = Convert.ToString(hamming, 2).PadLeft(ekleme, '0'); /* eksik bit durumu oldugunda ekleme degiskeni kadar binaryHamming degiskenini tamamlar */
                    for (int i = a.Length + ekleme - 1; 0 <= i; i--)
                    {
                        if ((i == 0 || i == 1 || i == 3 || i == 7 || i == 15) && (hammingYerlestirme <= ekleme - 1))
                        {
                            addedA[i] = binaryHamming[hammingYerlestirme];
                            hammingYerlestirme++;
                        }
                    }
                }
            }

            else /* inputa 4, 8, 16 bitlik bir deger girilmediyse hata mesaji goster */
            {
                MessageBox.Show("Lutfen 4, 8, 16 bitlik binary girdi girin.", "Hata" , MessageBoxButtons.OK, MessageBoxIcon.Error);
                a = "";
                System.Windows.Forms.Application.Restart();
            }
            string konumYazaci = ""; /* labela bitlerin konumlarini yazdirmak icin */

            for (int i = addedA.Length - 1; i >= 0; i--)
                hammingliInput = hammingliInput + (addedA[i]);

            label2.Text = "Girilen Deger:" + a;
            label3.Text = "Hamming Code Eklenen Hali:" + hammingliInput;
            label4.Text = "Binary Hamming Code:" + binaryHamming;

            for(int i = 0; i < hammingliInput.Length; i++) /* bit konumlarini yazdirdim degistirilmek istenen bitin konumunun girilmesi kolay olmasi icin */
                konumYazaci = konumYazaci + "Konum " + (hammingliInput.Length - i) + "--> " + hammingliInput[i] + "\n";

            hataLabel.Text = "Degerini Degistirmek İstediginiz Bitin Konumunu Giriniz:\n" + konumYazaci;
            hataLabel.Width = 280;
            hataLabel.Height = this.Height - 20;
            hataLabel.Location = new Point(400,20);
            this.Controls.Add(hataLabel);

            hataButonu.Text = "Degistir";
            hataButonu.Width = 63;
            hataButonu.Height = 27;
            hataButonu.Location = new Point(this.Width - hataButonu.Width*2 ,52);
            hataButonu.Click += HataButton_Click;
            this.Controls.Add(hataButonu);

            hataYapilacakKonumBox.Width = 30;
            hataYapilacakKonumBox.Height = 30;
            hataYapilacakKonumBox.Location = new Point((this.Width - hataButonu.Width * 2) + 15, 18);
            hataYapilacakKonumBox.MaxLength = 2;
            hataYapilacakKonumBox.TextChanged += hataYapilacakKonumBox_TextChanged;
            this.Controls.Add(hataYapilacakKonumBox);
            button1.Enabled = false;

        }

        private void hataYapilacakKonumBox_TextChanged(object sender, EventArgs e) //kullanicidan alinacak hatanin yapilacagi konum
        {
            h = this.hataYapilacakKonumBox.Text;
        }

        private void label1_Click(object sender, EventArgs e) //inputun ustundeki label
        {

        }

        private void label4_Click(object sender, EventArgs e) //hamming codeun yazilacagi label
        {

        }

        private void label2_Click(object sender, EventArgs e) //inputtan alinan degerin yazilacagi label
        {

        }

        private void label3_Click(object sender, EventArgs e) //inputun hamming code eklenmis hali
        {

        }
        private void HataButton_Click(object sender, EventArgs e) //hata butonuna tiklandi
        {
            //Hatayi bulma kodunu buraya yaz.
            string hammingliHataliData = ""; // hata yapildiktan sonraki data

            try
            {
                parselayici = int.Parse(h);
            }
            catch
            {
                Console.WriteLine($"'{h}' bir sayi degil.");
            }

            hNumara = hammingliInput.Length - parselayici;
            char[] hataliData = new char[hammingliInput.Length];
            for (int i = 0; i < hammingliInput.Length; i++)
            {
                if (i == hNumara)
                {
                    if (hammingliInput[hNumara] == '0') // bu islemleri yapmiyor
                        hataliData[hNumara] = '1';
                    else if (hammingliInput[hNumara] == '1')
                        hataliData[hNumara] = '0';
                }
                else
                    hataliData[i] = hammingliInput[i];
            }
            for (int i = 0; i < hammingliInput.Length; i++) //hatali halini bir stringe atadim
                hammingliHataliData += hataliData[i];
            if (hammingliInput.Length == 7) // 4 bit girilmis
            {
                for (int i = 0; i < hammingliHataliData.Length; i++)
                {
                    if (i == 3 || i == 5 || i == 6)
                    {
                        if (hammingliHataliData[i] != hammingliInput[i])
                        {
                            char[] tmp = new char[3];
                            tmp2 += hammingliHataliData[3];
                            tmp2 += hammingliHataliData[5];
                            tmp2 += hammingliHataliData[6];
                            hataliHamming = Convert.ToInt32(tmp2, 2);
                            break;
                        }
                    }
                    else if (hataliData[i] == '1')
                        hataliHamming = hataliHamming ^ (hammingliHataliData.Length - i);
                    else
                        hataliHamming = hataliHamming ^ 0;
                }
            }
            else if (hammingliInput.Length == 12) // 8 bit girilmis
            {
                for (int i = 0; i < hammingliHataliData.Length; i++)
                {
                    if (i == 4 || i == 8 || i == 10 || i == 11)
                    {
                        if (hammingliHataliData[i] != hammingliInput[i])
                        {
                            char[] tmp = new char[4];
                            tmp2 += hammingliHataliData[4];
                            tmp2 += hammingliHataliData[8];
                            tmp2 += hammingliHataliData[10];
                            tmp2 += hammingliHataliData[11];
                            hataliHamming = Convert.ToInt32(tmp2, 2);
                            break;
                        }
                    }
                    else if (hataliData[i] == '1')
                        hataliHamming = hataliHamming ^ (hammingliHataliData.Length - i);
                    else
                        hataliHamming = hataliHamming ^ 0;
                }
            }
            else // 16 bit girilmis
            {
                for (int i = 0; i < hammingliHataliData.Length; i++)
                {
                    if (i == 5 || i == 13 || i == 17 || i == 19 || i == 20)
                    {
                        if (hammingliHataliData[i] != hammingliInput[i])
                        {
                            char[] tmp = new char[3];
                            tmp2 += hammingliHataliData[5];
                            tmp2 += hammingliHataliData[13];
                            tmp2 += hammingliHataliData[17];
                            tmp2 += hammingliHataliData[19];
                            tmp2 += hammingliHataliData[20];
                            hataliHamming = Convert.ToInt32(tmp2, 2);
                            break;
                        }
                    }
                    else if (hataliData[i] == '1')
                        hataliHamming = hataliHamming ^ (hammingliHataliData.Length - i);
                    else
                        hataliHamming = hataliHamming ^ 0;
                }
            }
            int hataliKonum = hataliHamming ^ hamming;

            hataliDataLabel.Text = "Hatali Data: " + hammingliHataliData + "\nBinary Sendrom Kelimesi: " + Convert.ToString(hataliKonum, 2).PadLeft(ekleme, '0') + "\nHatali Konum: " + hataliKonum;
            hataliDataLabel.Width = 180;
            hataliDataLabel.Height = 52;
            hataliDataLabel.Location = new Point(100, 300);
            this.Controls.Add(hataliDataLabel);

            hataButonu.Enabled = false; /* butona yalnizca bir kere basmayi saglamak icin */
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) /* yeniden deger girmek ve uygulamayi yeniden baslatmak icin buton */
        {
            System.Windows.Forms.Application.Restart();

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/erennkose/hamming-error-correcting-code-simulator");
        }
    }
}

