namespace EcosystemSimulation;

class Ecosystem
{
    private List<EcosystemEntity> entities = new List<EcosystemEntity>();
    private int mapSize;

    /// <summary>
    /// Initializes a new instance of the Ecosystem class with the specified map size.
    /// </summary>
    /// <param name="size">The size of the map for the ecosystem. This value represents the maximum range for entity positions.</param>
    public Ecosystem(int size)
    {
        mapSize = size;
    }
    /// <summary>
    /// Adds the specified entity to the ecosystem.
    /// </summary>
    /// <param name="entity">The entity to be added to the ecosystem.</param>
    public void AddEntity(EcosystemEntity entity)
    {
        entities.Add(entity);
        Console.WriteLine($"Entity {entity.Name} added to the ecosystem.");
    }

    /// <summary>
    /// Removes the specified entity from the ecosystem.
    /// </summary>
    /// <param name="entity">The entity to be removed from the ecosystem.</param>
    public void RemoveEntity(EcosystemEntity entity)
    {
        entities.Remove(entity);
    }

    /// <summary>
    /// Creates a new offspring animal entity based on the given parent animal.
    /// The offspring animal will have half the energy of the parent animal, and the parent animal's energy will also be halved.
    /// The offspring animal's speed will be increased by 1 compared to the parent animal.
    /// </summary>
    /// <param name="parent">The parent animal entity from which the offspring will be created.</param>
    /// <returns>A new animal entity representing the offspring of the given parent animal.</returns>
    private EcosystemEntity CreateOffspringAnimal(Animal parent)
    {
        var offspringName = parent.Name + "_Junior";
        var offspringPosition = parent.Position;
        var offspringEnergy = parent.Energy / 2;
        var offspringSpeed = parent.Speed + 1;
        parent.Energy /= 2;

        if (parent is Herbivore)
            return new Herbivore(offspringName, offspringEnergy, offspringPosition, offspringSpeed, parent);
        if (parent is Carnivore)
            return new Carnivore(offspringName, offspringEnergy, offspringPosition, offspringSpeed, parent);
        if (parent is Omnivore)
            return new Omnivore(offspringName, offspringEnergy, offspringPosition, offspringSpeed, parent);

        throw new Exception("Unknown animal type for reproduction.");
    }

    /// <summary>
    /// Creates a new offspring plant entity based on the given parent plant.
    /// The offspring plant will have half the energy of the parent plant, and the parent plant's energy will also be halved.
    /// </summary>
    /// <param name="parent">The parent plant entity from which the offspring will be created.</param>
    /// <returns>A new plant entity representing the offspring of the given parent plant.</returns>
    private EcosystemEntity CreateOffspringPlant(Plant parent)
    {
        var offspringName = parent.Name + "_Junior";
        var offspringPosition = parent.Position;
        var offspringEnergy = parent.Energy / 2;
        parent.Energy /= 2;
        return new Plant(offspringName, offspringEnergy, offspringPosition);
    }


    /// <summary>
    /// Simulates a step in the ecosystem.
    /// </summary>
    public void SimulateStep()
    {
        Console.WriteLine("Simulating a step in the ecosystem:");
        List<EcosystemEntity> newEntities = new List<EcosystemEntity>();

        // Iterate over a copy of the entities list to avoid modifying the collection during iteration.
        foreach (var entity in entities.ToList())
        {
            // Check if the entity's energy is below or equal to zero.
            if (entity.Energy <= 0)
            {
                Console.WriteLine($"{entity.Name} has died.");
                RemoveEntity(entity);
                continue;
            }

            // Let the entity act.
            entity.Act();

            // Check if the entity is an animal.
            if (entity is Animal animal)
            {
                // Iterate over all other entities in the ecosystem.
                foreach (var other in entities)
                {
                    // Check if the other entity is not the same as the current animal and they are in the same position.
                    if (animal != other && animal.Position == other.Position)
                    {
                        // Check the type of the animal and the type of the other entity to determine the interaction.
                        if (animal is Herbivore && other is Plant plant && plant.Energy > 0)
                        {
                            animal.Eat(plant);
                        }
                        else if (animal is Carnivore && other is Animal prey && prey.Energy > 0)
                        {
                            animal.Eat(prey);
                        }
                        else if (animal is Omnivore omnivore)
                        {
                            if (other is Plant plantOmnivore && plantOmnivore.Energy > 0)
                            {
                                omnivore.Eat(plantOmnivore);
                            }
                            else if (other is Herbivore herbivore && herbivore.Energy > 0)
                            {
                                omnivore.Eat(herbivore);
                            }
                            else
                            {
                                Console.WriteLine($"{omnivore.Name} cannot eat {other.Name}.");
                            }
                        }
                    }
                }

                // Check if the animal's energy is above 50.
                if (animal.Energy > 50)
                {
                    animal.Reproduce();
                    var offspring = CreateOffspringAnimal(animal);
                    newEntities.Add(offspring);
                }
            }

            // Check if the entity is a plant.
            if (entity is Plant plantToReproduce)
            {
                // Check if the plant's energy is above 50.
                if (plantToReproduce.Energy > 50)
                {
                    plantToReproduce.Reproduce();
                    var offspring = CreateOffspringPlant(plantToReproduce);
                    newEntities.Add(offspring);
                }
            }
        }

        // Remove dead animals from the entities list.
        entities.RemoveAll(e => e is Animal a && a.Energy <= 0);
        // Add new entities to the entities list.
        entities.AddRange(newEntities);
    }

    /// <summary>
    /// Displays the current state of the ecosystem, including the names, positions, and energies of all entities.
    /// </summary>
    public void DisplayState()
    {
        Console.WriteLine("Ecosystem state:");
        foreach (var entity in entities)
        {
            Console.WriteLine($"{entity.Name} at position {entity.Position}, energy: {entity.Energy}.");
        }
    }

    /// <summary>
    /// Triggers a random event in the ecosystem.
    /// The event can be a storm, drought, or a predator spawning.
    /// </summary>
    public void TriggerRandomEvent()
    {
        Random random = new Random();

        if (random.Next(1, 100) <= 20)
        {
            Console.WriteLine("A storm is approaching!");
            foreach (var animal in entities.OfType<Animal>())
            {
                animal.Energy -= animal.Energy / 2;
            }
        }

        if (random.Next(1, 100) <= 10)
        {
            Console.WriteLine("A drought is affecting the ecosystem!");
            foreach (var plant in entities.OfType<Plant>())
            {
                plant.Energy -= plant.Energy / 2;
            }
        }

        if (random.Next(1, 100) <= 15)
        {
            Console.WriteLine("A predator is spawning in the ecosystem!");
            int x = random.Next(0, mapSize);
            int y = random.Next(0, mapSize);
            int energy = 30;
            int speed = 3;

            var predator = new Carnivore("Predator", energy, (x, y), speed);

            AddEntity(predator);
        }
    }
}