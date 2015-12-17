using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Task3_StaffDatabase
{
    internal class Xml : IOperation
    {
        private XmlSerializer srlz = new XmlSerializer(typeof(List<Person>));
        public List<Person> Read()
        {
            List<Person> persons;
            using (FileStream fs = new FileStream("xPersons.xml", FileMode.OpenOrCreate))
            {
                if (fs.Length != 0)
                {
                    persons = (List<Person>)srlz.Deserialize(fs);
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
            using (FileStream fs = new FileStream("xPersons.xml", FileMode.Create))
            {
                srlz.Serialize(fs, persons);
            }
        }
    }
}
