using System;
using System.Collections;
using System.IO;

class ExpressionEvaluator : IEnumerable, ICloneable
{
    private string expression;

    public ExpressionEvaluator(string expr)
    {
        expression = expr.Replace(" ", "");
    }

    public int Evaluate()
    {
        Stack stack = new Stack();
        for (int i = expression.Length - 1; i >= 0; i--)
            stack.Push(expression[i]);

        return ParseExpr(stack);
    }

    private int ParseExpr(Stack stack)
    {
        if (stack.Count == 0)
            throw new InvalidOperationException("Empty expression");

        char c = (char)stack.Pop();

        if (char.IsDigit(c))
            return c - '0';

        if (c == 'M' || c == 'm')
        {
            if ((char)stack.Pop() != '(') throw new InvalidOperationException("Expected '('");
            int left = ParseExpr(stack);
            if ((char)stack.Pop() != ',') throw new InvalidOperationException("Expected ','");
            int right = ParseExpr(stack);
            if ((char)stack.Pop() != ')') throw new InvalidOperationException("Expected ')'");

            return c == 'M' ? Math.Max(left, right) : Math.Min(left, right);
        }

        throw new InvalidOperationException($"Unexpected character: {c}");
    }

    public IEnumerator GetEnumerator()
    {
        return expression.GetEnumerator();
    }

    public object Clone()
    {
        return new ExpressionEvaluator(expression);
    }
}

class Program
{
    static void Main()
    {
        string address = "D:\\Універ\\2 курс\\2 семестр\\Крос-платформне програмуваня\\Лаби\\Lab_9\\Program_1\\input.txt";
        string expr = File.ReadAllText(address);

        var evaluator = new ExpressionEvaluator(expr);
        Console.WriteLine("Результат: " + evaluator.Evaluate());
    }
}
