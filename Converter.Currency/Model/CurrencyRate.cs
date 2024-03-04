namespace Converter.Currency.Model;
public class CurrencyRate
{
    public string FromCurrency { get; set; }
    public string ToCurrency { get; set; }

    public int Length { get; set; } = 1;
    public double Rate { get; set; } = 1.0;
    public double Calculate(double Amount)
    {
        return Rate * Amount;
    }

    public CurrencyRate Reverse()
    {
        return new CurrencyRate
        {
            Rate = 1 / Rate,
            FromCurrency = ToCurrency,
            ToCurrency = FromCurrency,
            Length = Length
        };
    }
}
