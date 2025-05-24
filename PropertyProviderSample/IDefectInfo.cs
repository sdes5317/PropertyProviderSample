namespace PropertyProviderSample
{
    public interface IDefectInfo
    {
        int Id { get; set; }
        float DieX { get; set; }
        float DieY { get; set; }
        float[] Features { get; set; }
    }
}
