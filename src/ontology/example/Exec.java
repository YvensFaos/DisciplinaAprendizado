package ontology.example;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import ontology.manager.LodManager;
import ontology.manager.OntologyManager;

import com.hp.hpl.jena.ontology.Individual;
import com.hp.hpl.jena.ontology.OntClass;
import com.hp.hpl.jena.ontology.OntModel;
import com.hp.hpl.jena.ontology.OntModelSpec;
import com.hp.hpl.jena.query.QuerySolution;
import com.hp.hpl.jena.query.ResultSet;
import com.hp.hpl.jena.rdf.model.Property;
import com.hp.hpl.jena.rdf.model.RDFNode;
import com.hp.hpl.jena.rdf.model.Resource;

public class Exec {
	public String ontologyName;
	public String owlPath;
	public String uriDBpediaLinkedConcept;
	public OntologyManager ontologyManager;
	public LodManager lodManager;
	public List<Individual> myIndividuals = new ArrayList<Individual>();
	
	public Exec(String ontologyName, String owlPath, String uriDBpediaLinkedConcept){
		this.ontologyName = ontologyName;
		this.owlPath = owlPath;
		this.uriDBpediaLinkedConcept = uriDBpediaLinkedConcept;
		ontologyManager = new OntologyManager(OntModelSpec.OWL_MEM, OntModelSpec.OWL_MEM_MICRO_RULE_INF,ontologyName);
		lodManager = new LodManager("http://dbpedia.org/sparql");
		try {
			System.out.println(owlPath);
			ontologyManager.loadOntology(new File(owlPath));
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		}
	}
	
