
using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Text;

namespace Compiler
{
    class MyVisitor : CompilerBaseVisitor<CompilerParser>
    {
        public override CompilerParser VisitStatement([NotNull] CompilerParser.StatementContext context)
        {
            if (context.Start.Type == CompilerParser.WRITE)
                Console.WriteLine(CompilerParser.memory[context.children[2].GetText()]);
            else VisitChildren(context);
            return null;
        }

        public override CompilerParser VisitExpression([NotNull] CompilerParser.ExpressionContext context)
        {
            // Кидаем результат в специальное поле, чтобы потом получить к нему доступ из контекста
            context.val = EvaluateExpression(context);
            return null;
        }

        private float EvaluateExpression(CompilerParser.ExpressionContext ctx)
        {
            float left, right;

            // Если выражение является обычным числом(left, right и sign будут null) - просто возвращаем его
            // В случае с одинокой переменной - читаем из ее значение памяти
            if (ctx.left == null) return ctx.Start.Type == CompilerParser.ID
                    ? (float)CompilerParser.memory[ctx.Start.Text]
                    : Convert.ToSingle(ctx.Start.Text);

            // Если левая часть выражения является переменной - вытаскиваем ее значение
            if (ctx.left.Start.Type == CompilerParser.ID) 
                left = Convert.ToSingle(CompilerParser.memory[ctx.left.Start.Text]); 
            // Если левая часть выражения тоже является выражением - рекурсивно вычисляем его значение
            else if (ctx.left.left != null) left = EvaluateExpression(ctx.left);
            // Если числом (у него левый узел всегда будет null), значит просто конвертируем его в float
            else left = Convert.ToSingle(ctx.left.Start.Text);

            // Тоже самое с правой частью, только смотрим на правый узел
            if (ctx.right.Start.Type == CompilerParser.ID)
                right = Convert.ToSingle(CompilerParser.memory[ctx.right.Start.Text]);
            else if (ctx.right.right != null) right = EvaluateExpression(ctx.right);
            else right = Convert.ToSingle(ctx.right.Start.Text);

            // Выполняем нужное арифметическое действие с операндами и возвращаем результат 
            switch(ctx.sign.Text)
            {
                case "/": return left / right;
                case "*": return left * right;
                case "+": return left + right;
                case "-": return left - right;
                default: throw new Exception($"Incorrect sign '{ctx.sign.Text}'");
            }
        }

        public override CompilerParser VisitAssignment([NotNull] CompilerParser.AssignmentContext context)
        {
            // Получаем доступ к родительскому контексту (всегда будет Statement, поэтому на null можно не проверять)
            var parentContext = context.Parent as CompilerParser.StatementContext;

            // Для разрешения неоднозначности между Expression и Condition (в обоих есть альтернатива ID)
            // То есть в выражении вида a = b переменная b может быть как логической и числовой, и при таком
            // выражении, исходя из грамматики, визитор всегда будет уходить в узел Condition. Поэтому берем
            // напрямую значение переменной b из памяти и копируем в переменную a. 
            if(context.Start.Type == CompilerParser.ID 
                && context.Start.StopIndex - context.Start.Text.Length + 1 == context.Stop.StartIndex)
            {
                AddToMemory(parentContext.Start.Text, CompilerParser.memory[context.Start.Text]);
                return null;
            }

            // В зависимости от того, что представляет собой выражение, посещаем соответствующий узел
            // и записываем в память его значение 
            if (context.exp != null)
            {
                Visit(context.exp);
                AddToMemory(parentContext.Start.Text, context.exp.val);
            }
            else
            {
                Visit(context.cond);
                AddToMemory(parentContext.Start.Text, context.cond.val);
            }
            return null;
        }

        public override CompilerParser VisitCondition([NotNull] CompilerParser.ConditionContext context)
        {
            // Если в качестве условия передан результат сравнения двух выражений
            if (context.left != null && context.right != null)
                if (context.gt != null) context.val = EvaluateExpression(context.left) > 
                                                      EvaluateExpression(context.right);
                else if (context.lt != null) context.val = EvaluateExpression(context.left) <
                                                           EvaluateExpression(context.right);
                else context.val = context.not == null
                    ? EvaluateExpression(context.left) == EvaluateExpression(context.right)
                    : EvaluateExpression(context.left) != EvaluateExpression(context.right);
            else if(context.leftC != null && context.rightC != null)
            {
                Visit(context.leftC);
                Visit(context.rightC);
                if (context.AND() != null) context.val = context.leftC.val && context.rightC.val;
                else context.val = context.leftC.val || context.rightC.val;
            }
            // Если ID не пустой - читаем соответствующую переменную из памяти и инвертируем ее
            else if (context.ID() != null)
                context.val = !Convert.ToBoolean(CompilerParser.memory[context.Stop.Text]);
            // Если передана bool переменная
            else if (context.not == null)
                 context.val = Convert.ToBoolean(context.Start.Text);
            else context.val = !Convert.ToBoolean(context.Stop.Text);
            return null;
        }

        public override CompilerParser VisitIf([NotNull] CompilerParser.IfContext context)
        {
            VisitCondition(context.cond);
            if (context.cond.val) Visit(context.state);
            else if (context.altState != null) Visit(context.altState);
            return null;
        }

        public override CompilerParser VisitWhile([NotNull] CompilerParser.WhileContext context)
        {
            VisitCondition(context.cond);
            if (context.cond.val)
            {
                // Перебираем все дочерние Statement'ы, по сути представляет собой одну итерацию 
                for (int i = 0; i < context.statement().Length; i++)
                {
                    // Если встречаем break, то сразу выходим из правила
                    if (context.statement()[i].@break != null) return null;
                    // Если встретили continue, то выходим только из текущей итерации
                    if (context.statement()[i].cont != null) break;
                    // В остальных случаях просто заходим в очередной statement
                    Visit(context.statement()[i]);
                }
                // Следующая итерация
                return VisitWhile(context);
            }
            return null;
        }

        private void AddToMemory(string key, object value)
        {
            if (CompilerParser.memory.ContainsKey(key)) 
                CompilerParser.memory[key] = value;
            else CompilerParser.memory.Add(key, value);
        }
    }
}