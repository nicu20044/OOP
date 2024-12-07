namespace EcosystemSimulation;

/// <summary>
/// Represents an animal in the ecosystem.
/// </summary>
abstract class Animal : EcosystemEntity, IInteraction
{
    /// <summary>
    /// The animal's speed.
    /// </summary>
    public int Speed { get; set; }

    /// <summary>
    /// The type of food the animal eats.
    /// </summary>
    public string FoodType { get; set; }

    /// <summary>
    /// The parent of the animal.
    /// </summary>
    public Animal Parent { get; private set; }

    /// <summary>
    /// The offspring of the animal.
    /// </summary>
    public List<Animal> Offspring { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Animal"/> class.
    /// </summary>
    /// <param name="name">The name of the animal.</param>
    /// <param name="energy">The energy of the animal.</param>
    /// <param name="position">The position of the animal in the ecosystem.</param>
    /// <param name="speed">The speed of the animal.</param>
    /// <param name="foodType">The type of food the animal eats.</param>
    /// <param name="parent">The parent of the animal. Default is null.</param>
    public Animal(string name, int energy, (int x, int y) position, int speed, string foodType, Animal parent = null)
        : base(name, energy, position)
    {
        Speed = speed;
        FoodType = foodType;
        Parent = parent;
        Offspring = new List<Animal>();
        Parent?.Offspring.Add(this);
    }

    /// <summary>
    /// Allows the animal to eat a given ecosystem entity.
    /// </summary>
    /// <param name="food">The food to be eaten.</param>
    public abstract void Eat(EcosystemEntity food);
    
    
    /// <summary>
    /// Allows the animal to move within the ecosystem's boundaries.
    /// The animal's position is updated based on its speed and a random direction.
    /// </summary>
    public void Move()
    {
        if (Position.x < 0 || Position.x >= 10 || Position.y < 0 || Position.y >= 10)
            return;
        var randomDirection = new Random();

        Position = (Position.x + (randomDirection.Next(0, 2) == 0 ? Speed : 0), 
                     Position.y + (randomDirection.Next(0, 2) == 1 ? Speed : 0));

        Console.WriteLine($"{Name} moved to position {Position}.");
    }

    /// <summary>
    /// Allows the animal to attack another animal.
    /// The animal cannot attack its parent or offspring.
    /// If the attack is successful, the energy of the attacked animal is halved and set to 0.
    /// </summary>
    /// <param name="prey">The animal to be attacked.</param>
    public void Attack(Animal prey)
    {
        if (prey == Parent)
        {
            Console.WriteLine($"{Name} cannot attack its parent, {prey.Name}.");
            return;
        }
        if (Offspring.Contains(prey))
        {
            Console.WriteLine($"{Name} cannot attack its offspring, {prey.Name}.");
            return;
        }
    
        Console.WriteLine($"{Name} attacks {prey.Name}!");
        Energy += prey.Energy / 2;
        prey.Energy = 0;
    }
    /// <summary>
    /// Allows the animal to reproduce.
    /// </summary>
    public void Reproduce()
    {
        Console.WriteLine($"{Name} is reproducing.");
    }
}


