using AutoMapper;
using NZWalks.Models.Domains;
using NZWalks.Models.DTOs;

namespace NZWalks.Mappings;

public class AutoMappingProfiles : Profile
{
    public AutoMappingProfiles()
    {
        CreateMap<Region, RegionDto>().ReverseMap();  //CreateMap<Source, Destination>();    
        CreateMap<AddRegionRequestDto, Region>().ReverseMap();
        CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
        CreateMap<AddWalkRequestDto, Walk>().ReverseMap();
        CreateMap<Walk, WalkDto>().ReverseMap();
        CreateMap<UpdateWalkRequestDto, Walk>().ReverseMap();
    }
}

