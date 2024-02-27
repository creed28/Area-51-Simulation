# Area 51 Simulation

This is a C# program that uses multithreading to simulate the movement of agents within a secured facility represented by an elevator system. 
It consists of several classes designed to mimic the operation of an elevator and the behavior of agents with different security
clearance levels.

# Classes

## Elevator

- Simulates the functionality of an elevator within the facility.
- Capable of moving between different floors: Ground (G), Sublevel (S), and two Top Levels (T1, T2).
- Can be called to a specific floor and can move to a random floor.

## Agent

- Represents an agent within the facility, characterized by a security clearance level.
- Security clearance levels include Confidential, Secret, and Top-Secret.

## Simulation

- Responsible for running the simulation.
- Controls the flow of agents entering and leaving the facility, interacting with the elevator system.
- Handles user input to stop the simulation.

# Simulation Process

- Agents with random security clearance levels enter the facility.
- Agents request elevator access to move between floors based on their security clearance.
- Elevator responds to requests and transports agents between floors.
- Agents perform tasks on different floors based on their security clearance.
- Agents leave the facility.

# To run the simulation:

- Compile and execute the Area51_Simulation project.
- The simulation will start automatically.


# Notes

- The simulation is designed to provide a simplified representation of agent movement within a secured facility.
- The behavior of the elevator and agents is determined by predefined rules and random events.
