using System;
using System.Collections.Generic;
using System.IO;

class Program_1
{
    static void Main()
    {
        string address = "D:\\Універ\\2 курс\\2 семестр\\Крос-платформне програмуваня\\Лаби\\Lab_9\\Program_1\\input.txt";
        string expression = File.ReadAllText(address).Replace(" ", "");
        int result = Evaluate(expression);
        Console.WriteLine("Результат: " + result);
    }

    static int Evaluate(string expr)
    {
        Stack<char> stack = new Stack<char>();
        for (int i = expr.Length - 1; i >= 0; i--)
        {
            stack.Push(expr[i]);
        }
        return ParseExpr(stack);
    }

    static int ParseExpr(Stack<char> stack)
    {
        if (stack.Count == 0)
            throw new InvalidOperationException("Stack is empty.");

        char c = stack.Pop();

        if (char.IsDigit(c))
        {
            return c - '0';
        }
        else if (c == 'M' || c == 'm')
        {
            if (stack.Pop() != '(')
                throw new InvalidOperationException("Expected '(' after M or m");

            int left = ParseExpr(stack);

            if (stack.Pop() != ',')
                throw new InvalidOperationException("Expected ',' between arguments");

            int right = ParseExpr(stack);

            if (stack.Pop() != ')')
                throw new InvalidOperationException("Expected ')' after arguments");

            return c == 'M' ? Math.Max(left, right) : Math.Min(left, right);
        }

        throw new InvalidOperationException("Invalid expression at character: " + c);
    }
}
