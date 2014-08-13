package dao;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class ConnectionDAO {

	private static ConnectionDAO instance;
	private Connection connection;

	public static ConnectionDAO getInstance() {
		if(instance == null)
		{
			instance = new ConnectionDAO();
		}
		return instance;
	}

	private ConnectionDAO()
	{
		String url = "jdbc:mysql://localhost:3306/";
		String dbName = "amoviedatabase";
		String driver = "com.mysql.jdbc.Driver";
		String username = "root";
		String password = "root";
		
		try {
			Class.forName(driver).newInstance();
			connection = DriverManager.getConnection(url+dbName,username,password);
		} catch (InstantiationException | IllegalAccessException | ClassNotFoundException e) {
			e.printStackTrace();
		} catch (SQLException e) {
			e.printStackTrace();
		}
	}
	
	public Connection getConnection() {
		return connection;
	}
}