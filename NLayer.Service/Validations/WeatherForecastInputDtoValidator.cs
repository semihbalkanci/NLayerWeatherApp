using FluentValidation;
using NLayer.Core.DTOs;

namespace NLayer.Service.Validations
{
    public class WeatherForecastInputDtoValidator : AbstractValidator<WeatherForecastInputDto>
    {
        public WeatherForecastInputDtoValidator()
        {
            //RuleFor(x => x.Unit).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Unit).IsEnumName(typeof(UnitType)).WithMessage("{PropertyName} should be celcius/fahrenheit");
            //RuleFor(x => x.Temperature).NotNull().WithMessage("{PropertyName} is required");
            RuleFor(x => x.Temperature).InclusiveBetween(int.MinValue, int.MaxValue).WithMessage("{PropertyName} must be between int min and max values");
            //RuleFor(x => x.Locations).NotNull().WithMessage("{PropertyName} is required").NotEmpty().WithMessage("{PropertyName} is required");
        }
    }
    public enum UnitType
    {
        celcius,
        fahrenheit
    }
}

