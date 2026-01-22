using AutoMapper;
using bidPursuit.Application.Vehicles.Commands.CreateVehicle;
using bidPursuit.Application.Vehicles.Commands.UpdateVehicle;
using bidPursuit.Domain.Entities;

namespace bidPursuit.Application.Vehicles.Mapper;

public class VehicleMapper : Profile
{
    public VehicleMapper()
    {
        CreateMap<CreateVehicleCommand, Vehicle>();
        CreateMap<UpdateVehicleCommand, Vehicle>();
    }
}
