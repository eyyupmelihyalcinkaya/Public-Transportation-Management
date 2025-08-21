using GPSService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSService.Interfaces
{
    public interface IVehicleLocationSimulator
    {

        Task StartSimulationAsync(CancellationToken cancellationToken);
        Task StopSimulationAsync();
        Task<IEnumerable<Vehicle>> GetActiveVehicleAsync();
        Task UpdateVehicleLocationAsync(Vehicle vehicle);
        Task<VehicleLocation> GenerateNextLocationAsync(Vehicle vehicle);
        Task StartRouteSimulation(int routeId);
        Task StopRouteSimulation(int routeId);
        bool IsRouteSimulationActive(int routeId);
        Task<IEnumerable<Vehicle>> GetRouteVehiclesAsync(int routeId);
        int GetActiveRouteCount();
        Task<int> GetTotalActiveVehicleCountAsync();
        Task<SimulationStatistics> GetSimulationStatisticsAsync();
        Task<Vehicle?> GetVehicleAsync(string vehicleId);
        Task HandleRouteSelectionCommandAsync(RouteSelectionCommand command);




    }
}
