namespace FAQManager
{
    public interface IFaqBackend
    {
        void Set(string key, string value);

        string Get(string key);
    }
}