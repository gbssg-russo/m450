using System.Globalization;
using M450.UnitTesting.App;

if (args.Length is < 2 or > 3
    || !decimal.TryParse(args[0], CultureInfo.InvariantCulture, out var unitPrice)
    || !int.TryParse(args[1], CultureInfo.InvariantCulture, out var quantity)
    || (args.Length == 3
        && !decimal.TryParse(args[2], CultureInfo.InvariantCulture, out _)))
{
    ShowUsage();
    return 1;
}

var discountPercentage = args.Length == 3
    ? decimal.Parse(args[2], CultureInfo.InvariantCulture)
    : 0m;

try
{
    var calculator = new PriceCalculator();
    var total = calculator.CalculateTotal(unitPrice, quantity, discountPercentage);

    Console.WriteLine($"Total: {total.ToString("F2", CultureInfo.InvariantCulture)}");
    return 0;
}
catch (ArgumentOutOfRangeException exception)
{
    Console.Error.WriteLine(exception.Message);
    return 2;
}

static void ShowUsage()
{
    Console.WriteLine("Usage: dotnet run --project src/M450.UnitTesting.App -- <unit-price> <quantity> [discount-percentage]");
    Console.WriteLine("Example: dotnet run --project src/M450.UnitTesting.App -- 25.50 3 10");
}
