package dao;

import java.sql.SQLException;
import java.util.ArrayList;
import java.util.List;

import model.AActor;
import model.ADirector;
import model.AGenre;
import model.AMovie;
import ontology.manager.LodManager;
import ontology.util.Util;

import com.hp.hpl.jena.query.QuerySolution;
import com.hp.hpl.jena.query.ResultSet;
import com.hp.hpl.jena.rdf.model.RDFNode;

public class AMovieDAO {
	private LodManager lodManager;
	private List<AGenre> genres;
	private int quantity;
	
	public AMovieDAO() {
		super();
		lodManager = new LodManager("http://dbpedia.org/sparql");
		genres = AGenre.getGenres();
		quantity = 5;
	}
	
	public List<AMovie> getMovies()
	{
		List<AMovie> movies = new ArrayList<AMovie>();
		
		for(AGenre genre : genres)
		{
			System.out.println("\nGênero: "+genre.getGenre() + ": " + genre.getGenreURL());
			String sparql = "SELECT distinct ?movie ?name WHERE { ?movie a <http://dbpedia.org/ontology/Film> ; <http://dbpedia.org/property/name> ?name ;  <http://purl.org/dc/terms/subject> "+genre.getGenreURL()+" . } LIMIT "+quantity+" OFFSET 0";
			String sparql2, sparql3;
			ResultSet result = lodManager.query(Util.concatSPARQLPrefix(sparql));
			ResultSet results2, results3;
			while(result.hasNext()){
				AMovie amovie = new AMovie();
				QuerySolution queryMovie = result.nextSolution();
				sparql2 = "SELECT * WHERE { ?movie a <http://dbpedia.org/ontology/Film> ; <http://dbpedia.org/property/name> \"&\"@en ; <http://dbpedia.org/property/director> ?director ; <http://dbpedia.org/property/country> ?country ; <http://dbpedia.org/property/language> ?language . OPTIONAL { ?movie <http://dbpedia.org/property/runtime> ?runtime . } OPTIONAL { ?movie <http://dbpedia.org/ontology/releaseDate> ?release_date . }  } LIMIT 10 OFFSET 0";
				
				RDFNode movie = queryMovie.get("name");
				String movieLiteral = movie.toString();
				
				if(movieLiteral.contains("@en"))
				{
					movieLiteral = movieLiteral.substring(0, movieLiteral.indexOf('@'));
				}
				
				if(movieLiteral.contains("^^"))
				{
					movieLiteral = movieLiteral.substring(0, movieLiteral.indexOf('^'));
				}
				
				amovie.setName(movieLiteral);
				
				sparql2 = sparql2.replaceAll("&", movieLiteral);
				results2 = lodManager.query(Util.concatSPARQLPrefix(sparql2));
				
				if(results2.hasNext())
				{
					QuerySolution queryMovieDetails = results2.nextSolution();
					
					String directorName = "";
					RDFNode director = queryMovieDetails.get("director");
					if(director != null)
					{
						sparql3 = "SELECT ?director_name WHERE { ?movie a <http://dbpedia.org/ontology/Film> ;  <http://dbpedia.org/property/name> \""+movieLiteral+"\"@en ; <http://dbpedia.org/property/director> ?director . ?director <http://www.w3.org/2000/01/rdf-schema#label> ?director_name . } LIMIT 1 OFFSET 0";
						results3 = lodManager.query(Util.concatSPARQLPrefix(sparql3));
						
						if(results3 != null && results3.hasNext())
						{
							QuerySolution queryDirectorDetails = results3.nextSolution();
							director = queryDirectorDetails.get("director_name");
							directorName = director.toString();
						}
					}
					amovie.setDirector(new ADirector(directorName));

					String countryString = "";
					RDFNode country = queryMovieDetails.get("country");
					if(country != null)
					{
						countryString = country.toString();
					}
					amovie.setCountry(countryString);
					
					String languageString = "";
					RDFNode language = queryMovieDetails.get("language");
					if(language != null)
					{
						languageString = country.toString();
					}
					amovie.setLanguage(languageString);
					
					int releaseDateString = -1;
					RDFNode releaseDate = queryMovieDetails.get("release_date");
					if(releaseDate != null)
					{
						releaseDateString = Integer.parseInt(releaseDate.toString().substring(0, 4));
					}
					amovie.setYear(releaseDateString);
					
					int durationString = -1;
					RDFNode runtime = queryMovieDetails.get("runtime");
					if(runtime != null)
					{
						if(runtime.toString().contains("^"))
						{
							durationString = (int)Float.parseFloat(runtime.toString().substring(0, runtime.toString().indexOf('^')));
						}
					}
					amovie.setDuration(durationString);
					
					sparql3 = "SELECT ?actor_name WHERE { ?movie a <http://dbpedia.org/ontology/Film> ;  <http://dbpedia.org/property/name> \""+movieLiteral+"\"@en ; <http://dbpedia.org/property/starring> ?starring . ?starring <http://www.w3.org/2000/01/rdf-schema#label> ?actor_name . } LIMIT 10 OFFSET 0";
					results3 = lodManager.query(Util.concatSPARQLPrefix(sparql3));
					
					while(results3.hasNext())
					{
						QuerySolution queryDirectorDetails = results3.nextSolution();
						RDFNode actor = queryDirectorDetails.get("actor_name");
						String actorName = actor.toString();
						
						if(!amovie.containsActorNamed(actorName))
						{
							amovie.getActors().add(new AActor(actorName));
						}
					}
				}
				
				amovie.setGenre(genre.getGenre());
				movies.add(amovie);
				System.out.println(amovie.toString());
			}
		}
		
		return movies;
	}
	
	public static void main(String[] args) throws SQLException {
		AMovieDAO dao = new AMovieDAO();
		List<AMovie> movies = dao.getMovies();
		AMovieBD bd = new AMovieBD();
		bd.insertMovies(movies);
	}
}
