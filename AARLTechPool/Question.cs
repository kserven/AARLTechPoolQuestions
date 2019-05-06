namespace AARLTechPool
{
    internal class Question
    {
        public string Id { get; set; }
        public string Answer { get; set; }
        public string Meta { get; set; }
        public string QuestionText { get; set; }
        public string A { get; set; }
        public string B { get; set; }
        public string C { get; set; }
        public string D { get; set; }


        public Question() { }
        public Question(string id, string answer, string meta, string question, string a, string b, string c, string d)
        {
            Id = id;
            Answer = answer;
            Meta = meta;
            QuestionText = question;
            A = a;
            B = b;
            C = c;
            D = d;
        }

    }
}
