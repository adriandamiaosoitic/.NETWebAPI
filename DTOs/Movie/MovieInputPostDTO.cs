public class MovieInputPostDTO{
    public string Title { get; set; }

    public long DirectorId { get; set; }
    
    
    public MovieInputPostDTO(string title, long directorId){
        Title = title; 
        DirectorId = directorId;
    }
}