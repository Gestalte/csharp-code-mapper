using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Diagnostics;

namespace CSharpCodeMapper.UnitTests
{
    [TestFixture]
    public class CollectorTests
    {
        const string programText =
@"using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace TopLevel
{
    using Microsoft;
    using System.ComponentModel;

    namespace Child1
    {
        using Microsoft.Win32;
        using System.Runtime.InteropServices;

        interface IFoo
        {
            void DoSomething();
            int AddSomething(int value1, int value2);
        }

        class Foo : IFoo
        {     
            enum Season
            {
                Spring,
                Summer,
                Autumn,
                Winter
            }

            enum ErrorCode : ushort
            {
                None = 0,
                Unknown = 1,
                ConnectionLost = 100,
                OutlierReading = 200
            }

            public Foo(){}

            public delegate int PerformCalculation(int x, int y);

            Action<string> stringAction = str => {};

            public int Age = 0;

            public void DoSomething(){}

            public int AddSomething(int value1, int value2){return value1+value2;}
        }
    }

    namespace Child2
    {
        using System.CodeDom;
        using Microsoft.CSharp;

        class Bar { }
    }
}";

        [Test]
        public void VisitReturnsDeclarationDtos()
        {
            SyntaxTree tree = CSharpSyntaxTree.ParseText(programText);
            CompilationUnitSyntax root = tree.GetCompilationUnitRoot();

            var collector = new DeclarationCollector.Collector();

            collector.Visit(root);

            collector.Display.ForEach(f=> Console.WriteLine($"{f.LineNumber} {f.DeclarationType} {f.Type} {f.Name}{f.Parameters}"));
            
            Assert.That(collector.Display, Is.Not.EqualTo(null));
        }
    }
}