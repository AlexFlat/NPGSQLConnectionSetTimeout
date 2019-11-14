using Xunit;

namespace NPGSQLConnectionSetTimeout
{
    /// <summary>
    /// XUnit collection definition for embedded database tests. 
    /// See https://xunit.github.io/docs/shared-context.html#collection-fixture for details.
    /// </summary>
    [CollectionDefinition(EmbeddedDatabaseCollection.CollectionName)]
    public class EmbeddedDatabaseCollection : ICollectionFixture<EmbeddedDatabaseFixture>
    {
        public const string CollectionName = "Embedded Database Collection";
    }
}
