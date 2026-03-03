using Goods.Domain.Drivers;
using Goods.Services.Drivers.Repositories.Converters;
using Goods.Services.Drivers.Repositories.Interfaces;
using Goods.Services.Drivers.Repositories.Models;
using Goods.Services.Drivers.Repositories.Queries;
using Goods.Tools.Utils;
using static Goods.Tools.Utils.NumberUtils;

namespace Goods.Services.Drivers.Repositories;

internal class DriversRepository : IDriversRepository
{
    public void SaveDriver(DriverBlank driverBlank)
    {
        DatabaseUtils.Execute(
            Sql.DriverSave,
            (parameters) =>
            {
                parameters.AddWithValue("p_id", driverBlank.Id!.Value);
                parameters.AddWithValue("p_full_name", driverBlank.FullName!);
                parameters.AddWithValue("p_gender", (Int32)driverBlank.Gender!);
                parameters.AddWithValue("p_driver_license_category", (Int32)driverBlank.DriverLicenseCategory!);
                parameters.AddWithValue("p_birthday", (DateOnly)driverBlank.Birthday!);
                parameters.AddWithValue("p_experience", (Int32)driverBlank.Experience!);
                parameters.AddWithValue("p_pay_per_hour", (Decimal)driverBlank.PayPerHour!);
            }
        );
    }

    public Page<Driver> GetDriversPage(Int32 page, Int32 countInPage)
    {
        (Int32 offset, Int32 limit) = NormalizeRange(page, countInPage);

        return DatabaseUtils
            .GetPage(
                Sql.GetDriversPage,
                (parameters) =>
                {
                    parameters.AddWithValue("@p_offset", offset);
                    parameters.AddWithValue("@p_limit", limit);
                },
                (reader) => reader.ToDriverDb()
            )
            .Convert(driverDb => driverDb.ToDriver());
    }

    public Driver? GetDriver(Guid driverId)
    {
        return DatabaseUtils
            .Get<DriverDb?>(
                Sql.GetDriverById,
                (parameters) =>
                {
                    parameters.AddWithValue("@p_driver_id", driverId);
                },
                (reader) => reader.ToDriverDb()
            )
            ?.ToDriver();
    }

    public void MarkDriverAsRemoved(Guid driverId)
    {
        DatabaseUtils.Execute(
            Sql.MarkDriverAsRemoved,
            (parametres) =>
            {
                parametres.AddWithValue("p_driver_id", driverId);
            }
        );
    }
}
