# stride-arch-ecs
An example in Stride using Arch ECS.

# Features
## Ease of use
Systems can be added through the GameSettings class in the Stride editor for easy management.
![image](https://github.com/Doprez/stride-arch-ecs/assets/73259914/4840313e-42b5-499e-9ba2-06cb18d77953)

all that you need to do is inherit from the SystemBase class and add it to the list.

## Easy access
When inheriting from SystemBase and registered in settings each System has access to both the Arch World and Strides Service registry.

# Future goals
- Allow EntityComponents to create Arch Entities with Arch Components
- Multithreading
- create a basic game/demo example
