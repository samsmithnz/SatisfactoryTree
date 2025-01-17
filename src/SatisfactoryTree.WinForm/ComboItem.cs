namespace SatisfactoryTree.WinForm
{
    public class ComboItem
    {
        public string Id { get; set; }
        public string Text { get; set; }

        public ComboItem(string id, string text)
        {
            Id = id;
            Text = text;
        }
    }
}
