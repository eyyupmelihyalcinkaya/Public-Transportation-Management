using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPSService.Models
{
    public class SimulationConfig
    {
        public int UpdateIntervalSeconds { get; set; } = 5;
        public int MaxVehiclesPerRoute { get; set; } = 4;
        public double SpeedVariationPercentage { get; set; } = 20; 
        public double MinSpeedKmH { get; set; } = 10; 
        public double MaxSpeedKmH { get; set; } = 60;
        public bool EnableRandomDelays { get; set; } = true;
        public int DelayProbabilityPercentage { get; set; } = 15; // yüzde 15 gecikme ihtimali 
        public int MaxDelayMinutes { get; set; } = 10; // maksimum gecikme süresi dakika cinsinden
        public bool EnablePassengerSimulation { get; set; } = true; // Yolcu simülasyonu etkin mi?
    }
}
