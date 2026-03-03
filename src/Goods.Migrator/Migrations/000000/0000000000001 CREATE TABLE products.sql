CREATE TABLE products (
	id uuid NOT NULL,
	category int4 NOT NULL,
	name varchar NOT NULL,
	description varchar NULL,
	price float8 NOT NULL,
	createddatetimeutc timestamp NOT NULL,
	modifieddatetimeutc timestamp NULL,
	isremoved bool NOT NULL,
	CONSTRAINT products_pk PRIMARY KEY (id)
);

CREATE TABLE drivers (
  id uuid NOT NULL,
  full_name varchar NOT NULL,
  gender int4 NOT NULL,
  driver_license_category int4 NOT NULL,
  birthday date NOT NULL,
  experience int4 NOT NULL,
  pay_per_hour decimal(4,2) NULL,
  is_removed bool NULL,
  CONSTRAINT drivers_pk PRIMARY KEY (id)
);

CREATE TABLE vehicles (
  id uuid NOT NULL,
  driver_id uuid NULL,
  name varchar NOT NULL,
  state_number varchar NOT NULL,
  vehicle_category int4 NOT NULL,
  average_speed decimal(3,1) NOT NULL,
  fuel_consumption decimal(3,1) NOT NULL,
  is_removed bool NULL,
  CONSTRAINT vehicles_pk PRIMARY KEY (id),
  CONSTRAINT vehicles_driver_fk FOREIGN KEY (driver_id) REFERENCES drivers(id)
);