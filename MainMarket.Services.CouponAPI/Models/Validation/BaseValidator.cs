﻿using FluentValidation;
using MainMarket.Services.CouponAPI.Exceptions;

namespace MainMarket.Services.CouponAPI.Models.Validation;

public static class BaseValidator<T> where T : class
{
    public static void Validate(IValidator<T> validator, T model)
    {
        var validationResult = validator.Validate(model);
        if (!validationResult.IsValid)
        {
            var failures = validationResult.Errors.Where(error => error != null).AsEnumerable();
            throw new CustomValidationException(failures);
        }

    }
}
