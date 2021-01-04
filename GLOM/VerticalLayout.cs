using System;
using GLOM.Geometry;


namespace GLOM
{
    public struct VerticalLayoutInfo
    {
        
    }
    public class VerticalLayout:AbstractUIContainer<VerticalLayoutInfo>
    {
        public override Size NaturalSize
        {
            get
            {
                float w = 0;
                float h = 0;
                foreach (var tuple in Children)
                {
                    w += tuple.Item1.PreferredSize.Width;
                    h = Math.Max(h, tuple.Item1.PreferredSize.Height);
                }

                return new Size(w, h);
            }
        }
        public override Size MinSize
        {
            get
            {
                float w = 0;
                float h = 0;
                foreach (var tuple in Children)
                {
                    w += tuple.Item1.MinSize.Width;
                    h = Math.Max(h, tuple.Item1.MinSize.Height);
                }

                return new Size(w, h);
            }
        }
        
        public override void Layout(Point pos, Size space)
        {
            Size myPreferredSize = PreferredSize;
            int pad = (int)(space.Height - myPreferredSize.Height) / Children.Count;
            float yAcc = 0;
            foreach (var tuple in Children)
            {
                IUINode comp = tuple.Item1;
                Size compISize = comp.PreferredSize;
               
                float w = compISize.Width;
                float h = compISize.Height;

                if (ExpandChildenHorizontally)
                {
                    w = myPreferredSize.Width;

                }

                if (ExpandChildrenVertically)
                {
                    h += pad;
                }
                
                comp.Layout(new Point(0, yAcc),
                    new Size(w, h));
                yAcc += comp.Size.Height;
            }
        }

       
    }
}