using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;

namespace TrueClock;

public sealed class ModEntry : Mod
{
    private static readonly TimeSpan AlertDuration = TimeSpan.FromSeconds(5);

    private readonly Dictionary<int, DateTime> lastAlertDateBySlot = new();

    private ModConfig config = new();
    private Texture2D? pixel;
    private Texture2D? clockIcon;
    private SoundEffect? alarmSound;
    private SoundEffectInstance? alarmInstance;
    private DateTime alertUntil = DateTime.MinValue;
    private string activeAlertMessage = string.Empty;
    private Vector2 clockPosition = new(16, 112);

    public override void Entry(IModHelper helper)
    {
        this.config = helper.ReadConfig<ModConfig>();
        this.config.Normalize();
        helper.WriteConfig(this.config);

        helper.Events.GameLoop.GameLaunched += this.OnGameLaunched;
        helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
        helper.Events.GameLoop.ReturnedToTitle += this.OnReturnedToTitle;
        helper.Events.Display.RenderedHud += this.OnRenderedHud;
        helper.Events.Display.RenderedWorld += this.OnRenderedWorld;
    }

    private void OnGameLaunched(object? sender, GameLaunchedEventArgs e)
    {
        this.RegisterGenericModConfigMenu();
    }

    private void OnUpdateTicked(object? sender, UpdateTickedEventArgs e)
    {
        if (!Context.IsWorldReady)
            return;

        DateTime now = DateTime.Now;

        if (this.IsAlertActive(now))
        {
            if (this.alarmInstance is not null && this.alarmInstance.State == SoundState.Stopped)
                this.alarmInstance.Play();
        }
        else
            this.StopAlarm();

        this.CheckAlerts(now);
    }

    private void OnReturnedToTitle(object? sender, ReturnedToTitleEventArgs e)
    {
        this.StopAlarm();
        this.alertUntil = DateTime.MinValue;
        this.activeAlertMessage = string.Empty;
    }

    private void OnRenderedHud(object? sender, RenderedHudEventArgs e)
    {
        if (!Context.IsWorldReady || Game1.eventUp || Game1.activeClickableMenu is not null)
            return;

        this.EnsureDrawingAssets();

        SpriteBatch spriteBatch = e.SpriteBatch;
        DateTime now = DateTime.Now;
        string timeText = this.config.Use24HourClock ? now.ToString("HH:mm") : now.ToString("h:mm tt");
        Vector2 textSize = Game1.smallFont.MeasureString(timeText);
        Rectangle background = new(
            (int)this.clockPosition.X - 8,
            (int)this.clockPosition.Y - 12,
            (int)textSize.X + 64,
            64);

        this.DrawStardewBox(spriteBatch, background, drawShadow: true);
        if (this.clockIcon is not null)
            spriteBatch.Draw(this.clockIcon, new Rectangle(background.X + 16, background.Y + 20, 24, 24), Color.White);
        spriteBatch.DrawString(
            Game1.smallFont,
            timeText,
            new Vector2(background.X + 48, background.Y + 20),
            Game1.textColor);

        if (this.IsAlertActive(now) && !string.IsNullOrWhiteSpace(this.activeAlertMessage))
            this.DrawToast(spriteBatch, this.activeAlertMessage, background.Bottom + 8);
    }

    private void OnRenderedWorld(object? sender, RenderedWorldEventArgs e)
    {
        if (!Context.IsWorldReady || !this.IsAlertActive(DateTime.Now))
            return;

        this.EnsureDrawingAssets();

        if (this.pixel is null || this.clockIcon is null)
            return;

        Vector2 playerScreen = Game1.GlobalToLocal(Game1.viewport, Game1.player.Position);
        int bubbleWidth = 64;
        int bubbleHeight = 56;
        int x = (int)playerScreen.X;
        int y = (int)playerScreen.Y - 86;
        Rectangle bubble = new(x, y, bubbleWidth, bubbleHeight);

        this.DrawStardewBox(e.SpriteBatch, bubble, drawShadow: true);
        e.SpriteBatch.Draw(this.clockIcon, new Rectangle(x + 20, y + 16, 24, 24), Color.White);
        e.SpriteBatch.Draw(this.pixel, new Rectangle(x + 29, y + bubbleHeight - 2, 6, 8), Color.White);
    }

