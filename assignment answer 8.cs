using System;

namespace Assignment03OOP
{
    // =========================================================================
    // PART 1: STATIC BINDING (EARLY BINDING) - METHOD HIDING WITH NEW
    // =========================================================================

    // Q1: Shape Class
    public class Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public Shape(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double Area()
        {
            return Width * Height;
        }

        public override string ToString()
        {
            return $"(Width = {Width}, Height = {Height})";
        }
    }

    // Q2: Cube Class inheriting from Shape
    public class Cube : Shape
    {
        public double Depth { get; set; }

        public Cube(double width, double height, double depth) : base(width, height)
        {
            Depth = depth;
        }

        // Method Hiding using new keyword
        public new double Area()
        {
            return base.Area() * Depth;
        }

        public void Print()
        {
            Console.WriteLine($"Width: {Width}, Height: {Height}, Depth: {Depth}");
        }
    }


    // =========================================================================
    // PART 2: DYNAMIC BINDING (LATE BINDING) - VIRTUAL / OVERRIDE
    // =========================================================================

    // Q5: Person Class
    public class Person
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public void Greet()
        {
            Console.WriteLine("I am a Person.");
        }

        public virtual void Display()
        {
            Console.WriteLine($"ID: {ID}, Name: {Name}, Age: {Age}");
        }
    }

    // Q6: Doctor Class
    public class Doctor : Person
    {
        public string Specialty { get; set; }

        public new void Greet()
        {
            Console.WriteLine("I am a Doctor.");
        }

        public override void Display()
        {
            Console.WriteLine($"Doctor ID: {ID}, Name: {Name}, Age: {Age}, Specialty: {Specialty}");
        }
    }

    // Q6: Engineer Class
    public class Engineer : Person
    {
        public string Field { get; set; }
        public int YearsOfExperience { get; set; }

        public new void Greet()
        {
            Console.WriteLine("I am an Engineer.");
        }

        public override void Display()
        {
            Console.WriteLine($"Engineer ID: {ID}, Name: {Name}, Age: {Age}, Field: {Field}, Experience: {YearsOfExperience} years");
        }
    }


    // =========================================================================
    // PART 3: INTERFACES
    // =========================================================================

    // Q9: Pre-interface class demonstration
    public class CarBase
    {
        public void MoveForward() => Console.WriteLine("Car is moving forward.");
        public void MoveBackward() => Console.WriteLine("Car is moving backward.");
    }

    // Q10: Interfaces Definition
    public interface IMoveable
    {
        void MoveForward();
        void MoveBackward();
    }

    public interface IFlyable
    {
        void MoveUp();
        void MoveDown();
    }

    // Q11: Implementing Interfaces in Classes
    public class Car : IMoveable
    {
        public void MoveForward() => Console.WriteLine("Car is driving forward on road.");
        public void MoveBackward() => Console.WriteLine("Car is reversing on road.");
    }

    // Q14: Explicit Interface Implementation in Ship
    public class Ship : IMoveable
    {
        void IMoveable.MoveForward()
        {
            Console.WriteLine("Ship is sailing forward on sea.");
        }

        public void MoveBackward()
        {
            Console.WriteLine("Ship is backing up on sea.");
        }
    }

    public class Airplane : IMoveable, IFlyable
    {
        public void MoveForward() => Console.WriteLine("Airplane is rolling forward on runway.");
        public void MoveBackward() => Console.WriteLine("Airplane is moving backward on runway.");
        public void MoveUp() => Console.WriteLine("Airplane is flying up into the air.");
        public void MoveDown() => Console.WriteLine("Airplane is descending down to land.");
    }

    // Q13: Interface Inheritance
    public interface IVehicle : IMoveable, IFlyable
    {
        // Inherits members from both IMoveable and IFlyable without adding new members
    }

    public class Vehicle : IVehicle
    {
        public virtual void MoveForward() => Console.WriteLine("Vehicle moving forward.");
        public void MoveBackward() => Console.WriteLine("Vehicle moving backward.");
        public void MoveUp() => Console.WriteLine("Vehicle moving up.");
        public void MoveDown() => Console.WriteLine("Vehicle moving down.");
    }


    // =========================================================================
    // MAIN PROGRAM (DEMONSTRATION & EXECUTION)
    // =========================================================================

    internal class Program
    {
        static void Main(string[] args)
        {
            // =========================================================================
            // PART 1 DEMO
            // =========================================================================
            Console.WriteLine("=========================================================================");
            Console.WriteLine("                         PART 1: STATIC BINDING                          ");
            Console.WriteLine("=========================================================================");

            Shape shape = new Shape(2, 3);
            Console.WriteLine("shape.Area(): " + shape.Area()); // Output: 6

            Cube cube = new Cube(2, 3, 4);
            Console.WriteLine("cube.Area(): " + cube.Area());   // Output: 24

            Shape shapeRef = new Cube(2, 3, 4);
            Console.WriteLine("shapeRef.Area(): " + shapeRef.Area()); // Output: 6
            Console.WriteLine("Explanation: Uses Shape's Area because 'new' uses Static/Early Binding (Compiler looks at Reference Type: Shape).");

            Console.WriteLine("\n--- Q4: Object Reference & ToString ---");
            object obj = new Cube(1, 2, 3);
            Console.WriteLine("obj.ToString(): " + obj.ToString());
            Console.WriteLine("Explanation: ToString() is virtual in System.Object and overridden in Shape, so it uses Late Binding and runs Shape's ToString().");

            Console.WriteLine("\n-------------------------------------------------------------------------\n");

            // =========================================================================
            // PART 2 DEMO
            // =========================================================================
            Console.WriteLine("=========================================================================");
            Console.WriteLine("                         PART 2: DYNAMIC BINDING                         ");
            Console.WriteLine("=========================================================================");

            Person docPerson = new Doctor { ID = 1, Name = "Dr. Ahmed", Age = 35, Specialty = "Cardiology" };
            Person engPerson = new Engineer { ID = 2, Name = "Eng. Saeed", Age = 28, Field = "Software", YearsOfExperience = 5 };

            Console.WriteLine("--- ProcessPerson(Doctor) ---");
            ProcessPerson(docPerson);

            Console.WriteLine("\n--- ProcessPerson(Engineer) ---");
            ProcessPerson(engPerson);

            Console.WriteLine("\n-------------------------------------------------------------------------\n");

            // =========================================================================
            // PART 3 DEMO
            // =========================================================================
            Console.WriteLine("=========================================================================");
            Console.WriteLine("                           PART 3: INTERFACES                            ");
            Console.WriteLine("=========================================================================");

            Console.WriteLine("--- Q12: Concrete Types vs Interface References ---");
            Car myCar = new Car();
            myCar.MoveForward();

            Airplane myPlane = new Airplane();
            myPlane.MoveForward();
            myPlane.MoveUp();

            IMoveable carRef = new Car();
            IMoveable planeRef = new Airplane();
            carRef.MoveForward();
            planeRef.MoveForward();

            Console.WriteLine("\nCan you call MoveUp() on planeRef?");
            Console.WriteLine("No! Because planeRef type is IMoveable which only contains MoveForward and MoveBackward.");
            Console.WriteLine("To call MoveUp(), you need IFlyable or Airplane reference type: ((IFlyable)planeRef).MoveUp();");

            Console.WriteLine("\n--- Q14: Explicit Interface Implementation Test ---");
            Ship myShip = new Ship();
            // myShip.MoveForward(); // Compiler Error! Cannot call directly on object reference.
            Console.WriteLine("Calling explicit interface method via interface reference:");
            IMoveable shipRef = myShip;
            shipRef.MoveForward();

            Console.WriteLine("\n=========================================================================");
            Console.WriteLine("End of Assignment 03 OOP Solution");
            Console.WriteLine("=========================================================================");

            Console.ReadLine();
        }

        // Q7 Helper Method
        static void ProcessPerson(Person person)
        {
            person.Greet();   // Call resolved at compile time (Static Binding) -> Calls Person.Greet()
            person.Display(); // Call resolved at runtime (Dynamic Binding) -> Calls derived Display()
        }
    }
}


