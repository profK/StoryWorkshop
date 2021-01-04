using System.Collections.Generic;
using GLOM.Geometry;

namespace Proteus.Core
{
    public struct Insets
    {
        public float Top;
        public float Right;
        public float Bottom;
        public float Left;

        public Insets(float top, float right, float bottom, float left)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }
    }
    public class PWindow:HTMLComponent
    {
        private Insets _borderInsets;
        private bool _buttonDown = false;
        private Point _grabOffset;
        
        public PWindow(string borderImageURL, Insets insets) : base(MakeHTML())
        {
            _styleSettings["border-image"] = "url('" + borderImageURL + "')";
            _styleSettings["border-width"] =  insets.Top.ToString() + "px " + insets.Right.ToString() + "px " +
                                              insets.Bottom.ToString() + "px " + insets.Left.ToString()+"px";
            _styleSettings["border-image-slice"] =
                insets.Top.ToString() + " " + insets.Right.ToString() + " " +
                insets.Bottom.ToString() + " " + insets.Left.ToString();
            _borderInsets = insets;
            OnMouseDown += DoMouseDown;
            OnMouseUp += DoMouseUp;
            OnMouseMove += DoMouseMove;
        }

        private void DoMouseMove(MouseEvent obj)
        {
            if (_buttonDown)
            {
                ProteusContext.Log("move="+obj.ClientPosX+","+obj.ClientPosY);
                float deltaX = obj.ClientPosX - _grabOffset.X;
                float deltay = obj.ClientPosY - _grabOffset.Y;
                Transformation = Transformation.Translate(deltaX, deltay);
                Render(Matrix.Identity);
            }
            
        }

        private void DoMouseUp(MouseEvent obj)
        {
            _buttonDown = false;
        }

        private void DoMouseDown(MouseEvent obj)
        {
            _buttonDown = true;
            ProteusContext.Log(obj.ClientPosX + "," + obj.ClientPosY);
            _grabOffset = new Point(obj.ClientPosX, obj.ClientPosY);
        }


        private static string MakeHTML()
        {
            return "<dialog open></dialog>";
        }
    }
}