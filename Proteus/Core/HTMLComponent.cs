using System;
using System.Collections.Generic;
using GLOM;
using GLOM.Geometry;
using Microsoft.JSInterop;

namespace Proteus.Core
{
    [Serializable]
    public struct MouseEvent
    {
        public float ClientPosX { get; set; }
        public float ClientPosY { get; set; }
    }
    
    public class HTMLComponent:AbstractUIComponent
    {
        
        
        internal IJSObjectReference _domElement;
        internal Dictionary<string, string> _styleSettings = new Dictionary<string, string>();
        internal DotNetObjectReference<HTMLComponent> _objRefFromJS;

        public event Action<MouseEvent> OnMouseDown;
        public event Action<MouseEvent> OnMouseUp;
        public event Action<MouseEvent> OnMouseMove;

        public HTMLComponent(string html)
        {
            _domElement = ProteusContext.JSInvoke<IJSObjectReference>(
                "Proteus.htmlToElement", html);
            _objRefFromJS = DotNetObjectReference.Create(this);
            float[] sza= ProteusContext.JSInvoke<float[]>(
                "Proteus.getElementSize", _domElement);
            Size size = new Size(sza[0], sza[1]);
            ProteusContext.Log(html);
            ProteusContext.Log("html size=" + size.ToString());
            NaturalSize = size;
            MinSize = NaturalSize;
            ProteusContext.JSInvokeVoid("Proteus.setMouseCallbacks",_domElement,
                _objRefFromJS,"DoJSMouseDown","DoJSMouseUp","DoJSMouseMove");
        }

        [JSInvokable]
        public void DoJSMouseDown(MouseEvent evt)
        {
            ProteusContext.Log("mouse down");
            OnMouseDown?.Invoke(evt);
        }
        
        [JSInvokable]
        public void DoJSMouseUp(MouseEvent evt)
        {
            ProteusContext.Log("mouse up");
            OnMouseUp?.Invoke(evt);
        }
        
        [JSInvokable]
        public void DoJSMouseMove(MouseEvent evt)
        {
            ProteusContext.Log("evt=" + evt.ClientPosX + "," + evt.ClientPosY);
            OnMouseMove?.Invoke(evt);
        }
        
               
        public override void RenderLocal(Matrix localXform)
        {
            ProteusContext.Log("Render matrix=" + localXform.ToString());
            Point origin = localXform.TransformPoint(Point.Zero);
            //ProteusContext.JSInvokeVoid(
            //    "Proteus.setElementLayout",_domElement,origin.X,
            //    origin.Y,Size.Width,Size.Height);
            _styleSettings["position"] = "fixed";
            _styleSettings["left"] = origin.X + "px";
            _styleSettings["top"] = origin.Y + "px";
            _styleSettings["width"] = Size.Width+"px";
            _styleSettings["height"] = Size.Height + "px";
            ProteusContext.JSInvokeVoid(
                "Proteus.resetStyle",_domElement,_styleSettings);
        }
    }
}