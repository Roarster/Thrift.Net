namespace Thrift.Net.Tests.Compilation.Binding.FieldBinder
{
    using NSubstitute;
    using Thrift.Net.Compilation.Binding;
    using Thrift.Net.Compilation.Model;
    using Thrift.Net.Tests.Utility;
    using Xunit;

    public class FieldIdTests
    {
        private readonly IFieldContainerBinder containerBinder = Substitute.For<IFieldContainerBinder>();
        private readonly IBinderProvider binderProvider = Substitute.For<IBinderProvider>();
        private readonly FieldBinder binder;

        public FieldIdTests()
        {
            this.binder = new FieldBinder(this.containerBinder, this.binderProvider);
        }

        [Fact]
        public void Bind_FieldIdProvided_SetsFieldId()
        {
            // Arrange
            var fieldContext = ParserInput
                .FromString("1: i32 Id")
                .ParseInput(parser => parser.field());

            // Act
            var field = this.binder.Bind<FieldDefinition>(fieldContext);

            // Assert
            Assert.Equal(1, field.FieldId);
        }

        [Fact]
        public void Bind_FieldIdNotProvided_DefaultsIdToZero()
        {
            // Arrange
            var fieldContext = ParserInput
                .FromString("i32 Id")
                .ParseInput(parser => parser.field());

            // Act
            var field = this.binder.Bind<FieldDefinition>(fieldContext);

            // Assert
            Assert.Equal(0, field.FieldId);
        }

        [Fact]
        public void Bind_FieldIdNotProvided_UsesSiblingToSetId()
        {
            // Arrange
            var previousField = CreateFieldWithId(5);

            var fieldContext = ParserInput
                .FromString("i32 Id")
                .ParseInput(parser => parser.field());
            this.containerBinder.GetPreviousSibling(fieldContext).Returns(previousField);

            // Act
            var field = this.binder.Bind<FieldDefinition>(fieldContext);

            // Assert
            Assert.Equal(6, field.FieldId);
        }

        [Fact]
        public void Bind_FieldIdNegative_SetsFieldId()
        {
            // Arrange
            var fieldContext = ParserInput
                .FromString("-1: i32 Id")
                .ParseInput(parser => parser.field());

            // Act
            var field = this.binder.Bind<FieldDefinition>(fieldContext);

            // Assert
            Assert.Equal(-1, field.FieldId);
        }

        [Fact]
        public void Bind_FieldIdSupplied_SetsRawFieldId()
        {
            // Arrange
            var fieldContext = ParserInput
                .FromString("4: i32 Id")
                .ParseInput(parser => parser.field());

            // Act
            var field = this.binder.Bind<FieldDefinition>(fieldContext);

            // Assert
            Assert.Equal("4", field.RawFieldId);
        }

        [Fact]
        public void Bind_FieldIdNotAnInteger_SetsFieldIdNull()
        {
            // Arrange
            var fieldContext = ParserInput
                .FromString("abc: i32 Id")
                .ParseInput(parser => parser.field());

            // Act
            var field = this.binder.Bind<FieldDefinition>(fieldContext);

            // Assert
            Assert.Null(field.FieldId);
        }

        private static FieldDefinition CreateFieldWithId(int fieldId)
        {
            return new FieldDefinition(
                fieldId,
                fieldId.ToString(),
                FieldRequiredness.Default,
                FieldType.Bool,
                "IsEnabled");
        }
    }
}