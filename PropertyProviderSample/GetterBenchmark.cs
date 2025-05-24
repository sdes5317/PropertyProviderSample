using BenchmarkDotNet.Attributes;
using System.Reflection;

namespace PropertyProviderSample
{
    public class GetterBenchmark
    {
        private IDefectInfo original;
        private IDefectInfo proxy;

        [GlobalSetup]
        public void Setup()
        {
            original = new DefectInfo
            {
                Id = 123,
                DieX = 1.23f,
                DieY = 4.56f,
                Features = new float[] { 0.1f, 0.2f }
            };

            proxy = DispatchProxy.Create<IDefectInfo, GetterInterceptor<IDefectInfo>>();
            ((GetterInterceptor<IDefectInfo>)proxy).Target = original;
        }

        [Benchmark(Description = "直接呼叫 original.Id")]
        public int DirectCall() => original.Id;

        [Benchmark(Description = "代理呼叫 proxy.Id")]
        public int ProxyCall() => proxy.Id;
    }
}
