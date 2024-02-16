using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsencesAPI.Common.Model;

public class Employee : BaseEntity
{
    public string Name { get; set; } = default!;
    public int? EmployeeNumber { get; set; }
    public string Department { get; set; } = default!;
    public List<Absence> Absences { get; set; } = default!;
    public Management Manager { get; set; } = default!;
}
