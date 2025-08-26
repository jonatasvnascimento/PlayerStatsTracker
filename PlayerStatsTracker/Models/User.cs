using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerStatsTracker.Models;

public class User
{
    public required int Id { get; set; }
    public required string Username { get; set; }
    public string? UserCreated { get; set; }
    public string? UserModified { get; set; }
    public DateTime? DateCreated { get; set; }
    public DateTime? DateModified { get; set; }
}
