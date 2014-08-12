package ontology.main;

import java.io.File;
import java.io.FileNotFoundException;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import ontology.model.LodManager;
import ontology.model.OntologyManager;

import com.hp.hpl.jena.ontology.Individual;
import com.hp.hpl.jena.ontology.OntClass;
import com.hp.hpl.jena.ontology.OntModel;
import com.hp.hpl.jena.ontology.OntModelSpec;
import com.hp.hpl.jena.query.QuerySolution;
import com.hp.hpl.jena.query.ResultSet;
import com.hp.hpl.jena.rdf.model.Property;
import com.hp.hpl.jena.rdf.model.RDFNode;
import com.hp.hpl.jena.rdf.model.Resource;


	public class ExecExemple {
		public String ontologyName;
		public String owlPath;
		public String uriDBpediaLinkedConcept;
		public OntologyManager ontologyManager;
		public LodManager lodManager;
		public static List<Individual> filmIndividuals = new ArrayList<Individual>();
		public static List<Individual> diretorIndividuals = new ArrayList<Individual>();
		
		public ExecExemple(String ontologyName, String owlPath, String uriDBpediaLinkedConcept){
			this.ontologyName = ontologyName;
			this.owlPath = owlPath;
			this.uriDBpediaLinkedConcept = uriDBpediaLinkedConcept;
			ontologyManager = new OntologyManager(OntModelSpec.OWL_MEM, OntModelSpec.OWL_MEM_MICRO_RULE_INF,ontologyName);
			lodManager = new LodManager("http://dbpedia.org/sparql");
			try {
			ontologyManager.loadOntology(new File(owlPath));
			} catch (FileNotFoundException e) {
				e.printStackTrace();
			}
		}
		
		public void instanciarOntologia() {
				//CLASSES DA MINHA ONTOLOGIA
				OntClass filmeClass =  ontologyManager.getOntologyClass("Filme");
				OntClass diretorClass =  ontologyManager.getOntologyClass("Diretor");
				//PROPRIEDADES(DATAPROPERTY & OBJECTPROPERTY) DA ONTOLOGIA
				Property temNome = ontologyManager.getOntologyProperty("tem_nome");
				Property temDataLancamento = ontologyManager.getOntologyProperty("tem_data_de_lançamento");
				Property possuiDiretor = ontologyManager.getOntologyProperty("possui_diretor");
				Property temAreaDeAtuacao = ontologyManager.getOntologyProperty("tem_área_de_atuação");

				//SPARQL PARA RECUPERAR 10 FILMES COM TITULO, DATA_LANCAMENTO, DIRETOR, NOME_DIRETOR, AREA_ATUACAO
				String sparqlQuery = "SELECT ?filme ?titulo ?data_lancamento ?diretor ?nome_diretor ?area_atuacao  WHERE { " +
						"?filme <http://www.w3.org/1999/02/22-rdf-syntax-ns#type> <"+uriDBpediaLinkedConcept+">." +
						"?filme <http://dbpedia.org/property/name> ?titulo." +
						"?filme <http://dbpedia.org/ontology/releaseDate> ?data_lancamento." +
						"?filme <http://dbpedia.org/ontology/director> ?diretor." +
						"?diretor <http://xmlns.com/foaf/0.1/name> ?nome_diretor." +
						"?diretor <http://dbpedia.org/property/occupation> ?area_atuacao." +
						"}LIMIT 10";

				ResultSet result = lodManager.query(sparqlQuery);
				while(result.hasNext()){
					QuerySolution querySolution = result.nextSolution();
					RDFNode filme= querySolution.get("filme");
					RDFNode titulo = querySolution.get("titulo");
					RDFNode dataLancamento = querySolution.get("data_lancamento");
					RDFNode diretor = querySolution.get("diretor");
					RDFNode nomeDiretor = querySolution.get("nome_diretor");
					RDFNode areaAtuacao = querySolution.get("area_atuacao");		

					System.out.println("FILME: "+filme);
					System.out.println("TITULO: "+titulo);
					System.out.println("DATA LANCAMENTO: "+dataLancamento);
					System.out.println("DIRETOR: "+diretor);
					System.out.println("NOME DIRETOR: "+nomeDiretor);
					System.out.println("AREA DE ATUACAO: "+areaAtuacao);
					System.out.println();

					//CRIANDO UMA INSTANCIA DE DIRETOR
					Individual individualDiretor = ontologyManager.createOntologyIndividual(diretorClass, diretor.asResource().getLocalName());
					individualDiretor.addProperty(temNome, nomeDiretor.asLiteral());
					individualDiretor.addProperty(temDataLancamento, dataLancamento.asLiteral());

					//CRIANDO UMA INSTANCIA DE FILME
					Individual individualFilme = ontologyManager.createOntologyIndividual(filmeClass, filme);
					individualFilme.addProperty(temNome, titulo);
					individualFilme.addProperty(temAreaDeAtuacao, areaAtuacao);
					individualFilme.addProperty(possuiDiretor,individualDiretor);

					filmIndividuals.add(individualFilme);
					diretorIndividuals.add(individualDiretor);
				}
		}

		public void gerarInferencias() {
			OntModel infModel = ontologyManager.makeInference();

			Individual filme = filmIndividuals.get(0);
			String filmeUri = filme.getURI();
			// list the asserted types
			for (Iterator<Resource> i = filme.listRDFTypes(false); i.hasNext(); ) {
				System.out.println( filme.getURI() + " is asserted in class " + i.next() );
			}
			
			System.out.println("-----------------------------------------------------------------");
			
			// list the inferred types
			filme = infModel.getIndividual(filmeUri);
			for (Iterator<Resource> i = filme.listRDFTypes(false); i.hasNext(); ) {
				System.out.println( filme.getURI() + " is inferred to be in class " + i.next() );
			}
		}
		
		
		
		public static void main(String[] args) {
			String ontologyName = "http://www.owl-ontologies.com/Ontology1351200004.owl";
			String owlPath = "ontology/filme.owl";
			String uriDBpediaLinkedConcept = "http://dbpedia.org/ontology/Film";
			
			ExecExemple exec = new ExecExemple(ontologyName, owlPath, uriDBpediaLinkedConcept);
			exec.instanciarOntologia();
			exec.gerarInferencias();
			
		}
	}
