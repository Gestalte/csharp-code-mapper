using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeclarationCollector
{
    public class Collector
    {
        public static List<DeclarationDto> GetDeclarations(string sourceCode)
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(sourceCode);

            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

            var walker = new DeclarationCollector.SyntaxWalker();

            walker.Visit(root);

            return walker.Display;
        }
    }
}
