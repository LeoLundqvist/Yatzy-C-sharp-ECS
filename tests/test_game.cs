using NUnit.Framework;
using System;
using System.IO;

namespace Yatzy_C_sharp_ECS.Tests
{
    public class YatzyTests
    {
        [Test]
        public void TestThrowDice()
        {
            var entity = new Entity(1);
            entity.AddComponent(new DiceComponent { });
            entity.AddComponent(new SaveDiceComponent { });

            var diceSystem = new DiceSystem();

            diceSystem.ThrowDice(entity);

            var diceComponent = entity.GetComponent<DiceComponent>();
            var savedDiceComponent = entity.GetComponent<SaveDiceComponent>();

            // Ensure that all dice values are between 1 and 6
            foreach (var diceValue in diceComponent.DiceValue)
            {
                Assert.GreaterOrEqual(diceValue, 1);
                Assert.LessOrEqual(diceValue, 6);
            }

            // Ensure that no dice is saved after throwing
            foreach (var savedDice in savedDiceComponent.SaveDice)
            {
                Assert.IsFalse(savedDice);
            }
        }

        [Test]
        public void TestSaveDice()
        {
            var entity = new Entity(1);
            entity.AddComponent(new DiceComponent { });
            entity.AddComponent(new SaveDiceComponent { });

            var diceSystem = new DiceSystem();

            // Set up initial dice values
            for (int i = 0; i < 6; i++)
            {
                entity.GetComponent<DiceComponent>().DiceValue[i] = i + 1;
            }

            diceSystem.SaveDice(entity, 1);
            diceSystem.SaveDice(entity, 3);
            diceSystem.SaveDice(entity, 5);

            var savedDiceComponent = entity.GetComponent<SaveDiceComponent>();

            // Ensure that dice are saved correctly
            Assert.IsTrue(savedDiceComponent.SaveDice[0]);
            Assert.IsTrue(savedDiceComponent.SaveDice[2]);
            Assert.IsTrue(savedDiceComponent.SaveDice[4]);

            // Ensure that other dice are not saved
            Assert.IsFalse(savedDiceComponent.SaveDice[1]);
            Assert.IsFalse(savedDiceComponent.SaveDice[3]);
            Assert.IsFalse(savedDiceComponent.SaveDice[5]);
        }

        [Test]
        public void TestChoosePoint()
        {
            // Set up console input and output redirection
            string input = "1\n";
            var reader = new StringReader(input);
            Console.SetIn(reader);
            StringWriter writer = new StringWriter();
            Console.SetOut(writer);

            var playerEntity = new Entity(1);
            playerEntity.AddComponent(new DiceComponent { DiceValue = new int[] { 1, 1, 1, 1, 1, 1 } });
            playerEntity.AddComponent(new ScoreComponent { ScoreNotTaken = new bool[] { true, true, true, true, true, true } });

            var inputEntity = new Entity(2);
            inputEntity.AddComponent(new InputComponent { });

            var gameSystem = new GameSystem();
            var scoreSystem = new ScoreSystem();

            gameSystem.SetupSaveDice(playerEntity);

            gameSystem.Input(inputEntity);

            var inputComponent = inputEntity.GetComponent<InputComponent>();

            Assert.AreEqual(1, inputComponent.Input);

            diceSystem.ChoosePoint(playerEntity, inputEntity, gameSystem, scoreSystem);

            // Ensure that points are calculated correctly
            var scoreComponent = playerEntity.GetComponent<ScoreComponent>();
            Assert.AreEqual(6, scoreComponent.ScoreValue);

            // Ensure that the correct message is printed
            StringAssert.Contains("You got 6 points", writer.ToString());
        }
    }
}
