using Goods.Domain.Drivers.Enums;

namespace Goods.Services.Drivers.Repositories.Models;

public class DriverDb(
    Guid id,
    String fullName,
    Gender gender,
    LicenseCategory driverLicenseCategory,
    Int32 experience,
    DateOnly birthday,
    Decimal payPerHour,
    Boolean isRemoved)
{
    public Guid Id { get; set; } = id;
    public String FullName { get; set; } = fullName;
    public Gender Gender { get; set; } = gender;
    public LicenseCategory DriverLicenseCategory { get; set; } = driverLicenseCategory;
    public DateOnly Birthday { get; set; } = birthday;
    public Int32 Experience { get; set; } = experience;
    public Decimal PayPerHour { get; set; } = payPerHour;
    public Boolean IsRemoved { get; set; } = isRemoved;
}