/*
=================================================================================
                      COMPREHENSION & THEORY ANSWERS
=================================================================================

Q3 Explanation:
- shapeRef.Area() prints 6 (the base Shape area).
- Why? Because Cube uses the 'new' keyword to hide Area(). Method hiding uses early/static binding.
  The compiler checks the reference type (Shape), not the object type (Cube), and binds to Shape.Area() at compile time.

Q4 Explanation:
- obj.ToString() runs Shape's ToString().
- Why? ToString() is declared as 'virtual' in System.Object and 'override' in Shape.
  Virtual methods use late/dynamic binding where the method call is resolved at runtime based on the actual object in memory.

Q8 Explanation:
- If we remove 'virtual' from Display() in Person, the compiler shows a warning in Doctor and Engineer:
  "Doctor.Display() hides inherited member Person.Display(). Use the new keyword if hiding was intended."
- It means C# assumes you want to hide the method instead of overriding it.

Q9 Pre-interface problem:
- Forcing every vehicle class to inherit a base class with all methods causes problems because not all vehicles can fly (e.g. Car cannot MoveUp).
- Interfaces solve this by separating capabilities (IMoveable, IFlyable) so classes only implement what they can actually do.

Q13 Benefit of Interface Inheritance:
- It combines multiple interface contracts into one (e.g. IVehicle combines IMoveable and IFlyable).
- Any class implementing IVehicle is guaranteed to support all movement behaviors without duplicating code declarations.

Q14 Explicit Interface Implementation:
- When a method is explicitly implemented (void IMoveable.MoveForward()), it is hidden from the public interface of the class.
- Calling myShip.MoveForward() directly gives a compiler error.
- You must cast the object to the interface first: ((IMoveable)myShip).MoveForward();


Q15 Comparison Table:
----------------------------------------------------------------------------------
| Feature                         | Static Binding (new) | Dynamic Binding (override) |
----------------------------------------------------------------------------------
| Keyword in base                 | None (or any method) | virtual / abstract / override |
| Keyword in derived              | new                  | override                  |
| Resolved at                     | Compile time         | Runtime                   |
| Behavior via base reference     | Runs Base Version    | Runs Derived Version      |
----------------------------------------------------------------------------------


Q16 Explanation:
- 'override' requires 'virtual' because C# needs an explicit agreement from the base class allowing derived classes to change its runtime behavior (Polymorphism).
- 'new' works on any method because it doesn't change the base method behavior; it simply creates a brand new independent method in the derived class with the same name.
=================================================================================
*/