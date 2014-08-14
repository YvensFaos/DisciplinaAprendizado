package model;

import java.util.ArrayList;
import java.util.List;

public class AMovie {

	private int index;
	
	public int getIndex() {
		return index;
	}

	public void setIndex(int index) {
		this.index = index;
	}

	private int year;
	private String name;
	private List<AActor> actors;
	private String summary;
	private ADirector director;
	private String genre;
	private String country;
	private String language;
	private int duration;
	
	public AMovie() {
		this(0, 0, "", new ArrayList<AActor>(), "", new ADirector(), "", "", "", 0);
	}

	public AMovie(int index, int year, String name, String genre,
			String country, String language, int duration) {
		this(index, year, name, new ArrayList<AActor>(), "", new ADirector(), genre, country, language, duration);
	}

	public AMovie(int index, int year, String name, List<AActor> actors,
			String summary, ADirector director, String genre, String country,
			String language, int duration) {
		super();
		this.index = index;
		this.year = year;
		this.name = name;
		this.actors = actors;
		this.summary = summary;
		this.director = director;
		this.genre = genre;
		this.country = country;
		this.language = language;
		this.duration = duration;
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	public String getSummary() {
		return summary;
	}

	public void setSummary(String summary) {
		this.summary = summary;
	}

	public String getGenre() {
		return genre;
	}

	public void setGenre(String genre) {
		this.genre = genre;
	}

	public int getDuration() {
		return duration;
	}

	public void setDuration(int duration) {
		this.duration = duration;
	}

	public int getYear() {
		return year;
	}

	public void setYear(int year) {
		this.year = year;
	}

	public String getCountry() {
		return country;
	}

	public void setCountry(String country) {
		this.country = country;
	}

	public String getLanguage() {
		return language;
	}

	public void setLanguage(String language) {
		this.language = language;
	}
	
	public List<AActor> getActors() {
		return actors;
	}

	public void setActors(List<AActor> actors) {
		this.actors = actors;
	}

	public ADirector getDirector() {
		return director;
	}

	public void setDirector(ADirector director) {
		this.director = director;
	}

	@Override
	public String toString() {
		return "AMovie [index=" + index + ", year=" + year + ", name=" + name
				+ ", actors=" + actors + ", summary=" + summary + ", director="
				+ director + ", genre=" + genre + ", country=" + country
				+ ", language=" + language + ", duration=" + duration + "]";
	}
}
