namespace WebApplication5.Services
{
    public interface ICryptoService
    {
        public Task<bool> IsCidEqualAsync(string cid);
    }
}
