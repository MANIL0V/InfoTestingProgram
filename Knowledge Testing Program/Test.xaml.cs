using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Runtime.InteropServices;
using System.IO;
using System.Collections;
using MessageBox = System.Windows.Forms.MessageBox;
using RadioButton = System.Windows.Controls.RadioButton;

namespace Knowledge_Testing_Program
{
    /// <summary>
    /// Логика взаимодействия для Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        public class Questions
        {
            public int right { get; set; }
            public string[] answ { get; set; }
            public string que { get; set; }
            public int num { get; set; }

            public Questions(int n, int r, string[] a, string q)
            {
                num = n;
                right = r;
                answ = a;
                que = q;
            }


        }

        public ArrayList questions = new ArrayList();
        public ArrayList randomQuestions = new ArrayList();

        string fpath;
        string fname; 

        System.Xml.XmlReader xmlReader;

        string qw;
        string[] answ = new string[4];
        int questionsCount = 0;

        int count = 0;
        int right; 
        int otv;   
        int n;     
        int nv;    
        int mode;         

        public Test()
        {
            InitializeComponent();

            radioButton1.Visibility = System.Windows.Visibility.Hidden;
            radioButton2.Visibility = System.Windows.Visibility.Hidden;
            radioButton3.Visibility = System.Windows.Visibility.Hidden;
            radioButton4.Visibility = System.Windows.Visibility.Hidden;

            fpath = System.Windows.Forms.Application.StartupPath;
            fname = "\\KTP.xml";

            try
            {
                xmlReader = new System.Xml.XmlTextReader(fpath + fname);
                xmlReader.Read();

                mode = 0;
                n = 0;

                this.showHead();
                this.showDescription();
                this.getQw();
            }

            catch
            {
                label1.Text = "Ошибка доступа к файлу  " +
                    fpath + fname;

                System.Windows.Forms.MessageBox.Show("Ошибка доступа к файлу.\n" +
                    fpath + fname + "\n",
                    "Экзаменатор",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Error);

                mode = 2;
            }
        }

        private void showHead()
        {
            do xmlReader.Read();
            while (xmlReader.Name != "head");

            xmlReader.Read();

            this.Title = xmlReader.Value;

            xmlReader.Read();
        }

        private void showDescription()
        {
            do
                xmlReader.Read();
            while (xmlReader.Name != "description");

            xmlReader.Read();

            label1.Text = xmlReader.Value;

            xmlReader.Read();

            do
                xmlReader.Read();
            while (xmlReader.Name != "qw");

            xmlReader.Read();
        }

        private void getQw()
        {

            bool ex = true;

            while (ex)
            {
                xmlReader.Read();

                if (xmlReader.Name == "q")
                {
                    qw = xmlReader.GetAttribute("text");
                    
                    xmlReader.Read();
                    int i = 0;
                    count++;
                    while (xmlReader.Name != "q")
                    {
                        xmlReader.Read();

                        
                        if (xmlReader.Name == "a")
                        {
                            
                            if (xmlReader.GetAttribute("right") == "yes")
                                right = i;

                            
                            xmlReader.Read();
                            if (i < 4)
                                answ[i] = xmlReader.Value;

                            
                            xmlReader.Read();

                            i++;
                        }
                    }

                    Questions question = new Questions(count, right, new string[] { answ[0], answ[1], answ[2], answ[3] }, qw);
                    questions.Add(question);
                    xmlReader.Read();

                    ex = true;
                }
                else
                    ex = false;
            }

            randomQuestions = ShuffleList(questions);

        }

        private void showQw(Questions question)
        {
            label1.Text = question.que;

            radioButton1.Content = question.answ[0];
            radioButton2.Content = question.answ[1];
            radioButton3.Content = question.answ[2];
            radioButton4.Content = question.answ[3];

            radioButton5.IsChecked = true;
            button1.IsEnabled = false;
        }

        private void radioButton1_Click(object sender, RoutedEventArgs e)
        {
            if ((RadioButton)sender == radioButton1) otv = 0;
            if ((RadioButton)sender == radioButton2) otv = 1;
            if ((RadioButton)sender == radioButton3) otv = 2;
            if ((RadioButton)sender == radioButton4) otv = 3;

            button1.IsEnabled = true;
        }

        private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int[] rows = { 0, 1, 2, 3 };
            Random rand = new Random();
            int rnd1, rnd2, temp;

