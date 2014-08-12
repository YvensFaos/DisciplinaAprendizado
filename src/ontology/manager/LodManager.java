package ontology.manager;

import com.hp.hpl.jena.query.Query;
import com.hp.hpl.jena.query.QueryExecution;
import com.hp.hpl.jena.query.QueryFactory;
import com.hp.hpl.jena.query.ResultSet;
import com.hp.hpl.jena.query.ResultSetFactory;
import com.hp.hpl.jena.sparql.engine.http.QueryEngineHTTP;

public class LodManager { 
	private String endPointURL;
	private Query query;
	private QueryExecution queryExecution;
	private ResultSet resultSet;

	public  LodManager(){ }
	
	public  LodManager(String endPointURL){
		this.endPointURL=endPointURL;
	}
	
	public ResultSet query(String sparqlQuery){
		query = QueryFactory.create(sparqlQuery);
		queryExecution = new QueryEngineHTTP(endPointURL,query) ;
		resultSet = ResultSetFactory.makeRewindable(queryExecution.execSelect()) ;
		return resultSet;
	}

	public String getEndPointURL() {
		return endPointURL;
	}

	public void setEndPointURL(String endPointURL) {
		this.endPointURL = endPointURL;
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

	public ResultSet getResultSet() {
		return resultSet;
	}

	public void setResultSet(ResultSet resultSet) {
		this.resultSet = resultSet;
	}

}