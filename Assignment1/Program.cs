// Course code : SODV2202
// Term/Year : Fall/2018
// Assignment code: PA1
// Author : Anderson Resende Viana
// BVC username : a.resendeviana683
// Date created : 2018-10-08
// Description : The activity was completed to attend the subject "Object Oriented Programming" 
// in the program Software Development at Bow Valley College.


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment1
{
    public abstract class Token
    {
        protected string _symbol;

        public virtual string Symbol
        {
            get
            {
                return _symbol;
            }
            set
            {
                _symbol = value;
            }
        }

        public Token(string input)
        {
            Symbol = input;
        }
    }

    public class Operator : Token
    {
        protected int _priority;

        public int Priority
        {
            get
            {
                if ((Symbol == "/") || (Symbol == "*"))
                {
                    int highPriority = 2;
                    return highPriority;
                }
                else if ((Symbol == "+") || (Symbol == "-")) // -> control will be done at main code
                {
                    int lowPriority = 1;
                    return lowPriority;
                }
                else
                {
                    string nullPriority = null;
                    return Convert.ToInt32(nullPriority); // -> added just to ignore brackets priority
                }
            }
            set { }
        }

        public Operator(string input) : base(input)
        {
        }

    }

    public class Operand : Token
    {
        public Operand(string input) : base(input)
        {
        }
    }

    public class Program
    {

        public static string ProcessCommand(string input)
        {

                string[] mathSymbols = { "/", "*", "+", "-" };


                double Calculate(Token mathFunction, Token firstValue, Token secondValue)
                {
                    double result;
                    switch (mathFunction.Symbol)
                    {
                        case "+":
                            result = Convert.ToDouble(firstValue.Symbol) + Convert.ToDouble(secondValue.Symbol);
                            return result;
                        case "-":
                            result = Convert.ToDouble(firstValue.Symbol) - Convert.ToDouble(secondValue.Symbol);
                            return result;
                        case "*":
                            result = Convert.ToDouble(firstValue.Symbol) * Convert.ToDouble(secondValue.Symbol);
                            return result;
                        case "/":
                            result = Convert.ToDouble(firstValue.Symbol) / Convert.ToDouble(secondValue.Symbol);
                            return result;

                        default: return 0;
                    }
                }


                string StandardizeExpression(string userInput)
                {

                    var tempExpression = userInput.Replace(" ", "");

                    StringBuilder rewrittenExpression = new StringBuilder(" ");

                    foreach (char aChar in tempExpression)
                    {
                        if (!Char.IsDigit(aChar) && aChar != '.') // replace every math symbol and brackets with a version of itself with spaces
                        {
                            string spacedChar = " " + aChar + " ";
                            rewrittenExpression.Append(spacedChar);
                        }
                        else
                        {
                            rewrittenExpression.Append(aChar);
                        }
                    }

                    rewrittenExpression.Append(" ");

                    return rewrittenExpression.ToString();

                }


                Stack<Token> BuildPostfixStack(string normalizedExpression)
                {

                    Stack<Operator> stackOfOperators = new Stack<Operator>();

                    Stack<Token> postfixExpressionStack = new Stack<Token>();

                    StringBuilder numberBuilder = new StringBuilder();

                    for (int i = 0; i < normalizedExpression.Length; i++)
                    {
                        string token = normalizedExpression[i].ToString();

                        if (token == "." || token.All(char.IsDigit))
                        {

                            if (normalizedExpression[i + 1] == ' ') // end of the chars that compose the number
                            {
                                numberBuilder.Append(token);
                                Operand operandToken = new Operand(numberBuilder.ToString());
                                postfixExpressionStack.Push(operandToken); // move Token to RPN expression
                                numberBuilder.Clear(); // clear SB to start building the next number
                            }
                            else
                            {
                                numberBuilder.Append(token);
                            }

                        }

                        if (token == "(" || token == ")")
                        {
                            Operator bracketToken = new Operator(token);

                            if (bracketToken.Symbol == "(")
                            {
                                stackOfOperators.Push(bracketToken);
                            }

                            if (bracketToken.Symbol == ")")
                            {
                                while (stackOfOperators.Count != 0 && stackOfOperators.Peek().Symbol != "(")
                                {
                                    postfixExpressionStack.Push(stackOfOperators.Pop()); // Move token from the stack to the postfix expression
                                }
                                stackOfOperators.Pop();
                            }
                        }

                        if (token == "-")
                        {
                            if (i == 2) // if the expression starts with a - symbol, it means the first number is negative
                            {
                                numberBuilder.Append(token);
                            }
                            else if ((i > 2 && mathSymbols.Contains(normalizedExpression[i - 3].ToString())) || (i > 2 && normalizedExpression[i - 3] == '(') || (i > 2 && normalizedExpression[i - 3] == '(')) // since math symbols are replaced at the beginning (i.e: 1+-2, becomes 1 +  - 2) we have to check if what is stored 3 indexes before '-' is a math symbol, if it is so, this '-' sign belogns to an unary number. It also check if i-2 is a bracket
                            {
                                numberBuilder.Append(token);
                            }
                            else
                            {
                                Operator operatorToken = new Operator(token);

                                if (stackOfOperators.Count == 0)
                                {
                                    stackOfOperators.Push(operatorToken);
                                }

                                else
                                {
                                    while (stackOfOperators.Count != 0 && (stackOfOperators.Peek().Priority >= operatorToken.Priority)) // if the priority of item on top of the stack is higher or equal to the priority of the current item
                                    {
                                        postfixExpressionStack.Push(stackOfOperators.Pop()); // Move token from the stack to the postfix expression
                                    }
                                    stackOfOperators.Push(operatorToken);
                                }
                            }
                        }

                        if (token == "+" || token == "*" || token == "/") // mathSymbols.Contains(token)) - refactored because it was not passing the subtraction test 12 - - 2
                        {

                            Operator operatorToken = new Operator(token);
                            if (stackOfOperators.Count == 0)
                            {
                                stackOfOperators.Push(operatorToken);

                            }

                            else
                            {
                                while (stackOfOperators.Count != 0 && (stackOfOperators.Peek().Priority >= operatorToken.Priority)) // if the priority of item on top of the stack is higher or equal to the priority of the current item
                                {
                                    postfixExpressionStack.Push(stackOfOperators.Pop()); // Move token from the stack to the postfix expression
                                }
                                stackOfOperators.Push(operatorToken);
                            }
                        }
                    }

                    if (stackOfOperators.Count != 0)
                    {
                        while (stackOfOperators.Count != 0)
                        {
                            postfixExpressionStack.Push(stackOfOperators.Pop());
                        }
                    }

                    return postfixExpressionStack;
                }


                Stack<Token> ReverseStack(Stack<Token> regularRPNStack)  //REVERSING STACK to start calculating
                {
                    var tempHolder = new List<Token>(); // reversing stack

                    while (regularRPNStack.Count != 0)
                    {
                        tempHolder.Add(regularRPNStack.Pop()); // remove items from stack and add them to temp list (FIFO)
                    }
                    foreach (var item in tempHolder)
                    {
                        regularRPNStack.Push(item);
                    }

                    return regularRPNStack;
                } 


                // Calculating RPN expression: whenever a Operand is found on top of postfixExpressionStack, send it to stackOfNumbers, 
                //if a Operator is found on top of postfixExpressionStack system will get the two last Operands that were sent to stackOfNumbers. 
                //System will perform the operation and the result will be send to stackOfNumbers.


                string CalculateResult(Stack<Token> reversedStack)
                {

                    Stack<Token> resultStack = new Stack<Token>(); // the last Operand in this stack will hold the final result

                    while (reversedStack.Count != 0)
                    {
                        for (int i = 0; i < reversedStack.Count; i++) // postfixExpStack is already in RPN, so if we have a mininum of 2 operands and 1 operator
                        {
                            if (reversedStack.Peek() is Operand)
                            {
                                resultStack.Push(reversedStack.Pop());
                            }
                            if (reversedStack.Peek() is Operator)
                            {
                                Token operatorSign = reversedStack.Pop();
                                Token secondOperand = resultStack.Pop();
                                Token firstOperand = resultStack.Pop();
                                var result = Calculate(operatorSign, firstOperand, secondOperand);
                                Operand calculatedValue = new Operand(result.ToString());
                                resultStack.Push(calculatedValue);
                            }
                        }
                    }

                    Token lastToken = resultStack.Pop();

                    return lastToken.Symbol;

                }


            try
            {
                string expression = StandardizeExpression(input);

                Stack<Token> postfixStack = BuildPostfixStack(expression);

                Stack<Token> reversedPostfixStack = ReverseStack(postfixStack);

                string answer = CalculateResult(reversedPostfixStack);

                return answer;
            }
            catch (Exception e)
            {
                return "Error evaluating expression: " + e;
            }

        }

        static void Main(string[] args)
        {
            string input;
            while ((input = Console.ReadLine()) != "exit")
            {
                Console.WriteLine(ProcessCommand(input));
            }
        }
    }
}
