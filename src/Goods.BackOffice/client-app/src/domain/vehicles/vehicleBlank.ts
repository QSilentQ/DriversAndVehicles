import { VehicleCategory } from "./enums/vehicleCategory";
import { Vehicle } from "./vehicle";

export interface VehicleBlank {
  id: string | null;
  driver_id: string | null;
  name: string | null;
  state_number: string | null;
  vehicle_category: VehicleCategory | null;
  average_speed: number | null;
  fuel_consumption: number | null;
}

export namespace VehicleBlank {
  export function getDefault(): VehicleBlank {
    return {
      id: null,
      driver_id: null,
      name: null,
      state_number: null,
      vehicle_category: null,
      average_speed: null,
      fuel_consumption: null
    };
  }

  export function fromVehicle(vehicle: Vehicle): VehicleBlank {
    return {
      id: vehicle.id,
      driver_id: vehicle.driver_id,
      name: vehicle.name,
      state_number: vehicle.state_number,
      vehicle_category: vehicle.vehicle_category,
      average_speed: vehicle.average_speed,
      fuel_consumption: vehicle.fuel_consumption
    }
  }
}