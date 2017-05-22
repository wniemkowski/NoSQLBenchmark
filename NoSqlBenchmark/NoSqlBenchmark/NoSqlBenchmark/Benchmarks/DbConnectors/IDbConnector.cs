namespace NoSqlBenchmark.Benchmarks
{
    interface IDbConnector
    {
        void Connect();
        T Insert<T>(T data);
        T Update<T>(long id);
        T Read<T>(long id);
        void FlushDb();
        void InitScheme<T>();
    }
}
