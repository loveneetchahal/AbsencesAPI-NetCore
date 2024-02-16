using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsencesAPI.Common.Model;

public class Absence : BaseEntity
{
    public DateTime Date { get; set; } = default!;
    public string Type { get; set; } = default!;
    public List<Employee> Employees { get; set; } = default!;
    public Stats Statistic { get; set; } = default!;
}
