using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace brainwipe.hosts.tests
{
    [TestClass]
    public class HostMapTests
    {
        [ExpectedException(typeof(FormatException))]
        [TestMethod]
        public void GIVEN_NonsenseIP_WHEN_CreateHostMap_THEN_Exception()
        {
            // Act - Bang
            var map = new HostMap("BANANAS", "localhost");
        }

        [TestMethod]
        public void GIVEN_Map_WHEN_ToString_THEN_RepresentationCorrect()
        {
            // Arrange
            var map = new HostMap("127.0.0.1", "localhost");

            // Act
            var result = map.ToString();

            // Assert
            Assert.AreEqual("127.0.0.1\tlocalhost", result);
        }

        [ExpectedException(typeof(ArgumentNullException))]
        [TestMethod]
        public void GIVEN_NullHostName_WHEN_Create_THEN_Exception()
        {
            // Act - Bang!
            var map = new HostMap("127.0.0.1", null);
        }


        [TestMethod]
        public void GIVEN_IPv6_WHEN_ToString_THEN_RepresentationIsCorrect()
        {
            // Arrange
            var map = new HostMap("::1", "localhost");

            // Act
            var result = map.ToString();

            // Assert
            Assert.AreEqual("::1\tlocalhost", result);
        }

        [TestMethod]
        public void GIVEN_VarietyOfMaps_WHEN_IsMap_THEN_FoundCorrectly()
        {
            // Arrange
            var correctMaps = new[] {"127.0.0.1\tlocalhost", "127.0.0.1      localhost" };
            var incorrectMaps = new[] {"BANANAS\tlocalhost", "# Comment", "127.0.0.1\t  ", "\tlocalhost", "", " ", "              "};

            // Act
            var resultAreCorrect = correctMaps.Where(HostMap.IsMap).ToList();
            var resultIncorrect = incorrectMaps.Where(HostMap.IsMap).ToList();

            // Assert
            Assert.AreEqual(2, resultAreCorrect.Count);
            Assert.IsFalse(resultIncorrect.Any());
        }


        [TestMethod]
        public void GIVEN_EqualByValue_WHEN_Equals_THEN_True()
        {
            // Arrange
            var one = new HostMap("127.0.0.1", "localhost");
            var two = new HostMap("127.0.0.1", "localhost");

            // Act
            var result = one.Equals(two);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GIVEN_NotEqualByValue_WHEN_Equals_THEN_False()
        {
            // Arrange
            var one = new HostMap("127.0.0.1", "localhost");
            var two = new HostMap("192.168.0.1", "gateway");

            // Act
            var result = one.Equals(two);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void GIVEN_EqualByValue_WHEN_GetHashCode_THEN_True()
        {
            // Arrange
            var one = new HostMap("127.0.0.1", "localhost");
            var two = new HostMap("127.0.0.1", "localhost");

            // Act
            var oneHash = one.GetHashCode();
            var twoHash = two.GetHashCode();

            // Assert
            Assert.AreEqual(oneHash, twoHash);
        }

        [TestMethod]
        public void GIVEN_NotEqualByValue_WHEN_GetHashCode_THEN_False()
        {
            // Arrange
            var one = new HostMap("127.0.0.1", "localhost");
            var two = new HostMap("192.168.0.1", "gateway");

            // Act
            var oneHash = one.GetHashCode();
            var twoHash = two.GetHashCode();

            // Assert
            Assert.AreNotEqual(oneHash, twoHash);
        }
    }
}
