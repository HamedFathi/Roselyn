using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Formatting;
using RoslynQuoter;

namespace Roselyn
{
    public static class RoslynExtensions
    {
        public static string FormatCsharpSourceCode(this string sourceCode)
        {
            var tree = CSharpSyntaxTree.ParseText(sourceCode);
            var root = (CompilationUnitSyntax)tree.GetRoot();
            var workspace = new AdhocWorkspace();
            var syntax = Formatter.Format(root, workspace);
            return syntax.GetText().ToString();
        }
        public static SyntaxNode FormatCsharpSourceCode(this SyntaxNode syntaxNode)
        {
            var workspace = new AdhocWorkspace();
            return Formatter.Format(syntaxNode, workspace);
        }
        public static string ToRoslynAPI(this string source, bool useDefaultFormatting = true, bool openParenthesisOnNewLine = false
                        , bool closingParenthesisOnNewLine = false, bool removeRedundantModifyingCalls = false, bool shortenCodeWithUsingStatic = false)
        {
            var quoter = new Quoter()
            {
                UseDefaultFormatting = useDefaultFormatting,
                ShortenCodeWithUsingStatic = shortenCodeWithUsingStatic,
                RemoveRedundantModifyingCalls = removeRedundantModifyingCalls,
                OpenParenthesisOnNewLine = openParenthesisOnNewLine,
                ClosingParenthesisOnNewLine = closingParenthesisOnNewLine
            };
            return quoter.Quote(source).ToString();
        }
    }
}
