using BusinessModel;

namespace Utils
{
    public class LocationUtils
    {

        public static double CalculateDistanceBetweenTwoLocationsInMiles(GeoCode location1, GeoCode location2)
        {

            if(location1 == location2)
            {
                return 0;
            }

            // The math module contains
            // a function named toRadians
            // which converts from degrees
            // to radians.
            var lon1 = location1.Longitude;
            var lat1 = location1.Latitude;
            var lon2 = location2.Longitude;
            var lat2 = location2.Latitude;

            lon1 = ToRadians(lon1);
            lon2 = ToRadians(lon2);
            lat1 = ToRadians(lat1);
            lat2 = ToRadians(lat2);

            // Haversine formula
            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;
            double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Pow(Math.Sin(dlon / 2), 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));

            // Radius of earth in
            // miles.
            double r = 3956;

            // calculate the result
            return (c * r);
        }

        public static double CalculateDistanceBetweenTwoLocationsInKm(GeoCode location1, GeoCode location2)
        {
            if (location1 == location2)
            {
                return 0;
            }

            // The math module contains
            // a function named toRadians
            // which converts from degrees
            // to radians.
            var lon1 = location1.Longitude;
            var lat1 = location1.Latitude;
            var lon2 = location2.Longitude;
            var lat2 = location2.Latitude;

            lon1 = ToRadians(lon1);
            lon2 = ToRadians(lon2);
            lat1 = ToRadians(lat1);
            lat2 = ToRadians(lat2);

            // Haversine formula
            double dlon = lon2 - lon1;
            double dlat = lat2 - lat1;
            double a = Math.Pow(Math.Sin(dlat / 2), 2) +
                       Math.Cos(lat1) * Math.Cos(lat2) *
                       Math.Pow(Math.Sin(dlon / 2), 2);

            double c = 2 * Math.Asin(Math.Sqrt(a));

            // Radius of earth in
            // kilometers. Use 3956
            // for miles
            double r = 6371;

            // calculate the result
            return (c * r);
        }

        public static double ToRadians(
           double angleIn10thofaDegree)
        {
            return (angleIn10thofaDegree *
                           Math.PI) / 180;
        }




    }
}