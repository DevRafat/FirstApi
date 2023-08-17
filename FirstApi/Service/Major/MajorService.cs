using FirstApi.Service.Cache;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace FirstApi.Service.Major
{
    public class MajorService:IMajorService
    {
        private readonly AplicationDbContext _context;
        private readonly ICache _cache;
        public MajorService(AplicationDbContext context, ICache cache)
        {
            _context = context;
            _cache = cache;
        }

        public async Task<List<Tables.Major>> GetAll()
        {

         
            var data = _cache.Get("MajorList");
            if(data==null)
                _cache.Add("MajorList", _context.Majors.ToList());
            else
            {
                return (List<Tables.Major>)data;
            }

            return await _context.Majors.ToListAsync();

        }

        public async Task<Tables.Major> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void SendMessage(string email)
        {
            Console.WriteLine("the message send to email "+email +" at "+DateTime.Now);
        }
    }
}
