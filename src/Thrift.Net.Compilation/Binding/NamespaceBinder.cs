namespace Thrift.Net.Compilation.Binding
{
    using Thrift.Net.Compilation.Symbols;
    using Thrift.Net.Compilation.Symbols.Builders;
    using static Thrift.Net.Antlr.ThriftParser;

    /// <summary>
    /// Used to bind <see cref="NamespaceStatementContext" /> objects into
    /// <see cref="Namespace" /> objects.
    /// </summary>
    public class NamespaceBinder : Binder<NamespaceStatementContext, Namespace, IDocument>
    {
        private readonly IBinderProvider binderProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="NamespaceBinder" /> class.
        /// </summary>
        /// <param name="binderProvider">Used to get binders for nodes.</param>
        public NamespaceBinder(IBinderProvider binderProvider)
        {
            this.binderProvider = binderProvider;
        }

        /// <inheritdoc />
        protected override Namespace Bind(NamespaceStatementContext node, IDocument parent)
        {
            var builder = new NamespaceBuilder()
                .SetNode(node)
                .SetParent(parent)
                .SetBinderProvider(this.binderProvider)
                .SetScope(node.namespaceScope?.Text)
                .SetNamespaceName(node.ns?.Text);

            return builder.Build();
        }
    }
}