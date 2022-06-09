using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;


namespace System
{
    using NamespaceAnnotation = ISyntaxNodeAnnotation<NamespaceDeclarationSyntax>;


    public static class NamespaceAnnotationExtensions
    {
        public static string GetFullName(this NamespaceAnnotation namespaceAnnotation,
            CompilationUnitSyntax compilationUnit)
        {
            var output = namespaceAnnotation.Get(
                compilationUnit,
                @namespace => @namespace.GetFullName());

            return output;
        }
    }
}
