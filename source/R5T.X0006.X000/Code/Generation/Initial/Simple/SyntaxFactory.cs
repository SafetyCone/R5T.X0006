using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;

using UnannotatedSyntaxFactory = R5T.L0011.X000.Generation.Initial.Simple.SyntaxFactory;


namespace R5T.X0006.X000.Generation.Initial.Simple
{
    public static class SyntaxFactory
    {
        public static AnnotatedNode<AccessorDeclarationSyntax> CreateAccessorGet_NoSemicolon()
        {
            var untypedAnnotatedNode = UnannotatedSyntaxFactory.CreateAccessorGet_NoSemicolon_Annotated();

            var output = AnnotatedNode.From(untypedAnnotatedNode);
            return output;
        }

        public static AnnotatedNode<PropertyDeclarationSyntax> CreateProperty(
            string typeName,
            string propertyName)
        {
            var untypedAnnotatedNode = UnannotatedSyntaxFactory.CreateProperty_Annotated(typeName, propertyName);

            var output = AnnotatedNode.From(untypedAnnotatedNode);
            return output;
        }
    }
}
