namespace AARLTechPool
{
    internal class Question
    {
        public string Id { get; set; }
        public string Answer { get; set; }
        public string Meta { get; set; }
        public string QuestionText { get; set; }
        public string[] Choices { get; set; }

        public Question() { }
        public Question(string id, string answer, string meta, string question, string[] choices)
        {
            Id = id;
            Answer = answer;
            Meta = meta;
            QuestionText = question;
            Choices = choices;
        }

    }
}
