using GLOM.Geometry;

namespace GLOM
{
    public class AbstractUnitaryUIContainer:AbstractUIComponent, IUIUnitaryContainer
    {
        public IUINode Content { get; set; }

        public override Size NaturalSize
        {
            get
            {
                return Content.NaturalSize;
            }
        }

        public override void Layout(Point position, Size space)
        {
            base.Layout(position, space);
            Content.Layout(position,space);
        }

        public override void RenderLocal(Matrix localXform)
        {
            Content.Render(localXform);
        }
    }
}