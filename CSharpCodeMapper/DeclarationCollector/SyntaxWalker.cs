using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Collections.Generic;
using System.Linq;

namespace DeclarationCollector
{
    public class SyntaxWalker : CSharpSyntaxWalker
    {
        public List<DeclarationDto> Display { get; set; }

        public SyntaxWalker()
        {
            Display = new List<DeclarationDto>();
        }

        public static int GetLineNumber(CSharpSyntaxNode node)
        {
            var parent = node.SyntaxTree.GetRoot();
            var text = parent.GetText();
            var lineNum = text.Lines.GetLineFromPosition(node.Span.Start).LineNumber;

            return lineNum;
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            Display.Add(new DeclarationDto
                 (GetLineNumber(node)
                 , "Method"
                 , node.Identifier.ToString()
                 , node.ReturnType.ToString()
                 , node.ParameterList.ToString()
                 ));

            base.VisitMethodDeclaration(node);
        }

        public override void VisitFieldDeclaration(FieldDeclarationSyntax node)
        {
            node.Declaration.Variables.ToList()
                .ForEach(f => Display.Add(new DeclarationDto(GetLineNumber(node), "Field", f.Identifier.ToString(), f.GetLeadingTrivia().ToString())));

            base.VisitFieldDeclaration(node);
        }

        public override void VisitPropertyDeclaration(PropertyDeclarationSyntax node)
        {
            Display.Add(new DeclarationDto
                (GetLineNumber(node)
                , "Property"
                , node.Identifier.ToString()
                , node.Type.ToString()
                ));

            base.VisitPropertyDeclaration(node);
        }

        public override void VisitClassDeclaration(ClassDeclarationSyntax node)
        {
            Display.Add(new DeclarationDto
                (GetLineNumber(node)
                , "Class"
                , node.Identifier.ToString()
                ));

            base.VisitClassDeclaration(node);
        }

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            Display.Add(new DeclarationDto
                (GetLineNumber(node)
                , "Constructor"
                , node.Identifier.ToString()
                , parameters: node.ParameterList.ToString()
                ));

            base.VisitConstructorDeclaration(node);
        }

        public override void VisitDelegateDeclaration(DelegateDeclarationSyntax node)
        {
            Display.Add(new DeclarationDto
                (GetLineNumber(node)
                , "Delegate"
                , node.Identifier.ToString()
                , node.ReturnType.ToString()
                , parameters: node.ParameterList.ToString()
                ));

            base.VisitDelegateDeclaration(node);
        }

        public override void VisitDestructorDeclaration(DestructorDeclarationSyntax node)
        {
            Display.Add(new DeclarationDto
                (GetLineNumber(node)
                , "Destructor"
                , node.Identifier.ToString()
                , parameters: node.ParameterList.ToString()
                ));

            base.VisitDestructorDeclaration(node);
        }

        public override void VisitEnumDeclaration(EnumDeclarationSyntax node)
        {
            Display.Add(new DeclarationDto
                (GetLineNumber(node)
                , "Enum"
                , node.Identifier.ToString()
                ));

            base.VisitEnumDeclaration(node);
        }

        public override void VisitEventDeclaration(EventDeclarationSyntax node)
        {
            Display.Add(new DeclarationDto
                (GetLineNumber(node)
                , "Event"
                , node.Identifier.ToString()
                , node.Type.ToString()
                ));

            base.VisitEventDeclaration(node);
        }

        public override void VisitInterfaceDeclaration(InterfaceDeclarationSyntax node)
        {
            Display.Add(new DeclarationDto
                (GetLineNumber(node)
                , "Interface"
                , node.Identifier.ToString()
                , parameters: node.TypeParameterList?.ToString() ?? ""
                ));

            base.VisitInterfaceDeclaration(node);
        }

        public override void VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
        {
            Display.Add(new DeclarationDto
                (GetLineNumber(node)
                , "Namespace"
                , node.Name.ToString()
                ));

            base.VisitNamespaceDeclaration(node);
        }

        public override void VisitRecordDeclaration(RecordDeclarationSyntax node)
        {
            Display.Add(new DeclarationDto
                (GetLineNumber(node)
                , "Namespace"
                , node.Identifier.ToString()
                , parameters: node.ParameterList?.ToString() ?? ""
                ));

            base.VisitRecordDeclaration(node);
        }

        public override void VisitStructDeclaration(StructDeclarationSyntax node)
        {
            Display.Add(new DeclarationDto
                (GetLineNumber(node)
                , "Struct"
                , node.Identifier.ToString()
                ));

            base.VisitStructDeclaration(node);
        }
    }
}