using System;
using System.Collections.Generic;

namespace Yatzy_C_sharp_ECS
{
    //Input
    public class InputComponent
    {
        public int Input { get; }
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
        public int ScoreValue { get; }
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
                for (int i = 0; i < 6; i++)
                {
                    //Kollar om man har sparat en tärning
                    if(savedDiceComponent.SaveDice[i])
                    {
                        diceComponent.DiceValue[i] = rnd.Next(6) + 1;
                        savedDiceComponent.SaveDice[i] = false;
                    }
                    Console.WriteLine("Dice " + (i+1) + ": " + diceComponent.DiceValue[i]);

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
                for (int i = 0; i < 6; i++)
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


            }


        }

    class Program
    {
        static void Main(string[] args)
        {
            var inputEntity = new Entity(1);
            inputEntity.AddComponent(new InputComponent { });

            var playerEntity = new Entity(2);
            playerEntity.AddComponent(new DiceComponent { });
            playerEntity.AddComponent(new SaveDiceComponent { });
            playerEntity.AddComponent(new ScoreComponent { });

            var diceSystem = new DiceSystem();
            diceSystem.ThrowDice(playerEntity);

            var gameSystem = new GameSystem();
            gameSystem.Setup(playerEntity);

            bool playing = true;
            while (playing)
            {
                Console.WriteLine("Do you want to save any dice, write the number you want to save and if no write 0");

                gameSystem.Input(inputEntity);

                switch (input)
                {
                    case 1:

                        break;
                    case 2:
                        break;
                    case 3:
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 0:
                        playing = false;
                        break;
                }
            }


        }
    }

        


}
