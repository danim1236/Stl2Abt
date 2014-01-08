namespace BioGenie.Stl.Objects
{
    public class LineSegment
    {
        public LineSegment(Vertex p1, Vertex p2)
        {
            P1 = p1;
            P2 = p2;
        }

        public Vertex P1 { get; set; }
        public Vertex P2 { get; set; }

        public float Length{get { return (P2.ToVector3() - P1.ToVector3()).Length; }}
    }
}