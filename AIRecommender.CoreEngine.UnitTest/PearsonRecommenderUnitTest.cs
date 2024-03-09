using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace AIRecommender.CoreEngine.UnitTest
{
    [TestClass]
    public class PearsonRecommenderUnitTest
    {
        PearsonRecommender pearsonRecommender;
        [TestInitialize]
        public void Init()
        {
            pearsonRecommender = new PearsonRecommender();
        }

        [TestMethod]
        public void GetCorrelation_EqualLength_PositiveCorrelation()
        {
            // Arrange
            int[] baseData = { 1, 2, 3 };
            int[] otherData = { 2, 4, 6 };
            double expectedCorrelation = 1.0; // Perfect positive correlation

            // Act
            double correlation = pearsonRecommender.GetCorrelation(baseData, otherData);

            // Assert
            Assert.AreEqual(expectedCorrelation, correlation, delta: 0.001);
        }

        [TestMethod]
        public void GetCorrelation_EqualLength_NegativeCorrelation()
        {
            // Arrange
            int[] baseData = { 1, 2, 3 };
            int[] otherData = { -2, -4, -6 };
            double expectedCorrelation = -1.0; // Perfect negative correlation

            // Act
            double correlation = pearsonRecommender.GetCorrelation(baseData, otherData);

            // Assert
            Assert.AreEqual(expectedCorrelation, correlation, delta: 0.001);
        }

        [TestMethod]
        public void GetCorrelation_UnequalLengthShorterOtherData_EqualizeLength()
        {
            // Arrange
            int[] baseData = { 1, 2, 0 };
            int[] otherData = { 2, 4 };
            double expectedCorrelation = 1.0; // Perfect positive correlation (padded with 1s)

            // Act
            double correlation = pearsonRecommender.GetCorrelation(baseData, otherData);

            // Assert
            Assert.AreEqual(expectedCorrelation, correlation, delta: 0.001);
        }

        [TestMethod]
        public void GetCorrelation_UnequalLengthShorterBaseData_EqualizeLength()
        {
            // Arrange
            int[] baseData = { 1, 2 };
            int[] otherData = { 2, 4, 6 };
            double expectedCorrelation = 1.0; // Perfect positive correlation (truncated)

            // Act
            double correlation = pearsonRecommender.GetCorrelation(baseData, otherData);

            // Assert
            Assert.AreEqual(expectedCorrelation, correlation, delta: 0.001);
        }

        [TestMethod]
        public void GetCorrelation_ZeroHandlingNegative_IncrementValues()
        {
            // Arrange
            int[] baseData = { 0, 0, 1 };
            int[] otherData = { 0, 1, 0 };
            double expectedCorrelation = -0.5; // Non-zero correlation after handling zeros

            // Act
            double correlation = pearsonRecommender.GetCorrelation(baseData, otherData);

            // Assert
            Assert.AreEqual(expectedCorrelation, correlation, delta: 0.01); // Adjust delta for zero handling
        }
        [TestMethod]
        public void GetCorrelation_ZeroHandlingPositive_IncrementValues()
        {
            // Arrange
            int[] baseData = { 0, 1, 0 };
            int[] otherData = { 0, 0, 1 };
            double expectedCorrelation = -0.5; // Non-zero correlation after handling zeros

            // Act
            double correlation = pearsonRecommender.GetCorrelation(baseData, otherData);

            // Assert
            Assert.AreEqual(expectedCorrelation, correlation, delta: 0.01); // Adjust delta for zero handling
        }
    }
}
