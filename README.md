# M450: Repeat unit testing in C#

This repository contains a small console application designed for practising the complete unit-testing workflow. The application calculates the price of an order from a unit price, a quantity, and an optional discount.

The repository deliberately contains **no test project and no unit tests**. Your task is to create them yourself, decide which cases matter, and run the tests. This lets you repeat the setup instead of only editing prepared tests.

## Requirements

- [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- An editor or IDE such as Visual Studio, Visual Studio Code, or JetBrains Rider

Check your installation:

```bash
dotnet --version
```

## Explore and run the application

Restore and build the solution:

```bash
dotnet restore
dotnet build
```

Run the application with a unit price, quantity, and optional discount percentage:

```bash
dotnet run --project src/M450.UnitTesting.App -- 25.50 3 10
```

The result should be `68.85` because three items cost `76.50` before a 10% discount.

The calculation is in `PriceCalculator.cs`. It is separate from console input and output so that you can call it directly from unit tests.

## Exercise: create the test project

You can complete the exercise with either **xUnit** or **MSTest**. Choose one framework and run its command from the repository root. Do not run both commands because they use the same project name and directory.

### Option A: xUnit

```bash
dotnet new xunit \
  --name M450.UnitTesting.App.Tests \
  --output tests/M450.UnitTesting.App.Tests \
  --framework net10.0
```

The `xunit` template creates the project and adds the packages required to discover and run xUnit tests.

### Option B: MSTest

```bash
dotnet new mstest \
  --name M450.UnitTesting.App.Tests \
  --output tests/M450.UnitTesting.App.Tests \
  --framework net10.0
```

The `mstest` template creates the project and adds the packages required to discover and run MSTest tests.

### Finish the setup

After creating either project:

1. Add the test project to the solution:

   ```bash
   dotnet sln M450.UnitTesting.sln add tests/M450.UnitTesting.App.Tests
   ```

2. Add a reference from the test project to the application project:

   ```bash
   dotnet add tests/M450.UnitTesting.App.Tests/M450.UnitTesting.App.Tests.csproj reference src/M450.UnitTesting.App/M450.UnitTesting.App.csproj
   ```

3. Delete the generated `UnitTest1.cs` file and create `PriceCalculatorTests.cs` in the test project.

At this point the solution should have this structure:

```text
M450.UnitTesting.sln
src/
  M450.UnitTesting.App/
    M450.UnitTesting.App.csproj
    PriceCalculator.cs
    Program.cs
tests/
  M450.UnitTesting.App.Tests/
    M450.UnitTesting.App.Tests.csproj
    PriceCalculatorTests.cs
```

## Exercise: write the tests

Write the tests yourself. Use the **Arrange, Act, Assert** structure and descriptive names such as `CalculateTotal_WithValidValues_ReturnsExpectedTotal`.

Cover at least these behaviours:

- a price and quantity without a discount;
- a price and quantity with a discount;
- a quantity of zero;
- a discount of 100%;
- a negative unit price;
- a negative quantity;
- a discount below 0% or above 100%.

Use the attributes and assertions belonging to the framework you selected:

| Purpose | xUnit | MSTest |
| --- | --- | --- |
| Mark a test class | No attribute required | `[TestClass]` |
| One test case | `[Fact]` | `[TestMethod]` |
| Several inline cases | `[Theory]` with `[InlineData(...)]` | `[TestMethod]` with `[DataRow(...)]` |
| Compare expected and actual values | `Assert.Equal(expected, actual)` | `Assert.AreEqual(expected, actual)` |
| Verify an exception | `Assert.Throws<T>(...)` | `Assert.ThrowsExactly<T>(...)` |

For invalid inputs, verify both that an exception is thrown and that it is the expected exception type.

### If you chose xUnit

- Put `[Fact]` above a method that checks one example.
- Put `[Theory]` above a method and add one or more `[InlineData(...)]` attributes when the same behaviour should be checked with different inputs.
- A test class does not need a special class attribute.
- Use `Assert.Equal` for results and `Assert.Throws<T>` for expected exceptions.

See [Getting Started with xUnit](https://xunit.net/docs/getting-started/v3/getting-started) for more information.

### If you chose MSTest

- Put `[TestClass]` above the test class.
- Put `[TestMethod]` above each test method.
- Add one or more `[DataRow(...)]` attributes to a test method when the same behaviour should be checked with different inputs.
- Use `Assert.AreEqual` for results and `Assert.ThrowsExactly<T>` for expected exceptions.

See [Get started with C# and MSTest](https://learn.microsoft.com/en-us/dotnet/core/testing/unit-testing-csharp-with-mstest) for more information.

Do not test the implementation line by line. Test the public behaviour of `PriceCalculator` so that the implementation can later change without requiring unrelated tests to change.

## Run the tests

From the repository root, run:

```bash
dotnet test
```

Also try these useful commands:

```bash
dotnet test --list-tests
dotnet test --filter "FullyQualifiedName~PriceCalculatorTests"
dotnet test --verbosity normal
```

For the final check, run a clean build and all tests:

```bash
dotnet clean
dotnet test
```

## Repeat the workflow

To practise again, remove only the `tests` directory and its entry from the solution, then repeat the setup steps. Do not remove the application project.

Before finishing, be able to explain:

- why the test project needs a project reference;
- what Arrange, Act, and Assert mean;
- how xUnit facts and theories compare with MSTest test methods and data rows;
- why you selected xUnit or MSTest for your test project;
- why each test should be independent;
- what makes a useful test name.
