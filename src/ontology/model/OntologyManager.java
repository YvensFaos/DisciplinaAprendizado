package ontology.model;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.Iterator;
import java.util.List;

import com.hp.hpl.jena.ontology.AllValuesFromRestriction;
import com.hp.hpl.jena.ontology.DatatypeProperty;
import com.hp.hpl.jena.ontology.EnumeratedClass;
import com.hp.hpl.jena.ontology.Individual;
import com.hp.hpl.jena.ontology.MaxCardinalityRestriction;
import com.hp.hpl.jena.ontology.MinCardinalityRestriction;
import com.hp.hpl.jena.ontology.ObjectProperty;
import com.hp.hpl.jena.ontology.OntClass;
import com.hp.hpl.jena.ontology.OntModel;
import com.hp.hpl.jena.ontology.OntModelSpec;
import com.hp.hpl.jena.ontology.OntProperty;
import com.hp.hpl.jena.ontology.Restriction;
import com.hp.hpl.jena.ontology.SomeValuesFromRestriction;
import com.hp.hpl.jena.query.Query;
import com.hp.hpl.jena.query.QueryExecution;
import com.hp.hpl.jena.query.QueryExecutionFactory;
import com.hp.hpl.jena.query.QueryFactory;
import com.hp.hpl.jena.query.ResultSet;
import com.hp.hpl.jena.rdf.model.ModelFactory;
import com.hp.hpl.jena.rdf.model.Property;
import com.hp.hpl.jena.rdf.model.RDFNode;

public class OntologyManager {
  private OntModel baseModel;
  private OntModelSpec ontologyType;
  private OntModelSpec ontologyInferenceType;
  private String baseUri;
  //Query
  private Query query;
  private QueryExecution queryExecution;
  
	public OntologyManager(OntModelSpec ontologyType, OntModelSpec ontologyInferenceType, String baseUri) {
		this.baseUri = baseUri+"#";
		this.ontologyType = ontologyType;
	  	this.ontologyInferenceType = ontologyInferenceType;
		this.baseModel = ModelFactory.createOntologyModel(ontologyType);
	}
	
	public OntClass createOntologyClass(String name){
		OntClass cls = baseModel.createClass(baseUri + name);
		return cls;
	}
	
	public OntClass getOntologyClass(String name){
		OntClass cls = baseModel.getOntClass(baseUri + name);
		return cls;
	}
	
	public Iterator<OntClass> getAllOntClass(){
		return baseModel.listClasses();
	}
	
	public Individual createOntologyIndividual(OntClass ontologyClass, String name){
		Individual individual = baseModel.createIndividual(baseUri + name, ontologyClass );
		return individual;
	}
	
	public Individual createOntologyIndividual(OntClass ontologyClass, RDFNode node){
		Individual individual = baseModel.createIndividual(baseUri + node.asResource().getLocalName(), ontologyClass );
		return individual;
	}
	
	public Individual getOntologyIndividual(String uri){
		Individual individual = baseModel.getIndividual(uri);
		return individual;
	}
	public Iterator<Individual> getAllIndividual(){
		return baseModel.listIndividuals();
	}
	
	public Property getOntologyProperty(String name){
		Property property = baseModel.getProperty(baseUri + name);
		return property;
	}
	public Iterator<OntProperty> getAllProperty(){
		return baseModel.listAllOntProperties();
	}
	
	public ObjectProperty createOntologyObjectProperty(List<OntClass> domains, List<OntClass> ranges, String name, String description){
		ObjectProperty objectProperty = baseModel.createObjectProperty(baseUri + name);
		for (OntClass domain : domains) {
			objectProperty.addDomain(domain);
		}
		for (OntClass range : ranges) {
			objectProperty.addRange(range);
		}
		objectProperty.addLabel(description,"en");
		return objectProperty;
	}
	
	public ObjectProperty getOntologyObjectProperty(String name){
		ObjectProperty objectProperty = baseModel.getObjectProperty(baseUri + name);
		return objectProperty;
	}
	public Iterator<ObjectProperty> getAllObjectProperty(){
		return baseModel.listObjectProperties();
	}
	
	public DatatypeProperty createOntologyDatatypeProperty(List<OntClass> domains, OntClass range, String name, String description){
		DatatypeProperty datatypeProperty = baseModel.createDatatypeProperty(baseUri + name);
		for (OntClass domain : domains) {
			datatypeProperty.addDomain(domain);
		}
		datatypeProperty.addRange(range);
		datatypeProperty.addLabel(description, "en" );
		return datatypeProperty;
	}
	
