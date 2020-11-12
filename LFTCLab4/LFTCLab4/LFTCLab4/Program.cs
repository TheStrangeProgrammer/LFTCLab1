using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LFTCLab4
{
    
    class Program
    {
        class State
        {
            public State(string stateName)
            {
                this.stateName = stateName;
            }
            public string stateName;
            public override string ToString()
            {
                return stateName;
            }
        }
        class Transition
        {
            public Transition(string stateNameFrom, string symbolName, string stateNameTo)
            {
                if (states.Exists((state)=> state.stateName == stateNameFrom))
                {
                    from=states.Find((state) => state.stateName == stateNameFrom);
                }
                if (states.Exists((state) => state.stateName == stateNameTo))
                {
                    to=states.Find((state) => state.stateName == stateNameTo);
                }
                if (symbols.Exists((symbol) => symbol.symbolName == symbolName))
                {
                    symbol=symbols.Find((symbol) => symbol.symbolName == symbolName);
                }
            }
            public State from;
            public State to;
            public Symbol symbol;
            public override string ToString()
            {
                return from.ToString() + "-" + symbol.ToString() + "->" + to.ToString() + "\n";
            }
        }
        class Symbol
        {
            public Symbol(string symbolName)
            {
                this.symbolName = symbolName;
            }
            public string symbolName;
            public override string ToString()
            {
                return symbolName;
            }
        }
        static List<State> states=new List<State>();
        static List<Symbol> symbols=new List<Symbol>();
        static List<Transition> transitions= new List<Transition>();
        static List<State> finalStates= new List<State>();
        static State initialState;
        static void Main(string[] args)
        {
            ReadFile();
            RunMenu();
        }
        static void ReadFile()
        {
            string text =File.ReadAllText(@"D:\faculta\s1\Limbaje formale si tehnici de compilare\LFTCLab1\LFTCLab4\LFTCLab4\LFTCLab4\FA.in");
            text=text.Replace("\r","");
            string[] splits = text.Split('\n');
            
            for (int i=0;i< splits.Length; i++)
            {
                string[] toAdds = splits[i].Split(' ');
                switch (i) {
                    case 0:
                        foreach (string toAdd in toAdds)
                            states.Add(new State(toAdd));
                        break;
                    case 1:
                        foreach (string toAdd in toAdds)
                            symbols.Add(new Symbol(toAdd));
                        break;
                    case 2:
                            initialState = new State(toAdds[0]);
                        break;
                    case 3:
                        foreach (string toAdd in toAdds)
                            finalStates.Add(new State(toAdd));
                        break;
                    default:
                        transitions.Add(new Transition(toAdds[0], toAdds[1], toAdds[2]));
                        break;


                }
            }

        }
        static void RunMenu()
        {
            bool run = true;
            while (run)
            {
                DisplayMenu();
                int option = Int32.Parse(Console.ReadLine());
                switch (option)
                {
                    case 0:
                        run = false;
                        break;
                    case 1:
                        VerifySequence();
                        break;
                    case 2:
                        DisplayStates();
                        break;
                    case 3:
                        DisplayAlphabet();
                        break;
                    case 4:
                        DisplayTransitions();
                        break;
                    case 5:
                        DisplayFinalStates();
                        break;
                }
            }
        }

        private static void DisplayMenu()
        {
            Console.WriteLine("0.Exit");
            Console.WriteLine("1.Verify Sequence");
            Console.WriteLine("2.Display States");
            Console.WriteLine("3.Display Alphabet");
            Console.WriteLine("4.Display Transitions");
            Console.WriteLine("5.Display Final States");
            Console.WriteLine();
        }

        private static void DisplayFinalStates()
        {
            foreach(State state in finalStates)
            {
                Console.Write(state.ToString());
            }
            Console.WriteLine();
        }

        private static void DisplayTransitions()
        {
            foreach (Transition transition in transitions)
            {
                Console.Write(transition.ToString());
            }
            Console.WriteLine();
        }

        private static void DisplayAlphabet()
        {
            foreach (Symbol symbol in symbols)
            {
                Console.Write(symbol.ToString());
            }
            Console.WriteLine();
        }

        private static void DisplayStates()
        {
            foreach (State state in states)
            {
                Console.Write(state.ToString());
            }
            Console.WriteLine();
        }

        static void VerifySequence()
        {
            string sequence = Console.ReadLine();
            State currentState = initialState;
            
        }
       
        
    }
}
