using System;
using System.Numerics;
using Task03Lib.Numbers;


namespace Task03Lib; 

/// <summary>
/// Класс, представляющий комплексное число.
/// </summary>
public class ComplexClass : IComplexable { 
    public float Re { get; } 
    public float Im { get; }
         
    public ComplexClass(float re, float im)
    {
        Re = re;
        Im = im;
    }

    public override string ToString()
    {
        return $"{Re} + {Im}i";
    }

    public IComplexable Addition(IComplexable с1, IComplexable с2)
    {
        return new ComplexClass(с1.Re + с1.Re, с2.Im + с2.Im);
    }

    public IComplexable Subtraction(IComplexable c1, IComplexable c2)
    {
        return new ComplexStruct(c1.Re - c2.Re, c1.Im - c2.Im);
    }

    public IComplexable Multiplication(IComplexable c1, IComplexable c2)
    {
        return new ComplexClass((c1.Re * c2.Re - c1.Im * c2.Im), (c1.Re * c2.Im + c2.Re * c1.Im));
    }
}