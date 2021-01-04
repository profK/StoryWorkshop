using System;
using System.Numerics;
using System.Runtime.Intrinsics.X86;

namespace GLOM.Geometry

{

    public readonly struct Point
    {
        internal readonly Vector3 _vec3;

        public float X
        {
            get { return _vec3.X; }
        }

        public float Y
        {
            get { return _vec3.Y; }
        }

        public static readonly Point Zero = new Point(0, 0);

        public Point(float x, float y)
        {
            _vec3 = new Vector3(x, y,0);
        }

        public Point(Vector3 vec)
        {
            _vec3 = vec;
        }

        public override string ToString()
        {
            return _vec3.X + "," + _vec3.Y;
        }
    }

    public readonly struct Size
    {
        internal readonly Vector3 _vec3;

        public float Width
        {
            get { return _vec3.X; }
        }

        public float Height
        {
            get { return _vec3.Y; }
        }

        public Size(float width, float height)
        {
            _vec3 = new Vector3(width, height,0);
        }

        public override string ToString()
        {
            return _vec3.X + "," + _vec3.Y;
        }   
    }

    public readonly struct Matrix
    {
        private readonly Matrix4x4 _mat;

        //default is identity matrix
        
        public static readonly Matrix Identity = new Matrix(Matrix4x4.Identity);
        
        public Matrix(Matrix4x4 matrix)
        {
            _mat = matrix;
        }

        public Point Position
        {
            get
            {
                Vector3 p = _mat.Translation;
                return new Point(p.X,p.Y);
            }
           
        }

        public Point TransformPoint(Point pt)
        {
            return new Point(Vector3.Transform(pt._vec3, _mat));

        }

        public Matrix AtPosition(Point p)
        {
            Matrix4x4 newMat = _mat;
            newMat.Translation = new Vector3(p.X,p.Y,0);
            return new Matrix(newMat);
        }

        public Matrix Multiply(in Matrix transformation)
        {
            return new Matrix(Matrix4x4.Multiply(transformation._mat,_mat));
        }

        public override string ToString()
        {
            return MatLine(_mat.M11, _mat.M12, _mat.M13, _mat.M14) + "\n" +
                   MatLine(_mat.M21, _mat.M22, _mat.M23, _mat.M24) + "\n" +
                   MatLine(_mat.M31, _mat.M32, _mat.M33, _mat.M34) + "\n" +
                   MatLine(_mat.M41, _mat.M42, _mat.M43, _mat.M44);
        }

        private string MatLine(float a, float b, float c, float d)
        {
            return String.Format("{0,4}{1,4}{2,4},{3,4}", a, b, c, d);
        }

        public Matrix Translate(in float deltaX, in float deltay)
        {
            Matrix4x4 newMat = _mat;
            newMat.Translation = newMat.Translation + new Vector3(deltaX, deltay, 0);
            return new Matrix(newMat);
        }
    }
}