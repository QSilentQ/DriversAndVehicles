import {
	Container,
	Paper,
	Table,
	TableBody,
	TableCell,
	TableContainer,
	TableHead,
	TableRow,
	Typography
} from '@mui/material';
import React, { useEffect, useState } from 'react';
import { Vehicle } from '../../domain/vehicles/vehicle';
import { VehiclesProvider } from '../../domain/vehicles/vehicleProvider';
import { VehicleCategory } from '../../domain/vehicles/enums/vehicleCategory';
import { Button } from '../../shared/components/buttons/button';
import { ConfirmModal } from '../../shared/components/modals/confirmModal';
import { Notification } from '../../shared/components/notification';
import { TablePagination } from '../../shared/components/tablePagination';
import { ConfirmModalState } from '../../shared/types/confirmModalState';
import { Pagination } from '../../tools/types/pagination';
import { VehicleEditorModal } from './modals/vehicleEditorModal';

type VehicleEditorModalState = {
	vehicleId: string | null;
	isOpen: boolean;
};

interface RemoveVehicleConfirmModalState extends ConfirmModalState {
	vehicleId: string | null;
}

export function VehiclesPage() {
	const [vehicles, setVehicles] = useState<Vehicle[]>([]);
	const [pagination, setPagination] = useState<Pagination>(Pagination.default);

	const [vehicleEditorModalState, setVehicleEditorModalState] = useState<VehicleEditorModalState>({
		vehicleId: null,
		isOpen: false
	});
	const [removeVehicleConfirmModalState, setRemoveVehicleConfirmModalState] =
		useState<RemoveVehicleConfirmModalState>({
			vehicleId: null,
			...ConfirmModalState.getClosed()
		});

	const [errorMessage, setErrorMessage] = useState<string | null>(null);

	useEffect(() => {
		loadVehiclesPage({ ...pagination });
	}, []);

	async function loadVehiclesPage(newPagination: Pagination) {
		const vehiclesPage = await VehiclesProvider.getVehiclesPage(
			newPagination.page,
			newPagination.countInPage
		);

		setVehicles(vehiclesPage.values);
		setPagination((pagination) => ({
			...pagination,
			page: newPagination.page,
			countInPage: newPagination.countInPage,
			totalRows: vehiclesPage.totalRows
		}));
	}

	function changePage(page: number) {
		loadVehiclesPage({ ...pagination, page });
	}

	function changeCountInPage(countInPage: number) {
		loadVehiclesPage({ ...pagination, countInPage });
	}

	function openVehicleEditorModal(vehicleId?: string) {
		setVehicleEditorModalState({ vehicleId: vehicleId ?? null, isOpen: true });
	}

	function closeVehicleEditorModal(isEdited: boolean) {
		if (isEdited) loadVehiclesPage({ ...pagination, page: 1 });
		setVehicleEditorModalState({ vehicleId: null, isOpen: false });
	}

	function openRemoveVehicleConfirmModal(vehicleId: string, name: string) {
		setRemoveVehicleConfirmModalState({
			vehicleId,
			...ConfirmModalState.getOpen(
				`Вы действительно хотите удалить транспортное средство "${name}"?`
			)
		});
	}

	async function closeRemoveVehicleConfirmModal(isConfirmed: boolean) {
		if (isConfirmed) {
			if (removeVehicleConfirmModalState.vehicleId == null)
				throw 'Cannot remove vehicle with VehicleId = null';

			const result = await VehiclesProvider.markVehicleAsRemoved(
				removeVehicleConfirmModalState.vehicleId
			);
			if (!result.isSuccess) {
				setErrorMessage(result.errors.map((error) => error.message).join('. '));
				return;
			}

			loadVehiclesPage({ ...pagination, page: 1 });
		}

		setRemoveVehicleConfirmModalState({ vehicleId: null, ...ConfirmModalState.getClosed() });
	}

	return (
		<Container
			sx={{ height: '100%', display: 'flex', flexDirection: 'column', gap: '12px' }}
			maxWidth={false}
			disableGutters>
			<Paper
				elevation={3}
				sx={{
					display: 'flex',
					alignItems: 'center',
					justifyContent: 'space-between',
					paddingX: '12px',
					paddingY: '6px'
				}}>
				<Typography variant='h6'>Транспортные средства</Typography>
				<Button variant='add' title='Создать' onClick={() => openVehicleEditorModal()} />
			</Paper>
			<Paper elevation={3} sx={{ height: 'calc(100% - 52px)' }}>
				<TableContainer sx={{ height: 'inherit' }}>
					<Table stickyHeader>
						<TableHead>
							<TableRow>
								<TableCell>Название</TableCell>
								<TableCell>Гос. номер</TableCell>
								<TableCell>Категория</TableCell>
								<TableCell>Средняя скорость</TableCell>
								<TableCell>Расход топлива</TableCell>
								<TableCell>Управление</TableCell>
							</TableRow>
						</TableHead>
						<TableBody>
							{vehicles.length === 0 && (
								<TableRow>
									<TableCell colSpan={6}>Пусто</TableCell>
								</TableRow>
							)}
							{vehicles.map((vehicle) => {
								return (
									<TableRow key={`vehicle__${vehicle.id}`}>
										<TableCell width='20%'>{vehicle.name}</TableCell>
										<TableCell width='15%'>{vehicle.state_number}</TableCell>
										<TableCell width='15%'>
											{vehicle.vehicle_category != null
												? VehicleCategory.getDisplayName(vehicle.vehicle_category)
												: '—'}
										</TableCell>
										<TableCell width='15%'>{vehicle.average_speed ?? '—'}</TableCell>
										<TableCell width='15%'>{vehicle.fuel_consumption ?? '—'}</TableCell>
										<TableCell>
											<Button
												type='icon'
												variant='edit'
												size='small'
												onClick={() => openVehicleEditorModal(vehicle.id)}
											/>
											<Button
												type='icon'
												variant='remove'
												size='small'
												onClick={() =>
													openRemoveVehicleConfirmModal(vehicle.id, vehicle.name)
												}
											/>
										</TableCell>
									</TableRow>
								);
							})}
						</TableBody>
					</Table>
				</TableContainer>
				<TablePagination
					countInPageOptions={Pagination.getPageSizeOptions()}
					page={pagination.page}
					countInPage={pagination.countInPage}
					totalRows={pagination.totalRows}
					changePage={(page) => changePage(page)}
					changeCountInPage={(countInPage) => changeCountInPage(countInPage)}
				/>
			</Paper>
			<VehicleEditorModal
				vehicleId={vehicleEditorModalState.vehicleId}
				onClose={closeVehicleEditorModal}
				isOpen={vehicleEditorModalState.isOpen}
			/>
			<ConfirmModal
				title={removeVehicleConfirmModalState.title}
				onClose={(isConfirmed) => closeRemoveVehicleConfirmModal(isConfirmed)}
				isOpen={removeVehicleConfirmModalState.isOpen}
			/>
			{String.isNotNullOrWhitespace(errorMessage) && (
				<Notification severity='error' message={errorMessage} onClose={() => setErrorMessage(null)} />
			)}
		</Container>
	);
}
