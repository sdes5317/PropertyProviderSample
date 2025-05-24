namespace PropertyProviderSample
{
    public class DefectInfo : IDefectInfo
    {
        public int Id { get; set; }
        public float DieX { get; set; }
        public float DieY { get; set; }
        public float[] Features { get; set; }
    }
}
