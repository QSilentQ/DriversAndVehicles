using Goods.Domain.Drivers.Enums;

namespace Goods.Domain.Drivers;

public class Driver(
    Guid id,
    String fullName,
    Gender gender,
    LicenseCategory driverLicenseCategory,
    Int32 experience,
    DateOnly birthday,
    Decimal payPerHour,
    Boolean isRemoved)
{
    public Guid Id { get; } = id;
    public String FullName { get; } = fullName;
    public Gender Gender { get; } = gender;
    public LicenseCategory DriverLicenseCategory { get; } = driverLicenseCategory;
    public DateOnly Birthday { get; } = birthday;
    public Int32 Experience { get; } = experience;
    public Decimal PayPerHour { get; } = payPerHour;
    public Boolean IsRemoved { get; } = isRemoved;
}
