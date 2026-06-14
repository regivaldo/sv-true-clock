using System;
using System.Collections.Generic;

namespace TrueClock;

internal sealed class ModConfig
{
    public bool Use24HourClock { get; set; } = true;

    public int ClockX { get; set; } = -1;

    public int ClockY { get; set; } = 8;

    public List<AlertConfig> Alerts { get; set; } = AlertConfig.CreateDefaultSlots();

    public void Normalize()
    {
        this.ClockX = Math.Clamp(this.ClockX, -1, 10000);
        this.ClockY = Math.Clamp(this.ClockY, 0, 10000);

        this.Alerts ??= new List<AlertConfig>();

        while (this.Alerts.Count < AlertConfig.SlotCount)
            this.Alerts.Add(new AlertConfig());

        if (this.Alerts.Count > AlertConfig.SlotCount)
            this.Alerts.RemoveRange(AlertConfig.SlotCount, this.Alerts.Count - AlertConfig.SlotCount);

        foreach (AlertConfig alert in this.Alerts)
        {
            alert.Hour = Math.Clamp(alert.Hour, 0, 23);
            alert.Minute = Math.Clamp(alert.Minute, 0, 59);
            alert.Message ??= string.Empty;
        }
    }
}

internal sealed class AlertConfig
{
    public const int SlotCount = 5;

    public bool Enabled { get; set; }

    public int Hour { get; set; } = 8;

    public int Minute { get; set; }

    public string Message { get; set; } = string.Empty;

    public static List<AlertConfig> CreateDefaultSlots()
    {
        List<AlertConfig> alerts = new();

        for (int index = 0; index < SlotCount; index++)
            alerts.Add(new AlertConfig());

        return alerts;
    }
}
