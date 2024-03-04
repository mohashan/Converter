
using Converter.Currency.Services;

namespace Converter.Currency.Test
{
    public class NonParallelCollectionDefinitionClass
    {
    }

    /// <summary>
    /// All good unit tests should be 100% isolated. Using shared state (e.g. depending on a static property that is modified by each test) is regarded as bad practice.
    /// </summary>

    public class RateControllerTest
    {
        private readonly ConvertService converter = new ConvertService();

        [Fact]
        public void Check_Reverse_Rate()
        {
            /// Arrange
            converter.ClearConfiguration();
            var lst = new List<Tuple<string, string, double>>();
            lst.Add(Tuple.Create("A", "B", 0.5));

            //Act
            converter.UpdateConfiguration(lst);
            var BA = converter.Convert("B", "A", 1.0);

            // Assert
            Assert.Equal(2.0,BA,2);
        }

        [Fact]
        public void Throw_Error_If_Rate_Not_Exist()
        {
            /// Arrange
            converter.ClearConfiguration();
            var lst = new List<Tuple<string, string, double>>();
            lst.Add(Tuple.Create("A", "B", 2.0));
            lst.Add(Tuple.Create("C", "D", 8.0));

            //Act
            converter.UpdateConfiguration(lst);
            Action Act = ()=> converter.Convert("A", "D", 1.0);

            // Assert
            Assert.Throws<Exception>(Act);
        }

        [Fact]
        public void Check_Rate_Is_Exist()
        {
            /// Arrange
            converter.ClearConfiguration();
            var lst = new List<Tuple<string, string, double>>();
            lst.Add(Tuple.Create("A", "B", 2.0));
            lst.Add(Tuple.Create("C", "D", 8.0));

            //Act
            Action act = () => converter.Convert("A", "D", 1.0);

            // Assert
            Assert.Throws<Exception>(act);
        }

        [Fact]
        public void Check_Made_Rate()
        {
            /// Arrange
            converter.ClearConfiguration();
            var lst = new List<Tuple<string, string, double>>();
            lst.Add(Tuple.Create("A", "B", 2.0));
            lst.Add(Tuple.Create("B", "C", 4.0));
            lst.Add(Tuple.Create("C", "D", 8.0));

            //Act
            converter.UpdateConfiguration(lst);
            var AD = converter.Convert("A", "D", 1.0);

            // Assert
            Assert.Equal(64.0, AD, 2);
        }
    }
}