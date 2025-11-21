using CodingTest.Entities;
using FluentValidation;

namespace CodingTest.Validators
{
    public class ContactValidator : AbstractValidator<Contact>
    {
        public ContactValidator()
        {
            RuleFor(c => c.Account).NotNull();
            RuleFor(c => c.FirstName).MaximumLength(200).NotEmpty();
            RuleFor(c => c.LastName).MaximumLength(200).NotEmpty();
            RuleFor(c => c.PhoneNumber).MaximumLength(15);
            RuleFor(c => c.MobileNumber).MaximumLength(15);
            RuleFor(c => c.EmailAddress).MaximumLength(250).EmailAddress();
            RuleFor(c => c.Role).NotNull();
        }
    }
}
