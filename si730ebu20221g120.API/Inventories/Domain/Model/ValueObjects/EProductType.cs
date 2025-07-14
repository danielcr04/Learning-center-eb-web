namespace si730ebu20221g120.API.Inventories.Domain.Model.ValueObjects;

public enum EProductType
{
    BuildToPrint = 0,  
    BuildToSpecification = 1,
    MadeToStock = 2,
    MadeToOrder = 3,
    MadeToAssemble = 4
}

public static class EProductTypeExtensions
{
    public static EProductType ParseProductType(string productTypeStr)
    {
        return productTypeStr.ToUpper() switch
        {
            "BTP" => EProductType.BuildToPrint,
            "BTS" => EProductType.BuildToSpecification,
            "MTS" => EProductType.MadeToStock,
            "MTO" => EProductType.MadeToOrder,
            "MTA" => EProductType.MadeToAssemble,
            _ => throw new ArgumentException("Invalid product type")
        };
    }

    public static string ToProductTypeString(this EProductType productType)
    {
        return productType switch
        {
            EProductType.BuildToPrint => "BuildToPrint",
            EProductType.BuildToSpecification => "BuildToSpecification",
            EProductType.MadeToStock => "MadeToStock",
            EProductType.MadeToOrder => "MadeToOrder",
            EProductType.MadeToAssemble => "MadeToAssemble",
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}