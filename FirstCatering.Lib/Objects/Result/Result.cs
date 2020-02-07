namespace FirstCatering.Lib.Objects.Result
{
    /// <summary>
    /// An action result
    /// </summary>
    public class Result : IResult
    {
        /// <summary>
        /// Whether the result is an error
        /// </summary>
        public bool IsError { get; protected set; }

        /// <summary>
        /// Whether result was a success
        /// </summary>
        public bool IsSuccess { get; protected set; }

        /// <summary>
        /// Result message
        /// </summary>
        public string Message { get; protected set; }

        /// <summary>
        /// Initialises a new result
        /// </summary>
        protected Result() { }

        /// <summary>
        /// Creates an error result
        /// </summary>
        /// <returns><see cref="IResult"/> error result</returns>
        public static IResult Error()
            => new Result { IsError = true, IsSuccess = false };

        /// <summary>
        /// Creates an error result with the specified <paramref name="message"/>
        /// </summary>
        /// <param name="message">Error message</param>
        /// <returns><see cref="IResult"/> error result</returns>
        public static IResult Error(string message)
            => new Result { IsError = true, IsSuccess = false, Message = message };

        /// <summary>
        /// Creates a success result
        /// </summary>
        /// <returns><see cref="IResult"/> success result</returns>
        public static IResult Success()
            => new Result { IsError = false, IsSuccess = true };

        /// <summary>
        /// Creates a success result with the specified <paramref name="message"/>
        /// </summary>
        /// <param name="message">Success message</param>
        /// <returns><see cref="IResult"/> success result</returns>
        public static IResult Success(string message)
            => new Result { IsError = false, IsSuccess = true, Message = message };
    }
}