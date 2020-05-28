using System.Collections.Generic;

namespace ConsoleApp
{
    public class Nested
    {
        public string Name { get; set; }
        
        public bool Active { get; set; }
        
        public double Number { get; set; }
    }
    
    public class Model : Nested
    {
        public Nested RefNull { get; set; }

        public List<Nested> List { get; set; }
    }
}