﻿// Ignore Spelling: Validator

using CheckYourEligibility.Admin.Boundary.Requests;
using CheckYourEligibility.Admin.Domain.Constants.ErrorMessages;
using CheckYourEligibility.API.Domain.Validation;
using FluentValidation;

namespace CheckYourEligibility.Admin.Domain.Validation;

public class CheckEligibilityRequestDataValidator_Fsm : AbstractValidator<CheckEligibilityRequestData_Fsm>
{
    public CheckEligibilityRequestDataValidator_Fsm()
    {
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage(ValidationMessages.LastName);

        RuleFor(x => x.DateOfBirth)
            .NotEmpty()
            .Must(DataValidation.BeAValidDate)
            .WithMessage(ValidationMessages.DOB);

        When(x => !string.IsNullOrEmpty(x.NationalInsuranceNumber), () =>
        {
            RuleFor(x => x.NationalAsylumSeekerServiceNumber)
                .Empty()
                .WithMessage(ValidationMessages.NI_and_NASS);
            RuleFor(x => x.NationalInsuranceNumber)
                .NotEmpty()
                .Must(DataValidation.BeAValidNi)
                .WithMessage(ValidationMessages.NI);
        }).Otherwise(() =>
        {
            RuleFor(x => x.NationalAsylumSeekerServiceNumber)
                .NotEmpty()
                .WithMessage(ValidationMessages.NI_or_NASS);
        });
    }
}