	public void instanciarOntologia() {
//		TODO	1º)Obter a Classe ao qual queira instanciar.
		OntClass bandClasse = ontologyManager.getOntologyClass("Band");
		OntClass musicoClasse = ontologyManager.getOntologyClass("Musician");
		OntClass albumClasse = ontologyManager.getOntologyClass("Album");
		OntClass instrumentoClasse = ontologyManager.getOntologyClass("Musical_Instrument");
		OntClass trackClasse = ontologyManager.getOntologyClass("Music");
		
//		TODO	2º)Obter as Propriedades(DataProperty & ObjectProperty) que pertencem a minhaClasse.
		Property temMembro = ontologyManager.getOntologyProperty("has_member");
		Property temAlbum = ontologyManager.getOntologyProperty("has_album");
		Property tocaInstrumento = ontologyManager.getOntologyProperty("plays_instrument");
		Property temFaixa = ontologyManager.getOntologyProperty("has_track");
		
//		TODO	3ª)Criar uma consulta SPARQL para obter as Instancias na base DBpedia
//		String sparqlGetHeavyBand = 
//				"select distinct ?band where " +
//				"{?band <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <http://schema.org/MusicGroup>. " +
//				" ?band <http://dbpedia.org/ontology/genre> <http://dbpedia.org/resource/Heavy_metal_music> } " +
//				"LIMIT 30";
		
		String sparqlGetDeathcoreBand = 
				"select distinct ?band where " +
				"{?band <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <http://schema.org/MusicGroup>. " +
				" ?band <http://dbpedia.org/ontology/genre> <http://dbpedia.org/resource/Deathcore> } " +
				"LIMIT 30";
		
		String sparqlGetAlbum = 
				"select distinct ?album where " +
				"{?album <http://dbpedia.org/ontology/artist> <@> . " +
				" ?album <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <http://schema.org/MusicAlbum> } " +
				"LIMIT 3";
		
		String sparqlGetTracks =
				"select distinct  ?track where " +
				"{?track <http://dbpedia.org/property/fromAlbum> <@>  } " +
				"LIMIT 15";
		
		String sparqlGetMembers = 
				"select distinct ?member where " +
				"{ <@> <http://dbpedia.org/ontology/bandMember>  ?member } " +
				"LIMIT 10";
		
		String sparqlGetInstruments = 
				"select distinct ?instrument where " +
				"{<@> <http://dbpedia.org/ontology/instrument>  ?instrument } " +
				"LIMIT 8";

		boolean debug = true;
		
		//TODO adicionar a consulta para pegar faixas de um album. 
		
		ResultSet result = lodManager.query(sparqlGetDeathcoreBand);
		while(result.hasNext()){
			QuerySolution queryBanda = result.nextSolution();
			
			RDFNode    banda = queryBanda.get("band");
			Individual bandaIndividual = ontologyManager.createOntologyIndividual(bandClasse,banda);
			
			if(debug)
			{
				System.out.println(banda.toString());
			}
			boolean hasAlbum = false;
			boolean hasMember = false;
			
			String queryGetAlbum = sparqlGetAlbum.replaceAll("@", banda.toString());
			
			ResultSet albumResult = lodManager.query(queryGetAlbum);
			while(albumResult != null && albumResult.hasNext()){
				QuerySolution queryAlbum = albumResult.nextSolution();
				RDFNode album = queryAlbum.get("album");
				
				if(debug)
				{
					System.out.println("> "+album.toString());
				}
				Individual albumIndividual = ontologyManager.createOntologyIndividual(albumClasse, album);
				
				bandaIndividual.addProperty(temAlbum, albumIndividual);
				hasAlbum = true;
				
				String queryGetTracks = sparqlGetTracks.replaceAll("@", album.toString());
				
				ResultSet tracksResult = lodManager.query(queryGetTracks);
				while(tracksResult != null && tracksResult.hasNext())
				{
					QuerySolution queryInstrument = tracksResult.nextSolution();
					RDFNode track = queryInstrument.get("track");
					
					if(debug)
					{
						System.out.println(" >> "+track.toString());
					}
					Individual trackIndividual = ontologyManager.createOntologyIndividual(trackClasse, track);
					
					albumIndividual.addProperty(temFaixa, trackIndividual);
				}
			}
			
			String queryGetMembers = sparqlGetMembers.replaceAll("@", banda.toString());
			ResultSet memberResult = lodManager.query(queryGetMembers);
			while(memberResult != null && memberResult.hasNext()){
				QuerySolution queryMember = memberResult.nextSolution();
				RDFNode member = queryMember.get("member");
				
				if(debug)
				{
					System.out.println("# "+member.toString());
				}
				Individual memberIndividual = ontologyManager.createOntologyIndividual(musicoClasse, member);
				
				bandaIndividual.addProperty(temMembro, memberIndividual);
				hasMember = true;
				
				String queryGetInstruments = sparqlGetInstruments.replaceAll("@", member.toString());
				
				ResultSet instrumentResult = lodManager.query(queryGetInstruments);
				while(instrumentResult != null && instrumentResult.hasNext())
				{
					QuerySolution queryInstrument = instrumentResult.nextSolution();
					RDFNode instrument = queryInstrument.get("instrument");
					
					if(debug)
					{
						System.out.println(" ## "+instrument.toString());
					}
					Individual memberInstrument = ontologyManager.createOntologyIndividual(instrumentoClasse, instrument);
					
					memberIndividual.addProperty(tocaInstrumento, memberInstrument);
				}
			}
			
			if(hasAlbum || hasMember)
			{
				myIndividuals.add(bandaIndividual);
			}
		}
	}

	public void gerarInferencias() {
		OntModel infModel = ontologyManager.makeInference();
		for (Individual individual : myIndividuals) {
			
			System.out.println("INDIVIDUAL: "+individual);
			
			String individualURI = individual.getURI();
			for (Iterator<Resource> i = individual.listRDFTypes(false); i.hasNext(); ) {
				System.out.println( individual.getURI() + " EST�O RELACIONADA COM A CLASSE " + i.next() );
			}
			
			individual = infModel.getIndividual(individualURI);
			
			if(individual != null)
			{
				Iterator<Resource> i = individual.listRDFTypes(false);
				if(i != null)
				{
					for (; i.hasNext(); ) {
						System.out.println( individual.getURI() + " FOI INFERIDA E RELACIONADA  A CLASSE " + i.next() );
					}
				}
			}
			System.out.println();
		}
	}
	
	public static void main(String[] args) {
		String ontologyName = "http://www.yvens.com.br/Music"; //TODO	
		String owlPath = "ontology/music_ont.owl"; //TODO	
		String uriDBpediaLinkedConcept = "http://dbpedia.org/ontology/Band"; //TODO
		
		
		Exec exec = new Exec(ontologyName, owlPath, uriDBpediaLinkedConcept);
		exec.instanciarOntologia();
		exec.gerarInferencias();
	}
}
