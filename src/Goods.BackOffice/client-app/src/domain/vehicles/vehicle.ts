import { Page } from "../../tools/types/page";
import { VehicleCategory } from "./enums/vehicleCategory";

export class Vehicle {
  constructor(
    public readonly id: string,
    public readonly driver_id: string,
    public readonly name: string,
    public readonly state_number: string,
    public readonly vehicle_category: VehicleCategory,
    public readonly average_speed: number,
    public readonly fuel_consumption: number,
  ) {}
}

export function mapToVehiclesPage(data: any): Page<Vehicle> {
  return Page.convert(data, mapToVehicle);
}

export function mapToVehicles(data: any[]): Vehicle[] {
  return data.map(mapToVehicle);
}

export function mapToVehicle(data: any): Vehicle {
  return new Vehicle(
    data.id,
    data.driver_id,
    data.name,
    data.state_number,
    data.vehicle_category,
    data.average_speed,
    data.fuel_consumption,
  );
}