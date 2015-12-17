using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace Task3_StaffDatabase
{
    class Binary : IOperation
    {
        BinaryFormatter bf = new BinaryFormatter();
        public List<Person> Read() 
        {
            List<Person> persons;
            using (FileStream fs = new FileStream("bPersons.dat", FileMode.OpenOrCreate)) 
            {
                if (fs.Length != 0) 
                {
                    persons = (List<Person>)bf.Deserialize(fs);
                }
                else
                {
                    persons = new List<Person>();
                }
            }
            return persons;
        }
        public void Write(List<Person> persons) 
        {
            using (FileStream fs = new FileStream("bPersons.dat", FileMode.Create))
            {
                bf.Serialize(fs, persons);
            }
        }
    }
}
