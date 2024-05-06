using System;
using System.Collections.Generic;

// Interface for Carrier strategy
public interface ICarrierStrategy
{
    string GetCarrierDescription();
}

// Interface for Engine strategy
public interface IEngineStrategy
{
    string GetEngineDescription();
}

// Interface for Towing strategy
public interface ITowingStrategy
{
    string GetTowingDescription();
}

// Abstract class for optional features
public abstract class VehicleFeature
{
    protected Vehicle vehicle;

    // Constructor to initialize with a vehicle
    public VehicleFeature(Vehicle vehicle)
    {
        this.vehicle = vehicle;
    }

    // Abstract method to get feature description
    public abstract string GetDescription();
}

// Concrete decorator class for Sound System
public class SoundSystemDecorator : VehicleFeature
{
    // Constructor to initialize with a vehicle
    public SoundSystemDecorator(Vehicle vehicle) : base(vehicle) { }

    // Implementation of GetDescription method
    public override string GetDescription()
    {
        return vehicle.GetDescription() + ", Sound System";
    }
}

// Concrete decorator class for Wi-Fi
public class WiFiDecorator : VehicleFeature
{
    // Constructor to initialize with a vehicle
    public WiFiDecorator(Vehicle vehicle) : base(vehicle) { }

    // Implementation of GetDescription method
    public override string GetDescription()
    {
        return vehicle.GetDescription() + ", WiFi";
    }
}

// Concrete decorator class for Assist Camera
public class AssistCameraDecorator : VehicleFeature
{
    // Constructor to initialize with a vehicle
    public AssistCameraDecorator(Vehicle vehicle) : base(vehicle) { }

    // Implementation of GetDescription method
    public override string GetDescription()
    {
        return vehicle.GetDescription() + ", Assist Camera";
    }
}

// Abstract class for Vehicle
public abstract class Vehicle
{
    // References to strategies
    protected ICarrierStrategy carrier;
    protected IEngineStrategy engine;
    protected ITowingStrategy towing;
    protected List<Technician> observers = new List<Technician>();

    // Method to add observer (technician)
    public void AddObserver(Technician observer)
    {
        observers.Add(observer);
    }

    // Method to notify all observers
    public void NotifyObservers()
    {
        foreach (var observer in observers)
        {
            observer.Update(this);
        }
    }

    // Method to add optional feature
    public void AddFeature(VehicleFeature feature)
    {
        Console.WriteLine("Adding feature: " + feature.GetDescription());
        // Implement feature addition logic here
    }

    // Abstract method to get vehicle description
    public abstract string GetDescription();
}

// Concrete class for Motor Bike
public class MotorBike : Vehicle
{
    // Constructor to initialize with strategies
    public MotorBike(ICarrierStrategy carrier, IEngineStrategy engine, ITowingStrategy towing)
    {
        this.carrier = carrier;
        this.engine = engine;
        this.towing = towing;
    }

    // Implementation of GetDescription method
    public override string GetDescription()
    {
        return $"{engine.GetEngineDescription()} MotorBike with {carrier.GetCarrierDescription()}";
    }
}

// Concrete class for Light Motor Vehicle
public class LightMotorVehicle : Vehicle
{
    // Constructor to initialize with strategies
    public LightMotorVehicle(ICarrierStrategy carrier, IEngineStrategy engine, ITowingStrategy towing)
    {
        this.carrier = carrier;
        this.engine = engine;
        this.towing = towing;
    }

    // Implementation of GetDescription method
    public override string GetDescription()
    {
        return $"{engine.GetEngineDescription()} Light Motor Vehicle with {carrier.GetCarrierDescription()}";
    }
}

// Concrete class for Heavy Motor Vehicle
public class HeavyMotorVehicle : Vehicle
{
    // Constructor to initialize with strategies
    public HeavyMotorVehicle(ICarrierStrategy carrier, IEngineStrategy engine, ITowingStrategy towing)
    {
        this.carrier = carrier;
        this.engine = engine;
        this.towing = towing;
    }

    // Implementation of GetDescription method
    public override string GetDescription()
    {
        return $"{engine.GetEngineDescription()} Heavy Motor Vehicle with {carrier.GetCarrierDescription()}";
    }
}

// Concrete class for Technician
public class Technician
{
    private double hourlyRate;

    // Constructor to initialize hourly rate
    public Technician(double hourlyRate)
    {
        this.hourlyRate = hourlyRate;
    }

    // Method to update technician with vehicle information
    public void Update(Vehicle vehicle)
    {
        Console.WriteLine($"Technician updated about {vehicle.GetDescription()}");
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Create vehicles
        Vehicle motorBike = new MotorBike(new GoodAndDriverCarrier(), new SmallEngine(), new CanTowTowing());
        Vehicle lightMotorVehicle = new LightMotorVehicle(new Max2PeopleCarrier(), new MediumEngine(), new CanTowTowing());
        Vehicle heavyMotorVehicle = new HeavyMotorVehicle(new Max20PeopleCarrier(), new ExtraLargeEngine(), new CannotTowTowing());

        // Create technicians
        Technician motorBikeTechnician = new Technician(100);
        Technician lightMotorTechnician = new Technician(120);
        Technician heavyMotorTechnician = new Technician(140);

        // Add technicians as observers
        motorBike.AddObserver(motorBikeTechnician);
        lightMotorVehicle.AddObserver(lightMotorTechnician);
        heavyMotorVehicle.AddObserver(heavyMotorTechnician);

        // Simulate adding features
        motorBike.AddFeature(new SoundSystemDecorator(motorBike));
        lightMotorVehicle.AddFeature(new WiFiDecorator(lightMotorVehicle));
        heavyMotorVehicle.AddFeature(new AssistCameraDecorator(heavyMotorVehicle));

        // Notify observers
        motorBike.NotifyObservers();
        lightMotorVehicle.NotifyObservers();
        heavyMotorVehicle.NotifyObservers();
    }
}

// Concrete implementations for CarrierStrategy
public class GoodAndDriverCarrier : ICarrierStrategy
{
    public string GetCarrierDescription()
    {
        return "Good and Driver";
    }
}

public class Max2PeopleCarrier : ICarrierStrategy
{
    public string GetCarrierDescription()
    {
        return "2 people max, and bag";
    }
}

public class Max20PeopleCarrier : ICarrierStrategy
{
    public string GetCarrierDescription()
    {
        return "20 people max";
    }
}

// Concrete implementations for EngineStrategy
public class SmallEngine : IEngineStrategy
{
    public string GetEngineDescription()
    {
        return "Small";
    }
}

public class MediumEngine : IEngineStrategy
{
    public string GetEngineDescription()
    {
        return "Medium";
    }
}

public class ExtraLargeEngine : IEngineStrategy
{
    public string GetEngineDescription()
    {
        return "Extra Large";
    }
}

// Concrete implementations for TowingStrategy
public class CanTowTowing : ITowingStrategy
{
    public string GetTowingDescription()
    {
        return "Can Tow";
    }
}

public class CannotTowTowing : ITowingStrategy
{
    public string GetTowingDescription()
    {
        return "Cannot Tow";
    }
}
