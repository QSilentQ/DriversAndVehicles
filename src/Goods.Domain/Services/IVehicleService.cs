using Goods.Domain.Vehicles;
using Goods.Tools.Types.Results;

namespace Goods.Domain.Services;

public interface IVehicleService
{
    Result SaveVehicle(VehicleBlank vehicleBlank);
    Page<Vehicle> GetVehiclesPage(Int32 page, Int32 countInPage);
    Vehicle? GetVehicle(Guid driver_id);
    Result MarkVehicleAsRemoved(Guid driver_id);
}
