using Goods.Domain.Vehicles;
using Goods.Services.Vehicles.Repositories.Converters;
using Goods.Services.Vehicles.Repositories.Interfaces;
using Goods.Services.Vehicles.Repositories.Models;
using Goods.Services.Vehicles.Repositories.Queries;
using Goods.Tools.Utils;
using static Goods.Tools.Utils.NumberUtils;

namespace Goods.Services.Vehicles.Repositories
{
    internal class VehiclesRepository : IVehiclesRepository
    {
        public void SaveVehicle(VehicleBlank vehicleBlank)
        {
            DatabaseUtils.Execute(
                Sql.VehicleSave,
                (parametres) =>
                {
                    parametres.AddWithValue("p_id", vehicleBlank.Id!.Value);
                    parametres.AddWithValue("p_driver_id", vehicleBlank.DriverId);
                    parametres.AddWithValue("p_name", vehicleBlank.Name!);
                    parametres.AddWithValue("p_state_number", vehicleBlank.StateNumber!);
                    parametres.AddWithValue("p_vehicle_category", (Int32)vehicleBlank.VehicleCategory!);
                    parametres.AddWithValue("p_average_speed", (Decimal)vehicleBlank.AverageSpeed!);
                    parametres.AddWithValue("p_fuel_consumption", (Decimal)vehicleBlank.FuelConsumption!);
                }
            );
        }

        public Page<Vehicle> GetVehiclesPage(Int32 page, Int32 countInPage)
        {
            (Int32 offset, Int32 limit) = NormalizeRange(page, countInPage);

            return DatabaseUtils
                .GetPage(
                    Sql.GetVehiclesPage,
                    (parametres) =>
                    {
                        parametres.AddWithValue("@p_offset", offset);
                        parametres.AddWithValue("@p_limit", limit);
                    },
                    (reader) => reader.ToVehicleDb()
                )
                .Convert(vehicleDb => vehicleDb.ToVehicle());
        }

        public Vehicle? GetVehicle(Guid vehicleId)
        {
            return DatabaseUtils
                .Get<VehicleDb?>(
                    Sql.GetVehicleById,
                    (parametres) =>
                    {
                        parametres.AddWithValue("@p_vehicle_id", vehicleId);
                    },
                    (reader) => reader.ToVehicleDb()
                )
                ?.ToVehicle();
        }

        public void MarkVehicleAsRemoved(Guid vehicleId)
        {
            DatabaseUtils.Execute(
                Sql.MarkVehicleAsRemoved,
                (parametres) =>
                {
                    parametres.AddWithValue("p_vehicle_id", vehicleId);
                }
            );
        }
    }
}
