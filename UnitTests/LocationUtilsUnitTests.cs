using System;
using Xunit;
using FluentAssertions;
using Services;
using BusinessModel;
using Moq;
using Utils;

namespace UnitTests
{
    public class LocationUtilsUnitTests
    {
        private GeoCode adbLocation = new ()
        {
            Latitude = 27.147594,
            Longitude = 38.294403
        };
        
        
        GeoCode bcnLocation = new ()
        {
            Latitude = 2.07593,
            Longitude = 41.303027
        };
        GeoCode madLocation = new ()
        {
            Latitude = -3.546667,
            Longitude = 40.455556
        };
        GeoCode amsLocation = new ()
        {
            Latitude = 4.763385,
            Longitude = 52.309069
        };

        [Fact]
        public void CalculateDistanceBetweenTwoLocationsInMiles_Between_ADB_AND_BCN_ReturnsDistanceBetweenThen()
        {

            // Act
            double distance = LocationUtils.CalculateDistanceBetweenTwoLocationsInMiles(adbLocation, bcnLocation);

            // Assert
            distance.Should().BeApproximately(1341, 1, "That's the aerial distance between the two locations");
        }

        [Fact]
        public void CalculateDistanceBetweenTwoLocationsInMiles_Between_ADB_AND_AMS_ReturnsDistanceBetweenThen()
        {

            // Act
            double distance = LocationUtils.CalculateDistanceBetweenTwoLocationsInMiles(adbLocation, amsLocation);

            // Assert
            distance.Should().BeApproximately(1444, 1, "That's the aerial distance between the two locations");
        }

        [Fact]
        public void CalculateDistanceBetweenTwoLocationsInMiles_Between_BCN_AND_MAD_ReturnsDistanceBetweenThen()
        {

            // Act
            double distance = LocationUtils.CalculateDistanceBetweenTwoLocationsInMiles(bcnLocation, madLocation);

            // Assert
            distance.Should().BeApproximately(299, 1, "That's the aerial distance between the two locations");
        }

        [Fact]
        public void CalculateDistanceBetweenTwoLocationsInMiles_WhenGivenTwoLocations_ReturnsZero_IfLocationsAreSame()
        {

            // Act
            double distance = LocationUtils.CalculateDistanceBetweenTwoLocationsInMiles(bcnLocation, bcnLocation);

            // Assert
            distance.Should().Be(0);
        }

        [Fact]
        public void CalculateDistanceBetweenTwoLocationsInKm_Between_ADB_AND_BCN_ReturnsDistanceBetweenThen()
        {
            // Arrange


            // Act
            double distance = LocationUtils.CalculateDistanceBetweenTwoLocationsInKm(adbLocation, bcnLocation);

            // Assert
            distance.Should().BeApproximately(2160, 1, "That's the aerial distance between the two locations");
        }

        [Fact]
        public void CalculateDistanceBetweenTwoLocationsInKm_WhenGivenTwoLocations_ReturnsZero_IfLocationsAreSame()
        {
            // Arrange

            // Act
            double distance = LocationUtils.CalculateDistanceBetweenTwoLocationsInKm(adbLocation, adbLocation);

            // Assert
            distance.Should().Be(0);
        }
    }
}
