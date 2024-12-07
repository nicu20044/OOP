namespace EcosystemSimulation;

/// <summary>
/// Represents an entity in the ecosystem.
/// </summary>
abstract class EcosystemEntity
{
    /// <summary>
    /// The name of the entity.
    /// </summary>
    public string Name { get; set; }
    /// <summary>
    /// The energy level of the entity.
    /// </summary>
    public int Energy { get; set; }
    /// <summary>
    /// The position of the entity in the ecosystem.
    /// </summary>
    public (int x, int y) Position { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EcosystemEntity"/> class.
    /// </summary>
    /// <param name="name">The name of the entity.</param>
    /// <param name="energy">The initial energy level of the entity.</param>
    /// <param name="position">The initial position of the entity in the ecosystem.</param>
    public EcosystemEntity(string name, int energy, (int x, int y) position)
    {
        Name = name;
        Energy = energy;
        Position = position;
    }
    /// <summary>
    /// Defines the behavior of the entity in each simulation step.
    /// </summary>
    public abstract void Act();
}
