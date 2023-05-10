namespace Carsharing_Lombardi_Saturnio.Models
{
    public class Message
    {
        private string content;
        private DateTime date;

        public string Content { get => content; set => content = value; }
        public DateTime Date { get => date; set => date = value; }

        public void GetMessages() { }
        public void CreateMessage() { }
    }
}
