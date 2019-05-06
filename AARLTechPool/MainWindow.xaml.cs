using System.Collections.Generic;
using System.Windows;

namespace AARLTechPool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
                Choices =
                {
                    [0] = "A. Providing personal radio communications for as many citizens as possible",
                    [1] = "B. Providing communications for international non-profit organizations",
                    [2] = "C. Advancing skills in the technical and communication phases of the radio art",
                    [3] = "D. All of these choices are correct"
                }
            };

            questions.Add(question);
            questions.Add(question);

        }
    }
}
