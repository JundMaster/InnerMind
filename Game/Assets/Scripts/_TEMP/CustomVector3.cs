using System;
[Serializable]
 public struct CustomVector3
    {
        public int x;
        public int y;
        public int z;

        public static CustomVector3 operator +(CustomVector3 c1,
                                               CustomVector3 c2)
        {
            CustomVector3 c3 = new CustomVector3(c1.x + c2.x,
                                                 c1.y + c2.y,
                                                 c1.z + c2.z);
            return c3;
        }
        public static CustomVector3 operator -(CustomVector3 c1,
                                               CustomVector3 c2)
        {
            CustomVector3 c3 = new CustomVector3(c1.x - c2.x,
                                                 c1.y - c2.y,
                                                 c1.z - c2.z);
            return c3;
        }

        public static CustomVector3 operator ++(CustomVector3 c1)
        {
            return new CustomVector3(c1.x + 1,
                                     c1.y + 1,
                                     c1.z + 1);
        }

        public static CustomVector3 operator --(CustomVector3 c1)
        {
            return new CustomVector3(c1.x - 1,
                                     c1.y - 1,
                                     c1.z - 1);
        }

        public static bool operator ==(CustomVector3 c1,
                                       CustomVector3 c2) => c1.Equals(c2);
        public static bool operator !=(CustomVector3 c1,
                                       CustomVector3 c2) => !c1.Equals(c2);

        public CustomVector3(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            CustomVector3 other = (CustomVector3)obj;
            return x == other.x && y == other.y && z == other.z;
        }

        public override string ToString() => $"[{x}, {y}, {z}]";
    }