using System.Reflection;

namespace PropertyProviderSample
{
    public class GetterInterceptor<T> : DispatchProxy
    {
        public T Target { get; set; }
        /// <summary>
        /// 依賴一個PropertyProviver來存取資料庫的欄位值
        /// </summary>
        public PropertyProviver PropertyProviver { get; set; } = new PropertyProviver();
        public IJsonSchemaService JsonSchemaService { get; set; }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            // 只攔截屬性的 getter  
            //if (targetMethod.Name.StartsWith("get_"))
            //{
            //    //Console.WriteLine($"【攔截到】呼叫 {targetMethod.Name}");
            //    // 若要回傳自訂值，可在此直接 return  
            //}

            //效能考量, 在這邊最好能使用泛型
            //故這邊還須依賴查詢JsonSchema的服務
            //JsonSchemaService.GetColumnType();
            //PropertyProviver.GetValue<int>()

            // 呼叫原本的 Getter  
            return targetMethod.Invoke(Target, args);
        }
    }
}
