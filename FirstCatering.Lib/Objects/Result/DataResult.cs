namespace FirstCatering.Lib.Objects.Result
{
    /// <summary>
    /// An action result with data
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public class DataResult<T> : Result, IDataResult<T>
    {
        /// <summary>
        /// Result data
        /// </summary>
        public T Data { get; protected set; }

        /// <summary>
        /// Initialises a new <see cref="DataResult{T}"/>
        /// </summary>
        protected DataResult() { }

        /// <summary>
        /// Creates an error data result
        /// </summary>
        /// <returns><see cref="IDataResult{T}"/> error result</returns>
        public static new IDataResult<T> Error()
            => new DataResult<T> { IsError = true, IsSuccess = false };

        /// <summary>
        /// Creates a new error data result with the specified <paramref name="message"/>
        /// </summary>
        /// <param name="message">Error message</param>
        /// <returns><see cref="IDataResult{T}"/> error result</returns>
        public static new IDataResult<T> Error(string message)
            => new DataResult<T> { IsError = true, IsSuccess = false, Message = message };

        /// <summary>
        /// Creates a success data result
        /// </summary>
        /// <returns><see cref="IDataResult{T}"/> success data result</returns>
        public static new IDataResult<T> Success()
            => new DataResult<T> { IsError = false, IsSuccess = true };

        /// <summary>
        /// Creates a success data result with the specified <paramref name="message"/>
        /// </summary>
        /// <param name="message">Success message</param>
        /// <returns><see cref="IDataResult{T}"/> success result</returns>
        public static new IDataResult<T> Success(string message)
            => new DataResult<T> { IsError = false, IsSuccess = true, Message = message };

        /// <summary>
        /// Creates a success data result with the specified <paramref name="data"/>
        /// </summary>
        /// <param name="data">Result data</param>
        /// <returns><see cref="IDataResult{T}"/> Success data result</returns>
        public static IDataResult<T> Success(T data)
            => new DataResult<T> { IsError = false, IsSuccess = true, Data = data };

        /// <summary>
        /// Creates a success data result with the specified <paramref name="data"/>
        /// and <paramref name="message"/>
        /// </summary>
        /// <param name="data">Result data</param>
        /// <param name="message">Success message</param>
        /// <returns><see cref="IDataResult{T}"/> success result</returns>
        public static IDataResult<T> Success(T data, string message)
            => new DataResult<T> { IsError = false, IsSuccess = true, Data = data, Message = message };
    }
}