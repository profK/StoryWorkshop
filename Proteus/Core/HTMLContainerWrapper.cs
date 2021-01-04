using GLOM;
using GLOM.Geometry;
using Microsoft.JSInterop;

namespace Proteus.Core

{
    public class HTMLContainerWrapper<TInfo>:IUIContainer<TInfo>
    {
        private IJSObjectReference _div;
        private AbstractUIContainer<TInfo> _wrapped;

        public HTMLContainerWrapper(AbstractUIContainer<TInfo> layout)
        {
            _wrapped = layout;
            _div = ProteusContext.JSInvoke<IJSObjectReference>(
                "Proteus.htmlToElement", "<div></div>");
        }

        public Matrix Transformation => _wrapped.Transformation;

        public Size PreferredSize => _wrapped.PreferredSize;

        public Size MinSize => _wrapped.MinSize;

        public Size Size => _wrapped.Size;
        public Size OverrideSize
        {
            get => _wrapped.OverrideSize;
            set => _wrapped.OverrideSize = value;
        }

        public Size NaturalSize => _wrapped.NaturalSize;

        public void Layout(Point position, Size space)
        {
            _wrapped.Layout(position, space);
        }

        public void Render(Matrix parentMatrix)
        {
            _wrapped.Render(parentMatrix);
        }

        public bool ExpandChildenHorizontally
        {
            get => _wrapped.ExpandChildenHorizontally;
            set => _wrapped.ExpandChildenHorizontally = value;
        }

        public bool ExpandChildrenVertically
        {
            get => _wrapped.ExpandChildrenVertically;
            set => _wrapped.ExpandChildrenVertically = value;
        }

        public void Add(IUINode child, TInfo layoutInfo = default(TInfo))
        {
            _wrapped.Add(child, layoutInfo);
            HTMLComponent htmlComp = child as HTMLComponent;
            ProteusContext.JSInvokeVoid(
                "Proteus.setElementParent",htmlComp._domElement,
                _div);
        }

        public void Remove(IUINode child)
        {
            _wrapped.Remove(child);
            HTMLComponent htmlComp = child as HTMLComponent;
            ProteusContext.JSInvokeVoid(
                "Proteus.removeElementFromParent", htmlComp._domElement);
        }
    }
}