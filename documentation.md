# Project Description
### 1. Overview
This documentation provides an in-depth guide to developing a Yatzy game using the ECS (Entity-Component-System) architecture in C#. It serves as a comprehensive resource for understanding and contributing to the project.

### 2. Technical Specifications and Design
- **Entities:** Comprises of Player, each being an instance of Entity.
- **Components:** Includes structures like InputComponent, SaveDiceComponent, DiceComponent, and ScoreComponent, holding respective states.
- **Systems:** Encompass functionalities such as DiceSystem for rolling dice and calculating scores, and GameSystem for managing game flow and user input.


### 4. Development Guidelines and Standards
- **ECS Adherence:** Adhere to the ECS architecture for integrating new features or making modifications.
- **Modularity:** Ensure modularity in development for streamlined updates and addition of new features.
- **Code Standard:** Follow C# coding standards to enhance code readability and maintainability.

### 5.Testing Framework
- **Unit Testing with NUnit:** Focuses on testing individual components and systems using NUnit, located in the tests/ directory.
- **Integration Testing Plans:** Future plans may include integration testing to assess the overall functionality of the game.
- **Unit Testing with NUnit:** End-User Functional Testing: Includes tests for user inputs, game logic, and overall game experience.

### 6. Version Information
- **Game Version:** 1.0.0
- **C# Version:** 9.0 or higher

