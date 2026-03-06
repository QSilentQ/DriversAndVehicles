using Goods.Domain.Drivers;
using Goods.Domain.Services;
using Goods.Domain.Vehicles;
using Goods.Tools.Types;
using Quartz;

namespace Goods.Scheduler.Jobs;

public class ReassignDrivers(IVehicleService vehicleService, IDriverService driverService) : IJob
{
    private readonly IVehicleService VehicleService = vehicleService;
    private readonly IDriverService DriverService = driverService;

    public async Task Execute(IJobExecutionContext context)
    {
        Page<Vehicle> vehiclesPage = VehicleService.GetVehiclesPage(1, 1000);
        Page<Driver> driversPage = DriverService.GetDriversPage(1, 1000);
        Vehicle[] vehicles = vehiclesPage.Values;
        Driver[] drivers = driversPage.Values;

        if (vehicles.Length == 0)
        {
            Console.WriteLine("Нет транспортных средств для переназначения.");
            await Task.CompletedTask;
            return;
        }

        foreach (Vehicle vehicle in vehicles)
        {
            Guid? driverId = drivers.Length > 0
                ? drivers[Random.Shared.Next(drivers.Length)].Id
                : null;

            VehicleBlank blank = ToBlank(vehicle, driverId);
            VehicleService.SaveVehicle(blank);
        }

        Console.WriteLine($"Переназначение завершено. Обработано ТС: {vehicles.Length}.");
        await Task.CompletedTask;
    }

    private static VehicleBlank ToBlank(Vehicle vehicle, Guid? driverId)
    {
        return new VehicleBlank
        {
            Id = vehicle.Id,
            DriverId = driverId,
            Name = vehicle.Name,
            StateNumber = vehicle.StateNumber,
            VehicleCategory = vehicle.VehicleCategory,
            AverageSpeed = vehicle.AverageSpeed,
            FuelConsumption = vehicle.FuelConsumption,
            ModifiedDatetimeUTC = DateTime.UtcNow,
            LastDriverChangedDatetimeUtc = driverId is not null ? DateTime.UtcNow : null,
            IsRemoved = vehicle.IsRemoved
        };
    }
}
