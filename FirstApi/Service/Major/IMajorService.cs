namespace FirstApi.Service.Major
{
    public interface IMajorService
    {
        Task<List<Tables.Major>> GetAll();
        Task<Tables.Major> GetById(int id);

        void SendMessage(string email);

    }
}
