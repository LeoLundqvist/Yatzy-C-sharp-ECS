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

        public void ChoosePoint(Entity entity)
        {

        }
    }

    public class GameSystem
    {
        public void Play()
        {



        }

        public void Setup(Entity entity)
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
            //fixar input system
            var inputEntity = new Entity(1);
            inputEntity.AddComponent(new InputComponent { });
            var inputComponent = inputEntity.GetComponent<InputComponent>();

            //fixar player entity
            var playerEntity = new Entity(2);
            playerEntity.AddComponent(new DiceComponent { });
            playerEntity.AddComponent(new SaveDiceComponent { });
            playerEntity.AddComponent(new ScoreComponent { });

            var gameSystem = new GameSystem();

            var diceSystem = new DiceSystem();

            bool playing = true;

            //antal ronder
            for (int i = 0; i < 13; i++)
            {
                Console.WriteLine("ROUND START!");

                gameSystem.Setup(playerEntity);

                //antal kast försök
                for (int j = 0; j < 2; j++)
                {
                    diceSystem.ThrowDice(playerEntity);
                    playing = true;

                    //spelet
                    while (playing)
                    {
                        diceSystem.WriteDice(playerEntity);

                        Console.WriteLine("Do you want to save any dice, write the number you want to save and if no write 7");

                        //if (Int32.TryParse(inputString, out int input)) { }
                        gameSystem.Input(inputEntity);
                        Console.Clear();

                        switch (inputComponent.Input)
                        {
                            case 1:
                                diceSystem.SaveDice(playerEntity, inputComponent.Input);
                                break;
                            case 2:
                                diceSystem.SaveDice(playerEntity, inputComponent.Input);
                                break;
                            case 3:
                                diceSystem.SaveDice(playerEntity, inputComponent.Input);
                                break;
                            case 4:
                                diceSystem.SaveDice(playerEntity, inputComponent.Input);
                                break;
                            case 5:
                                diceSystem.SaveDice(playerEntity, inputComponent.Input);
                                break;
                            case 6:
                                diceSystem.SaveDice(playerEntity, inputComponent.Input);
                                break;
                            case 7:
                                playing = false;
                                break;
                        }
                    }
                }
                //choose points

            }
        }
    }
}
