using LibGit2Sharp;

namespace Logic.Tests.TestTypes;

public sealed class TestCommit : Commit
{
    public override string Sha => "Test";

    public override string MessageShort => "Message123";
}