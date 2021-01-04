using GLOM.Geometry;

namespace GLOM
{
    public interface IUINode
    {
        public Matrix Transformation { get; }
        public Size PreferredSize { get; }
        public Size MinSize { get; }
        public Size Size { get;  }
        
        public Size OverrideSize { get; set; }
        
        public Size NaturalSize { get;  }

        public void Layout(Point position, Size space);

        public void Render(Matrix parentMatrix);
    }
}