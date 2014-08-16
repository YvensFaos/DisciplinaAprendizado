package dao;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

import model.AActor;
import model.ADirector;
import model.AMovie;

public class AMovieBD {
	
	public AMovie getMovie(AMovie amovie) throws SQLException
	{
		Connection connection = ConnectionDAO.getInstance().getConnection();
		
		Statement st = connection.createStatement(); 
		ResultSet res = st.executeQuery("SELECT * FROM AMOVIE WHERE NAME = '"+amovie.getName()+"'");
		
		if (res.next()) 
		{ 
			int id = res.getInt("idamovie"); 
			String name = res.getString("name"); 
			String genre = res.getString("genre"); 
			String country = res.getString("country"); 
			String language = res.getString("language"); 
			int duration = res.getInt("duration"); 
			int year = res.getInt("year"); 
			
			amovie = new AMovie(id, year, name, genre, country, language, duration); 
		}
		
		return amovie;
	}
	
	public boolean existMovie(AMovie amovie) throws SQLException
	{
		return getMovie(amovie).getIndex() != 0;
	}
	
	public List<AMovie> getMovies() throws SQLException
	{
		List<AMovie> list = new ArrayList<AMovie>();
		
		Connection connection = ConnectionDAO.getInstance().getConnection();
		
		Statement st = connection.createStatement(); 
		ResultSet res = st.executeQuery("SELECT * FROM AMOVIE");
		ResultSet res2;
		
		while (res.next()) 
		{ 
			int id = res.getInt("idamovie"); 
			String name = res.getString("name"); 
			String genre = res.getString("genre"); 
			String country = res.getString("country"); 
			String language = res.getString("language"); 
			int duration = res.getInt("duration"); 
			int year = res.getInt("year"); 
			
			AMovie amovie = new AMovie(id, year, name, genre, country, language, duration); 
			res2 = st.executeQuery("SELECT A.IDAACTOR, A.NAME FROM AMOVIE M, AACTOR A, ACTOR_MOVIE AM WHERE M.IDAMOVIE = AM.IDAMOVIE AND A.IDAACTOR = AM.IDAACTOR");
			
			while (res2.next()) 
			{
				int idactor = res2.getInt("idaactor");
				String nameactor = res2.getString("name");
				
				AActor actor = new AActor(idactor, nameactor);
				
				amovie.getActors().add(actor);
			}
			
			res2 = st.executeQuery("SELECT D.IDADIRECTOR, D.NAME FROM AMOVIE M, ADIRECTOR D, DIRECTOR_MOVIE DM WHERE M.IDAMOVIE = DM.IDAMOVIE AND D.IDADIRECTOR = DM.IDADIRECTOR");
			
			if(res2.next())
			{
				int idadirector = res2.getInt("idadirector");
				String namedirector = res2.getString("name");
				
				ADirector director = new ADirector(idadirector, namedirector);
				
				amovie.setDirector(director);
			}
			
			list.add(amovie);
		}
		
		return list;
	}
	
	public int insertMovies(List<AMovie> movies) throws SQLException
	{
		int added = 0;
		int qtty = 0;
		for (AMovie aMovie : movies) {
			if(!existMovie(aMovie))
			{
				added += insertMovie(aMovie);
				System.out.printf("%d/%d\n", ++qtty, movies.size());
			}
			else
			{
				qtty++;
			}
		}
		
		return added;
	}
	
