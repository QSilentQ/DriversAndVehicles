import { DriverLicenseCategory } from "./enums/driverLicenseCategory";
import { Gender } from "./enums/gender";
import { Driver } from "./driver";

export interface DriverBlank {
  id: string | null;
  first_name: string | null;
  second_name: string | null;
  last_name: string | null;
  gender: Gender | null;
  driver_license_category: DriverLicenseCategory | null;
  birthday: Date | null;
  experience: Date | null;
  pay_per_hour: number | null;
}

export namespace DriverBlank {
  export function getDefault(): DriverBlank {
    return {
      id: null,
      first_name: null,
      second_name: null,
      last_name: null,
      gender: null,
      driver_license_category: null,
      birthday: null,
      experience: null,
      pay_per_hour: null
    };
  }

  export function fromDriver(driver: Driver): DriverBlank {
    return {
      id: driver.id,
      first_name: driver.first_name,
      second_name: driver.second_name,
      last_name: driver.last_name,
      gender: driver.gender,
      driver_license_category: driver.driver_license_category,
      birthday: driver.birthday,
      experience: driver.experience,
      pay_per_hour: driver.pay_per_hour
    }
  }
}