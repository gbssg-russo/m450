# Arrange, Act, Assert (AAA)

Arrange, Act, Assert—usually shortened to **AAA**—is a simple pattern for giving every unit test a clear structure. It separates preparing a scenario, executing the behaviour under test, and checking the result.

## The three phases

### 1. Arrange

Prepare everything the test needs:

- create the system under test;
- choose the input values;
- define the expected result;
- configure dependencies or test data when necessary.

For this application, the system under test is normally a `PriceCalculator`. The Arrange phase should describe the scenario without performing the calculation that the test is meant to verify.

### 2. Act

Perform the one operation whose behaviour you want to test. A focused unit test normally has one clear Act statement, such as calling `CalculateTotal`.

If the test expects an exception, the action may be placed inside a delegate or lambda passed to the testing framework's exception assertion.

### 3. Assert

Compare what happened with what should have happened. Depending on the scenario, this may mean checking:

- a returned value;
- an exception type;
- a change in state;
- a small set of closely related results.

An assertion should express the expected public behaviour. Avoid reproducing the production calculation inside the test because the same mistake could then exist in both places.

## Test skeleton

Use the following outline as a guide. Replace the placeholders yourself; it is intentionally not a finished test.

```csharp
// Add [Fact] for xUnit or [TestMethod] for MSTest.
public void CalculateTotal_Scenario_ExpectedBehaviour()
{
    // Arrange
    var calculator = new PriceCalculator();
    var unitPrice = /* choose a value */;
    var quantity = /* choose a value */;
    var discountPercentage = /* choose a value */;
    var expectedTotal = /* determine the expected result */;

    // Act
    var actualTotal = calculator.CalculateTotal(
        unitPrice,
        quantity,
        discountPercentage);

    // Assert
    // Use Assert.Equal with xUnit or Assert.AreEqual with MSTest.
}
```

The comments are useful while learning. Once the structure is obvious and blank lines clearly separate the phases, a team may decide whether to keep them.

## Example thought process

For the behaviour "a 100% discount produces a total of zero":

1. **Arrange:** create the calculator and select a positive price, a positive quantity, a 100% discount, and an expected result of zero.
2. **Act:** call `CalculateTotal` once with those inputs.
3. **Assert:** compare the returned total with zero using the assertion syntax for your selected framework.

Apply the same reasoning to valid calculations, boundary values, and invalid inputs.

## Common mistakes

- Mixing setup and assertions so the scenario is hard to recognize.
- Calling several unrelated methods in the Act phase.
- Calculating the expected value with the same formula as the application.
- Testing several unrelated behaviours in one method.
- Hiding important test inputs in shared setup code.
- Catching an exception manually without asserting its exact expected type.
- Depending on another test to run first.

## Checklist for each test

Before considering a test complete, check that:

- its name describes the method, scenario, and expected behaviour;
- the Arrange phase contains only the data and setup needed for this scenario;
- the Act phase performs one focused operation;
- the Assert phase checks a specific observable result;
- the test can run independently and repeatedly;
- a failing assertion would clearly indicate which behaviour is broken.

## Your task

Use AAA for every `PriceCalculator` test listed in the main README. After writing the tests, compare them with a classmate's tests and identify the Arrange, Act, and Assert phases in each one.

For a more detailed discussion of the pattern, read [The Arrange, Act, and Assert (AAA) Pattern in Unit Test Automation](https://semaphore.io/blog/aaa-pattern-test-automation) by Semaphore.
