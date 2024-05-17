using System;
using System.Collections.Generic;

namespace Systems
{
    // System responsible for dice-related operations
    public class DiceSystem
    {
        // Throws the dice for the specified entity
        public void ThrowDice(Entity entity)
        {
            var diceComponent = entity.GetComponent<DiceComponent>();
            var savedDiceComponent = entity.GetComponent<SaveDiceComponent>();

            // Check if the entity has both dice and saved dice components
            if (diceComponent != null && savedDiceComponent != null)
            {
                Random rnd = new Random();
                for (int i = 0; i < diceComponent.DiceValue.Length; i++)
                {
                    // Check if the dice is not saved, then roll it
                    if (!savedDiceComponent.SaveDice[i])
                    {
                        diceComponent.DiceValue[i] = rnd.Next(6) + 1;
                    }
                    // Reset the saved status of all dice
                    savedDiceComponent.SaveDice[i] = false;
                }
            }
        }

        // Saves or unsaves a dice based on user input
        public void SaveDice(Entity entity, int input)
        {
            var saveDiceComponent = entity.GetComponent<SaveDiceComponent>();

            // Check if the entity has a save dice component
            if (saveDiceComponent != null)
            {
                // Toggle the saved status of the dice based on input
                switch (input)
                {
                    case 1:
                        saveDiceComponent.SaveDice[0] = !saveDiceComponent.SaveDice[0];
                        break;
                    case 2:
                        saveDiceComponent.SaveDice[1] = !saveDiceComponent.SaveDice[1];
                        break;
                    case 3:
                        saveDiceComponent.SaveDice[2] = !saveDiceComponent.SaveDice[2];
                        break;
                    case 4:
                        saveDiceComponent.SaveDice[3] = !saveDiceComponent.SaveDice[3];
                        break;
                    case 5:
                        saveDiceComponent.SaveDice[4] = !saveDiceComponent.SaveDice[4];
                        break;
                    case 6:
                        saveDiceComponent.SaveDice[5] = !saveDiceComponent.SaveDice[5];
                        break;
                }
            }
        }

        // Writes the current state of dice to the console
        public void WriteDice(Entity entity)
        {
            var diceComponent = entity.GetComponent<DiceComponent>();
            var savedDiceComponent = entity.GetComponent<SaveDiceComponent>();

            // Check if the entity has both dice and saved dice components
            if (diceComponent != null && savedDiceComponent != null)
            {
                for (int i = 0; i < diceComponent.DiceValue.Length; i++)
                {
                    // Display whether the dice is saved or not
                    if (savedDiceComponent.SaveDice[i])
                    {
                        Console.WriteLine($"Dice {i + 1}: {diceComponent.DiceValue[i]} (Saved)");
                    }
                    else
                    {
                        Console.WriteLine($"Dice {i + 1}: {diceComponent.DiceValue[i]}");
                    }
                }
            }
        }

        // Allows the player to choose a score for the current dice combination
        public void ChoosePoint(Entity playerEntity, Entity inputEntity, GameSystem gameSystem, ScoreSystem scoreSystem)
        {
            var scoreComponent = playerEntity.GetComponent<ScoreComponent>();
            var inputComponent = inputEntity.GetComponent<InputComponent>();
            bool notDecided = true;

            // Continue choosing points until a decision is made
            while (notDecided)
            {
                Console.WriteLine("Your dice result");
                WriteDice(playerEntity);
                Console.WriteLine("What combination score do you choose?");

                // Display available combinations that haven't been taken yet
                for (int i = 0; i < 6; i++)
                {
                    if (scoreComponent.ScoreNotTaken[i])
                    {
                        Console.WriteLine($"{i + 1}. All {i + 1}s");
                    }
                }

                // Get input from the player
                gameSystem.Input(inputEntity);

                // Validate input and update score accordingly
                if (inputComponent.Input >= 1 && inputComponent.Input <= 6 && scoreComponent.ScoreNotTaken[inputComponent.Input - 1])
                {
                    scoreComponent.ScoreValue += scoreSystem.Combination1_6(playerEntity, inputComponent.Input);
                    Console.WriteLine($"You got {scoreSystem.Combination1_6(playerEntity, inputComponent.Input)} points");
                    Console.WriteLine($"Your total score is: {scoreComponent.ScoreValue}");
                    Console.Write("Press enter to continue... ");
                    Console.ReadLine();
                    scoreComponent.ScoreNotTaken[inputComponent.Input - 1] = false;
                    notDecided = false;
                }

                Console.Clear();
            }
        }
    }

    // System responsible for scoring related operations
    public class ScoreSystem
    {
        // Calculates the score for combination 1-6 based on the number provided
        public int Combination1_6(Entity entity, int number)
        {
            int amount = CountAmount(entity, number);
            int points = amount * number;
            return points;
        }

        // Calculates the score for combination 7 based on the number provided
        public int Combination7(Entity entity, int number)
        {
            int amount = CountAmount(entity, number);
            int points = amount * number;
            return points;
        }

        // Counts the number of occurrences of a specific number in the dice
        public int CountAmount(Entity entity, int number)
        {
            int amountOfNumbers = 0;
            var diceComponent = entity.GetComponent<DiceComponent>();

            // Count occurrences of the specified number in the dice
            if (diceComponent != null)
            {
                for (int i = 0; i < diceComponent.DiceValue.Length; i++)
                {
                    if (diceComponent.DiceValue[i] == number)
                    {
                        amountOfNumbers++;
                    }
                }
            }
            return amountOfNumbers;
        }
    }

    // System responsible for game-related operations
    public class GameSystem
    {
        // Sets up the saved dice component for an entity
        public void SetupSaveDice(Entity entity)
        {
            var saveDiceComponent = entity.GetComponent<SaveDiceComponent>();
            if (saveDiceComponent != null)
            {
                // Initialize saved dice status for each dice
                for (int i = 0; i < saveDiceComponent.SaveDice.Length; i++)
                {
                    saveDiceComponent.SaveDice[i] = false;
                }
            }
        }

        // Sets up the score component for an entity
        public void SetupScore(Entity entity)
        {
            var scoreComponent = entity.GetComponent<ScoreComponent>();
            if (scoreComponent != null)
            {
                // Initialize score value and mark all score options as not taken
                scoreComponent.ScoreValue = 0;
                for (int i = 0; i < scoreComponent.ScoreNotTaken.Length; i++)
                {
                    scoreComponent.ScoreNotTaken[i] = true;
                }
            }
        }

        // Takes input from the player
        public void Input(Entity entity)
        {
            var inputComponent = entity.GetComponent<InputComponent>();

            if (inputComponent != null)
            {
                // Read input from the console
                string inputString = Console.ReadLine();

                // Parse input to integer
                if (Int32.TryParse(inputString, out int input))
                {
                    inputComponent.Input = input;
                }
                else
                {
                    // Set input to 0 if parsing fails
                    inputComponent.Input = 0;
                }
            }
        }
    }
}