using System;    
    public class DirectorOutputGetByIdDTO
    {
        public long Id { get; private set; }
        public string Name { get; private set; }
        
        public DirectorOutputGetByIdDTO(long id, string name){

            if(name == null){ //Programação defensiva, validação que nao pode ser quebrada
                throw new ArgumentNullException("Name is null");
            }
            Id = id;
            Name = name;
        }
        
    }