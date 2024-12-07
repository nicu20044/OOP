namespace EcosystemSimulation;

class Plant : EcosystemEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Plant"/> class.
    /// </summary>
    /// <param name="name">The name of the plant.</param>
    /// <param name="energy">The initial energy of the plant.</param>
    /// <param name="position">The initial position of the plant as a tuple (x, y).</param>
    public Plant(string name, int energy, (int x, int y) position)
        : base(name, energy, position)
    {
    }

    /// <summary>
    /// Performs an action for the plant.
    /// Decreases the energy by 1 and prints a message indicating the plant's growth.
    /// </summary>
    public override void Act()
    {
        Energy -= 1;
        Console.WriteLine($"{Name} grows at position {Position}.");
    }

    /// <summary>
    /// Simulates the reproduction of the plant.
    /// Prints a message indicating the plant's reproduction.
    /// </summary>
    public void Reproduce()
    {
        Console.WriteLine($"{Name} is reproducing.");
    }
}