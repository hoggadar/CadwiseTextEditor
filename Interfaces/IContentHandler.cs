namespace TextEditor.Ineterfases
{
    public interface IContentHandler
    {
        string DeleteWords(string str, int minLength);
        string DeletePunctuation(string str);
    }
}
