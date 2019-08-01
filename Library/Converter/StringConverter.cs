namespace Library.Converter
{
    public class StringConverter
    {
        public static string EmptyStringIfNull(string text)
        {
            try
            {
                if (text != null)
                {
                    return text;
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
    }
}
