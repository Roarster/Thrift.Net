namespace Thrift.Net.Compilation.Model
{
    using System.Collections.Generic;

    /// <summary>
    /// Represents the type of a field.
    /// </summary>
    public class FieldType : ISymbol
    {
        /// <summary>
        /// The 'bool' base type.
        /// </summary>
        public static readonly FieldType Bool = CreateResolvedType("bool");

        /// <summary>
        /// The 'byte' base type.
        /// </summary>
        public static readonly FieldType Byte = CreateResolvedType("byte");

        /// <summary>
        /// The 'i8' base type.
        /// </summary>
        public static readonly FieldType I8 = CreateResolvedType("i8");

        /// <summary>
        /// The 'i16' base type.
        /// </summary>
        public static readonly FieldType I16 = CreateResolvedType("i16");

        /// <summary>
        /// The 'i32' base type.
        /// </summary>
        public static readonly FieldType I32 = CreateResolvedType("i32");

        /// <summary>
        /// The 'i64' base type.
        /// </summary>
        public static readonly FieldType I64 = CreateResolvedType("i64");

        /// <summary>
        /// The 'double' base type.
        /// </summary>
        public static readonly FieldType Double = CreateResolvedType("double");

        /// <summary>
        /// The 'string' base type.
        /// </summary>
        public static readonly FieldType String = CreateResolvedType("string");

        /// <summary>
        /// The 'binary' base type.
        /// </summary>
        public static readonly FieldType Binary = CreateResolvedType("binary");

        /// <summary>
        /// The 'slist' base type.
        /// </summary>
        public static readonly FieldType SList = FieldType.CreateResolvedType("slist");

        private static readonly Dictionary<string, FieldType> BaseTypeMap =
            new Dictionary<string, FieldType>
            {
                { Bool.Name, Bool },
                { Byte.Name, Byte },
                { I8.Name, I8 },
                { I16.Name, I16 },
                { I32.Name, I32 },
                { I64.Name, I64 },
                { Double.Name, Double },
                { String.Name, String },
                { Binary.Name, Binary },
                { SList.Name, SList },
            };

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldType" /> class.
        /// </summary>
        /// <param name="name">The name of the type.</param>
        /// <param name="identifierPartsCount">
        /// The number of parts that make up the type name.
        /// </param>
        /// <param name="isResolved">
        /// Indicates whether the type has been resolved successfully or not.
        /// </param>
        public FieldType(string name, int identifierPartsCount, bool isResolved)
        {
            this.Name = name;
            this.IdentifierPartsCount = identifierPartsCount;
            this.IsResolved = isResolved;
        }

        /// <summary>
        /// Gets the list of base (built-in) types.
        /// </summary>
        public static IReadOnlyCollection<FieldType> BaseTypes => BaseTypeMap.Values;

        /// <summary>
        /// Gets the name of the type.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets a value indicating whether the type has successfully been
        /// resolved. If this is false it means the type could not be found.
        /// </summary>
        public bool IsResolved { get; }

        /// <summary>
        /// Gets the number of parts that make up the type name. This will be
        /// 1 for a simple type like `i32`, and 2 for a multi-part identifier
        /// like `Enums.UserType`.
        /// </summary>
        public int IdentifierPartsCount { get; }

        /// <summary>
        /// Creates a new type marked as resolved.
        /// </summary>
        /// <param name="typeName">The name of the type.</param>
        /// <returns>
        /// The type.
        /// </returns>
        public static FieldType CreateResolvedType(string typeName)
        {
            var nameParts = typeName.Split('.');

            return new FieldType(typeName, nameParts.Length, true);
        }

        /// <summary>
        /// Creates a new type marked as unresolved.
        /// </summary>
        /// <param name="typeName">The name of the type.</param>
        /// <returns>
        /// The type.
        /// </returns>
        public static FieldType CreateUnresolvedType(string typeName)
        {
            var nameParts = typeName.Split('.');

            return new FieldType(typeName, nameParts.Length, false);
        }

        /// <summary>
        /// Gets the base type with the specified name.
        /// </summary>
        /// <param name="typeName">The name of the type.</param>
        /// <returns>
        /// The base type, or null if the specified name isn't a base type.
        /// </returns>
        public static FieldType ResolveBaseType(string typeName)
        {
            if (BaseTypeMap.TryGetValue(typeName, out var fieldType))
            {
                return fieldType;
            }

            return null;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return this.Name;
        }
    }
}