using AbsencesAPI.Business.Services;
using AbsencesAPI.Business.Validation.Absence;
using AbsencesAPI.Business.Validation.Employee;
using AbsencesAPI.Business.Validation.Management;
using AbsencesAPI.Business.Validation.Stats;
using AbsencesAPI.Common.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AbsencesAPI.Business;

public class DIConfiguration
{
    public static void RegisterServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DtoEntityMapperProfile));
        services.AddScoped<IManangementService, ManagementService>();
        services.AddScoped<IStatsService, StatsService>();
        services.AddScoped<IEmployeeService, EmployeeService>();
        services.AddScoped<IAbsenceService, AbsenceService>();

        services.AddScoped<ManagementCreateValidator>();
        services.AddScoped<ManagementUpdateValidator>();
        services.AddScoped<StatsCreateValidator>();
        services.AddScoped<StatsUpdateValidator>();
        services.AddScoped<EmployeeCreateValidator>();
        services.AddScoped<EmployeeUpdateValidator>();
        services.AddScoped<AbsenceCreateValidator>();
        services.AddScoped<AbsenceUpdateValidator>();
    }
}
