using Converter.Currency.Model;

namespace Converter.Currency;

public sealed class ConverterAssistant
{
    private IEnumerable<Tuple<string,string, double>> rates { get; set; }
    public List<CurrencyRate> RateList { get; set; }
    private RateController RateController;

    /// <summary>
    /// Delete All Rates
    /// </summary>
    public void ClearConfiguration()
    {
        RateList.Clear();
        rates = null;
        instance = null;
    }

    /// <summary>
    /// Validate Currency Rates
    /// </summary>
    /// <param name="_rates">Collection of rates</param>
    /// <exception cref="ArgumentNullException">rates is null</exception>
    /// <exception cref="ArgumentException">Rate is ziro or origin and destination are the same</exception>
    private void CheckRatesValidation(IEnumerable<Tuple<string, string, double>> _rates)
    {
        if (_rates == null) throw new ArgumentNullException(nameof(_rates));
        if (_rates.Any(c => c.Item3 == 0)) throw new ArgumentException(nameof(_rates));
        if (_rates.Any(c => c.Item1 == c.Item2)) throw new ArgumentException(nameof(_rates));
    }
    private ConverterAssistant(IEnumerable<Tuple<string, string, double>> _rates) 
    {
        /// Check Input Data Validation
        CheckRatesValidation(_rates);

        /// Add Reverse Rates
        rates = _rates.Concat(_rates.
            Select(c => Tuple.Create(c.Item2, c.Item1, 1.0 / c.Item3)));

        RateList = rates.Select(c => new CurrencyRate
        {
            FromCurrency = c.Item1,
            ToCurrency = c.Item2,
            Rate = c.Item3,
            Length = 1
        }).ToList();

        /// Instantiation of rate controller in order to make it 
        /// single point of management rates
        RateController = new RateController(this.RateList);
        
        /// Make unknown rates with shortest path
        RateController.ArrangeRates();
    }
    private static readonly object lockObj = new object ();
    private static ConverterAssistant instance = null;

    /// <summary>
    /// Implement Singleton using parameters
    /// </summary>
    /// <param name="_rates">Raw Input rates</param>
    /// <returns></returns>
    public static ConverterAssistant GetInstance(IEnumerable<Tuple<string, string, double>> _rates)
    {
        
            if (instance == null)
            {
                lock (lockObj)
                    {
                        if (instance == null)
                        {
                            instance = new ConverterAssistant(_rates);
                        }
                    }
            }
            return instance;
    }
}
