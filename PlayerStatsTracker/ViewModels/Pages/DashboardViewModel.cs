using PlayerStatsTracker.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace PlayerStatsTracker.ViewModels.Pages
{
    public partial class DashboardViewModel : ObservableObject
    {
        public ObservableCollection<MatchRow> Matches { get; } = new();

        [ObservableProperty] private MatchRow? selectedMatch;

        // Totais e KPIs
        [ObservableProperty] private int games;
        [ObservableProperty] private int wins;
        [ObservableProperty] private int losses;
        [ObservableProperty] private int totalKills;
        [ObservableProperty] private int totalDeaths;
        [ObservableProperty] private int totalAssists;
        [ObservableProperty] private int totalHeadshots;
        [ObservableProperty] private int totalRounds;
        [ObservableProperty] private int totalDamage;

        [ObservableProperty] private double kd;               // Kills / Deaths
        [ObservableProperty] private double kad;              // (Kills + Assists) / Deaths
        [ObservableProperty] private double headshotPercent;  // 0–100
        [ObservableProperty] private double winPercent;       // 0–100
        [ObservableProperty] private double killsPerMatch;
        [ObservableProperty] private double damagePerRound;
        [ObservableProperty] private double firstKillsTotal;
        [ObservableProperty] private double firstDeathsTotal;

        public DashboardViewModel()
        {
            // exemplos (pode remover)
            Matches.Add(new MatchRow { Kills = 20, Deaths = 10, Assists = 5, Headshots = 9, Rounds = 24, Damage = 1800, DamageTaken = 2100, Win = false, FirstKills = 2, FirstDeaths = 1 });
            Matches.Add(new MatchRow { Kills = 10, Deaths = 13, Assists = 6, Headshots = 4, Rounds = 26, Damage = 1500, DamageTaken = 2200, Win = false, FirstKills = 1, FirstDeaths = 2 });

            HookCollection();
            Recalculate();
        }

        private void HookCollection()
        {
            Matches.CollectionChanged += (_, e) =>
            {
                if (e.NewItems != null)
                    foreach (MatchRow m in e.NewItems) m.PropertyChanged += OnRowChanged;

                if (e.OldItems != null)
                    foreach (MatchRow m in e.OldItems) m.PropertyChanged -= OnRowChanged;

                Recalculate();
            };

            foreach (var m in Matches)
                m.PropertyChanged += OnRowChanged;
        }

        private void OnRowChanged(object? sender, PropertyChangedEventArgs e) => Recalculate();

        private static double SafeDiv(double a, double b) => b == 0 ? 0 : a / b;

        private void Recalculate()
        {
            Games = Matches.Count == 0 ? 1 : Matches.Count;

            Wins = Matches.Count(m => m.Win);
            Losses = (Matches.Count) - Wins;

            TotalKills = Matches.Sum(m => m.Kills);
            TotalDeaths = Matches.Sum(m => m.Deaths);
            TotalAssists = Matches.Sum(m => m.Assists);
            TotalHeadshots = Matches.Sum(m => m.Headshots);
            TotalRounds = Matches.Sum(m => m.Rounds);
            TotalDamage = Matches.Sum(m => m.Damage);

            Kd = SafeDiv(TotalKills, TotalDeaths);
            Kad = SafeDiv(TotalKills + TotalAssists, TotalDeaths);
            HeadshotPercent = TotalKills == 0 ? 0 : SafeDiv(TotalHeadshots, TotalKills) * 100.0;
            WinPercent = SafeDiv(Wins, Matches.Count) * 100.0;
            KillsPerMatch = SafeDiv(TotalKills, Games);
            DamagePerRound = SafeDiv(TotalDamage, TotalRounds);
            FirstKillsTotal = Matches.Sum(m => m.FirstKills);
            FirstDeathsTotal = Matches.Sum(m => m.FirstDeaths);
        }

        // Commands
        [RelayCommand] private void AddRow() => Matches.Add(new MatchRow());
        [RelayCommand] private void Clear() => Matches.Clear();
        [RelayCommand(CanExecute = nameof(CanRemoveRow))]
        private void RemoveRow(MatchRow? row)
        {
            if (row is null) return;
            Matches.Remove(row);
        }
        private bool CanRemoveRow(MatchRow? row) => row is not null;
    }
}
