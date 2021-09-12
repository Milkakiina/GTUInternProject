using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieDL
{
    public class MovieData
    {
        public static String[] titleList = { "Matrix", "Bladerunner", "Inception","Godfather","Star Wars"};

        public static String[] directorList = { "Wachowskis", "Dennis", "Nolan", "director1", "Lucas" };

        public static String[] genreList = { "Cyberpunk", "Cyberpunk", "sci-fi", "drama", "sci-fi" };

        public static DateTime[] releaseList = { new DateTime(1999, 3, 1), new DateTime(2000, 2, 1),
                                                new DateTime(2009, 1, 1), new DateTime(1980, 12, 3), new DateTime(1979, 5, 5) };

        

    }
}
