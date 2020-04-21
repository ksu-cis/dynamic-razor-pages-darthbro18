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
        [BindProperty]
        public string SearchTerms { get; set; }

        /// <summary>
        /// The filtered MPAA ratings
        /// </summary>
        [BindProperty]
        public string[] MPAARatings { get; set; }

        /// <summary>
        /// The filtered genre
        /// </summary>
        [BindProperty]
        public string[] MajorGenre { get; set; }

        /// <summary>
        /// The minimum IMDB rating
        /// </summary>
        [BindProperty]
        public double? IMDBMin { get; set; }

        /// <summary>
        /// The maximum IMDB rating
        /// </summary>
        [BindProperty]
        public double? IMDBMax { get; set; }

        /// <summary>
        /// The minimum Rotten Tomatoes rating
        /// </summary>
        [BindProperty]
        public double? RTMin { get; set; }

        /// <summary>
        /// The maximum Rotten Tomatoes rating
        /// </summary>
        [BindProperty]
        public double? RTMax { get; set; }
        
        /// <summary>
        /// Gets the search results for display on the page
        /// </summary>
        public void OnGet(double? IMDBMin, double? IMDBMax, string SearchTerms, string[] MPAARatings, string[] MajorGenre, double? RTMin, double? RTMax)
        {
            this.IMDBMin = IMDBMin;
            this.IMDBMax = IMDBMax;
            this.SearchTerms = SearchTerms;
            this.MPAARatings = MPAARatings;
            this.MajorGenre = MajorGenre;
            this.RTMin = RTMin;
            this.RTMax = RTMax;

            Movies = MovieDatabase.Search(SearchTerms);
            Movies = MovieDatabase.FilterByMPAARating(Movies, MPAARatings);
            Movies = MovieDatabase.FilterByGenre(Movies, MajorGenre);
            Movies = MovieDatabase.FilterByIMDBRating(Movies, IMDBMin, IMDBMax);
            Movies = MovieDatabase.FilterByRottenTomatoesRating(Movies, RTMin, RTMax);
        }
    }
}
