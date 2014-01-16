using OpenTK;

namespace BioGenie.Stl.Objects
{
    public class Ray
    {
        public Ray(Vertex p1, Vertex p2)
        {
            Start = p1;
            Vector3 dir = p2.ToVector3() - p1.ToVector3();
            Dir = new Normal(dir);
            Length = dir.Length;
        }

        public Vertex Start { get; set; }
        public Normal Dir { get; set; }
        public float Length { get; set; }
    }
}