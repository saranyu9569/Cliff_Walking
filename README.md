# Q-Learning Reinforcement Learning Implementation in C#

## Overview
This project implements a Q-Learning algorithm from scratch in C#, demonstrating a reinforcement learning approach to navigation in a grid-based environment.

## Features
- Custom Q-Learning implementation
- Grid-based environment with obstacles and a goal
- Configurable learning parameters (alpha, gamma, epsilon)
- Wind effect simulation during agent movement
- Exploration vs. exploitation strategy
- Best path visualization

## Environment
- 6x6 grid environment
- Starting point at (0,5)
- Goal point at (5,5)
- Obstacles marked with 'C'
- Open spaces marked with '*'

## Learning Parameters
- Learning rate (alpha): 0.1
- Discount factor (gamma): 0.6
- Exploration rate (epsilon): 0.1

## Actions
The agent can take 4 actions:
- Up
- Right
- Down
- Left

### Wind Effect
There's a 20% chance the agent will move right during an action, adding complexity to navigation.

## How to Run
1. Clone the repository
2. Open in Visual Studio or your preferred C# IDE
3. Run the program

## Key Components
- `QLearning.cs`: Core Q-Learning algorithm implementation
- `Environment.cs`: Grid environment setup
- `Program.cs`: Main execution script

## Training
The default training runs for 100 episodes to learn optimal navigation.

## Output
The program displays:
- Best path through the environment
- Final Q-Table with learned action values

## Dependencies
- .NET Core or .NET Framework

## License
MIT License
