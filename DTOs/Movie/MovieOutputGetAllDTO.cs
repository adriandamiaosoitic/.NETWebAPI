public class MovieOutputGetAllDTO{
    public long Id { get; set; }
    public string Title { get; set; }
    
    public MovieOutputGetAllDTO(long id, string title){
        Id = id;
        Title = title;
    }
    
}