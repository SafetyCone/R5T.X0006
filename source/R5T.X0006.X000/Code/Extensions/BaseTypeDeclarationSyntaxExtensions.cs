using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;


namespace System
{
    public static class BaseTypeDeclarationSyntaxExtensions
    {
        public static (T, ISyntaxNodeAnnotation<BaseListSyntax>) AcquireBaseList<T>(this T baseType)
            where T : BaseTypeDeclarationSyntax
        {
            var hasBaseList = baseType.HasBaseTypesList();

            if(hasBaseList)
            {
                var annotatedBaseList = hasBaseList.Result.Annotate_Typed(out var baseListAnnotation);

                baseType = baseType.WithBaseList(annotatedBaseList) as T;

                return (baseType, baseListAnnotation);
            }
            else
            {
                return baseType.AddBaseList();
            }
        }

        public static (T, ISyntaxNodeAnnotation<BaseListSyntax>) AddBaseList<T>(this T baseType)
            where T : BaseTypeDeclarationSyntax
        {
            var annotatedBaseList = SyntaxFactoryHelper.BaseList()
                .ModifyWith(baseList =>
                {
                    baseList = baseList.WithColonToken(
                        baseList.ColonToken
                            .AddLeadingTrivia(
                                SyntaxTriviaHelper.Space()));

                    return baseList;
                })
                .Annotate_Typed(out var baseListAnnotation);

            baseType = baseType.WithBaseList(annotatedBaseList) as T;

            return (baseType, baseListAnnotation);
        }

        public static SyntaxNodeCreationResult<TParent, BaseListSyntax> AcquireBaseList<T, TParent>(this ISyntaxNodeAnnotation<T> baseTypeAnnotation,
            TParent parentNode)
            where T : BaseTypeDeclarationSyntax
            where TParent : SyntaxNode
        {
            var hasBaseList = baseTypeAnnotation.Get(
                parentNode,
                baseType => baseType.HasBaseTypesList());

            if(hasBaseList)
            {
                var annotatedBaseList = hasBaseList.Result.Annotate_Typed(out var baseListAnnotation);

                parentNode = parentNode.Modify_TypedSynchronous(
                    baseTypeAnnotation,
                    baseType => baseType.WithBaseList(annotatedBaseList) as T);

                return SyntaxCreationResult.Node(
                    parentNode,
                    baseListAnnotation);
            }
            else
            {
                return baseTypeAnnotation.AddBaseList(parentNode);
            }
        }

        public static SyntaxNodeCreationResult<TParent, BaseListSyntax> AddBaseList<T, TParent>(this ISyntaxNodeAnnotation<T> baseTypeAnnotation,
            TParent parentNode)
            where T : BaseTypeDeclarationSyntax
            where TParent : SyntaxNode
        {
            var baseList = SyntaxFactoryHelper.BaseList()
                .Annotate_Typed(out var annotation);

            parentNode = parentNode.Modify_TypedSynchronous(
                baseTypeAnnotation,
                baseType => baseType.WithBaseList(baseList) as T);

            return SyntaxCreationResult.Node(
                parentNode,
                annotation);
        }
    }
}
