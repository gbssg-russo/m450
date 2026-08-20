namespace M450.UnitTesting.App;

/// <summary>
/// Calculates the total price of an order after an optional percentage discount.
/// </summary>
public sealed class PriceCalculator
{
    public decimal CalculateTotal(
        decimal unitPrice,
        int quantity,
        decimal discountPercentage = 0m)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(unitPrice);
        ArgumentOutOfRangeException.ThrowIfNegative(quantity);

        if (discountPercentage is < 0m or > 100m)
        {
            throw new ArgumentOutOfRangeException(
                nameof(discountPercentage),
                "The discount percentage must be between 0 and 100.");
        }

        var subtotal = unitPrice * quantity;
        var discount = subtotal * discountPercentage / 100m;

        return subtotal - discount;
    }
}
