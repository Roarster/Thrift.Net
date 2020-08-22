namespace Thrift.Net.Tests.Compilation.ThriftCompiler
{
    using System.Linq;
    using Thrift.Net.Compilation;
    using Thrift.Net.Compilation.Resources;
    using Thrift.Net.Tests.Extensions;
    using Xunit;

    public class NamespaceErrorTests : ThriftCompilerTests
    {
        [Fact]
        public void Compile_UnrecognisedNamespace_ReportsError()
        {
            this.AssertCompilerReturnsErrorId(
                "namespace $notalang$ mynamespace",
                CompilerMessageId.NamespaceScopeUnknown);
        }

        [Fact]
        public void Compile_UnrecognisedNamespace_IncludesScopeInErrorMessage()
        {
            this.AssertCompilerReturnsErrorMessage(
                "namespace notalang mynamespace",
                CompilerMessageId.NamespaceScopeUnknown,
                "notalang");
        }

        [Theory]
        [InlineData("*")]
        [InlineData("c_glib")]
        [InlineData("cpp")]
        [InlineData("csharp")]
        [InlineData("delphi")]
        [InlineData("go")]
        [InlineData("java")]
        [InlineData("js")]
        [InlineData("lua")]
        [InlineData("netcore")]
        [InlineData("perl")]
        [InlineData("php")]
        [InlineData("py")]
        [InlineData("py.twisted")]
        [InlineData("rb")]
        [InlineData("st")]
        [InlineData("xsd")]
        public void Compile_RecognizedNamespaceScope_DoesNotReportError(string scope)
        {
            // Arrange
            var compiler = new ThriftCompiler();
            var input = $"namespace {scope} MyNamespace";

            // Act
            var result = compiler.Compile(input.ToStream());

            // Assert
            Assert.Empty(result.Errors);
        }

        [Fact]
        public void Compile_ScopeMissing_ReportsError()
        {
            this.AssertCompilerReturnsErrorId(
                "$namespace mynamespace$",
                CompilerMessageId.NamespaceScopeMissing);
        }

        [Fact]
        public void Compile_NamespaceMissing_ReportsError()
        {
            this.AssertCompilerReturnsErrorId(
                "$namespace csharp$",
                CompilerMessageId.NamespaceMissing);
        }

        [Fact]
        public void Compile_ScopeAndNamespaceMissing_ReportsError()
        {
            this.AssertCompilerReturnsErrorId(
                "$namespace$",
                CompilerMessageId.NamespaceAndScopeMissing);
        }
    }
}