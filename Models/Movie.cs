public class Movie{

    public long Id { get; private set; }
    public string Title { get; set; }
    public string Year { get; set; }
    public string Gender { get; set; }

    public long DirectorId { get; set; }

    public Director Director { get; set; } //Objeto para navegação, não é obrigatório. Somente por questão de facilidade    
    public Movie(string title){
        Title = title;
    }
}