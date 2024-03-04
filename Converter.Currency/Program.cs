// See https://aka.ms/new-console-template for more information

using Converter.Currency;
using Converter.Currency.Services;
var converter = new ConvertService();

var lst = new List<Tuple<string, string, double>>();

lst.Add(Tuple.Create("A", "B", 0.5));
lst.Add(Tuple.Create("A", "C", 1.5));
lst.Add(Tuple.Create("C", "D", 2.5));
lst.Add(Tuple.Create("C", "E", 3.5));


converter.UpdateConfiguration(lst);

Console.WriteLine($"200 A >> {converter.Convert("A","E",200.0)}E");

converter.ClearConfiguration();

Console.WriteLine($"200 A >> {converter.Convert("A", "E", 200.0)} E");



