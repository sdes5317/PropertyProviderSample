# 需求
1. 需要把老舊專案的Dto Getter抽象化成支援兩種模式(正常物件, 記憶體快取+SQL存取)
2. 可能會有欄位mapping的需求,例:屬性Id在資料庫可能名稱叫做Id2
3. 盡量實作最少的程式碼

# 範例
1. 透過DispatchProxy類別來實現一個範例
2. 實測DispatchProxy產生的額外成本
