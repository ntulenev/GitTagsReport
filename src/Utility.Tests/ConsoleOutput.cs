namespace Utility.Tests;

/// <summary>
/// Provides a mechanism to capture console output for unit testing purposes.
/// </summary>
/// <remarks>
/// This class redirects the standard console output stream to a <see cref="StringWriter"/> during its lifetime,
/// allowing tests to capture and verify output written to the console. It implements <see cref="IDisposable"/>
/// to ensure proper cleanup of resources and restoration of the original output stream.
/// </remarks>
public sealed class ConsoleOutput : IDisposable
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ConsoleOutput"/> class, capturing current console output.
    /// </summary>
    /// <remarks>
    /// This constructor redirects the standard output stream from <see cref="System.Console.Out"/> 
    /// to a local <see cref="StringWriter"/> instance.
    /// </remarks>
    public ConsoleOutput()
    {
        _stringWriter = new StringWriter();
        _originalOutput = System.Console.Out;
        System.Console.SetOut(_stringWriter);
        _isDisposed = false;
    }

    /// <summary>
    /// Retrieves the content written to the console since the creation of this instance.
    /// </summary>
    /// <returns>A string containing the captured console output.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the method is called 
    /// after the object has been disposed.</exception>
    public string GetOutput()
    {
        ThrowIfDisposed();
        return _stringWriter.ToString();
    }

    /// <summary>
    /// Restores the original console output stream and releases any associated resources.
    /// </summary>
    /// <remarks>
    /// Calling this method restores the original <see cref="System.Console.Out"/> output stream and disposes of the <see cref="StringWriter"/>
    /// used to capture output. This method should be called typically in a using block or manually in test cleanup.
    /// </remarks>
    public void Dispose()
    {
        if (!_isDisposed)
        {
            System.Console.SetOut(_originalOutput);
            _stringWriter.Dispose();
            _isDisposed = true;
        }
    }

    private void ThrowIfDisposed()
    {
        if (_isDisposed)
        {
            throw new ObjectDisposedException(nameof(ConsoleOutput));
        }
    }

    private readonly StringWriter _stringWriter;
    private readonly TextWriter _originalOutput;
    private bool _isDisposed;
}


