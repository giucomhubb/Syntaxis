using System;  
using System.Collections.Generic;  
//a01562646, Giuliana Herrera
namespace SimpleSyntaxCompiler  
{  
    class Program  
    {  
        static void Main(string[] args)  
        {  
            Console.Write("Welcome, write an expression: ");
            string user= Console.ReadLine();
            List<String> tokens= Tokenize(user);
            Parser parser= new Parser(tokens);
            bool result= parser.ParseExpression();
            Console.WriteLine(result);   
            
           
        }  
  
        // Tokenizer  
        static List<string> Tokenize(string input)  
        {  
            var tokens = new List<string>();  
            var currentToken = "";  
  
            foreach (var ch in input)  
            {  
                if (char.IsWhiteSpace(ch))  
                {  
                    continue; // Skip whitespace  
                }  
  
                if (char.IsDigit(ch))  
                {  
                    currentToken += ch; // Build multi-digit numbers  
                }  
                else  
                {  
                    if (!string.IsNullOrEmpty(currentToken))  
                    {  
                        tokens.Add(currentToken); // Add the number token  
                        currentToken = "";  
                    }  
                    tokens.Add(ch.ToString()); // Add operator or parenthesis  
                }  
            }  
  
            if (!string.IsNullOrEmpty(currentToken))  
            {  
                tokens.Add(currentToken); // Add the last number token  
            }  
  
            return tokens;  
        }  
    }  
  
    // Parser  
    class Parser  
    {  
        private readonly List<string> _tokens;  
        private int _position;  
  
        public Parser(List<string> tokens)  
        {  
            _tokens = tokens;  
            _position = 0;  
        }  
  

        public bool ParseExpression()  
        {  
            if (!ParseTerm())  
                return false;  
  
            while (Match("+") || Match("-"))  
            {  
                if (!ParseTerm())  
                    return false;  
            }  
  
            return true;  
        }  
  
        private bool ParseTerm()  
        {  
            if (!ParseFactor())  
                return false;  
  
            while (Match("*") || Match("/"))  
            {  
                if (!ParseFactor())  
                    return false;  
            }  
  
            return true;  
        }  
  
        private bool ParseFactor()  
        {  
            return ParseNumber();  
        }  
  
        private bool ParseNumber()  
        {  
            if (IsAtEnd())  
                return false;  
  
            if (int.TryParse(CurrentToken(), out _))  
            {  
                Advance();  
                return true;  
            }  
  
            return false;  
        }  
  
        // Helper methods  
        private string CurrentToken()  
        {  
            return _position < _tokens.Count ? _tokens[_position] : null;  
        }  
  
        private void Advance()  
        {  
            if (!IsAtEnd())  
            {  
                _position++;  
            }  
        }  
  
        private bool Match(string expected)  
        {  
            if (CurrentToken() == expected)  
            {  
                Advance();  
                return true;  
            }  
  
            return false;  
        }  
  
        private bool IsAtEnd()  
        {  
            return _position >= _tokens.Count;  
        }  
    }  
}  