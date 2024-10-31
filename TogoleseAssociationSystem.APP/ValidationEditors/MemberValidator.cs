using FluentValidation;
using TogoleseAssociationSystem.Core.DTOs;

namespace TogoleseAssociationSystem.APP.ValidationEditors
{
    public class MemberValidator : AbstractValidator<MemberToAdd>
    {
        public MemberValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First Name is required");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last Name is required");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Email is not valid");

        }
    }
}
