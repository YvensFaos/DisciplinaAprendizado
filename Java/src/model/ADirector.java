package model;

public class ADirector {

	private int index;
	
	public int getIndex() {
		return index;
	}

	public void setIndex(int index) {
		this.index = index;
	}

	private String name;

	public ADirector(String name)
	{
		this(-1, name);
	}
	
	public ADirector(int index, String name) {
		super();
		this.index = index;
		this.name = name;
	}

	public ADirector() {
		this(-1, "");
	}

	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	@Override
	public String toString() {
		return "ADirector [index=" + index + ", name=" + name + "]";
	}

}
