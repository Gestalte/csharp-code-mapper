using static System.Console;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace Collector
{
    public class Harvestor : CSharpSyntaxWalker
    {
        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            WriteLine($"Method {node.ReturnType} {node.Identifier} {node.ParameterList}");
        }

        public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            node.Declaration.Variables.ToList().ForEach(f => WriteLine($"Field {node.Declaration.Type} {f.Identifier}"));

        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            WriteLine($"Method {node.Type} {node.Identifier}");
        }
    }
}