using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace AARLTechPool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow
    {
        private int _questionNumber = 420;

        private List<Question> _questions = new List<Question>();
        public MainWindow()
        {
            ResizeMode = ResizeMode.CanMinimize;

            InitializeComponent();

            ReadQuestionsFromFile();
        }

        private void ReadQuestionsFromFile()
        {
            using (var file = new StreamReader(@"TechPool.txt"))
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

                    file.ReadLine();
                    file.ReadLine();

                    _questions.Add(question);
                }
            }

            Id.Text = $"{_questions.Count} - {_questionNumber + 1} - {_questions[_questionNumber].Id}";

            Question.Text = _questions[_questionNumber].QuestionText;

            AButton.Content = _questions[_questionNumber].A;
            BButton.Content = _questions[_questionNumber].B;
            CButton.Content = _questions[_questionNumber].C;
            DButton.Content = _questions[_questionNumber].D;
        }

        private void NextButton_OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
                _questionNumber++;
                if (_questionNumber >= _questions.Count - 1) return;

                Id.Text = $"{_questionNumber + 1} - {_questions[_questionNumber].Id}";

                Question.Text = _questions[_questionNumber].QuestionText;

                AButton.Content = _questions[_questionNumber].A;
                BButton.Content = _questions[_questionNumber].B;
                CButton.Content = _questions[_questionNumber].C;
                DButton.Content = _questions[_questionNumber].D;

        }
    }
}
