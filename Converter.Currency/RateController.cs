using Converter.Currency.Model;

namespace Converter.Currency;

public class RateController
{
    public List<CurrencyRate> _Rates;

    public RateController(List<CurrencyRate> Rates)
    {
        _Rates = Rates;
    }

    /// <summary>
    /// Update rate if shorter path is found
    /// </summary>
    /// <param name="currencyRate">New Rate</param>
    public void UpdateRate(CurrencyRate currencyRate)
    {
        var rate = _Rates.
            FirstOrDefault(c => c.FromCurrency == currencyRate.FromCurrency &&
                                 c.ToCurrency == currencyRate.ToCurrency);
        if (rate == null)
            return;

        if(rate.Length > currencyRate.Length)
        {
            rate.Length = currencyRate.Length;
            rate.Rate = currencyRate.Rate;
        }
    }

    /// <summary>
    /// Add New rate 
    /// </summary>
    /// <param name="cRate">New Rate</param>
    public void AddRate(CurrencyRate cRate)
    {
        _Rates.Add(cRate);
        _Rates.Add(cRate.Reverse());
    }


    /// <summary>
    /// Check if a rate is exist update it else add it
    /// </summary>
    /// <param name="cRate"></param>
    /// <exception cref="ArgumentException"></exception>
    public void CheckRate(CurrencyRate cRate)
    {
        if (_Rates.Exists(c=>c.FromCurrency == cRate.FromCurrency && 
                                 c.ToCurrency == cRate.ToCurrency))
        {
            UpdateRate(cRate);
        }
        else
        {
            AddRate(cRate);
        }

    }

    /// <summary>
    /// New paths are found and check if it can be used
    /// </summary>
    public void ArrangeRates()
    {
        foreach (CurrencyRate rate in _Rates.ToList())
        {
            foreach (CurrencyRate cRate in _Rates.ToList())
            {
                if(rate.ToCurrency == cRate.FromCurrency && rate.FromCurrency != cRate.ToCurrency)
                    CheckRate(new CurrencyRate
                    {
                        FromCurrency = rate.FromCurrency,
                        ToCurrency = cRate.ToCurrency,
                        Length = rate.Length + cRate.Length,
                        Rate = rate.Rate * cRate.Rate
                    });
            }
        }
    }

}
