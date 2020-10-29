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
        static string fileToReadPath = "D:\\faculta\\s1\\Limbaje formale si tehnici de compilare\\LFTCLab1\\LFTCLab1a\\p1err.es";
        static Regex identifiers = new Regex(@"(\b([a-zA-Z_]+)\b)");
        static Regex stringConstant = new Regex("\"(.*?)\"");
        static Regex integerConstant = new Regex(@"\b([+-]?[1-9][0-9]*|0)\b");
        static Regex booleanConstant = new Regex(@"(true|false)");
        static Regex reservedWords = new Regex(@"(let|if|elseif|else|input|output|while|for)");
        static Regex operators = new Regex(@"(\+|\*|-|\/|%|==|<=|>=|=|<|>|\|\||&&|!)");
        static Regex commentsMultiline = new Regex(@"/\*.*?\*/");
        static Regex commentsSimple = new Regex(@"//.*$", RegexOptions.Multiline);
        static Regex separators1 = new Regex(@"(\s+|\n+|\t+|\r+|;|,)");
        static Regex separators2  = new Regex(@"(\(|\)|\[|\]|{|})");
        static void Main(string[] args)
        {
            ReadFile();
            File.WriteAllText(@"D:\faculta\s1\Limbaje formale si tehnici de compilare\LFTCLab1\LFTCLab3\LFTCLab3\LFTCLab3\PIF.out", programInternalForm.ToString());
            File.WriteAllText(@"D:\faculta\s1\Limbaje formale si tehnici de compilare\LFTCLab1\LFTCLab3\LFTCLab3\LFTCLab3\ST.out", symbolTable.ToString());
            Thread.Sleep(5000);
        }
        static string[] Tokenize(string[] tokens,Regex regex)
        {
            List<string> matches = new List<string>();
            foreach (string token in tokens) {
                string[] processedTokens = regex.Split(token);
                foreach (string processedToken in processedTokens) {
                    matches.Add(processedToken);
                }
            }

            return matches.ToArray();

        }
        /// <summary>
        /// 
        /// </summary>
        static void ReadFile() { 
            string textRead = File.ReadAllText(fileToReadPath);
            string[] tokens;
            tokens= Regex.Split(textRead, commentsMultiline.ToString());
            tokens= Tokenize(tokens, commentsSimple);
            tokens = Tokenize(tokens, separators1);
            tokens = Tokenize(tokens, separators2);
            tokens = Tokenize(tokens, operators);
            tokens = Tokenize(tokens, reservedWords);
            tokens = Tokenize(tokens, booleanConstant);
            tokens = Tokenize(tokens, integerConstant);
            tokens = Tokenize(tokens, stringConstant);
            tokens = Tokenize(tokens, identifiers);
            int i = 1;
            foreach (string token in tokens)
            {

                if (isEndline(token)) i++;
                if (isReservedWord(token) || isOperator(token) || isSeparator(token))
                {
                    programInternalForm.Add(token, new KeyValuePair<int, int>(0, 0));
                }
                else if (isIdentifier(token) || isConstant(token))
                {
                    KeyValuePair<int, int> index = symbolTable.Position(token);
                    programInternalForm.Add(token, index);
                }
                else if (token != "")
                {
                    Console.WriteLine("Lexical Error at line " + i);
                }


            }
            // IEnumerable<string> tokens = separators1.Split(textRead).ToList().Select(match=> separators2.Split(match));
            /*foreach (string match in matches)
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
                    else if(processedMatch!="")
                    {
                        Console.WriteLine("Lexical Error");
                    }


                }

            }*/
        }

        private static bool isEndline(string token)
        {
            return Regex.IsMatch(token, "\n");
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
