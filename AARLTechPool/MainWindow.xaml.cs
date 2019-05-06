using System;
using System.Collections.Generic;

namespace AARLTechPool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    internal partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var questions = new List<Question>();

            var question = new Question
            {
                Id = "T1A01",
                Answer = "C",
                Meta = "97.1",
                QuestionText =
                    "Which of the following is a purpose of the Amateur Radio Service as stated in the FCC rules and regulations?",
                A = "A. Providing personal radio communications for as many citizens as possible",
                B = "B. Providing communications for international non-profit organizations",
                C = "C. Advancing skills in the technical and communication phases of the radio art",
                D = "D. All of these choices are correct"
            };


            questions.Add(question);
            questions.Add(question);

            Console.WriteLine(questions[0].Id);
        }
    }
}
