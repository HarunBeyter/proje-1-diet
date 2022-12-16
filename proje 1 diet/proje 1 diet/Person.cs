using System;
using System.Collections.Generic;
using System.Text;

namespace proje_1_diet
{
    public class Person
    {
        public string Id { get; set; }
        public string Name { get; set; }    
        public string SurName { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public DateTime BirthTime { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string BloodType { get; set; }
        public List<meal> Breakfast { get; set; }
        public List<meal> lunch { get; set; }
        public List<meal> dinner { get; set; }
        public List<meal> snack { get; set; }
        public string[] Calories { get; set; }
        public string[] Water { get; set; }
        public string[] Excersize { get; set; }
        public string Diet { get; set; }
        public string Img { get; set; }
        public string ProfileImg { get; set; }
        public string Job { get; set; }
        public string Adress { get; set; }
        public string timeInfo { get; set; }
        public string Goal { get; set; }
        
    }
    public class meal
    {
        public string name { get; set; }
        public string fat { get; set; }
        public string protein { get; set; }
        public string calories { get; set; }
    }
  
}
