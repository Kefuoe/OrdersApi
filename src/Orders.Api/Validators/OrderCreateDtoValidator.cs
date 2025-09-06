using FluentValidation;
using Orders.Api.Dtos;

namespace Orders.Api.Validators;

public class OrderCreateDtoValidator : AbstractValidator<OrderCreatedDto>
{
    public OrderCreateDtoValidator()
    {
        RuleFor(x => x.UserId).GreaterThan(0);
        RuleFor(x => x.Items).NotNull().Must(x => x.Count > 0).WithMessage("An order must atleast have one item.");
        RuleForEach(x => x.Items).ChildRules(item => {
            item.RuleFor(i => i.Quantity).GreaterThan(0);
            item.RuleFor(i => i.UnitPrice).GreaterThanOrEqualTo(0);
        });
    }
}