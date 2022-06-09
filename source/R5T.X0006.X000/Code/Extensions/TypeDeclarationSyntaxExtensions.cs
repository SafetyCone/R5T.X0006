using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;


namespace System
{
    public static class TypeDeclarationSyntaxExtensions
    {
        public static (T, ISyntaxNodeAnnotation<PropertyDeclarationSyntax>) AddProperty<T>(this T type,
            PropertyDeclarationSyntax property)
            where T : TypeDeclarationSyntax
        {
            property = property.Annotate_Typed(out var propertyAnnotation);

            type = type.AddProperty_Simple(property);

            return (type, propertyAnnotation);
        }

        public static (T, ISyntaxNodeAnnotation<PropertyDeclarationSyntax>) AddProperty<T>(this T type,
            Func<PropertyDeclarationSyntax> propertyConstructor)
            where T : TypeDeclarationSyntax
        {
            var property = propertyConstructor();

            return type.AddProperty(property);
        }

        public static async Task<(T, ISyntaxNodeAnnotation<PropertyDeclarationSyntax>)> AddProperty<T>(this T type,
            Func<Task<PropertyDeclarationSyntax>> propertyConstructor)
            where T : TypeDeclarationSyntax
        {
            var property = await propertyConstructor();

            return type.AddProperty(property);
        }
    }
}
