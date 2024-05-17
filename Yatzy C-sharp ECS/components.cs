using System;
using System.Collections.Generic;

//all components
namespace Components
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
}