	public DatatypeProperty getOntologyDatatypeProperty(String name){
		DatatypeProperty datatypeProperty = baseModel.getDatatypeProperty(baseUri + name);
		return datatypeProperty;
	}
	public Iterator<DatatypeProperty> getAllDatatypeProperty(){
		return baseModel.listDatatypeProperties();
	}
	
	public AllValuesFromRestriction createOntologyAllValuesFromRestriction(OntClass cls, OntProperty property){
		// null denotes the URI in an anonymous restriction
		AllValuesFromRestriction allValuesFromRestriction = baseModel.createAllValuesFromRestriction(null, property, cls);
		return allValuesFromRestriction;
	}
	
	public SomeValuesFromRestriction createOntologySomeValuesFromRestriction(OntClass cls, OntProperty property, String uri){
		SomeValuesFromRestriction someValuesFromRestriction = baseModel.createSomeValuesFromRestriction(uri, property, cls);
		return someValuesFromRestriction;
	}
	
	public MinCardinalityRestriction createOntologyMinCardinalityQRestriction(OntProperty property, int cardinality){
		MinCardinalityRestriction minCardinalityRestriction = baseModel.createMinCardinalityRestriction(null, property, cardinality);
		return minCardinalityRestriction;
	}
	
	public MaxCardinalityRestriction createOntologyMaxCardinalityRestriction(OntProperty property, int cardinality){
		MaxCardinalityRestriction maxCardinalityRestriction = baseModel.createMaxCardinalityRestriction(null, property, cardinality);
		return maxCardinalityRestriction;
	}
	
	public EnumeratedClass createOntologyEnumeratedClass(String name, List<Individual> enumeratedClassIndividuals){
		EnumeratedClass enumeratedClass = baseModel.createEnumeratedClass(baseUri + name,null);
		for (Individual individual : enumeratedClassIndividuals) {
			enumeratedClass.addOneOf(individual);
		}
		return enumeratedClass;
	}
	public Iterator<EnumeratedClass> getAllEnumeratedClass(){
		return baseModel.listEnumeratedClasses();
	}
	
	public Restriction getOntologyRestriction(String name){
		Restriction restriction = baseModel.getRestriction(baseUri + name);
		return restriction;
	}
	public Iterator<Restriction> getAllRestriction(){
		return baseModel.listRestrictions();
	}
	
	public ResultSet query(String sparqlQuery) throws IOException{
		query = QueryFactory.create(sparqlQuery);
		queryExecution = QueryExecutionFactory.create(query, baseModel);
		ResultSet results = queryExecution.execSelect();
		return results;
	}
	
	public void loadOntology(File file) throws FileNotFoundException{
//		System.out.println(file.toString());
//		System.out.println(baseUri.toString());
		baseModel.read(new FileInputStream(file), baseUri);
	}
	
	public void loadOntology(String url) throws FileNotFoundException{
		baseModel.read(url);
	}
	
	public void saveOntology(File file) throws FileNotFoundException{
		baseModel.write(new FileOutputStream(file), "RDF/XML");
	}
	
	public void printOntology() throws FileNotFoundException{
		baseModel.write(System.out, "RDF/XML");
	}
	
	public void printOntology(OntModel model) throws FileNotFoundException{
		model.write(System.out, "RDF/XML");
	}
	
	public OntModel makeInference(){
		return ModelFactory.createOntologyModel(ontologyInferenceType,baseModel);
	}


	public OntModel getBaseModel() {
		return baseModel;
	}


	public void setBaseModel(OntModel baseModel) {
		this.baseModel = baseModel;
	}


	public OntModelSpec getOntologyType() {
		return ontologyType;
	}


	public void setOntologyType(OntModelSpec ontologyType) {
		this.ontologyType = ontologyType;
	}


	public OntModelSpec getOntologyInferenceType() {
		return ontologyInferenceType;
	}


	public void setOntologyInferenceType(OntModelSpec ontologyInferenceType) {
		this.ontologyInferenceType = ontologyInferenceType;
	}

	public String getBaseUri() {
		return baseUri;
	}


	public void setBaseUri(String baseUri) {
		this.baseUri = baseUri;
	}


	public Query getQuery() {
		return query;
	}


	public void setQuery(Query query) {
		this.query = query;
	}


	public QueryExecution getQueryExecution() {
		return queryExecution;
	}


	public void setQueryExecution(QueryExecution queryExecution) {
		this.queryExecution = queryExecution;
	}
	
	
	
}
