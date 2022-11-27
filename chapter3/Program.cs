namespace starbuzz
{
    /**** STEP 1 ****/
    public abstract class Beverage
    {
        /* An abstract class with cost implemented in
        subclasses */
        string Description { get; } = "Unknown Beverage";

        public abstract double Cost();
    }

    public abstract class CondimentDecorator : Beverage
    {
        Beverage beverage;

        // Ensure that decorators implement GetDescription
        public abstract string GetDescription();
    }

    /**** STEP 2 ****/
    // TO DO
}