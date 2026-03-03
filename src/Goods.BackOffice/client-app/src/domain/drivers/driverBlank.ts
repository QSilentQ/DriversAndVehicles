import { DriverLicenseCategory } from "./enums/driverLicenseCategory";
import { Gender } from "./enums/gender";
import { Driver } from "./driver";

export interface DriverBlank {
  id: string | null;
  full_name: string | null;
  gender: Gender | null;
  driver_license_category: DriverLicenseCategory | null;
  birthday: Date | null;
  experience: number | null;
  pay_per_hour: number | null;
}

export namespace DriverBlank {
  export function getDefault(): DriverBlank {
    return {
      id: null,
      full_name: null,
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
      full_name: driver.full_name,
      gender: driver.gender,
      driver_license_category: driver.driver_license_category,
      birthday: driver.birthday,
      experience: driver.experience,
      pay_per_hour: driver.pay_per_hour
    }
  }
}