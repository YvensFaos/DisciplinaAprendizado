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
			QuerySolution queryMovie = result.nextSolution();
			String sparql2 = "SELECT distinct * WHERE { ?movie a <http://dbpedia.org/ontology/Film> ; <http://dbpedia.org/property/name> \"&\"@en ; <http://dbpedia.org/property/director> ?director ; <http://dbpedia.org/property/country> ?country ; <http://dbpedia.org/property/language> ?language; <http://dbpedia.org/property/starring> ?starring; <http://dbpedia.org/ontology/releaseDate> ?release_date; <http://dbpedia.org/property/runtime> ?runtime . } LIMIT 1000 OFFSET 0";
			
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
			
			System.out.println(movieLiteral);
			sparql2 = sparql2.replaceAll("&", movieLiteral);
			System.out.println(sparql2);
			results2 = lodManager.query(Util.concatSPARQLPrefix(sparql2));
			while(results2.hasNext()){
				QuerySolution queryMovieDetails = results2.nextSolution();
				
				RDFNode movieD = queryMovieDetails.get("name");
				
				System.out.println(movieD);
			}
		}
		
		return movies;
	}
	
	public static void main(String[] args) {
		AMovieDAO dao = new AMovieDAO();
		dao.getMovies();
	}
}
