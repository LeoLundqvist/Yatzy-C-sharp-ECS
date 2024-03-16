using System;
using System.Collections.Generic;

namespace Yatzy_C_sharp_ECS
{
    //Input
    public class InputComponent
    {
        public int Input { get; set; }
    }

    //Tärningarna man väljer att spara
    public class SaveDiceComponent
    {
        public bool[] SaveDice { get; } = new bool[6];
    }

    //Tärningarna som man kastar
    public class DiceComponent
    {
        public int[] DiceValue { get; } = new int[6];

    }
    //Dina poäng
    public class ScoreComponent
    {
        public int ScoreValue { get; set; }
        public bool[] ScoreNotTaken { get; } = new bool[6];

    }

    //Entitet
    public class Entity
    {
        public int Id { get; }
        private Dictionary<Type, object> components = new Dictionary<Type, object>();
        public Entity(int id)
        {
            Id = id;
        }
        public void AddComponent<T>(T component)
        {
            components[typeof(T)] = component;
        }
        public T GetComponent<T>() where T : class
        {
            if (components.TryGetValue(typeof(T), out var component))
            {
                return component as T;
            }
            return null;
        }
    }

    public class DiceSystem
    {
        public void ThrowDice(Entity entity)
        {
            var diceComponent = entity.GetComponent<DiceComponent>();
            var savedDiceComponent = entity.GetComponent<SaveDiceComponent>();

            if (diceComponent != null && savedDiceComponent != null)
            {
                Random rnd = new Random();
                for (int i = 0; i < diceComponent.DiceValue.Length; i++)
                {
                    //Kollar om man har sparat en tärning
                    if(!savedDiceComponent.SaveDice[i])
                    {
                        diceComponent.DiceValue[i] = rnd.Next(6) + 1;
                    }
                    savedDiceComponent.SaveDice[i] = false;
                }
            }
        }
        public void SaveDice(Entity entity, int input)
        {
            var diceComponent = entity.GetComponent<DiceComponent>();
            var saveDiceComponent = entity.GetComponent<SaveDiceComponent>();

            if (saveDiceComponent != null)
            {
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
        public void WriteDice(Entity entity)
        {
            var diceComponent = entity.GetComponent<DiceComponent>();
            var savedDiceComponent = entity.GetComponent<SaveDiceComponent>();

            if (diceComponent != null && savedDiceComponent != null)
            {
                for (int i = 0; i < diceComponent.DiceValue.Length; i++)
                {
                    if (savedDiceComponent.SaveDice[i])
                    {
                        Console.WriteLine("Dice " + (i + 1) + ": " + diceComponent.DiceValue[i] + " (Saved)");
                    }
                    else
                    Console.WriteLine("Dice " + (i + 1) + ": " + diceComponent.DiceValue[i]);
                }
            }
        }
        public void ChoosePoint(Entity playerEntity, Entity inputEntity, GameSystem gameSystem, ScoreSystem scoreSystem)
        {
            var scoreComponent = playerEntity.GetComponent<ScoreComponent>();
            var inputComponent = inputEntity.GetComponent<InputComponent>();
            bool notDecided = true;

            while (notDecided)
            {
                Console.WriteLine("Your dice result");

                WriteDice(playerEntity);

                Console.WriteLine("What combination score do you choose?");

                //skriver ut alla valen för combinationer 1-6 om de inte redan blivit valda
                for(int i = 0; i < 6; i++)
                {
                    if (scoreComponent.ScoreNotTaken[i])
                    {
                        Console.WriteLine((i+1) + ". All " + (i + 1) + "s");
                    }
                }

                gameSystem.Input(inputEntity);
                
                // kollar så det input inte är för stort för scoreNotTaken arrayen
                if(inputComponent.Input >= 1 && inputComponent.Input <= 6)
                {
                    // kollar så man inte redan valt denna combinationen
                    if (scoreComponent.ScoreNotTaken[inputComponent.Input - 1])
                    {
                        //om det är mellan 1-6 får man poäng för alla tärningar med den siffran
                        if (inputComponent.Input >= 1 && inputComponent.Input <= 6 && scoreComponent.ScoreNotTaken[inputComponent.Input - 1])
                        {
                            scoreComponent.ScoreValue += scoreSystem.Combination1_6(playerEntity, inputComponent.Input);
                            Console.WriteLine("You got " + scoreSystem.Combination1_6(playerEntity, inputComponent.Input) + " points");
                            Console.WriteLine("Your total score is: " + scoreComponent.ScoreValue);
                            Console.Write("Press enter to continue... ");
                            Console.ReadLine();
                            scoreComponent.ScoreNotTaken[inputComponent.Input - 1] = false;
                            notDecided = false;
                        }
                        switch (inputComponent.Input)
                        {
                            case 1:
                                scoreComponent.ScoreNotTaken[inputComponent.Input - 1] = false;
                                notDecided = false;
                                break;
                        }

                    }
                }
                Console.Clear();    
            }
        }
    }

    public class ScoreSystem
    {
        public int Combination1_6(Entity entity, int number)
        {
            int amount = CountAmount(entity, number);
            int points = amount * number;
            return points;
        }
        public int Combination7(Entity entity, int number)
        {
            int amount = CountAmount(entity, number);
            int points = amount * number;
            return points;
        }
        public int CountAmount(Entity entity, int number)
        {
            int amountOfNumbers = 0;
            var diceComponent = entity.GetComponent<DiceComponent>();

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

    public class GameSystem
    {
        public void Play()
        {



        }

        public void SetupSaveDice(Entity entity)
        {
            var saveDiceComponent = entity.GetComponent<SaveDiceComponent>();
            if (saveDiceComponent != null)
            {
                for (int i = 0; i < saveDiceComponent.SaveDice.Length; i++)
                {
                    saveDiceComponent.SaveDice[i] = false;
                }
            }
        }
        public void SetupScore(Entity entity)
        {
            var scoreComponent = entity.GetComponent<ScoreComponent>();
            if (scoreComponent != null)
            {
                scoreComponent.ScoreValue = 0;
                for (int i = 0; i < scoreComponent.ScoreNotTaken.Length; i++)
                {
                    scoreComponent.ScoreNotTaken[i] = true;
                }
            }
        }

        public void Input(Entity entity)
        {
            var inputComponent = entity.GetComponent<InputComponent>();

            if (inputComponent != null)
            {
                string inputString = Console.ReadLine();

                if (Int32.TryParse(inputString, out int input))
                {
                    inputComponent.Input = input;
                }
                else
                {
                    inputComponent.Input = 0;
                }


            }


        }

        
    }

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
