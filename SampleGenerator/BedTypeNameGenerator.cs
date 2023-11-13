﻿using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace SampleGenerator
{
    [Generator]
    public class BedTypeNameGenerator : ISourceGenerator
    {
        public void Initialize(GeneratorInitializationContext context)
        {
            // not needed
        }

        public void Execute(GeneratorExecutionContext context)
        {
            // find all classes that end in "Bed" from the syntax tree
            var bedClasses = context.Compilation.SyntaxTrees
                .SelectMany(syntaxTree => syntaxTree.GetRoot().DescendantNodes())
                .OfType<ClassDeclarationSyntax>()
                .Where(classDeclaration => classDeclaration.Identifier.Text.EndsWith("Bed"));

            // find the ContainingNamespace of the library this generator is used on
            var mainMethod = context.Compilation.GetEntryPoint(context.CancellationToken);

            if (mainMethod is null)
            {
                return;
            }

            var containingNamespace = mainMethod.ContainingNamespace.ToDisplayString();

            // build up source
            var source = $@"// <auto-generated/>
using System;

namespace {containingNamespace}
{{
    public static class BedTypes
    {{";

            // add all the bed types
            foreach (var bedClass in bedClasses)
            {
                source += $@"          public const string {bedClass.Identifier.Text} = ""{bedClass.Identifier.Text}"";";
            }

            source += $@"
    }}
}}
";

            // Add the source code to the compilation
            context.AddSource($"BedTypes.g.cs", source);
        }
    }
}