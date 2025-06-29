using System;
using UnityEngine;

namespace FixMath.NET
{
    public struct FixVector3
    {
        public Fix64 SqrMagnitude => X * X + Y * Y + Z * Z;
        public Fix64 Magnitude => Fix64.Sqrt(SqrMagnitude);
        public FixVector3 Normalized
        {
            get
            {
                var len = Magnitude;
                if (len == 0)
                {
                    return Zero;
                }

                len = 1 / len;
                return new FixVector3(X * len, Y * len, Z * len);
            }
        }

        public Fix64 X;
        public Fix64 Y;
        public Fix64 Z;

        public static readonly FixVector3 Zero = new FixVector3(0, 0, 0);
        public static readonly FixVector3 One = new FixVector3(1, 1, 1);
        public static readonly FixVector3 Up = new FixVector3(0, 1, 0);
        public static readonly FixVector3 Down = new FixVector3(0, -1, 0);
        public static readonly FixVector3 Right = new FixVector3(1, 0, 0);
        public static readonly FixVector3 Left = new FixVector3(-1, 0, 0);
        public static readonly FixVector3 Forward = new FixVector3(0, 0, 1);
        public static readonly FixVector3 Back = new FixVector3(0, 0, -1);

        public FixVector3(float x, float y, float z)
        {
            X = (Fix64)x;
            Y = (Fix64)y;
            Z = (Fix64)z;
        }

        public FixVector3(Vector3 v)
        {
            X = (Fix64)v.x;
            Y = (Fix64)v.y;
            Z = (Fix64)v.z;
        }

        public FixVector3(int x, int y, int z)
        {
            X = new Fix64(x);
            Y = new Fix64(y);
            Z = new Fix64(z);
        }

        public FixVector3(Fix64 x, Fix64 y, Fix64 z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void Normalize()
        {
            var len = Magnitude;
            if (len > 0)
            {
                len = 1 / len;
                X *= len;
                Y *= len;
                Z *= len;
            }
        }

        public Vector3 ToVector3()
        {
            return new Vector3((float)X, (float)Y, (float)Z);
        }

        public FixedVector2 ToFixedVector2XZ()
        {
            return new FixedVector2(X, Z);
        }

        public FixedVector2 ToFixedVector2XY()
        {
            return new FixedVector2(X, Y);
        }

        public Fix64 this[int index]
        {
            get
            {
                switch (index)
                {
                    case 0:
                        return X;
                    case 1:
                        return Y;
                    case 2:
                        return Z;
                    default:
                        return 0;
                }
            }
            set
            {
                switch (index)
                {
                    case 0:
                        X = value;
                        break;
                    case 1:
                        Y = value;
                        break;
                    case 2:
                        Z = value;
                        break;
                }
            }
        }

        public static FixVector3 operator +(FixVector3 a, FixVector3 b)
        {
            return new FixVector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static FixVector3 operator -(FixVector3 a, FixVector3 b)
        {
            return new FixVector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static FixVector3 operator /(FixVector3 a, Fix64 v)
        {
            if (v == Fix64.Zero)
            {
                throw new System.Exception("Divided by zero");
            }
            return new FixVector3(a.X / v, a.Y / v, a.Z / v);
        }

        public static FixVector3 operator *(FixVector3 a, Fix64 v)
        {
            return new FixVector3(a.X * v, a.Y * v, a.Z * v);
        }

        public static FixVector3 operator *(Fix64 v, FixVector3 a)
        {
            return new FixVector3(a.X * v, a.Y * v, a.Z * v);
        }

        public static FixVector3 operator -(FixVector3 a)
        {
            return new FixVector3(-a.X, -a.Y, -a.Z);
        }

        public static bool operator ==(FixVector3 a, FixVector3 b)
        {
            return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
        }

        public static bool operator !=(FixVector3 a, FixVector3 b)
        {
            return a.X != b.X || a.Y != b.Y || a.Z != b.Z;
        }

        public override string ToString()
        {
            return $"({X},{Y},{Z})";
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj is not FixVector3 v)
            {
                return false;
            }

            return v == this;
        }

        public static Fix64 GetSqrMagnitude(FixVector3 v)
        {
            return v.X * v.X + v.Y * v.Y + v.Z * v.Z;
        }

        public static FixVector3 GetNormalized(FixVector3 v)
        {
            FixVector3 result = v;
            result.Normalize();
            return result;
        }

        public static Fix64 Dot(FixVector3 v1, FixVector3 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y + v1.Z * v2.Z;
        }

        public static FixVector3 Cross(FixVector3 v1, FixVector3 v2)
        {
            return new FixVector3(
                v1.Y * v2.Z - v2.Y * v1.Z,
                v1.Z * v2.X - v2.Z * v1.X,
                v1.X * v2.Y - v2.X * v1.Y
                );
        }

        public static Fix64 Angle(FixVector3 v1, FixVector3 v2)
        {
            var k = v1.Magnitude * v2.Magnitude;
            if (k == 0)
            {
                return Fix64.Zero;
            }
            var d = Dot(v1, v2);

            return Fix64.Acos(d / k);
        }
    }
}
