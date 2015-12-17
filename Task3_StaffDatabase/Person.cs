using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3_StaffDatabase
{
    [Serializable]
    public class Person
    {
        public Person()
        {

        }
        public Person(string firstName, string surName, int age, string post, int id) 
        {
            FirstName = firstName;
            SurName = surName;
            Age = age;
            Post = post;
            ID = id;
        }
        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string Post { get; set; }
        public int Age { get; set; }
        public int ID { get; set; }
        public override string ToString()
        {
            string str = "ID-" + ID + ", " + FirstName + ", " + SurName + ", " + Age + "лет " + Post;
            return str;
        }
    }
}
