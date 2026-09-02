using System;
using System.Collections.Generic;

class NFA
{
    static bool CheckString(string input)
    {
        // Start state
        HashSet<string> currentStates = new HashSet<string>();
        currentStates.Add("q0");

        Console.WriteLine("\nNFA TRACE:");

        foreach (char symbol in input)
        {
            HashSet<string> nextStates = new HashSet<string>();

            foreach (string state in currentStates)
            {
                switch (state)
                {
                    // q0
                    case "q0":
                        if (symbol == '/')
                            nextStates.Add("q1");
                        else
                            nextStates.Add("qtrap");
                        break;

                    // q1
                    case "q1":
                        if (symbol == '*')
                            nextStates.Add("q2");
                        else
                            nextStates.Add("qtrap");
                        break;

                    // q2
                    case "q2":
                        if (symbol == 'a')
                            nextStates.Add("q2");
                        else if (symbol == '/')
                            nextStates.Add("q2");
                        else if (symbol == '*')
                            nextStates.Add("q3");
                        else
                            nextStates.Add("qtrap");
                        break;

                    // q3
                    case "q3":
                        if (symbol == 'a')
                            nextStates.Add("q2");
                        else if (symbol == '*')
                            nextStates.Add("q3");
                        else if (symbol == '/')
                            nextStates.Add("q4");
                        else
                            nextStates.Add("qtrap");
                        break;

                    // q4 = accepting state
                    case "q4":
                        // Any character after q4 goes to qtrap
                        nextStates.Add("qtrap");
                        break;

                    // qtrap = dead state
                    case "qtrap":
                        nextStates.Add("qtrap");
                        break;
                }
            }

            currentStates = nextStates;

            Console.Write("Read '" + symbol + "' -> { ");

            foreach (string state in currentStates)
            {
                Console.Write(state + " ");
            }

            Console.WriteLine("}");
        }

        // q4 must be the final state
        return currentStates.Contains("q4");
    }


    static void Main()
    {
        Console.WriteLine("       NFA C-STYLE COMMENT CHECKER");
        

        // Ask for 3 strings
        for (int i = 1; i <= 2; i++)
        {
            Console.Write("\nEnter string " + i + ": ");
            string input = Console.ReadLine() ?? "";

            Console.WriteLine("String " + i + ": " + input);

            bool accepted = CheckString(input);

            if (accepted)
            {
                Console.WriteLine("\nRESULT: ACCEPTED");
                Console.WriteLine("The string is a valid C-style comment.");
            }
            else
            {
                Console.WriteLine("\nRESULT: REJECTED");
                Console.WriteLine("The string is NOT a valid C-style comment.");
            }
        }

       
    }
}