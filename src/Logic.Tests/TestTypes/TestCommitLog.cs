using System.Collections;
using LibGit2Sharp;

namespace Logic.Tests.TestTypes;

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