using System;
using GLOM.Geometry;

namespace GLOM
{
    public struct HorizontalLayoutInfo
    {
        
    }
    public class  HorizontalLayout:AbstractUIContainer<HorizontalLayoutInfo>
    {
        public override Size NaturalSize
        {
            get
            {
                float w = 0;
                float h = 0;
                foreach (var tuple in Children)
                {
                    h += tuple.Item1.PreferredSize.Height;
                    w = Math.Max(w, tuple.Item1.PreferredSize.Width);
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
                    h += tuple.Item1.MinSize.Height;
                    w = Math.Max(w, tuple.Item1.MinSize.Width);
                }

                return new Size(w, h);
            }
        }
        
        public override void Layout(Point pos, Size space)
        {
            Size myPreferredSize = PreferredSize;
            int pad = (int)(space.Height - myPreferredSize.Height) / Children.Count;
            float xAcc = 0;
            foreach (var tuple in Children)
            {
                IUINode comp = tuple.Item1;
                Size compISize = comp.PreferredSize;
                float w = compISize.Width;
                float h = compISize.Height;

                if (ExpandChildenHorizontally)
                {
                    w += pad;

                }

                if (ExpandChildrenVertically)
                {
                    h = myPreferredSize.Height;
                }
            
                comp.Layout(new Point(xAcc,0),
                    new Size(w, h));
                xAcc += comp.Size.Width;
            }
        }

       
    }
}