package model;

public class AActor {

	private int index;
	
	public int getIndex() {
		return index;
	}

	public void setIndex(int index) {
		this.index = index;
	}

	private String name;

	public AActor(int index, String name) {
		super();
		this.index = index;
		this.name = name;
	}

	public AActor() {
		this(-1, "");
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

}
