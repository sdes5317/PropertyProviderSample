using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System;
using System.Reflection;

namespace PropertyProviderSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // 這是使用 DispatchProxy 來攔截屬性 Getter 的範例
            NormalUsed();

            // 測試DispatchProxy產生的額外延遲
            // 從結果來看getter大約每秒要呼叫到百萬次 產生的成本才會大於50毫秒(人類有感的時間)
            // 每秒呼叫 【1 萬次】 ≈ 3.7 毫秒
            // 每秒呼叫 【100 萬次】 ≈ 367 毫秒
            // 每秒呼叫 【1 千萬次】 ≈ 3 670 毫秒
            // 這裡請跑在Release模式下，因為BenchmarkDotNet會自動處理JIT編譯和優化
            //BenchmarkRunner.Run<GetterBenchmark>();
        }

        private static void NormalUsed()
        {
            // 建立原始物件  
            IDefectInfo original = new DefectInfo
            {
                Id = 123,
                DieX = 1.23f,
                DieY = 4.56f,
                Features = new float[] { 0.1f, 0.2f }
            };

            // 建立代理物件  
            var proxy = DispatchProxy.Create<IDefectInfo, GetterInterceptor<IDefectInfo>>();
            (proxy as GetterInterceptor<IDefectInfo>).Target = original;

            // 呼叫實際存在的 Getter（例如 Id）  
            var value = proxy.Id;
            Console.WriteLine($"Returned value: {value}");

            // 等待使用者按鍵，確保主控台不會立即關閉  
            Console.WriteLine("按任意鍵結束");
            Console.ReadKey();
        }
    }

    public class GetterInterceptor<T> : DispatchProxy
    {
        public T Target { get; set; }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            // 只攔截屬性的 getter  
            //if (targetMethod.Name.StartsWith("get_"))
            //{
            //    //Console.WriteLine($"【攔截到】呼叫 {targetMethod.Name}");
            //    // 若要回傳自訂值，可在此直接 return  
            //}

            // 呼叫原本的 Getter  
            return targetMethod.Invoke(Target, args);
        }
    }
    public class DefectInfo : IDefectInfo
    {
        public int Id { get; set; }
        public float DieX { get; set; }
        public float DieY { get; set; }
        public float[] Features { get; set; }
    }
    public interface IDefectInfo
    {
        int Id { get; set; }
        float DieX { get; set; }
        float DieY { get; set; }
        float[] Features { get; set; }
    }

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
