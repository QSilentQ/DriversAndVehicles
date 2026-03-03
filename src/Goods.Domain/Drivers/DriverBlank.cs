using Goods.Domain.Drivers.Enums;

namespace Goods.Domain.Drivers;

public class DriverBlank
{
    public Guid? Id { get; set; }
    public String? FullName { get; set; }
    public Gender? Gender { get; set; }
    public LicenseCategory? DriverLicenseCategory { get; set; }
    public DateOnly? Birthday { get; set; }
    public Int32? Experience { get; set; }
    public Decimal? PayPerHour { get; set; }
    public Boolean IsRemoved { get; set; }
}
