package dao;

import java.util.ArrayList;
import java.util.List;

import com.hp.hpl.jena.query.QuerySolution;
import com.hp.hpl.jena.query.ResultSet;
import com.hp.hpl.jena.rdf.model.RDFNode;

import model.AMovie;
import ontology.manager.LodManager;
import ontology.util.Util;

public class AMovieDAO {
	private LodManager lodManager;
	
	public AMovieDAO() {
		super();
		lodManager = new LodManager("http://dbpedia.org/sparql");
	}

	public List<AMovie> getMovies()
	{
		List<AMovie> movies = new ArrayList<AMovie>();
		
		String sparql = "SELECT distinct ?movie ?name WHERE { ?movie a <http://dbpedia.org/ontology/Film> ; <http://dbpedia.org/property/name> ?name ;  <http://purl.org/dc/terms/subject> <http://dbpedia.org/resource/Category:Parody_films> . } LIMIT 1000 OFFSET 0";
		ResultSet result = lodManager.query(Util.concatSPARQLPrefix(sparql));
		ResultSet results2;
		while(result.hasNext()){
			AMovie amovie = new AMovie();
			QuerySolution queryMovie = result.nextSolution();
			String sparql2 = "SELECT * WHERE { ?movie a <http://dbpedia.org/ontology/Film> ; <http://dbpedia.org/property/name> \"&\"@en ; <http://dbpedia.org/property/director> ?director ; <http://dbpedia.org/property/country> ?country ; <http://dbpedia.org/property/language> ?language . OPTIONAL { ?movie <http://dbpedia.org/property/runtime> ?runtime . } OPTIONAL { ?movie <http://dbpedia.org/ontology/releaseDate> ?release_date . }  } LIMIT 1000 OFFSET 0";
			
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
			
			System.out.println(movieLiteral);
			sparql2 = sparql2.replaceAll("&", movieLiteral);
//			System.out.println(sparql2);
			results2 = lodManager.query(Util.concatSPARQLPrefix(sparql2));
			
			if(!results2.hasNext())
			{
				System.out.println("NULO!");
			}
			while(results2.hasNext()){
				QuerySolution queryMovieDetails = results2.nextSolution();
				
				System.out.println(queryMovieDetails.toString());
//				RDFNode director = queryMovieDetails.get("director");
//				RDFNode country = queryMovieDetails.get("country");
//				RDFNode language = queryMovieDetails.get("language");
//				RDFNode releaseDate = queryMovieDetails.get("releaseDate");
//				RDFNode runtime = queryMovieDetails.get("runtime");
//				
//				System.out.printf("%s %s %s %s %s %s\n", director.toString(), country.toString(), language.toString(), releaseDate.toString(), runtime.toString());
			}
		}
		
		return movies;
	}
	
	public static void main(String[] args) {
		AMovieDAO dao = new AMovieDAO();
		dao.getMovies();
	}
}
