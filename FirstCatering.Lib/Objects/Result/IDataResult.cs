namespace FirstCatering.Lib.Objects.Result
{
    /// <summary>
    /// Definition of a data result
    /// </summary>
    /// <typeparam name="T">Data type</typeparam>
    public interface IDataResult<out T> : IResult
    {
        /// <summary>
        /// Result data
        /// </summary>
        T Data { get; }
    }
}