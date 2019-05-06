using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace AARLTechPool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow
    {
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
            Id.Text = $"{_questionNumber + 1} - {_questions[_questionNumber].Id}";

            Question.Text = _questions[_questionNumber].QuestionText;

            AButton.Content = _questions[_questionNumber].A;
            BButton.Content = _questions[_questionNumber].B;
            CButton.Content = _questions[_questionNumber].C;
            DButton.Content = _questions[_questionNumber].D;

            CorrectAnswer.Text = _questions[_questionNumber].Answer;
            Info.Text = _questions[_questionNumber].Meta;
        }

        private void NextButton_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
                _questionNumber++;
                if (_questionNumber >= _questions.Count) return;

                DisplayInformation();

        }
    }
}
