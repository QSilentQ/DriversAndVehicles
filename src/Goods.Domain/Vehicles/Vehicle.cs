using Goods.Domain.Vehicles.Enums;

namespace Goods.Domain.Vehicles;

public class Vehicle(
    Guid id, 
    Guid driverId,
    String name,
    String stateNumber, 
    VehicleCategory vehicleCategory,
    Decimal averageSpeed,
    Decimal fuelConsumption,
    DateTime createdDatetimeUTC,
    DateTime modifiedDatetimeUTC,
    Boolean isRemoved)
{
    public Guid Id { get; } = id;
    public Guid DriverId { get; } = driverId;
    public String Name { get; } = name;
    public String StateNumber { get; } = stateNumber;
    public VehicleCategory VehicleCategory { get; } = vehicleCategory;
    public Decimal AverageSpeed { get; } = averageSpeed;
    public Decimal FuelConsumption { get; } = fuelConsumption;
    public DateTime CreatedDatetimeUTC { get; } = createdDatetimeUTC;
    public DateTime ModifiedDatetimeUTC { get; } = modifiedDatetimeUTC;
    public Boolean IsRemoved { get; } = isRemoved;
}
