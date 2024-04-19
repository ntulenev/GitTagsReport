using LibGit2Sharp;

namespace Logic.Tests.TestTypes;

public sealed class TestGitObject : GitObject
{
    public override string Sha => "Test";
}