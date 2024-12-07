using System;
using System.Collections.Generic;
using System.Drawing;

namespace EcosystemSimulation
{
    /// <summary>
    /// Represents a herbivore animal in the ecosystem.
    /// </summary>
    /// <summary>
    /// Represents a herbivore animal in the ecosystem.
    /// </summary>
    class Herbivore : Animal
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Herbivore"/> class.
        /// </summary>
        /// <param name="name">The name of the herbivore.</param>
        /// <param name="energy">The initial energy of the herbivore.</param>
        /// <param name="position">The initial position of the herbivore.</param>
        /// <param name="speed">The speed of the herbivore.</param>
        /// <param name="parent">The parent animal (optional).</param>
        public Herbivore(string name, int energy, (int x, int y) position, int speed, Animal Parent = null)
            : base(name, energy, position, speed, "Herbivore")
        {
        }

        /// <summary>
        /// Performs the herbivore's action, which includes moving, searching for plants to eat, and reducing energy.
        /// </summary>
        public override void Act()
        {
            Console.WriteLine($"{Name} moves and searches for plants to eat.");
            Move();
            Energy -= 1;
            if (Energy <= 0)
            {
                Console.WriteLine($"{Name} has died");
            }
        }

        /// <summary>
        /// Allows the herbivore to eat a plant.
        /// </summary>
        /// <param name="food">The plant to be eaten.</param>
        public override void Eat(EcosystemEntity food)
        {
            if (food is Plant plant)
            {
                Energy += plant.Energy;
                plant.Energy = 0;
                Console.WriteLine($"{Name} eats {plant.Name}.");
            }
        }
    }

    /// <summary>
    /// Represents a carnivore animal in the ecosystem.
    /// </summary>
    class Carnivore : Animal
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Carnivore"/> class.
        /// </summary>
        /// <param name="name">The name of the carnivore.</param>
        /// <param name="energy">The initial energy of the carnivore.</param>
        /// <param name="position">The initial position of the carnivore.</param>
        /// <param name="speed">The speed of the carnivore.</param>
        /// <param name="parent">The parent animal (optional).</param>
        public Carnivore(string name, int energy, (int x, int y) position, int speed, Animal Parent = null)
            : base(name, energy, position, speed, "Carnivore")
        {
        }

        /// <summary>
        /// Performs the carnivore's action, which includes moving, hunting prey, and reducing energy.
        /// </summary>
        public override void Act()
        {
            Console.WriteLine($"{Name} moves and hunts prey.");
            Move();
            Energy -= 5;
            if (Energy <= 0)
            {
                Console.WriteLine($"{Name} has died");
            }
        }

        /// <summary>
        /// Allows the carnivore to eat an animal.
        /// </summary>
        /// <param name="food">The animal to be eaten.</param>
        public override void Eat(EcosystemEntity food)
        {
            if (food is Animal prey && prey.Energy > 0)
            {
                Attack(prey);
            }
        }
    }

    /// <summary>
    /// Represents an omnivore animal in the ecosystem.
    /// </summary>
    class Omnivore : Animal
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Omnivore"/> class.
        /// </summary>
        /// <param name="name">The name of the omnivore.</param>
        /// <param name="energy">The initial energy of the omnivore.</param>
        /// <param name="position">The initial position of the omnivore.</param>
        /// <param name="speed">The speed of the omnivore.</param>
        /// <param name="parent">The parent animal (optional).</param>
        public Omnivore(string name, int energy, (int x, int y) position, int speed, Animal Parent = null)
            : base(name, energy, position, speed, "Omnivore")
        {
        }

        /// <summary>
        /// Performs the omnivore's action, which includes moving, searching for food, and reducing energy.
        /// </summary>
        public override void Act()
        {
            Console.WriteLine($"{Name} moves and searches for food.");
            Move();
            Energy -= 3;
            if (Energy <= 0)
            {
                Console.WriteLine($"{Name} has died");
            }
        }

        /// <summary>
        /// Allows the omnivore to eat either a plant or an animal.
        /// </summary>
        /// <param name="food">The entity (plant or animal) to be eaten.</param>
        public override void Eat(EcosystemEntity food)
        {
            if (food is Plant plant)
            {
                Energy += plant.Energy;
                plant.Energy = 0;
                Console.WriteLine($"{Name} eats {plant.Name}.");
            }
            else if (food is Animal prey && prey.Energy > 0)
            {
                Attack(prey);
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Ecosystem ecosystem = new Ecosystem(10);

            Plant plant1 = new Plant("Flower1", 10, (0, 0));
            Plant plant2 = new Plant("Flower2", 5, (1, 1));
            Plant plant3 = new Plant("Flower3", 10, (2, 2));
            Herbivore rabbit = new Herbivore("Rabbit", 20, (1, 1), 2);
            Herbivore rabbit2 = new Herbivore("Rabbit2", 30, (0, 2), 2);
            Herbivore rabbit3 = new Herbivore("Rabbit3", 20, (2, 1), 2);

            Carnivore wolf = new Carnivore("Wolf", 30, (2, 2), 3);
            Omnivore bear = new Omnivore("Bear", 55, (3, 3), 1);

            ecosystem.AddEntity(plant1);
            ecosystem.AddEntity(plant2);
            ecosystem.AddEntity(plant3);

            ecosystem.AddEntity(rabbit);
            ecosystem.AddEntity(rabbit2);
            ecosystem.AddEntity(rabbit3);
            ecosystem.AddEntity(wolf);
            ecosystem.AddEntity(bear);

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"\n--- Step {i + 1} ---");
                ecosystem.SimulateStep();
                ecosystem.DisplayState();
                ecosystem.TriggerRandomEvent();
            }
        }
    }
}