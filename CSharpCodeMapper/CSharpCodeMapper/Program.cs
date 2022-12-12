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
void Main()
{
	Stock stock=new Stock(""THPW"");
	stock.Price=27.10M;
	stock.PriceChanged += stock_PriceChanged; // Subscribe to the event.
	stock.Price=31.59M;
}

// You can define other methods, fields, classes and namespaces here

static void stock_PriceChanged(object sender,PriceChangedEventArgs e)
{
	if((e.NewPrice-e.LastPrice)/e.LastPrice>0.1M)
	{
		Console.WriteLine(""Alert, 10% stock price increase!"");
	}
}

// EventArgs is a base class for converying information for an event.
public class PriceChangedEventArgs:System.EventArgs
{
	public readonly decimal LastPrice;
	public readonly decimal NewPrice;
	
	public PriceChangedEventArgs(decimal lastPrice,decimal newPrice)
	{
		LastPrice = lastPrice;
		NewPrice = newPrice;
	}
}

public class Stock
{
	string symbol;
	decimal price;

	public Stock(string symbol) {this.symbol=symbol;}

	public event Action<object, PriceChangedEventArgs> PriceChanged;
	
	// The pattern required that you write a protected virtual method that
	// fires the event. The name must match the name of the event, prefixed
	// with the word On, and then accept a single EventArgs argument.
	protected virtual void OnPriceChanged(PriceChangedEventArgs e)
	{
		// In multithreaded scenarios, you need to assign the delegate to a 
		// temporary variable before testing and invoking it, to avoid an 
		// obvious thread-safety error.
		var temp=PriceChanged;
		if(temp!=null) temp(this,e);
	}
	
	public decimal Price
	{
		get{return price;}
		set
		{
			if(price==value)return;
			decimal oldPrice=price;
			price=value;
			OnPriceChanged(new PriceChangedEventArgs(oldPrice,price));
		}
	}
}
}";

SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);

CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

void WriteStuff(SyntaxNode member, int indentLevel)
{
    switch (member.Kind())
    {
        case SyntaxKind.NamespaceDeclaration:

            var namespaceDeclarationSyntax = (NamespaceDeclarationSyntax)member;
            WriteLine(member.Kind() + " " + namespaceDeclarationSyntax.Name);
            
            break;

        case SyntaxKind.FileScopedNamespaceDeclaration:

            var fileScopedNamespaceDeclarationSyntax = (FileScopedNamespaceDeclarationSyntax)member;
            WriteLine(member.Kind() + " " + fileScopedNamespaceDeclarationSyntax.Name);
            break;

        case SyntaxKind.ClassDeclaration:

            var classDeclarationSyntax = (ClassDeclarationSyntax)member;
            WriteLine(member.Kind() + " " + classDeclarationSyntax.Identifier);
            break;

        case SyntaxKind.StructDeclaration:
        case SyntaxKind.InterfaceDeclaration:
        case SyntaxKind.EnumDeclaration:
        case SyntaxKind.DelegateDeclaration:
        case SyntaxKind.EnumMemberDeclaration:
        case SyntaxKind.FieldDeclaration:

            var FieldDeclarationSyntax = (FieldDeclarationSyntax)member;
            FieldDeclarationSyntax.Declaration.Variables.ToList().ForEach(f => WriteLine(member.Kind() + " " + f.Identifier));
            break;

        case SyntaxKind.EventFieldDeclaration:

            var EventFieldDeclarationSyntax = (EventFieldDeclarationSyntax)member;
            EventFieldDeclarationSyntax.Declaration.Variables.ToList().ForEach(f => WriteLine(member.Kind() + " " + f.Identifier));
            break;

        case SyntaxKind.MethodDeclaration:

            var MethodDeclarationSyntax = (MethodDeclarationSyntax)member;
            WriteLine(member.Kind() + " " + MethodDeclarationSyntax.ReturnType.ToString() + " " + MethodDeclarationSyntax.Identifier+ MethodDeclarationSyntax.ParameterList.GetText().ToString());
            break;

        case SyntaxKind.ConstructorDeclaration:

            var ConstructorDeclarationSyntax = (ConstructorDeclarationSyntax)member;
            WriteLine(member.Kind() + " " + ConstructorDeclarationSyntax.Identifier + ConstructorDeclarationSyntax.ParameterList.GetText().ToString());
            break;

        case SyntaxKind.DestructorDeclaration:
        case SyntaxKind.PropertyDeclaration:

            var PropertyDeclarationSyntax = (PropertyDeclarationSyntax)member;
            WriteLine(member.Kind() + " " + PropertyDeclarationSyntax.Identifier);
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