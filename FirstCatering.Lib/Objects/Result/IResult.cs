namespace FirstCatering.Lib.Objects.Result
{
    /// <summary>
    /// Definition of a result
    /// </summary>
    public interface IResult
    {
        /// <summary>
        /// Whether the result is an error
        /// </summary>
        bool IsError { get; }

        /// <summary>
        /// Whether the result was a success
        /// </summary>
        bool IsSuccess { get; }

        /// <summary>
        /// Result message
        /// </summary>
        string Message { get; }
    }
}