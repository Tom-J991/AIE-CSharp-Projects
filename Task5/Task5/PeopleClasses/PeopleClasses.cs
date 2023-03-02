using Greetings;
using System;

namespace Greetings
{
    public class Person
    {
        public string name;
        public string email;
        public long phone;

        public virtual void SayGreeting()
        {
            Console.WriteLine("Hello, I'm " + this.name);
        }
    }

    public class Doctor : Person
    {
        public int salary;

        public override void SayGreeting()
        {
            Console.WriteLine("Hello, I'm Dr. " + this.name);
        }
    }
}

namespace PeopleClasses
{
    internal class PeopleClasses
    {
        // Create a Person class, and a subclass called Doctor.
        static void Main()
        {
            Console.WriteLine("- Create a Person class, and a subclass called Doctor -");

            Person guy;
            Doctor doc;

            guy = new Person();
            guy.name = "Peter Jackson";
            guy.email = "Peter.J@email.com";
            guy.phone = 0428217201;

            doc = new Doctor();
            doc.name = "John Baker";
            doc.email = "john.b63@email.com";
            doc.phone = 0499271856;
            doc.salary = 185250;

            guy.SayGreeting();
            doc.SayGreeting();

            Console.ReadKey();
        }
    }
}
