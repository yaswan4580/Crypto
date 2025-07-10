using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;
using WebApplication5.Validations;

namespace WebApplication5.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoesController : ControllerBase
    {
        private readonly WebApplication5Context _context;
        private readonly CryptoValidation _validator;
        public CryptoesController(WebApplication5Context context,CryptoValidation validator)
        {
            _context = context;
            _validator = validator;
        }

        [HttpGet("sorted-cryptos")]
        public async Task<ActionResult<IEnumerable<Crypto>>> GetSortedCryptos()
        {
            var cryptoes = await _context.Crypto
                                         .OrderBy(c => c.Name) 
                                         .ToListAsync();
            return Ok(cryptoes);
        }
        // GET: api/Cryptoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Crypto>>> GetCrypto()
        {
            return await _context.Crypto.ToListAsync();
        }

        // GET: api/Cryptoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Crypto>> GetCrypto(string id)
        {
            var crypto = await _context.Crypto.FindAsync(id);

            if (crypto == null)
            {
                return NotFound();
            }

            return crypto;
        }

        // PUT: api/Cryptoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCrypto(string id, Crypto crypto)
        {

            if (id != crypto.Cid)
            {
                return BadRequest();
            }

            _context.Entry(crypto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CryptoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cryptoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Crypto>> PostCrypto(Crypto crypto)
        {
            //var data =await _validator.ValidateAsync(crypto);
            //if(data.Errors.Count > 0){
            //    return BadRequest(new Error
            //    {
            //        message = [
            //            "Validation failed",
            //            string.Join(", ", data.Errors.Select(e => e.ErrorMessage))
            //        ]
            //    }

            //        );
            //}
            _context.Crypto.Add(crypto);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (CryptoExists(crypto.Cid))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetCrypto", new { id = crypto.Cid }, crypto);
        }

        // DELETE: api/Cryptoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCrypto(string id)
        {
            var crypto = await _context.Crypto.FindAsync(id);
            if (crypto == null)
            {
                return NotFound();
            }

            _context.Crypto.Remove(crypto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CryptoExists(string id)
        {
            return _context.Crypto.Any(e => e.Cid == id);
        }
    }
}
