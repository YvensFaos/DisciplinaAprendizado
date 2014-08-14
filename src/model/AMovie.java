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
	private List<String> actors;
	private String summary;
	private String director;
	private String genre;
	private String country;
	private String language;
	private int duration;

	public AMovie() {
		this(0, "", new ArrayList<String>(), "", "", "", "", "", 0);
	}

	public AMovie(int year, String name, List<String> actors, String summary,
			String director, String genre, String country, String language,
			int duration) {
		super();
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

	public List<String> getActors() {
		return actors;
	}

	public void setActors(List<String> actors) {
		this.actors = actors;
	}

	public String getSummary() {
		return summary;
	}

	public void setSummary(String summary) {
		this.summary = summary;
	}

	public String getDirector() {
		return director;
	}

	public void setDirector(String director) {
		this.director = director;
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

	@Override
	public String toString() {
		return "AMovie [index=" + index + ", year=" + year + ", name=" + name
				+ ", actors=" + actors + ", director=" + director
				+ ", country=" + country + ", language=" + language
				+ ", duration=" + duration + "]";
	}
}
