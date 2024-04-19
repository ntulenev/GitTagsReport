using LibGit2Sharp;

namespace Logic.Tests.TestTypes;

public sealed class TestTag : Tag
{
    public override GitObject Target
    {
        get { return new TestGitObject(); }
    }

    public override string FriendlyName => "Name123";
}