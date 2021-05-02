using System;
using System.Windows.Forms;
using Tesseract;

namespace EasyCharacterRecognition
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //言語（日本語なら"jpn"）今回は英語のみ
            string lang = "eng";

            //言語ファイルの格納先
            string dataDirPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"tessdata\");

            //画像ファイル
            string imgPath = System.IO.Path.Combine(System.IO.Directory.GetCurrentDirectory(), @"sample4.bmp");


            int retval = Test(imgPath, dataDirPath, lang);

            System.Environment.ExitCode = retval;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="imgPath"></param>
        /// <param name="dataDirPath"></param>
        /// <param name="lang"></param>
        /// <returns></returns>
        private static int Test(string imgPath, string dataDirPath, string lang)
        {

            if (!System.IO.File.Exists(imgPath))
            {
                MessageBox.Show("画像が見つかりませんでした");

                return 1;
            }

            //各々の言語の学習データの読み込み
            string traindedDataPath = System.IO.Path.Combine(dataDirPath, lang + ".traineddata");
            if (!System.IO.File.Exists(traindedDataPath))
            {
                MessageBox.Show(lang + ".traineddataがみつかりませんでした");
                return 2;
            }
            var tesseract = new Tesseract.TesseractEngine(dataDirPath, lang);

            // 画像ファイルの読み込み
            var img = new System.Drawing.Bitmap(imgPath);
           
            // OCRの実行と表示  
            var page = tesseract.Process(img);

            string getText = page.GetText();

            MessageBox.Show(page.GetText());

            return 0;
        }
    }
}
