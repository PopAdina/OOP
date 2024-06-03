using System;
using System.Collections.Generic;

/*Modelăm un robot de tip „GiantKillerRobot”, care are capacitatea de a distruge ținte specifice 
 cu ajutorul unui laser. Robotul trebuie să aibă următoarele caracteristici și funcționalități: 
 să aibă un laser căruia îi poate fi setată intensitatea, acesta poate avea mai multe tipuri de ținte:
 animale, oameni  și supereroi, robotul trebuie să opereze pe o planetă specifică Pământul, 
 atâta timp cât robotul este activ și există viață pe planetă, acesta va continua să își găsească și
 să distrugă țintele și robotul trebuie să fie capabil să identifice și să treacă la următoarea țintă 
 dacă cea curentă este distrusă.*/



// Program principal
public class Program
{
    public static void Main()
    {
        // Listează țintele inițiale
        var targets = new List<Target>
        {
            new Animal(),
            new Human(),
            new Superhero()
        };

        // Creează o instanță a planetei Pământ cu țintele inițiale
        Earth earth = new Earth(targets);

        // Creează și inițializează robotul
        GiantKillerRobot robot = new GiantKillerRobot();
        robot.Initialize();
        robot.EyeLaserIntensity = Intensity.Kill; // Setează intensitatea laserului la "Kill"
        robot.TargetTypes = new List<Target> { new Animal(), new Human(), new Superhero() }; // Setează tipurile de ținte

        // Execută misiunea robotului
        robot.ExecuteMission(earth);
        Console.ReadLine();
    }
}



    // Enum pentru intensitatea laserului
    public enum Intensity
{
    Stun, // Intensitate pentru a imobiliza ținta
    Kill  // Intensitate pentru a distruge ținta
}

// Clasa de bază pentru ținte
public abstract class Target
{
    public bool IsAlive { get; set; } = true; // Stare inițială a țintei, vie
    public abstract string Type { get; }      // Tipul țintei, implementat în clasele derivate
}

// Clase derivate pentru diferite tipuri de ținte
public class Animal : Target
{
    public override string Type => "Animal"; // Tipul de țintă: Animal
}

public class Human : Target
{
    public override string Type => "Human"; // Tipul de țintă: Om
}

public class Superhero : Target
{
    public override string Type => "Superhero"; // Tipul de țintă: Supererou
}

// Clasa pentru planetă
public abstract class Planet
{
    public abstract bool ContainsLife { get; } // Verifică dacă planeta conține viață
}

// Clasa pentru Pământ
public class Earth : Planet
{
    private List<Target> targets; // Lista țintelor de pe Pământ

    public Earth(List<Target> initialTargets)
    {
        targets = initialTargets;
    }

    public override bool ContainsLife => targets.Exists(t => t.IsAlive); // Verifică dacă există ținte vii

    // Metodă pentru a obține următoarea țintă vie
    public Target GetNextTarget()
    {
        return targets.Find(t => t.IsAlive);
    }
}

// Clasa pentru robot
public class GiantKillerRobot
{
    public Intensity EyeLaserIntensity { get; set; } // Intensitatea laserului
    public List<Target> TargetTypes { get; set; }    // Tipurile de ținte
    public bool Active { get; set; } = true;         // Starea robotului, activ
    private Target currentTarget;                    // Ținta curentă a robotului

    // Metodă pentru inițializarea robotului
    public void Initialize()
    {
        Console.WriteLine("Robot initialized.");
    }

    // Metodă pentru a trage cu laserul în țintă
    public void FireLaserAt(Target target)
    {
        if (EyeLaserIntensity == Intensity.Kill)
        {
            target.IsAlive = false; // Ținta este distrusă
            Console.WriteLine($"{target.Type} has been killed.");
        }
        else
        {
            Console.WriteLine($"{target.Type} has been stunned."); // Ținta este imobilizată
        }
    }

    // Metodă pentru a găsi următoarea țintă
    public void AcquireNextTarget(Earth earth)
    {
        currentTarget = earth.GetNextTarget();
        if (currentTarget == null)
        {
            Console.WriteLine("No more targets found."); // Nu mai sunt ținte disponibile
            Active = false; // Robotul nu mai este activ
        }
        else
        {
            Console.WriteLine($"New target acquired: {currentTarget.Type}"); // Țintă nouă găsită
        }
    }

    // Metodă pentru a executa misiunea robotului
    public void ExecuteMission(Earth earth)
    {
        while (Active && earth.ContainsLife)
        {
            if (currentTarget == null || !currentTarget.IsAlive)
            {
                AcquireNextTarget(earth); // Găsește următoarea țintă dacă cea curentă nu este vie
            }
            if (currentTarget != null && currentTarget.IsAlive)
            {
                FireLaserAt(currentTarget); // Trage în ținta curentă
            }
        }
        Console.WriteLine("Mission complete."); // Misiunea este completă
    }
}

// Program principal
/*public class Program
{
    public static void Main()
    {
        // Listează țintele inițiale
        var targets = new List<Target>
        {
            new Animal(),
            new Human(),
            new Superhero()
        };

        // Creează o instanță a planetei Pământ cu țintele inițiale
        Earth earth = new Earth(targets);

        // Creează și inițializează robotul
        GiantKillerRobot robot = new GiantKillerRobot();
        robot.Initialize();
        robot.EyeLaserIntensity = Intensity.Kill; // Setează intensitatea laserului la "Kill"
        robot.TargetTypes = new List<Target> { new Animal(), new Human(), new Superhero() }; // Setează tipurile de ținte

        // Execută misiunea robotului
        robot.ExecuteMission(earth);
        Console.ReadLine();
    }
    
}*/