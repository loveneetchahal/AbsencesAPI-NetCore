using AbsencesAPI.Common.DTOS.Absence;
using AbsencesAPI.Common.DTOS.Employee;
using AbsencesAPI.Common.DTOS.Management;
using AbsencesAPI.Common.DTOS.Stats;
using AbsencesAPI.Common.Model;
using AutoMapper;

namespace AbsencesAPI.Business;

public class DtoEntityMapperProfile : Profile
{
	public DtoEntityMapperProfile()
	{
		CreateMap<ManagementCreate, Management>()
			.ForMember(dest => dest.Id, opt => opt.Ignore());
		CreateMap<ManagementUpdate, Management>();
        CreateMap<Management, ManagementGet>();

        CreateMap<StatsCreate, Stats>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
        CreateMap<StatsUpdate, Stats>();
        CreateMap<Stats, StatsGet>()
            .ForMember(dest => dest.Absences, opt => opt.Ignore());

        CreateMap<EmployeeCreate, Employee>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Absences, opt => opt.Ignore())
            .ForMember(dest => dest.Manager, opt => opt.Ignore());
        CreateMap<EmployeeUpdate, Employee>()
            .ForMember(dest => dest.Absences, opt => opt.Ignore())
            .ForMember(dest => dest.Manager, opt => opt.Ignore());
        CreateMap<Employee, EmployeeDetails>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Manager, opt => opt.Ignore());
        CreateMap<Employee, EmployeeList>();

        CreateMap<AbsenceCreate, Absence>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Employees, opt => opt.Ignore())
            .ForMember(dest => dest.Statistic, opt => opt.Ignore());
        CreateMap<AbsenceUpdate, Absence>()
            .ForMember(dest => dest.Employees, opt => opt.Ignore())
            .ForMember(dest => dest.Statistic, opt => opt.Ignore());
        CreateMap<Absence, AbsenceGet>()
            .ForMember(dest => dest.Employees, opt => opt.Ignore());

    }
}