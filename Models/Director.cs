using System.Collections.Generic;

public class Director{

    public long Id { get; private set; }
    public string Name { get; set; }
    public ICollection<Movie> Movies { get; set; }

    public Director(string name){
        Name = name;
        Movies = new List<Movie>();
    }

}