    private void CheckAlerts(DateTime now)
    {
        for (int index = 0; index < this.config.Alerts.Count; index++)
        {
            AlertConfig alert = this.config.Alerts[index];

            if (!alert.Enabled || alert.Hour != now.Hour || alert.Minute != now.Minute)
                continue;

            DateTime today = now.Date;
            if (this.lastAlertDateBySlot.TryGetValue(index, out DateTime lastDate) && lastDate == today)
                continue;

            this.lastAlertDateBySlot[index] = today;
            this.TriggerAlert(alert, now);
        }
    }

    private void TriggerAlert(AlertConfig alert, DateTime now)
    {
        this.activeAlertMessage = alert.Message.Trim();
        this.alertUntil = now + AlertDuration;
        this.StartAlarm();
    }

    private void StartAlarm()
    {
        this.EnsureAlarmSound();
        this.StopAlarm();

        this.alarmInstance = this.alarmSound?.CreateInstance();
        if (this.alarmInstance is null)
            return;

        this.alarmInstance.IsLooped = true;
        this.alarmInstance.Volume = 0.55f;
        this.alarmInstance.Play();
    }

    private void StopAlarm()
    {
        if (this.alarmInstance is null)
            return;

        this.alarmInstance.Stop();
        this.alarmInstance.Dispose();
        this.alarmInstance = null;
    }

    private bool IsAlertActive(DateTime now)
    {
        return now < this.alertUntil;
    }

    private void RegisterGenericModConfigMenu()
    {
        this.config.Normalize();
        IGenericModConfigMenuApi? gmcm =
            this.Helper.ModRegistry.GetApi<IGenericModConfigMenuApi>("spacechase0.GenericModConfigMenu");

        if (gmcm is null)
            return;

        gmcm.Register(
            this.ModManifest,
            reset: () =>
            {
                this.config = new ModConfig();
                this.config.Normalize();
            },
            save: () =>
            {
                this.config.Normalize();
                this.Helper.WriteConfig(this.config);
            });

        gmcm.AddSectionTitle(this.ModManifest, () => this.I18n("config.clock.title"));
        gmcm.AddBoolOption(
            this.ModManifest,
            getValue: () => this.config.Use24HourClock,
            setValue: value => this.config.Use24HourClock = value,
            name: () => this.I18n("config.clock.use-24-hour.name"),
            tooltip: () => this.I18n("config.clock.use-24-hour.tooltip"));

        gmcm.AddSectionTitle(this.ModManifest, () => this.I18n("config.alerts.title"));

        for (int index = 0; index < AlertConfig.SlotCount; index++)
        {
            int slotIndex = index;
            int displaySlot = index + 1;

            gmcm.AddSectionTitle(this.ModManifest, () => this.I18n("config.alert.title", new { number = displaySlot }));
            gmcm.AddBoolOption(
                this.ModManifest,
                getValue: () => this.config.Alerts[slotIndex].Enabled,
                setValue: value => this.config.Alerts[slotIndex].Enabled = value,
                name: () => this.I18n("config.alert.enabled.name", new { number = displaySlot }));
            gmcm.AddNumberOption(
                this.ModManifest,
                getValue: () => this.config.Alerts[slotIndex].Hour,
                setValue: value => this.config.Alerts[slotIndex].Hour = value,
                name: () => this.I18n("config.alert.hour.name", new { number = displaySlot }),
                min: 0,
                max: 23,
                interval: 1,
                formatValue: value => value.ToString("00"));
            gmcm.AddNumberOption(
                this.ModManifest,
                getValue: () => this.config.Alerts[slotIndex].Minute,
                setValue: value => this.config.Alerts[slotIndex].Minute = value,
                name: () => this.I18n("config.alert.minute.name", new { number = displaySlot }),
                min: 0,
                max: 59,
                interval: 1,
                formatValue: value => value.ToString("00"));
            gmcm.AddTextOption(
                this.ModManifest,
                getValue: () => this.config.Alerts[slotIndex].Message,
                setValue: value => this.config.Alerts[slotIndex].Message = value ?? string.Empty,
                name: () => this.I18n("config.alert.message.name", new { number = displaySlot }));
        }
    }

