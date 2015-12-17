using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Task3_StaffDatabase
{
    class Menu
    {
        private string type;
        List<Person> persons;
        private IOperation io;
        public void Processing()
        {
            try
            {
                using (FileStream fs = File.OpenRead("option.ini"))
                {
                    byte[] array = new byte[fs.Length];
                    fs.Read(array, 0, array.Length);
                    type = System.Text.Encoding.Default.GetString(array).ToLower();
                    if (type == "xml")
                    {
                        io = new Xml();
                    }
                    else
                    {
                        io = new Binary();
                    }
                    Load(io);
                }
            }
            catch (FileNotFoundException ex)
            {
                type = "xml";
                using (FileStream fs = new FileStream("option.ini", FileMode.Create))
                {
                    byte[] array = System.Text.Encoding.Default.GetBytes(type);
                    fs.Write(array, 0, array.Length);
                }
                io = new Xml();
                Load(io);
            }
        }
        private void Load(IOperation io)
        {
            persons = io.Read();
            UserAction();
        }
        private void UserAction()
        {
            while (true)
            {
                string str;
                if (persons.Count == 0)
                {
                    Console.WriteLine("База данных успешно загружена, но не содержит данных.");
                    Console.WriteLine("Доступные действия:\n1. Добавить данные.\n2. Сохранить и выйти.");
                    str = Console.ReadLine();
                    switch (str)
                    {
                        case "1":
                            AddPerson();
                            break;
                        case "2":
                            SaveExit(io);
                            return;
                        default:
                            continue;
                    }
                }
                else
                {
                    Console.WriteLine("База данных успешно загружена.");
                    Console.WriteLine("Доступные действия:\n1. Добавить данные.\n2. Удалить данные\n3. Посмотреть данные сотрудника\n4. Посмотреть данные всех сотрудников\n5. Сохранить и выйти.");
                }
                str = Console.ReadLine();
                switch (str)
                {
                    case "1":
                        AddPerson();
                        break;
                    case "2":
                        DeletePerson();
                        break;
                    case "3":
                        GetPerson();
                        break;
                    case "4":
                        GetAllStaff();
                        break;
                    case "5":
                        SaveExit(io);
                        return;
                    default:
                        continue;
                }
            }
        }
        private void AddPerson()
        {
            Person per;
            Console.WriteLine("Введите имя сотрудника:");
            string fistName = Console.ReadLine();
            Console.WriteLine("Введите фамилию сотрудника:");
            string surName = Console.ReadLine();
            Console.WriteLine("Введите возраст сотрудника:");
            int age =Int32.Parse(Console.ReadLine());
            Console.WriteLine("Введите должность сотрудника:");
            string post = Console.ReadLine();
            if (persons.Count == 0)
            {
                per = new Person(fistName, surName, age, post, 1);
            }
            else
            {
                per = new Person(fistName, surName, age, post, persons.Last().ID + 1);
            }
            persons.Add(per);
            Console.WriteLine("Данные успешно добавленны. Чтобы продолжить нажмите Enter");
            Console.ReadLine();
        }
        private void DeletePerson()
        {
            int deleteId = 0;
            Console.WriteLine("Введите ID сотрудника которого хотите удалить:");
            string str = Console.ReadLine();
            try
            {
                deleteId = Int32.Parse(str);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Можно использовать только цифры. Чтобы продолжить нажмите Enter.");
                Console.ReadLine();
                return;
            }
            foreach (Person p in persons)
            {
                if (p.ID == deleteId)
                {
                    persons.Remove(p);
                    Console.WriteLine("Данные успешно удалены. Чтобы продолжить нажмите Enter.");
                    Console.ReadLine();
                    return;
                }
            }
            Console.WriteLine("Сотрудника с таким ID не существует. Чтобы продолжить нажмите Enter.");
            Console.ReadLine();
        }
        private void GetPerson()
        {
            int personId = 0;
            Console.WriteLine("Введите ID сотрудника:");
            string str = Console.ReadLine();
            try
            {
                personId = Int32.Parse(str);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Можно использовать только цифры. Чтобы продолжить нажмите Enter.");
                Console.ReadLine();
                return;
            }
            foreach (Person p in persons)
            {
                if (p.ID == personId)
                {
                    Console.WriteLine(p + "Чтобы продолжить нажмите Enter.");
                    Console.ReadLine();
                    return;
                }
            }
            Console.WriteLine("Сотрудника с таким ID не существует. Чтобы продолжить нажмите Enter.");
            Console.ReadLine();
        }
        private void GetAllStaff()
        {
            foreach (Person p in persons)
            {
                Console.WriteLine(p);
            }
            Console.WriteLine("Чтобы продолжить нажмите Enter.");
            Console.ReadLine();
        }
        private void SaveExit(IOperation io)
        {
            io.Write(persons);
        }
    }
}
