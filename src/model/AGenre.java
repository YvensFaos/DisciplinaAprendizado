package model;

import java.util.ArrayList;
import java.util.List;

public class AGenre {

	private static List<AGenre> genres;
	
	private String genreURL;
	private String genre;
	
	private AGenre(String genre, String genreURL) {
		super();
		this.genre = genre;
		this.genreURL = genreURL;
	}
	
	public String getGenreURL() {
		return genreURL;
	}

	public void setGenreURL(String genreURL) {
		this.genreURL = genreURL;
	}

	public String getGenre() {
		return genre;
	}

	public void setGenre(String genre) {
		this.genre = genre;
	}

	public static List<AGenre> getGenres()
	{
		if(genres == null)
		{
			genres = new ArrayList<AGenre>();
			
			genres.add(new AGenre("Parody", "<http://dbpedia.org/resource/Category:Parody_films>"));
			genres.add(new AGenre("Apocalyptic", "<http://dbpedia.org/resource/Category:Apocalyptic_films>"));
			genres.add(new AGenre("Mystery", "<http://dbpedia.org/resource/Category:Mystery_films>"));
			genres.add(new AGenre("Slasher", "<http://dbpedia.org/resource/Category:Slasher_films>"));
			genres.add(new AGenre("Dystopian", "<http://dbpedia.org/resource/Category:Dystopian_films>"));
			genres.add(new AGenre("Thriller", "<http://dbpedia.org/resource/Category:Thriller_films>"));
			genres.add(new AGenre("Romantic Drama", "<http://dbpedia.org/resource/Category:Romantic_drama_films>"));
			genres.add(new AGenre("Romance", "<http://dbpedia.org/resource/Category:Romance_films>"));
			genres.add(new AGenre("Comedy", "<http://dbpedia.org/resource/Category:British_comedy_films>"));
			genres.add(new AGenre("Science Fiction", "<http://dbpedia.org/resource/Category:Science_fiction_films_by_series>"));
			genres.add(new AGenre("Science Action", "<http://dbpedia.org/resource/Category:Science_fiction_action_films>"));
			genres.add(new AGenre("Fantasy", "<http://dbpedia.org/resource/Category:Fantasy_films>"));
		}
		
		return genres;
	}
	
	
}