            switch (mode)
            {
                case 0:    
                    radioButton1.Visibility = System.Windows.Visibility.Visible;
                    radioButton2.Visibility = System.Windows.Visibility.Visible;
                    radioButton3.Visibility = System.Windows.Visibility.Visible;
                    radioButton4.Visibility = System.Windows.Visibility.Visible;
                    this.showQw((Questions)randomQuestions[questionsCount]);

                    mode = 1;

                    button1.Content = "Далее";
                    button1.IsEnabled = false;

                    radioButton5.IsChecked = true;

                    for (int i = 0; i < 8; i++)
                    {
                        rnd1 = rand.Next(0, 4);
                        rnd2 = rand.Next(0, 4);
                        temp = rows[rnd1];
                        rows[rnd1] = rows[rnd2];
                        rows[rnd2] = temp;
                    }
                    Grid.SetRow(radioButton1, rows[0]);
                    Grid.SetRow(radioButton2, rows[1]);
                    Grid.SetRow(radioButton3, rows[2]);
                    Grid.SetRow(radioButton4, rows[3]);

                    break;

                case 1:
                    nv++;

                    for (int i = 0; i < 8; i++)
                    {
                        rnd1 = rand.Next(0, 4);
                        rnd2 = rand.Next(0, 4);
                        temp = rows[rnd1];
                        rows[rnd1] = rows[rnd2];
                        rows[rnd2] = temp;
                    }
                    Grid.SetRow(radioButton1, rows[0]);
                    Grid.SetRow(radioButton2, rows[1]);
                    Grid.SetRow(radioButton3, rows[2]);
                    Grid.SetRow(radioButton4, rows[3]);

                    radioButton1.IsChecked = false;
                    radioButton2.IsChecked = false;
                    radioButton3.IsChecked = false;
                    radioButton4.IsChecked = false;

                    if (otv == right) n++;

                    if (randomQuestions.Count != questionsCount)
                    {
                        this.showQw((Questions)randomQuestions[questionsCount]);

                    }
                    else
                    {
                        radioButton1.Visibility = System.Windows.Visibility.Hidden;
                        radioButton2.Visibility = System.Windows.Visibility.Hidden;
                        radioButton3.Visibility = System.Windows.Visibility.Hidden;
                        radioButton4.Visibility = System.Windows.Visibility.Hidden;
                        this.showLevel();

                        button1.Content = "Выход";
                        mode = 2;
                    }
                    break;
                case 2:   
                    this.Close(); 
                    break;
            }
            questionsCount++;
        }

        private void showLevel()
        {
            do
                xmlReader.Read();
            while (xmlReader.Name != "levels");
            xmlReader.Read();

            while (xmlReader.Name != "levels")
            {
                xmlReader.Read();

                if (xmlReader.Name == "level")
                    if (n >= System.Convert.ToInt32(
                        xmlReader.GetAttribute("score")))
                        break;
            }

            label1.Text =
                "Тестирование завершено.\n" +
                "Всего вопросов: " + nv.ToString() + ". " +
                "Правильных ответов: " + n.ToString() + ".\n" +
                xmlReader.GetAttribute("text");
        }

        #region GlassWindow
        [StructLayout(LayoutKind.Sequential)]
        private struct MARGINS
        {
            public int cxLeftWidth;      
            public int cxRightWidth;     
            public int cyTopHeight;      
            public int cyBottomHeight;   
        };

        [DllImport("DwmApi.dll")]
        private static extern int DwmExtendFrameIntoClientArea(IntPtr hwnd, ref MARGINS pMarInset);

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                WindowInteropHelper windowInteropHelper = new WindowInteropHelper(this);
                IntPtr myHwnd = windowInteropHelper.Handle;
                HwndSource mainWindowSrc = System.Windows.Interop.HwndSource.FromHwnd(myHwnd);

                mainWindowSrc.CompositionTarget.BackgroundColor = Color.FromArgb(0, 0, 0, 0);

                MARGINS margins = new MARGINS()
                {
                    cxLeftWidth = -1,
                    cxRightWidth = -1,
                    cyBottomHeight = -1,
                    cyTopHeight = -1
                };

                DwmExtendFrameIntoClientArea(myHwnd, ref margins);
            }
        }
        #endregion

        private void Grid_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (mode == 1)
            {
                if (System.Windows.Forms.MessageBox.Show("Вы действительно хотите выйти из программы теста?", "Подтверждение выхода", System.Windows.Forms.MessageBoxButtons.YesNo, System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                    e.Cancel = true;
                else
                    MessageBox.Show("Тест считается проваленным. Полученая оценка - НЕУДОВЛЕТВАРИТЕЛЬНО!");

            }
        }

        private ArrayList ShuffleList(ArrayList inputList)
        {
            ArrayList randomList = new ArrayList();
            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count);
                randomList.Add(inputList[randomIndex]);
                inputList.RemoveAt(randomIndex);
            }
            return randomList;
        }

        private void button2(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
