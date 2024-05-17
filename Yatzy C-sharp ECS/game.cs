using System;
using System.Collections.Generic;

using Entities;
using Components;
using Systems;

namespace Yatzy_C_sharp_ECS
{
    class Program
    {
        static void Main(string[] args)
        {
            // Instantiate game, dice, and score systems
            var gameSystem = new GameSystem();
            var diceSystem = new DiceSystem();
            var scoreSystem = new ScoreSystem();

            // Set up input entity
            var inputEntity = new Entity(1);
            inputEntity.AddComponent(new InputComponent { });
            var inputComponent = inputEntity.GetComponent<InputComponent>();

            // Set up player entity
            var playerEntity = new Entity(2);
            playerEntity.AddComponent(new DiceComponent { });
            playerEntity.AddComponent(new SaveDiceComponent { });
            playerEntity.AddComponent(new ScoreComponent { });

            // Initialize player's score
            gameSystem.SetupScore(playerEntity);

            // Iterate through rounds
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("ROUND START!");

                // Set up saved dice for the current round
                gameSystem.SetupSaveDice(playerEntity);

                // Iterate through dice rolls
                for (int j = 0; j < 2; j++)
                {
                    // Roll the dice
                    diceSystem.ThrowDice(playerEntity);
                    bool playing = true;

                    // Play the game
                    while (playing)
                    {
                        // Display current dice values
                        diceSystem.WriteDice(playerEntity);

                        Console.WriteLine("Type the number of the dice you want to save, or type anything else to continue");

                        // Take input from the player
                        gameSystem.Input(inputEntity);
                        Console.Clear();

                        // Save the selected dice or continue playing
                        if (inputComponent.Input >= 1 && inputComponent.Input <= 6)
                        {
                            diceSystem.SaveDice(playerEntity, inputComponent.Input);
                        }
                        else
                        {
                            playing = false;
                        }
                    }
                }
                
                // Choose points for the current round
                gameSystem.SetupSaveDice(playerEntity);
                diceSystem.ChoosePoint(playerEntity, inputEntity, gameSystem, scoreSystem);
            }
        }
    }
}