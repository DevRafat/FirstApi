using Microsoft.EntityFrameworkCore;

namespace FirstApi.Service.Major
{
    public class MajorService:IMajorService
    {
        private readonly AplicationDbContext _context;

        public MajorService(AplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Tables.Major>> GetAll()
        {
            return await _context.Majors.ToListAsync();

        }

        public async Task<Tables.Major> GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
