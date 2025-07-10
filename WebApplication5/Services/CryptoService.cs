using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;

namespace WebApplication5.Services
{
    public class CryptoService : ICryptoService
    {
        private readonly WebApplication5Context _context;
        public CryptoService(WebApplication5Context context)
        {
            _context = context;
        }
        public async Task<bool> IsCidEqualAsync(string cid)
        {
            return !await _context.Crypto.AnyAsync(c => c.Cid == cid);
        }
    }
    
}
