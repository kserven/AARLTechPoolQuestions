using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AARLTechPool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow
    {
        private int _numberCorrect;
        private int _questionNumber;

        private readonly List<Question> _questions = new List<Question>();
        public MainWindow()
        {
            ResizeMode = ResizeMode.CanMinimize;

            InitializeComponent();

            ReadQuestionsFromFile();

            _questionNumber = new Random().Next(0, _questions.Count);

            DisplayInformation();
        }

        private void ReadQuestionsFromFile()
        {
            var processModule = System.Diagnostics.Process.GetCurrentProcess().MainModule;
            if (processModule == null)
            {
                throw new InvalidOperationException();
            }

            var stream = new MemoryStream(Encoding.ASCII.GetBytes(Properties.Resources.TechPool));

            using (var file = new StreamReader(stream))
            {

                while(!file.EndOfStream)
                {
                    var question = new Question
                    {
                        Id = file.ReadLine() ?? throw new InvalidOperationException(),
                        QuestionText = file.ReadLine() ?? throw new InvalidOperationException(),
                        A = file.ReadLine(),
                        B = file.ReadLine(),
                        C = file.ReadLine(),
                        D = file.ReadLine()
                    };

                    question.Answer = question.Id[7].ToString();

                    var meta = new StringBuilder();

                    for (var i = 10; i < question.Id.Length; i++)
                    {
                        meta.Append(question.Id[i].ToString());                        
                    }

                    question.Meta = meta.ToString();

                    question.Id = question.Id.Substring(0, 5);

                    file.ReadLine();
                    file.ReadLine();

                    _questions.Add(question);
                }
            }
        }

        private void DisplayInformation()
        {
            Id.Text = _questions[_questionNumber].Id;

            Question.Text = _questions[_questionNumber].QuestionText;

            AButton.Background = Brushes.LightGray;
            BButton.Background = Brushes.LightGray;
            CButton.Background = Brushes.LightGray;
            DButton.Background = Brushes.LightGray;
            

            AButton.Content = _questions[_questionNumber].A;
            BButton.Content = _questions[_questionNumber].B;
            CButton.Content = _questions[_questionNumber].C;
            DButton.Content = _questions[_questionNumber].D;

            CorrectAnswer.Text = "";
            Info.Text = _questions[_questionNumber].Meta;

            var t1 = new[] { "T6C02", "T6C03", "T6C04", "T6C05" };
            var t2 = new[] { "T6C06", "T6C07", "T6C08", "T6C09" };
            var t3 = new[] { "T6C10", "T6C11" };

            var path = @"C:\Users\kserv\source\repos\AARLTechPool\AARLTechPool\Resources\";

            if (Array.IndexOf( t1 , _questions[_questionNumber].Id) > -1)
            {
                SupplementalImage.Source = new BitmapImage(new Uri(@path + "T1.jpg", UriKind.Absolute));
            }

            else if (Array.IndexOf(t2, _questions[_questionNumber].Id) > -1)
            {
                SupplementalImage.Source = new BitmapImage(new Uri(@path + "T2.jpg", UriKind.Absolute));
            }

            else if (Array.IndexOf(t3, _questions[_questionNumber].Id) > -1)
            {
                SupplementalImage.Source = new BitmapImage(new Uri(@path + "T3.jpg", UriKind.Absolute));
            }
            else
            {
                SupplementalImage.Source = null;
            }

        }

        private void CheckAnswer(string answer)
        {
            if (answer.Equals(_questions[_questionNumber].Answer))
            {
                CorrectAnswer.Text = answer + " is correct!";
                _numberCorrect++;
                Info.Text = _numberCorrect.ToString() + " correct so far out of " + _questions.Count.ToString();

                switch (answer)
                {
                    case "A":
                        AButton.Background = Brushes.LawnGreen;
                        break;
                    case "B":
                        BButton.Background = Brushes.LawnGreen;
                        break;
                    case "C":
                        CButton.Background = Brushes.LawnGreen;
                        break;
                    case "D":
                        DButton.Background = Brushes.LawnGreen;
                        break;
                }
            }
            else
            {
                CorrectAnswer.Text = answer + " is not correct. Sorry!";

                switch (answer)
                {
                    case "A":
                        AButton.Background = Brushes.LightCoral;
                        break;
                    case "B":
                        BButton.Background = Brushes.LightCoral;
                        break;
                    case "C":
                        CButton.Background = Brushes.LightCoral;
                        break;
                    case "D":
                        DButton.Background = Brushes.LightCoral;
                        break;
                }
            }
        }

        private void NextButton_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (_questions.Count == 0)
            {
                MessageBox.Show("You have reached the end of the questions");
                return;
            }

            _questions.RemoveAt(_questionNumber);

            _questionNumber = new Random().Next(0, _questions.Count);

            if (_questionNumber >= _questions.Count) return;

            DisplayInformation();
        }

        private void AButton_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheckAnswer("A");
        }

        private void BButton_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheckAnswer("B");
        }

        private void CButton_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheckAnswer("C");
        }

        private void DButton_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CheckAnswer("D");
        }
    }
}
