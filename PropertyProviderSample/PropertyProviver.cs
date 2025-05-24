using System.Runtime.CompilerServices;

namespace PropertyProviderSample
{
    public class PropertyProviver
    {
        public T GetValue<T>([CallerMemberName] string propertyName = "")
        {
            // 這裡可以實作從資料庫或其他來源取得屬性值的邏輯
            // 例如，從資料庫查詢屬性值
            // 也能透過優化在這邊"批量"快取
            // 這裡僅為示範，做單點存取
            //Console.WriteLine($"取得屬性 {propertyName} 的值");
            return default(T); // 回傳預設值或實際值
        }
    }
}
