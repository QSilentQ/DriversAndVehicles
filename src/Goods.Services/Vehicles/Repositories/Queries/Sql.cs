namespace Goods.Services.Vehicles.Repositories.Queries;

internal static class Sql
{
    internal static String VehicleSave =>
        @"
            INSERT INTO vehicles (
                id,
                driver_id,
                name,
                state_number,
                vehicle_category,
                average_speed,
                fuel_consumption,
                is_removed
            )
            VALUES (
                @p_id,
                @p_driver_id,
                @p_name,
                @p_state_number,
                @p_vehicle_category,
                @p_average_speed,
                @p_fuel_consumption,
                FALSE
            )
	        ON CONFLICT (id) DO UPDATE SET
                driver_id = @p_driver_id,
	        	name = @p_name,
                state_number = @p_state_number,
                vehicle_category = @p_vehicle_category,
                average_speed = @p_average_speed,
                fuel_consumption = @p_fuel_consumption
        ";

    internal static String GetVehiclesPage =>
        @"
			SELECT COUNT(*) OVER() as count, * FROM vehicles
			WHERE NOT is_removed
			OFFSET @p_offset
			LIMIT @p_limit
		";

    internal static String GetVehicleById => "SELECT * FROM vehicles WHERE id = @p_vehicle_id AND NOT is_removed";

    internal static String MarkVehicleAsRemoved =>
        @"
			UPDATE vehicles
			SET is_removed = TRUE,
			WHERE id = @p_vehicle_id
		";
}

