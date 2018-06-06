using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExpressionDemo
{
    /// <summary>
    /// 展示表达式树，协助用的
    /// </summary>
    public class ExpressionTreeVisualizer
    {
        public static void Show()
        {
            //{
            //    ParameterExpression parameterExpression = Expression.Parameter(typeof(People), "x");
            //    Expression<Func<People, bool>> expression = Expression.Lambda<Func<People, bool>>(
            //        Expression.GreaterThan(
            //            Expression.Property(parameterExpression, typeof(People).GetProperty("Age")), 
            //                                Expression.Constant(5, typeof(int))), 
            //        new ParameterExpression[]
            //        {
            //            parameterExpression
            //        });
            //}

            ////{
            ////    Expression<Func<int, int, int>> lambda = (m, n) => (m < 100 ? m : 100) * n + 1000;
            ////}
            //Expression<Func<People, bool>> lambda = x => x.Age > 5;
            //Expression<Func<People, bool>> lambda = x => x.Id.ToString().Equals("5");
            //Expression<Func<People, PeopleCopy>> lambda = p =>
            //            new PeopleCopy()
            //            {
            //                Id = p.Id,
            //                Name = p.Name,
            //                Age = p.Age
            //            };
            //{
            //    ParameterExpression parameterExpression = Expression.Parameter(typeof(int), "m");
            //    ParameterExpression parameterExpression2 = Expression.Parameter(typeof(int), "n");
            //    Expression<Func<int, int, int>> expression = Expression.Lambda<Func<int, int, int>>(
            //                Expression.Add(
            //                    Expression.Multiply(
            //                        Expression.Condition(
            //                            Expression.LessThan(parameterExpression, Expression.Constant(100, typeof(int))), 
            //                            parameterExpression, Expression.Constant(100, typeof(int))), parameterExpression2), Expression.Constant(1000, typeof(int))), 
            //        new ParameterExpression[]
            //    {
            //        parameterExpression,
            //        parameterExpression2
            //    });
            //}
        }
    }
}
