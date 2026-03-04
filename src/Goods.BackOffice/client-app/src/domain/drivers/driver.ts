import { Page } from "../../tools/types/page";
import { DriverLicenseCategory } from "./enums/driverLicenseCategory";
import { Gender } from "./enums/gender";

export class Driver {
  constructor(
    public readonly id: string,
    public readonly first_name: string,
    public readonly second_name: string,
    public readonly last_name: string,
    public readonly gender: Gender,
    public readonly driver_license_category: DriverLicenseCategory,
    public readonly birthday: Date,
    public readonly experience: Date,
    public readonly pay_per_hour: number,
  ) {}
}

export function mapToDriversPage(data: any): Page<Driver> {
  return Page.convert(data, mapToDriver);
}

export function mapToDrivers(data: any[]): Driver[] {
  return data.map(mapToDriver);
}

export function mapToDriver(data: any): Driver {
  return new Driver(
    data.id,
    data.first_name,
    data.second_name,
    data.last_name,
    data.gender,
    data.driver_license_category,
    data.birthday,
    data.experience,
    data.pay_per_hour,
  );
}
