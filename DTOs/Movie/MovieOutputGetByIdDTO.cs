public class MovieOutputGetByIdDTO{
    public long Id { get; set; }
    public string Title { get; set; }

    public long DirectorId { get; set; }

    public string DirectorName { get; set; }
    
    public MovieOutputGetByIdDTO(long id, string title, long directorId, string directorName)
    {
        Id = id;
        Title = title;
        DirectorId = directorId;
        DirectorName = directorName;
    }
    
}