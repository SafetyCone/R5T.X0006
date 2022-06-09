using System;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.T0126;


namespace System
{
    public static class MemberDeclarationSyntaxExtensions
    {
        public static (T, ISyntaxTokenAnnotation) AddStaticModifier<T>(this T member)
            where T : MemberDeclarationSyntax
        {
            // First, create and annotate the static token.
            var staticToken = SyntaxTokens.Static();

            staticToken = staticToken.Annotate_Typed(out var annotation);

            // Now insert the static token at the proper place.
            member = member.ModifyModifiers(modifiers =>
            {
                // First find the position at which to add the static modifier, which is after the last access modifier.
                var indexForStaticToken = modifiers.GetFirstIndexAvailableForStaticModifier();

                // If the static modifier is not the first modifier, add a separating space.
                if(IndexHelper.IsNotFirstIndex(indexForStaticToken))
                {
                    staticToken = staticToken.PrependSpace();
                }

                modifiers = modifiers.Insert(indexForStaticToken, staticToken);

                return modifiers;
            });

            return (member, annotation);
        }

        public static (T, ISyntaxTokenAnnotation) AddPartialModifier<T>(this T member)
            where T : MemberDeclarationSyntax
        {
            // First, create and annotate the partial token.
            var partialToken = SyntaxTokens.Partial();

            partialToken = partialToken.Annotate_Typed(out var annotation);

            // In insert the partial token at the proper place.
            member = member.ModifyModifiers(modifiers =>
            {
                // First find the position at which to add the partial modifier, which is after the last access modifier.
                var indexForPartialToken = modifiers.GetIndexForPartialModifier();

                // If the partial modifier is not the first modifier, prepend a separating space.
                if(IndexHelper.IsNotFirstIndex(indexForPartialToken))
                {
                    partialToken = partialToken.PrependSpace();
                }

                modifiers = modifiers.Insert(indexForPartialToken, partialToken);

                return modifiers;
            });

            return (member, annotation);
        }

        public static (T, ISyntaxTokenAnnotation) MakePartial<T>(this T member)
            where T : MemberDeclarationSyntax
        {
            var hasPartialModifier = member.HasPartialModifier();
            if (hasPartialModifier)
            {
                // If the member is already partial, skip.
                member = member.AnnotateToken_Typed(
                    hasPartialModifier.Result,
                    out var annotation);

                return (member, annotation);
            }
            else
            {
                // If not already static, add the partial modifier.
                return member.AddPartialModifier();
            }
        }

        /// <summary>
        /// Returns the member and a typed annotation for the static modifier token.
        /// </summary>
        public static (T, ISyntaxTokenAnnotation) MakeStatic<T>(this T member)
            where T : MemberDeclarationSyntax
        {
            var hasStaticModifier = member.HasStaticModifier();
            if (hasStaticModifier)
            {
                // If the member is already static, skip.
                member = member.AnnotateToken_Typed(
                    hasStaticModifier.Result,
                    out var annotation);

                return (member, annotation);
            }
            else
            {
                // If not already static, add the static modifier.
                return member.AddStaticModifier();
            }
        }
    }
}
