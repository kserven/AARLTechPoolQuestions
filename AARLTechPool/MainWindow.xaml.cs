using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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

            DisplayInformation();
        }

        private void ReadQuestionsFromFile()
        {
            var processModule = System.Diagnostics.Process.GetCurrentProcess().MainModule;
            if (processModule == null)
            {
                throw new InvalidOperationException();
            }

            string appPath = Path.GetDirectoryName(processModule.FileName);
            string filePath = Path.Combine(appPath ?? throw new InvalidOperationException(), "Resources");
            string fullFilename = Path.Combine(filePath, "TechPool.txt");

            using (var file = new StreamReader(fullFilename))
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
                _questions.RemoveAt(_questionNumber);

                _questionNumber = new Random().Next(0,_questions.Count+1);

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
