namespace NoSqlBenchmark.Benchmarks
{
    interface IDbConnector
    {
        void Connect();
        T Insert<T>(T data) where T : BaseModel;
        T Update<T>(long id, T data) where T : BaseModel;
        T Read<T>(long id) where T : BaseModel;
        void FlushDb();
        void InitScheme<T>() where T : BaseModel;
    }
}
