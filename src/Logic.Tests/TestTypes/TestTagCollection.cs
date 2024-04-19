using LibGit2Sharp;

namespace Logic.Tests.TestTypes;

public sealed class TestTagCollection : TagCollection
{
    public override IEnumerator<Tag> GetEnumerator()
    {
        yield return new TestTag();
    }
}