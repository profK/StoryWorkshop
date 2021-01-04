using GLOM;
using GLOM.Geometry;

namespace Proteus.Core
{
    public class HTMLUnitaryContainer:HTMLComponent,IUIUnitaryContainer
    {
        public HTMLUnitaryContainer(string html) : base(html)
        {
        }

        public IUINode Content { get; set; }

        public override Size NaturalSize
        {
            get { return Content.NaturalSize; }
        }

        public override void Layout(Point position, Size space)
        {
            base.Layout(position, space);
            Content.Layout(position,space);
        }

        public override void RenderLocal(Matrix localXform)
        {
            base.RenderLocal(localXform);
            Content.Render(localXform);
        }
    }
}