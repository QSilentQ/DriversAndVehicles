using Goods.Domain.Vehicles;
using Goods.Domain.Vehicles.Enums;
using Goods.Services.Vehicles.Repositories.Models;
using Npgsql;

namespace Goods.Services.Vehicles.Repositories.Converters;

internal static class VehiclesConverter
{
    internal static Vehicle[] ToVehicles(this VehicleDb[] vehicleDbs) => [.. vehicleDbs.Select(ToVehicle)];

    internal static Vehicle ToVehicle(this VehicleDb vehicleDb)
    {
        return new Vehicle(
            vehicleDb.Id,
            vehicleDb.DriverId,
            vehicleDb.Name,
            vehicleDb.StateNumber,
            vehicleDb.VehicleCategory,
            vehicleDb.AverageSpeed,
            vehicleDb.FuelConsumption,
            vehicleDb.CreatedDatetimeUTC,
            vehicleDb.ModifiedDatetimeUTC,
            vehicleDb.IsRemoved);
    }

    internal static VehicleDb ToVehicleDb(this NpgsqlDataReader reader)
    {
        return new VehicleDb(
            reader.GetGuid(reader.GetOrdinal("id")),
            reader.GetGuid(reader.GetOrdinal("driver_id")),
            reader.GetString(reader.GetOrdinal("name")),
            reader.GetString(reader.GetOrdinal("state_number")),
            (VehicleCategory)reader.GetInt32(reader.GetOrdinal("vehicle_category")),
            reader.GetDecimal(reader.GetOrdinal("average_speed")),
            reader.GetDecimal(reader.GetOrdinal("fuel_consumption")),
            reader.GetDateTime(reader.GetOrdinal("created_datetime_utc")),
            reader.GetDateTime(reader.GetOrdinal("modified_datetime_utc")),
            reader.GetBoolean(reader.GetOrdinal("is_removed"))
        );
    }
}
