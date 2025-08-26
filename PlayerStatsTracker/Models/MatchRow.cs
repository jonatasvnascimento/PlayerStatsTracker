using System.Windows.Media;

namespace PlayerStatsTracker.Models;

public partial class MatchRow : ObservableObject
{
    [ObservableProperty] private int kills;
    [ObservableProperty] private int deaths;
    [ObservableProperty] private int assists;
    [ObservableProperty] private int headshots;
    [ObservableProperty] private int rounds;
    [ObservableProperty] private int damage;
    [ObservableProperty] private int damageTaken;
    [ObservableProperty] private bool win;
    [ObservableProperty] private int firstKills;
    [ObservableProperty] private int firstDeaths;
    [ObservableProperty] private int fiveK;
    [ObservableProperty] private int flashAssists;
}
