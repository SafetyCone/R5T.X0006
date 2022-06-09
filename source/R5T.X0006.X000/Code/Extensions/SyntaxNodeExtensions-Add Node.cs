using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;

using R5T.T0126;


namespace System
{
    public static partial class SyntaxNodeExtensions
    {
        /// <summary>
        /// Method establishing a framework for adding a node to a parent node.
        /// Framework returns an annotation to the node, and a new parent node instance modified to include the node to be added.
        /// </summary>
        /// <typeparam name="TParentNode">The node to which a node should be added.</typeparam>
        /// <typeparam name="TNode">The node to add.</typeparam>
        public static (TParentNode, ISyntaxNodeAnnotation<TNode>) AddNode<TParentNode, TNode>(this TParentNode parentNode,
            TNode node,
            Func<TNode, TNode> preAdd,
            Func<TParentNode, TNode, TParentNode> add,
            Func<TParentNode, ISyntaxNodeAnnotation<TNode>, TParentNode> postAdd)
            where TParentNode : SyntaxNode
            where TNode : SyntaxNode
        {
            node = preAdd(node);

            node = node.Annotate_Typed(out var annotation);

            parentNode = add(parentNode, node);

            parentNode = postAdd(parentNode, annotation);

            return (parentNode, annotation);
        }

        /// <inheritdoc cref="AddNode{TParentNode, TNode}(TParentNode, TNode, Func{TNode, TNode}, Func{TParentNode, TNode, TParentNode}, Func{TParentNode, ISyntaxNodeAnnotation{TNode}, TParentNode})"/>
        public static async Task<(TParentNode, ISyntaxNodeAnnotation<TNode>)> AddNode<TParentNode, TNode>(this TParentNode parentNode,
            TNode node,
            Func<TNode, Task<TNode>> preAdd,
            Func<TParentNode, TNode, Task<TParentNode>> add,
            Func<TParentNode, ISyntaxNodeAnnotation<TNode>, Task<TParentNode>> postAdd)
            where TParentNode : SyntaxNode
            where TNode : SyntaxNode
        {
            node = await preAdd(node);

            node = node.Annotate_Typed(out var annotation);

            parentNode = await add(parentNode, node);

            parentNode = await postAdd(parentNode, annotation);

            return (parentNode, annotation);
        }
    }
}
