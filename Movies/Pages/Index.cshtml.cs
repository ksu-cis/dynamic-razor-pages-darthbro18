using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Movies.Pages
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// The movies to display on the index page
        /// </summary>
        public IEnumerable<Movie> Movies { get; protected set; }
        
        /// <summary>
        /// The current search terms
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string SearchTerms { get; set; }

        /// <summary>
        /// The filtered MPAA ratings
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// The filtered genre
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public string[] MajorGenre { get; set; }

        /// <summary>
        /// The minimum IMDB rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? IMDBMin { get; set; }

        /// <summary>
        /// The maximum IMDB rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? IMDBMax { get; set; }

        /// <summary>
        /// The minimum Rotten Tomatoes rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? RTMin { get; set; }

        /// <summary>
        /// The maximum Rotten Tomatoes rating
        /// </summary>
        [BindProperty(SupportsGet = true)]
        public double? RTMax { get; set; }
        
        /// <summary>
        /// Gets the search results for display on the page
        /// </summary>
        public void OnGet()
        {
            /*
            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenre(Movies, MajorGenre);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByRottenTomatoesRating(Movies, RTMin, RTMax);*/

            Movies = MovieDatabase.All;
            //Search movie titles for SearchTerms
            if (SearchTerms != null)
            {
                //Movies = Movies.Where(movie => movie.Title != null && movie.Title.Contains(SearchTerms, StringComparison.InvariantCultureIgnoreCase));
                Movies = from movie in Movies
                         where movie.Title != null && movie.Title.Contains(SearchTerms, StringComparison.InvariantCultureIgnoreCase)
                         select movie;
            }
            //Filter by MPAA Rating
            if (MPAARatings != null && MPAARatings.Length != 0)
            {
                Movies = from movie in Movies
                         where movie.MPAARating != null && MPAARatings.Contains(movie.MPAARating)
                         select movie;
            }
            //Filter by Genre
            if (MajorGenre != null && MajorGenre.Count() != 0)
            {
                Movies = from movie in Movies
                         where movie.MajorGenre != null && MajorGenre.Contains(movie.MajorGenre)
                         select movie;
            }
            //Filter by IMDB Rating
            if (IMDBMin == null && IMDBMax != null)
            {
                Movies = from movie in Movies
                         where movie.IMDBRating <= IMDBMax
                         select movie;
            }
            if (IMDBMin != null && IMDBMax == null)
            {
                Movies = from movie in Movies
                         where movie.IMDBRating >= IMDBMin
                         select movie;
            }
            if (IMDBMin != null && IMDBMax != null)
            {
                Movies = from movie in Movies
                         where movie.IMDBRating >= IMDBMin && movie.IMDBRating <= IMDBMax
                         select movie;
            }
            //Filter by Rotten Tomatoes Rating
            if (RTMin == null && RTMax != null)
            {
                Movies = from movie in Movies
                         where movie.RottenTomatoesRating <= RTMax
                         select movie;
            }
            if (RTMin != null && RTMax == null)
            {
                Movies = from movie in Movies
                         where movie.RottenTomatoesRating >= RTMin
                         select movie;
            }           
            if (RTMin != null && RTMax != null)
            {
                Movies = from movie in Movies
                         where movie.RottenTomatoesRating >= RTMin && movie.RottenTomatoesRating <= RTMax
                         select movie;
            }
        }
    }
}
