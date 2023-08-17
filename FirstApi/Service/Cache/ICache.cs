namespace FirstApi.Service.Cache
{
    public interface ICache
    {
        void Add(string key, object value);
        void Remove(string key);
        object Get(string key);

        void Clear();
            

    }
}
