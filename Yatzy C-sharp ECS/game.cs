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
            var gameSystem = new GameSystem();
            var diceSystem = new DiceSystem();
            var scoreSystem = new ScoreSystem();

            //fixar input system
            var inputEntity = new Entity(1);
            inputEntity.AddComponent(new InputComponent { });
            var inputComponent = inputEntity.GetComponent<InputComponent>();

            //fixar player entity
            var playerEntity = new Entity(2);
            playerEntity.AddComponent(new DiceComponent { });
            playerEntity.AddComponent(new SaveDiceComponent { });
            playerEntity.AddComponent(new ScoreComponent { });

            gameSystem.SetupScore(playerEntity);


            //antal ronder
            for (int i = 0; i < 6; i++)
            {
                Console.WriteLine("ROUND START!");

                gameSystem.SetupSaveDice(playerEntity);

                //antal kast försök
                for (int j = 0; j < 2; j++)
                {
                    diceSystem.ThrowDice(playerEntity);
                    bool playing = true;

                    //spelet
                    while (playing)
                    {
                        diceSystem.WriteDice(playerEntity);

                        Console.WriteLine("Type the number of the dice you want to save, or type anything else to continue");

                        gameSystem.Input(inputEntity);
                        Console.Clear();

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
                //choose points
                
                gameSystem.SetupSaveDice(playerEntity);
                diceSystem.ChoosePoint(playerEntity, inputEntity, gameSystem, scoreSystem);
                
            }
        }
    }
}
