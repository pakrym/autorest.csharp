namespace body_complex.Models.V20160229
{
    public partial class Fish
    {
        public System.String Fishtype { get; set; }
        public System.String? Species { get; set; }
        public System.Double Length { get; set; }
        public System.Collections.Generic.ICollection<Fish> Siblings { get; } = new System.Collections.Generic.List<Fish>();
    }
}