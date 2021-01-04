using System;
using System.Collections.Generic;
using GLOM.Geometry;

namespace GLOM
{
    public abstract class AbstractUIContainer<TInfo>:AbstractUIComponent, IUIContainer<TInfo>
    {
        public List<Tuple<IUINode, TInfo>> Children = new List<Tuple<IUINode, TInfo>>();
        public bool ExpandChildenHorizontally { get; set; }
        public bool ExpandChildrenVertically { get; set; }

        public virtual void Add(IUINode child, TInfo layoutInfo = default(TInfo))
        {
            Children.Add(new Tuple<IUINode, TInfo>(child,layoutInfo));
        }

        public virtual void Remove(IUINode child)
        {
            foreach (var tuple in Children)
            {
                if (tuple.Item1 == child)
                {
                    Children.Remove(tuple);
                    return;
                }
            }
        }
        
        public override void RenderLocal(Matrix localXform)
        {
            foreach (var tuple in Children)
            {
                tuple.Item1.Render(localXform);
            }
        }
    }
}