    private string I18n(string key, object? tokens = null)
    {
        return this.Helper.Translation.Get(key, tokens).ToString();
    }

    private void EnsureDrawingAssets()
    {
        if (this.pixel is not null && this.clockIcon is not null)
            return;

        GraphicsDevice graphicsDevice = Game1.graphics.GraphicsDevice;

        this.pixel ??= new Texture2D(graphicsDevice, 1, 1);
        this.pixel.SetData(new[] { Color.White });

        this.clockIcon ??= this.CreateClockIcon(graphicsDevice);
    }

    private Texture2D CreateClockIcon(GraphicsDevice graphicsDevice)
    {
        const int size = 16;
        Color[] pixels = new Color[size * size];
        Vector2 center = new(7.5f, 7.5f);

        for (int y = 0; y < size; y++)
        {
            for (int x = 0; x < size; x++)
            {
                float distance = Vector2.Distance(new Vector2(x, y), center);
                Color color = Color.Transparent;

                if (distance is >= 6.0f and <= 7.4f)
                    color = new Color(48, 40, 36, 255);
                else if (distance < 6.0f)
                    color = new Color(250, 244, 215, 255);

                pixels[y * size + x] = color;
            }
        }

        SetPixel(pixels, size, 7, 7, new Color(48, 40, 36, 255));
        SetPixel(pixels, size, 7, 6, new Color(48, 40, 36, 255));
        SetPixel(pixels, size, 7, 5, new Color(48, 40, 36, 255));
        SetPixel(pixels, size, 8, 8, new Color(48, 40, 36, 255));
        SetPixel(pixels, size, 9, 9, new Color(48, 40, 36, 255));
        SetPixel(pixels, size, 3, 1, new Color(200, 70, 58, 255));
        SetPixel(pixels, size, 12, 1, new Color(200, 70, 58, 255));

        Texture2D texture = new(graphicsDevice, size, size);
        texture.SetData(pixels);
        return texture;
    }

    private static void SetPixel(Color[] pixels, int width, int x, int y, Color color)
    {
        pixels[y * width + x] = color;
    }

    private void EnsureAlarmSound()
    {
        if (this.alarmSound is not null)
            return;

        const int sampleRate = 22050;
        const double durationSeconds = 0.35;
        int sampleCount = (int)(sampleRate * durationSeconds);
        byte[] buffer = new byte[sampleCount * sizeof(short)];

        for (int sample = 0; sample < sampleCount; sample++)
        {
            double time = sample / (double)sampleRate;
            double frequency = sample < sampleCount / 2 ? 880.0 : 660.0;
            short value = (short)(Math.Sin(2.0 * Math.PI * frequency * time) * short.MaxValue * 0.35);
            buffer[sample * 2] = (byte)(value & 0xff);
            buffer[(sample * 2) + 1] = (byte)((value >> 8) & 0xff);
        }

        this.alarmSound = new SoundEffect(buffer, sampleRate, AudioChannels.Mono);
    }

    private void DrawToast(SpriteBatch spriteBatch, string message, int top)
    {
        string text = message.Length > 60 ? $"{message[..57]}..." : message;
        Vector2 textSize = Game1.smallFont.MeasureString(text);
        int width = Math.Min((int)textSize.X + 32, Game1.uiViewport.Width - 32);
        Rectangle background = new(16, top, width, (int)textSize.Y + 24);

        this.DrawStardewBox(spriteBatch, background, drawShadow: true);
        spriteBatch.DrawString(Game1.smallFont, text, new Vector2(background.X + 16, background.Y + 12), Game1.textColor);
    }

    private void DrawStardewBox(SpriteBatch spriteBatch, Rectangle rectangle, bool drawShadow)
    {
        IClickableMenu.drawTextureBox(
            spriteBatch,
            Game1.menuTexture,
            new Rectangle(0, 256, 60, 60),
            rectangle.X,
            rectangle.Y,
            rectangle.Width,
            rectangle.Height,
            Color.White,
            1f,
            drawShadow);
    }
}
