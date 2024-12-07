# Ecosystem Simulation Project

This project simulates an ecosystem where animals and plants interact. It includes a variety of entities such as herbivores, carnivores, omnivores, and plants, with interactions such as eating, moving, and reproduction. The ecosystem can be modified through random events like storms, droughts, and predator spawns.

## Table of Contents

- [Overview](#overview)
- [Classes](#classes)
  - [Ecosystem](#ecosystem)
  - [EcosystemEntity](#ecosystementity)
  - [Animal](#animal)
  - [Herbivore](#herbivore)
  - [Carnivore](#carnivore)
  - [Omnivore](#omnivore)
  - [Plant](#plant)
- [Interfaces](#interfaces)
  - [IInteraction](#iinteraction)
- [Usage](#usage)
- [Random Events](#random-events)
- [Simulating Steps](#simulating-steps)
- [Display Ecosystem State](#display-ecosystem-state)

## Overview

The **Ecosystem Simulation** project is a representation of an ecosystem with different types of entities, each performing different actions based on their characteristics. The simulation supports interactions between herbivores, carnivores, omnivores, and plants, where animals can eat plants or other animals, and plants grow or reproduce. Additionally, random events like storms, droughts, and the appearance of predators can affect the ecosystem.

## Classes

### Ecosystem

The `Ecosystem` class manages the entire ecosystem and contains methods for adding/removing entities, simulating a step, and handling random events.

- **AddEntity(EcosystemEntity entity):** Adds an entity to the ecosystem.
- **RemoveEntity(EcosystemEntity entity):** Removes an entity from the ecosystem.
- **SimulateStep():** Simulates a step in the ecosystem, updating entities, handling interactions, and generating offspring.
- **TriggerRandomEvent():** Triggers a random event such as a storm, drought, or predator spawn.
- **DisplayState():** Displays the current state of the ecosystem.

### EcosystemEntity

`EcosystemEntity` is an abstract class representing any entity in the ecosystem. It contains properties such as `Name`, `Energy`, and `Position` and an abstract method `Act()` for performing actions.

### Animal

`Animal` is an abstract class derived from `EcosystemEntity`. It represents animals in the ecosystem. The class includes:

- **Speed:** Defines the speed of the animal.
- **FoodType:** Specifies the type of food the animal eats.
- **Parent:** A reference to the animal's parent (optional).
- **Offspring:** A list of the animal's offspring.

It includes the following methods:

- **Eat(EcosystemEntity food):** Allows the animal to eat another entity.
- **Move():** Moves the animal within the ecosystem.
- **Attack(Animal prey):** Attacks another animal, gaining energy.
- **Reproduce():** Allows the animal to reproduce and create offspring.

#### Herbivore

The `Herbivore` class represents a herbivorous animal that eats plants. It includes behavior specific to herbivores such as searching for plants and reducing energy.

#### Carnivore

The `Carnivore` class represents a carnivorous animal that hunts and eats other animals. It includes hunting behavior and managing energy by attacking prey.

#### Omnivore

The `Omnivore` class represents an omnivorous animal that eats both plants and other animals.

### Plant

The `Plant` class represents plants in the ecosystem. It includes methods for growing and reproducing.

## Interfaces

### IInteraction

The `IInteraction` interface defines methods that animals must implement for interacting with other animals, such as attacking and reproducing.

## Usage

1. **Create an Ecosystem:** Initialize the ecosystem with a map size that determines the boundaries of the ecosystem.
   
   ```csharp
   var ecosystem = new Ecosystem(10);
