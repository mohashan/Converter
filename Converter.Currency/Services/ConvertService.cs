using Converter.Currency.Contracts;

namespace Converter.Currency.Services
{
    /// <summary>
    /// Implementation of IConverter
    /// </summary>
    public class ConvertService : IConverter
    {
        private ConverterAssistant converterAssistant;
        public void ClearConfiguration()
        {
            converterAssistant?.ClearConfiguration();
        }

        public double Convert(string fromCurrency, string toCurrency, double amount)
        {
            var rate = converterAssistant?.RateList.FirstOrDefault(c => c.FromCurrency == fromCurrency &&
                                               c.ToCurrency == toCurrency);

            if (rate == null)
            {
                throw new Exception($"Rate not exist : {fromCurrency} : {toCurrency}");
            }

            return rate.Calculate(amount);
        }

        public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates)
        {
            converterAssistant = ConverterAssistant.GetInstance(conversionRates);
        }
    }
}
