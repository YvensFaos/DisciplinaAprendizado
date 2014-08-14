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
			
			if(res2 != null)
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
	
	public boolean insertDirector(ADirector director) throws SQLException
	{		
		Connection connection = ConnectionDAO.getInstance().getConnection();
		Statement st = connection.createStatement();
		int val = st.executeUpdate("INSERT INTO ADIRECTOR (name) VALUES("+director.getName()+")");
		
		return val == 1;
	}
	
	public static void main(String[] args) throws SQLException {
		new AMovieBD().getActors();
	}
}
