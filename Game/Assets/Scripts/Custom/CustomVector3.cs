using System;

/// <summary>
/// Serializable Custom Vector3 struct
/// </summary>
[Serializable]
 public struct CustomVector3
{
    public int x;
    public int y;
    public int z;

    /// <summary>
    /// Result operation from '+' operator
    /// </summary>
    /// <param name="c1">Vector num1</param>
    /// <param name="c2">Vector num2</param>
    /// <returns>Returns a vector after the operation</returns>
    public static CustomVector3 operator +(CustomVector3 c1,
                                            CustomVector3 c2)
    {
        CustomVector3 c3 = new CustomVector3(c1.x + c2.x,
                                                c1.y + c2.y,
                                                c1.z + c2.z);
        return c3;
    }

    /// <summary>
    /// Result operation from '-' operator
    /// </summary>
    /// <param name="c1">Vector num1</param>
    /// <param name="c2">Vector num2</param>
    /// <returns>Returns a vector after the operation</returns>
    public static CustomVector3 operator -(CustomVector3 c1,
                                            CustomVector3 c2)
    {
        CustomVector3 c3 = new CustomVector3(c1.x - c2.x,
                                                c1.y - c2.y,
                                                c1.z - c2.z);
        return c3;
    }

    /// <summary>
    /// Result operation from '++' operator
    /// </summary>
    /// <param name="c1">Vector num1</param>
    /// <returns>Returns a vector after the operation</returns>
    public static CustomVector3 operator ++(CustomVector3 c1)
    {
        return new CustomVector3(c1.x + 1,
                                    c1.y + 1,
                                    c1.z + 1);
    }

    /// <summary>
    /// Result operation from '--' operator
    /// </summary>
    /// <param name="c1">Vector num1</param>
    /// <returns>Returns a vector after the operation</returns>
    public static CustomVector3 operator --(CustomVector3 c1)
    {
        return new CustomVector3(c1.x - 1,
                                    c1.y - 1,
                                    c1.z - 1);
    }

    /// <summary>
    /// Result operation from '==' operator
    /// </summary>
    /// <param name="c1">Vector num1</param>
    /// <param name="c2">Vector num2</param>
    /// <returns>Returns true if both vectors are equal</returns>
    public static bool operator ==(CustomVector3 c1,
                                    CustomVector3 c2) => c1.Equals(c2);

    /// <summary>
    /// Result operation from '!=' operator
    /// </summary>
    /// <param name="c1">Vector num1</param>
    /// <param name="c2">Vector num2</param>
    /// <returns>Returns true if both vectors are different</returns>
    public static bool operator !=(CustomVector3 c1,
                                    CustomVector3 c2) => !c1.Equals(c2);

    /// <summary>
    /// Constructor for CustomVector3
    /// </summary>
    /// <param name="x">Parameter X</param>
    /// <param name="y">Parameter Y</param>
    /// <param name="z">Parameter Z></param>
    public CustomVector3(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    /// <summary>
    /// Overrides GetHashCode
    /// </summary>
    /// <returns>Returns hashcode for the current object</returns>
    public override int GetHashCode()
    {
        return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
    }

    /// <summary>
    /// Overrides equals to compare CustomVector3 variables
    /// </summary>
    /// <param name="obj"></param>
    /// <returns>Returns true if both objects are equal</returns>
    public override bool Equals(object obj)
    {
        CustomVector3 other = (CustomVector3)obj;
        return x == other.x && y == other.y && z == other.z;
    }

    /// <summary>
    /// Overrides ToString
    /// </summary>
    /// <returns>Returns a string with CustomVector3 x, y and z</returns>
    public override string ToString() => $"[{x}, {y}, {z}]";
}