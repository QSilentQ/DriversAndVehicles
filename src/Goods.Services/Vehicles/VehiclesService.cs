using Goods.Domain.Products;
using Goods.Domain.Services;
using Goods.Domain.Vehicles;
using Goods.Services.Products.Repositories;
using Goods.Services.Vehicles.Repositories.Interfaces;
using Goods.Tools.Types.Results;

namespace Goods.Services.Vehicles;

public class VehiclesService(IVehiclesRepository vehiclesRepository) : IVehicleService
{
    private const Int32 MAX_VEHICLE_NAME_LENGTH = 255;

    public Result SaveVehicle(VehicleBlank vehicleBlank)
    {
        if (vehicleBlank.VehicleCategory is null)
            return Result.Failed("Выберите категорию транспортного средства");

        if (!Enum.IsDefined(vehicleBlank.VehicleCategory.Value))
            return Result.Failed("Выбранная категория не существует. Пожалуйста, выберите другую");

        if (String.IsNullOrWhiteSpace(vehicleBlank.Name))
            return Result.Failed("Введите название транспортного средства");

        if (String.IsNullOrWhiteSpace(vehicleBlank.StateNumber))
            return Result.Failed("Введите номер транспортного средства");

        if (vehicleBlank.AverageSpeed is null)
            return Result.Failed("Введите среднюю скорость транспортного средства");

        if (vehicleBlank.FuelConsumption is null)
            return Result.Failed("Введите средний расход топлива транспортного средства");

        if (vehicleBlank.Name.Length == MAX_VEHICLE_NAME_LENGTH)
            return Result.Failed($"Название транспортного средства слишком длинное. Максимально допустимо {MAX_VEHICLE_NAME_LENGTH} символов");

        vehicleBlank.Id ??= Guid.NewGuid();

        vehiclesRepository.SaveVehicle(vehicleBlank);

        return Result.Success();
    }

    public Page<Vehicle> GetVehiclesPage(Int32 page, Int32 countInPage)
    {
        return vehiclesRepository.GetVehiclesPage(page, countInPage);
    }

    public Vehicle? GetVehicle(Guid vehicleId)
    {
        return vehiclesRepository.GetVehicle(vehicleId);
    }

    public Result MarkVehicleAsRemoved(Guid vehicleId)
    {
        Vehicle? existVehicle = GetVehicle(vehicleId);
        if (existVehicle is null)
            return Result.Failed("Транспортное средство не найдено. Возможно, оно было удалено");

        vehiclesRepository.MarkVehicleAsRemoved(vehicleId);
        return Result.Success();
    }
}
