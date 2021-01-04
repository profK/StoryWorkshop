using GLOM.Geometry;

namespace GLOM
{
    public interface IUIContainer<TInfo>:IUINode
    {
        bool ExpandChildenHorizontally { get; set; }
        bool ExpandChildrenVertically { get; set; }
      
        void Add(IUINode child, TInfo layoutInfo = default(TInfo));
        void Remove(IUINode child);
    }
}