	public int insertMovie(AMovie amovie) throws SQLException
	{
		Connection connection = ConnectionDAO.getInstance().getConnection();
		Statement st = connection.createStatement();
		 
		int val = st.executeUpdate("INSERT INTO AMOVIE (name, genre, country, language, duration, year) VALUES('"+amovie.getName()+"', '"+amovie.getGenre()+"', '"+amovie.getCountry()+"', '"+amovie.getLanguage()+"', "+amovie.getDuration()+", "+amovie.getYear()+")");
		if(val != 0)
		{
			amovie = getMovie(amovie);
		}
		if(!amovie.getDirector().getName().isEmpty())
		{
			if(!existDirector(amovie.getDirector()))
			{
				insertDirector(amovie.getDirector());
			}
			
			amovie.setDirector(getDirector(amovie.getDirector()));
			st.executeUpdate("INSERT INTO DIRECTOR_MOVIE VALUES ("+amovie.getIndex()+", "+amovie.getDirector().getIndex()+")");
		}
		if(!amovie.getActors().isEmpty())
		{
			for (AActor aactor : amovie.getActors()) {
				if(!existActor(aactor))
				{
					insertActor(aactor);
				}
				
				aactor = getActor(aactor);
				st.executeUpdate("INSERT INTO ACOR_MOVIE VALUES ("+amovie.getIndex()+", "+aactor.getIndex()+")");
			}
		}
		return val;
	}
	
	public List<AActor> getActors() throws SQLException
	{
		List<AActor> list = new ArrayList<AActor>();
		
		Connection connection = ConnectionDAO.getInstance().getConnection();
		
		Statement st = connection.createStatement(); 
		ResultSet res = st.executeQuery("SELECT * FROM AACTOR"); 
		while (res.next()) 
		{ 
			int id = res.getInt("idaactor"); 
			String name = res.getString("name"); 
			AActor aactor = new AActor(id, name); 
			
			list.add(aactor);
		}
			
		return list;
	}
	
	public boolean insertActor(AActor aactor) throws SQLException
	{		
		Connection connection = ConnectionDAO.getInstance().getConnection();
		Statement st = connection.createStatement();
		int val = st.executeUpdate("INSERT INTO AACTOR (name) VALUES("+aactor.getName()+")");
		
		return val == 1;
	}
	
	public AActor getActor(AActor aactor) throws SQLException
	{	
		Connection connection = ConnectionDAO.getInstance().getConnection();
		
		Statement st = connection.createStatement(); 
		ResultSet res = st.executeQuery("SELECT a.idactor FROM AACTOR D WHERE D.name = \""+aactor.getName()+"\""); 
		
		if(res.next())
		{
			aactor.setIndex(res.getInt("idadirector"));
		}
		
		return aactor;
	}
	
	public boolean existActor(AActor aactor) throws SQLException
	{
		return getActor(aactor).getIndex() != 0;
	}
	
	public ADirector getDirector(ADirector director) throws SQLException
	{
		Connection connection = ConnectionDAO.getInstance().getConnection();
		
		Statement st = connection.createStatement(); 
		ResultSet res = st.executeQuery("SELECT D.idadirector FROM ADIRECTOR D WHERE D.name = \""+director.getName()+"\""); 
		
		if(res.next())
		{
			director.setIndex(res.getInt("idadirector"));
		}
		return director;
	}
	
	public List<ADirector> getDirector() throws SQLException
	{
		List<ADirector> list = new ArrayList<ADirector>();
		
		Connection connection = ConnectionDAO.getInstance().getConnection();
		
		Statement st = connection.createStatement(); 
		ResultSet res = st.executeQuery("SELECT * FROM ADIRECTOR"); 
		while (res.next()) 
		{ 
			int id = res.getInt("idadirector"); 
			String name = res.getString("name"); 
			ADirector adirector = new ADirector(id, name); 
			
			list.add(adirector);
		}
			
		return list;
	}
	
	public boolean existDirector(ADirector director) throws SQLException
	{
		return getDirector(director).getIndex() != 0;
	}
	
	public boolean insertDirector(ADirector director) throws SQLException
	{		
		Connection connection = ConnectionDAO.getInstance().getConnection();
		Statement st = connection.createStatement();
		int val = st.executeUpdate("INSERT INTO ADIRECTOR (name) VALUES("+director.getName()+")");
		
		return val == 1;
	}
	
	public boolean insertDirectorMovie(AMovie movie) throws SQLException
	{		
		return insertDirector(movie.getDirector());
	}
	
	public static void main(String[] args) throws SQLException {
		//new AMovieBD().getActors();
		new AMovieBD().getMovie(new AMovie("Another Gay Movie"));
	}
}
