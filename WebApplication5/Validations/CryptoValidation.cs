using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;
using WebApplication5.Models;
using WebApplication5.Services;

namespace WebApplication5.Validations
{
    public class CryptoValidation : AbstractValidator<Crypto>
    {
        private readonly WebApplication5Context _context;

        public CryptoValidation(WebApplication5Context context)
        {
            _context = context;

            RuleFor(c => c.Cid)
                .NotEmpty().WithMessage("Id is required.")
                .Length(1, 50).WithMessage("Id must be between 1 and 50 characters.")
            .MustAsync(async (cid, cancellation) =>
            {
                var existingcrypto = await _context.Crypto.AnyAsync(a => a.Cid == cid, cancellation);
                return !existingcrypto;
            }).WithMessage("id must be unique.");

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Name is required.")
                .Length(1, 100).WithMessage("Name must be between 1 and 100 characters.");

            RuleFor(c => c.Reviewdate)
                .LessThan(DateTime.Now)
                .WithMessage("Review date must be in the past.")
                .GreaterThan(DateTime.Now.AddYears(-10))
                .WithMessage("Review date must be within the last 10 years.");
        }
    }
}
