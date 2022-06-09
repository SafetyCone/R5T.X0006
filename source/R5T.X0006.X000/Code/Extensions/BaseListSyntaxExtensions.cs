using System;

using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.L0011.X000.Generation.Initial.Simple;
using R5T.T0126;


namespace System
{
    public static class BaseListSyntaxExtensions
    {
        public static (BaseListSyntax, ISyntaxNodeAnnotation<BaseTypeSyntax>) AddBaseType(this BaseListSyntax baseList,
            BaseTypeSyntax baseType)
        {
            baseType = baseType.Annotate_Typed(out var baseTypeAnnotation);

            baseList = baseList.AddTypes(baseType);

            return (baseList, baseTypeAnnotation);
        }

        public static (BaseListSyntax, ISyntaxNodeAnnotation<BaseTypeSyntax>) AddBaseType(this BaseListSyntax baseList,
            string baseTypeName)
        {
            var baseType = SyntaxFactory.CreateBaseType(baseTypeName);

            return baseList.AddBaseType(baseType);
        }
    }
}
