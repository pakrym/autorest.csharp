namespace body_complex.Models.V20160229
{
    public partial class DotFishMarket
    {
        public V20160229.DotSalmon? SampleSalmon { get; set; }
        public System.Collections.Generic.ICollection<V20160229.DotSalmon> Salmons { get; } = new System.Collections.Generic.List<V20160229.DotSalmon>();
        public V20160229.DotFish? SampleFish { get; set; }
        public System.Collections.Generic.ICollection<V20160229.DotFish> Fishes { get; } = new System.Collections.Generic.List<V20160229.DotFish>();
    }
}