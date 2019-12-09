namespace body_complex.Models.V20160229
{
    public partial class Cat
    {
        public System.String? Color { get; set; }
        public System.Collections.Generic.ICollection<V20160229.Dog> Hates { get; } = new System.Collections.Generic.List<V20160229.Dog>();
    }
}