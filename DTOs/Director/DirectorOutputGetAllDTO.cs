public class DirectorOutputGetAllDTO{

    public long Id { get; private set; }
    public string Name { get; private set; }
    
     public DirectorOutputGetAllDTO(long id, string name){
            Id = id;
            Name = name;
        }
    
}