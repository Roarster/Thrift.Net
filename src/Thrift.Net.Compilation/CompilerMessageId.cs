namespace Thrift.Net.Compilation
{
    /// <summary>
    /// A list of messages output by the Thrift compiler.
    /// </summary>
    public enum CompilerMessageId
    {
        /// <summary>
        /// An enum has been defined without a name being specified.
        /// For example `enum {}`
        /// </summary>
        EnumMustHaveAName = 0,

        /// <summary>
        /// An enum member has been defined without a name being specified.
        /// For example `enum { = 1 }`.
        /// </summary>
        EnumMemberMustHaveAName = 1,

        /// <summary>
        /// An enum member has been defined with a negative value. For example
        /// `enum UserType { User = -1 }`.
        /// </summary>
        EnumValueMustNotBeNegative = 2,

        /// <summary>
        /// An enum member has been defined with a value that isn't an int. For
        /// example `enum UserType { User = 'hello' }`.
        /// </summary>
        EnumValueMustBeAnInteger = 3,

        /// <summary>
        /// An enum member has been defined but the value is missing from the
        /// assign expression. For example `enum UserType { User = }`.
        /// </summary>
        EnumValueMustBeSpecified = 4,

        /// <summary>
        /// The equals operator is missing between an enum member and its value.
        /// For example `enum UserType { User 1 }`.
        /// </summary>
        EnumMemberEqualsOperatorMissing = 5,

        /// <summary>
        /// An enum has been defined with no members. For example
        /// `enum MyEnum {}`.
        /// </summary>
        EnumEmpty = 6,

        /// <summary>
        /// The same enum member has been declared multiple times.
        /// For example `enum MyEnum { Member1, Member1 }`.
        /// </summary>
        EnumMemberDuplicated = 7,

        /// <summary>
        /// Another item has already been declared with the same name
        /// For example:
        /// ```thrift
        /// enum UserType {}
        /// struct UserType {}
        /// ```
        /// </summary>
        NameAlreadyDeclared = 8,

        /// <summary>
        /// The specified namespace scope is not in the list of known namespaces.
        /// For example `namespace notalang mynamespace`.
        /// </summary>
        NamespaceScopeUnknown = 100,

        /// <summary>
        /// A namespace has been specified without a scope. For example
        /// `namespace mynamespace`.
        /// </summary>
        NamespaceScopeMissing = 101,

        /// <summary>
        /// The namespace keyword has been specified, but without a scope or
        /// namespace being provided. For example `namespace`.
        /// </summary>
        NamespaceAndScopeMissing = 102,

        /// <summary>
        /// A namespace scope has been specified without a corresponding
        /// namespace being provided. For example `namespace csharp`.
        /// </summary>
        NamespaceMissing = 103,

        /// <summary>
        /// The same namespace scope has been specified multiple times. For example:
        /// ```
        /// namespace csharp Thrift.Net.Examples.A
        /// namespace csharp Thrift.Net.Examples.B
        /// ```
        /// </summary>
        NamespaceScopeAlreadySpecified = 104,

        /// <summary>
        /// The namespace statement has been terminated with a list separator.
        /// For example:
        /// ```
        /// namespace csharp Thrift.Net.Examples,
        /// namespace netstd Thrift.Net.Examples;
        /// </summary>
        NamespaceStatementTerminatedBySeparator = 105,

        /// <summary>
        /// A struct has been defined without a name. For example:
        /// `struct {}`.
        /// </summary>
        StructMustHaveAName = 200,

        /// <summary>
        /// The specified field name has already been used in the same struct.
        /// For example:
        /// ```
        /// struct User {
        ///   i32 Username
        ///   string Username
        /// }
        /// ```
        /// </summary>
        StructFieldNameAlreadyDefined = 201,

        /// <summary>
        /// The specified field Id has already been defined in the same struct.
        /// For example:
        /// ```
        /// struct User {
        ///   0: i32 Id,
        ///   0: string Username
        /// }
        /// ```
        /// </summary>
        StructFieldIdAlreadyDefined = 202,

        /// <summary>
        /// The specified field Id is not a positive integer. For example:
        /// ```
        /// struct User {
        ///   abc: i32 Id
        ///   -1: string Username
        /// }
        /// ```
        /// </summary>
        StructFieldIdMustBeAPositiveInteger = 203,

        /// <summary>
        /// The `slist` type is deprecated, and should no-longer be used. Please
        /// use the `string` type instead. For example:
        /// ```
        /// struct User {
        ///   slist Username
        /// }
        /// ```
        /// </summary>
        SlistDeprecated = 204,

        /// <summary>
        /// A field has been specified without a field Id. This can lead to backwards
        /// incompatible changes being made to the protocol by accident. For example:
        /// ```
        /// struct User {
        ///   string Username
        /// }
        /// ```
        /// </summary>
        FieldIdNotSpecified = 205,

        /// <summary>
        /// A syntax error has been reported by the Antlr parser.
        /// </summary>
        GenericParseError = 300,
    }
}