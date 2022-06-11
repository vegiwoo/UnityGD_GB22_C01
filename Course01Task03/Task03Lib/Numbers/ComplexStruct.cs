namespace Task03Lib.Numbers; 

/// <summary>
/// Структура, представляющая комплексное число.
/// </summary>
public struct ComplexStruct : IComplexable 
{ 
    public float Re { get; } 
    public float Im { get; }

    public ComplexStruct(float re, float im)
    {
        Re = re;
        Im = im;
    }
    
    public override string ToString()
    {
        return $"{Re} + {Im}i";
    }
    
    public IComplexable Addition(IComplexable c1, IComplexable c2)
    {
        return new ComplexStruct(c1.Re + c2.Re, c1.Im + c2.Im);
    }

    public IComplexable Subtraction(IComplexable c1, IComplexable c2)
    {
        return new ComplexStruct(c1.Re - c2.Re, c1.Im - c2.Im);
    }

    public IComplexable Multiplication(IComplexable c1, IComplexable c2)
    {
        return new ComplexStruct((c1.Re * c2.Re - c1.Im * c2.Im), (c1.Re * c2.Im + c2.Re * c1.Im));
    }
}