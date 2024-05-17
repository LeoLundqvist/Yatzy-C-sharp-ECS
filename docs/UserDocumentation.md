# User Documentation for Yatzy C# ECS

## Table of Contents
1. [Installation Guide](#installation-guide)
2. [User Manual](#user-manual)
3. [FAQs and Troubleshooting](#faqs-and-troubleshooting)
4. [Contact Information](#contact-information)

---

## 1. Installation Guide

### Prerequisites
Ensure you have the following installed on your system:
- .NET SDK

### Steps to Install and Run the Game
1. **Download Game Files**: Download all the necessary game files from the repository and place them in a single directory on your computer.

2. **Build and Run the Game**:
   - **Via Command Line**:
     1. Open a terminal or command prompt.
     2. Navigate to the directory containing the game files.
     3. Execute the following commands to build and run the game:
        ```sh
        dotnet build
        dotnet run
        ```

   - **Via IDE (e.g., Visual Studio)**:
     1. Open the project in your preferred C# IDE.
     2. Build and run the project using the IDE's build and run options.

---

## 2. User Manual

### Starting the Game
When the game starts, you will be prompted to begin playing. Follow the on-screen instructions to proceed.

### Game Controls
- **Input Numbers**: Type numbers (1-6) to save specific dice or make selections when prompted.
- **Enter Key**: Press Enter to confirm selections or continue when prompted.

### Objective
The goal of the game is to score the highest points by rolling dice and selecting combinations strategically.

### Game Flow
1. **Setup**:
   - At the start, the player's score is initialized.
   - Six rounds are played in total.

2. **Rounds**:
   - Each round consists of two dice throws.
   - After each throw, players can choose to save dice by entering the number corresponding to the dice (1-6).
   - If no valid input is given, the game proceeds to the next throw or point selection.

3. **Point Selection**:
   - At the end of each round, players select a scoring combination based on the dice results.
   - Points are calculated and added to the total score.

---

## 3. FAQs and Troubleshooting

### Q: The game won't start. What should I do?
A: Ensure that the .NET SDK is correctly installed and all game files are in the same directory. If using an IDE, ensure the project is loaded correctly.

### Q: How can I restart the game after losing?
A: The game automatically restarts at the beginning after six rounds. To play again, simply run the game again following the installation guide steps.

### Q: Is it possible to pause the game?
A: Currently, the game does not support a pause feature. The game flow continues based on user inputs.

---

## 4. Contact Information

For additional assistance, queries, or feedback, please reach out via GitHub. Visit LeoLundqvist on GitHub to contact me or report issues. We encourage users to open an issue for technical problems or feature suggestions related to the game.

