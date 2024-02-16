using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbsencesAPI.Common.Model;

public class Stats : BaseEntity
{
    public string Description { get; set; } = default!;
    public int? Value { get; set; }
    public List<Absence> Absences { get; set; } = default!;
}
