public class MovieOutputPutDTO{
    public long Id { get; set; }
    public string Title { get; set; }

    public MovieOutputPutDTO(long id, string title){
        Id = id;
        Title = title;
    }
}