public class DirectorOutputPostDTO
{
    public long Id { get; set; }
    public string Name { get; set; }

    public DirectorOutputPostDTO(long id, string name){
        Id = id;
        Name = name;
    }
}