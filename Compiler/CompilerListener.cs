//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.8
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from Compiler.g4 by ANTLR 4.8

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419


    using System.Collections;

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="CompilerParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.8")]
[System.CLSCompliant(false)]
public interface ICompilerListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="CompilerParser.compiler"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCompiler([NotNull] CompilerParser.CompilerContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CompilerParser.compiler"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCompiler([NotNull] CompilerParser.CompilerContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CompilerParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterStatement([NotNull] CompilerParser.StatementContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CompilerParser.statement"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitStatement([NotNull] CompilerParser.StatementContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CompilerParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAssignment([NotNull] CompilerParser.AssignmentContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CompilerParser.assignment"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAssignment([NotNull] CompilerParser.AssignmentContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CompilerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpression([NotNull] CompilerParser.ExpressionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CompilerParser.expression"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpression([NotNull] CompilerParser.ExpressionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CompilerParser.number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNumber([NotNull] CompilerParser.NumberContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CompilerParser.number"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNumber([NotNull] CompilerParser.NumberContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CompilerParser.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterCondition([NotNull] CompilerParser.ConditionContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CompilerParser.condition"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitCondition([NotNull] CompilerParser.ConditionContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CompilerParser.if"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterIf([NotNull] CompilerParser.IfContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CompilerParser.if"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitIf([NotNull] CompilerParser.IfContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="CompilerParser.while"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterWhile([NotNull] CompilerParser.WhileContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="CompilerParser.while"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitWhile([NotNull] CompilerParser.WhileContext context);
}
