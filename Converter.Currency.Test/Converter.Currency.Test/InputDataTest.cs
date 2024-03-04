using Converter.Currency.Services;

namespace Converter.Currency.Test
{
    public class InputDataTest
    {
        private readonly ConvertService converter = new ConvertService();

        
        [Fact]
        public void Check_If_Rate_Is_Zero()
        {
            /// Arrange
            converter.ClearConfiguration();
            var lst = new List<Tuple<string, string, double>>();
            lst.Add(Tuple.Create("A", "B", 0.0));

            //Act
            Action act = () => converter.UpdateConfiguration(lst);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void Check_If_From_And_To_Currencies_Are_Different()
        {
            /// Arrange
            converter.ClearConfiguration();
            var lst = new List<Tuple<string, string, double>>();
            lst.Add(Tuple.Create("A", "A", 1.0));

            //Act
            Action act = () => converter.UpdateConfiguration(lst);

            // Assert
            Assert.Throws<ArgumentException>(act);
        }

        [Fact]
        public void Check_If_Input_Is_Not_Null()
        {
            /// Arrange
            converter.ClearConfiguration();
            List<Tuple<string, string, double>> lst = null;

            //Act
            Action act = () => converter.UpdateConfiguration(lst);

            // Assert
            Assert.Throws<ArgumentNullException>(act);
        }
    }
}