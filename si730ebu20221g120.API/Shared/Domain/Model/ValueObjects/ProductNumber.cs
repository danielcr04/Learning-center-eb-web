namespace si730ebu20221g120.API.Shared.Domain.Model.ValueObjects;

public record ProductNumber(Guid Value)
{
    public static ProductNumber GenerateNew() => new(Guid.NewGuid());
}