using System.Collections;
using LibGit2Sharp;

namespace Logic.Tests;

public sealed class TestTagCollection : TagCollection
{
    public override IEnumerator<Tag> GetEnumerator()
    {
        yield return new TestTag();
    }
}

public sealed class TestTag : Tag
{
    public override GitObject Target
    {
        get { return new TestGitObject(); }
    }

    public override string FriendlyName => "Name123";
}

public sealed class TestGitObject : GitObject
{
    public override string Sha => "Test";
}

public sealed class TestCommitLog : IQueryableCommitLog
{
    public IEnumerator<Commit> GetEnumerator()
    {
        yield return new TestCommit();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public CommitSortStrategies SortedBy { get; }

    public ICommitLog QueryBy(CommitFilter filter)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<LogEntry> QueryBy(string path)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<LogEntry> QueryBy(string path, CommitFilter filter)
    {
        throw new NotImplementedException();
    }
}

public sealed class TestCommit : Commit
{
    public override string Sha => "Test";

    public override string MessageShort => "Message123";
}