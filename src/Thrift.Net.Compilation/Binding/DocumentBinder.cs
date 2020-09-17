namespace Thrift.Net.Compilation.Binding
{
    using System.Linq;
    using Thrift.Net.Compilation.Symbols;
    using Thrift.Net.Compilation.Symbols.Builders;
    using static Thrift.Net.Antlr.ThriftParser;

    /// <summary>
    /// Used to bind <see cref="Document" /> objects from <see cref="DocumentContext" />
    /// nodes.
    /// </summary>
    public class DocumentBinder : Binder<DocumentContext, Document>, IDocumentBinder
    {
        private readonly IBinderProvider binderProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="DocumentBinder" /> class.
        /// </summary>
        /// <param name="parent">The parent binder.</param>
        /// <param name="binderProvider">Used to get other binders.</param>
        public DocumentBinder(IBinder parent, IBinderProvider binderProvider)
            : base(parent)
        {
            this.binderProvider = binderProvider;
        }

        /// <inheritdoc />
        public bool IsEnumAlreadyDeclared(string enumName, EnumDefinitionContext enumNode)
        {
            var parent = enumNode.Parent as DefinitionsContext;

            return parent.enumDefinition()
                .Select(sibling => this.binderProvider
                    .GetBinder(sibling)
                    .Bind<Enum>(sibling))
                .Where(sibling => sibling.Name == enumName)
                .TakeWhile(sibling => sibling.Node != enumNode)
                .Any();
        }

        /// <inheritdoc />
        protected override Document Bind(DocumentContext node)
        {
            var documentBuilder = new DocumentBuilder()
                .SetNode(node)
                .AddNamespaces(node.header()?.namespaceStatement()
                    .Select(namespaceNode => this.binderProvider
                        .GetBinder(namespaceNode)
                        .Bind<Namespace>(namespaceNode)))
                .AddEnums(node.definitions().enumDefinition()
                    .Select(enumNode => this.binderProvider
                        .GetBinder(enumNode)
                        .Bind<Enum>(enumNode)))
                .AddStructs(node.definitions().structDefinition()
                    .Select(structNode => this.binderProvider
                        .GetBinder(structNode)
                        .Bind<Struct>(structNode)));

            return documentBuilder.Build();
        }
    }
}