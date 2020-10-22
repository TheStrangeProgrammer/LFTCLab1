using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

//a
namespace LFTCLab3
{
    class Program
    {
        static SymbolTable symbolTable = new SymbolTable();
        static ProgramInternalForm programInternalForm = new ProgramInternalForm();
        static string fileToReadPath = "D:\\faculta\\s1\\Limbaje formale si tehnici de compilare\\LFTCLab1\\LFTCLab1a\\p1.es";
        static Regex identifiers = new Regex("/^[a-zA-Z_][a-zA-Z0-9_]*$/");
        static Regex stringConstant = new Regex("");
        static Regex integerConstant = new Regex(@"[-+]?\b\d+\b");
        static Regex booleanConstant = new Regex(@"\b(true|false)\b");
        static Regex reservedWords = new Regex(@"\b(if|else|for|let)\b");
        static Regex operators = new Regex(@"\b(\+|\-|\=|\<)\b");
        static Regex commentsMultiline = new Regex(@"/\*.*?\*/");
        static Regex commentsSimple = new Regex(@"//.*$");
        static Regex separators1 = new Regex(@"\b( |\n|\t)\b");
        static Regex separators2  = new Regex(@"\b(\(|\)|\[|\]|\{|\})\b");
        static void Main(string[] args)
        {
            ReadFile();
            File.WriteAllText("PIF.out", programInternalForm.ToString());
            File.WriteAllText("ST.out", symbolTable.ToString());
            Thread.Sleep(5000);
        }
        
        /// <summary>
        /// 
        /// </summary>
        static void ReadFile() { 
            string textRead = File.ReadAllText(fileToReadPath);
            string[] matches = separators1.Split(textRead);
            foreach (string match in matches)
            {
                string[] processedMatches = separators2.Split(match);
                foreach (string processedMatch in processedMatches)
                {


                    if (isReservedWord(processedMatch) || isOperator(processedMatch) || isSeparator(processedMatch))
                    {
                        programInternalForm.Add(processedMatch, new KeyValuePair<int, int>(0,0));
                    }
                    else if (isIdentifier(processedMatch) || isConstant(processedMatch))
                    {
                        KeyValuePair<int,int> index = symbolTable.Position(processedMatch);
                        programInternalForm.Add(processedMatch, index);
                    }
                    else
                    {
                        Console.WriteLine("Lexical Error");
                    }


                }

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        static bool isConstant(string token)
        {
            return stringConstant.Match(token).Success||integerConstant.Match(token).Success || booleanConstant.Match(token).Success;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        static bool isIdentifier(string token)
        {
            return identifiers.Match(token).Success;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        static bool isReservedWord(string token)
        {
            return reservedWords.Match(token).Success;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        static bool isComment(string token)
        {
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        static bool isSeparator(string token) {
            return separators1.Match(token).Success|| separators2.Match(token).Success;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        static bool isOperator(string token)
        {
            return operators.Match(token).Success;
        }
    }
}
