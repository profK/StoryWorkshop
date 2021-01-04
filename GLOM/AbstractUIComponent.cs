using System;
using System.Numerics;
using GLOM.Geometry;

namespace GLOM
{
    public abstract class AbstractUIComponent:IUINode
    {
        public Matrix Transformation { get; protected set; }

      
   

        public virtual Size PreferredSize
        {
            get
            {
                return new Size(
                    Math.Max(NaturalSize.Width, OverrideSize.Width),
                    Math.Max(NaturalSize.Height, OverrideSize.Height));
            }
           
        }
        public virtual Size MinSize { get; protected set; }
        public virtual Size Size { get; protected set; }
        public virtual Size OverrideSize { get; set; }
        
        public virtual Size NaturalSize { get; protected set; }

        public AbstractUIComponent()
        {
            Transformation = new Matrix(Matrix4x4.Identity);
        }
        
        public virtual void Layout(Point position, Size space)
        {
            Transformation = Transformation.AtPosition(position);
            Size = space;
        }

        public void Render(Matrix parentMatrix)
        {
            Matrix currentXform = parentMatrix.Multiply(Transformation);
            RenderLocal(currentXform);
        }

        public abstract void RenderLocal(Matrix localXform);
    }
}