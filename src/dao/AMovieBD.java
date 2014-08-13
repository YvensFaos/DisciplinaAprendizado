package dao;

import java.sql.Connection;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;
import java.util.ArrayList;
import java.util.List;

import model.AActor;

public class AMovieBD {
	
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
		//TODO
		int val = st.executeUpdate("INSERT INTO AACTOR VALUES("+1+","+"'Easy'"+")");
		
		
		return val == 1;
	}
}
