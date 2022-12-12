using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Linq;

using static System.Console;

const string programText =
@"using System;
using System.Collections;
using System.Linq;
using System.Text;

namespace HelloWorld
{
    class Program
    {
        private int Field = 0;

        private int Age{get;set;}

        static void Main(string[] args)
        {
            Console.WriteLine(""Hello, World!"");
        }

        private void DoSomething(int var1, string var 2){}
    }
}";

SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);

CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

void WriteStuff(SyntaxNode member, int indentLevel)
{
    string indent(string str)
    {
        var result = "";
        for (int i = 0; i < indentLevel; i++)
        {
            result += "\t";
        }
        return result + str;
    }

    switch (member.Kind())
    {
        case SyntaxKind.NamespaceDeclaration:

            var namespaceDeclarationSyntax = (NamespaceDeclarationSyntax)member;
            WriteLine(indent(member.Kind() + " " + namespaceDeclarationSyntax.Name));
            
            break;

        case SyntaxKind.FileScopedNamespaceDeclaration:

            var fileScopedNamespaceDeclarationSyntax = (FileScopedNamespaceDeclarationSyntax)member;
            WriteLine(indent(member.Kind() + " " + fileScopedNamespaceDeclarationSyntax.Name));
            break;

        case SyntaxKind.ClassDeclaration:

            var classDeclarationSyntax = (ClassDeclarationSyntax)member;
            WriteLine(indent(member.Kind() + " " + classDeclarationSyntax.Identifier));
            break;

        case SyntaxKind.StructDeclaration:
        case SyntaxKind.InterfaceDeclaration:
        case SyntaxKind.EnumDeclaration:
        case SyntaxKind.DelegateDeclaration:
        case SyntaxKind.EnumMemberDeclaration:
        case SyntaxKind.FieldDeclaration:

            var FieldDeclarationSyntax = (FieldDeclarationSyntax)member;
            FieldDeclarationSyntax.Declaration.Variables.ToList().ForEach(f => WriteLine(indent(member.Kind() + " " + f.Identifier)));
            break;


        case SyntaxKind.EventFieldDeclaration:
        case SyntaxKind.MethodDeclaration:

            var MethodDeclarationSyntax = (MethodDeclarationSyntax)member;
            WriteLine(indent(member.Kind() + " " + MethodDeclarationSyntax.Identifier+ MethodDeclarationSyntax.ParameterList.GetText().ToString()));
            break;

        case SyntaxKind.ConstructorDeclaration:
        case SyntaxKind.DestructorDeclaration:
        case SyntaxKind.PropertyDeclaration:

            var PropertyDeclarationSyntax = (PropertyDeclarationSyntax)member;
            WriteLine(indent(member.Kind() + " " + PropertyDeclarationSyntax.Identifier));
            break;

        case SyntaxKind.EventDeclaration:
        case SyntaxKind.IndexerDeclaration:
        case SyntaxKind.RecordDeclaration:
        case SyntaxKind.RecordStructDeclaration:
            WriteLine(member.Kind() + " " + nameof(member));
            break;
    }

    member.ChildNodes().ToList().ForEach(f=>WriteStuff(f,indentLevel++));
    indentLevel--;
}

root.Members.ToList().ForEach(f => WriteStuff(f,0));