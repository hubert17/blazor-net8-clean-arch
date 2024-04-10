using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorNet8CleanArch.Application.DTOs;

public class TimesheetDto
{
    public int Id { get; set; }
    public int ProviderId { get; set; }
    public List<TimesheetEntry>? TimesheetEntryRows { get; set; }
    public bool IsDraft { get; set; }
    public int BatchId { get; set; }
}

public class TimesheetEntry
{
    public int Id { get; set; } 
    public DateTime TimeIn { get; set; } 
    public DateTime TimeOut { get; set; } 

}

