# True Clock

![Banner di True Clock](../screenshot/banner.png)

| Lingua |  |  |  |  |  |
|---|---|---|---|---|---|
| [English](../README.md) | [Português do Brasil](README.pt.md) | [Español](README.es.md) | [日本語](README.ja.md) | [Français](README.fr.md) | **Italiano** |

True Clock è una mod SMAPI per Stardew Valley che mostra l'ora locale reale nell'HUD del gioco. Supporta anche fino a cinque avvisi configurabili in tempo reale, con un fumetto con orologio sopra il giocatore, un piccolo messaggio nell'HUD e un breve suono di allarme.

L'orologio usa l'ora locale del computer, non l'orario di gioco di Stardew Valley.

## Screenshot

![Orologio nella posizione predefinita in alto a destra](../screenshot/game.jpg)

L'orologio parte nella posizione predefinita in alto a destra.

![Orologio spostato con impostazioni X e Y personalizzate](../screenshot/clock-position.jpg)

I giocatori possono spostare l'orologio modificandone la posizione X e Y.

![Pagina di configurazione con posizione dell'orologio e avvisi](../screenshot/config.jpg)

La pagina di configurazione include le opzioni di posizione dell'orologio e le impostazioni degli avvisi.

## Funzionalità

- Mostra l'ora locale reale nell'HUD.
- Permette ai giocatori di impostare la posizione dell'orologio con coordinate numeriche X e Y.
- Usa lo stile UI nativo di Stardew Valley.
- Supporta il formato 24 ore o AM/PM.
- Supporta fino a cinque avvisi configurabili.
- Ogni avviso può essere abilitato o disabilitato separatamente.
- Ogni avviso ha ora, minuto e un messaggio opzionale.
- Gli avvisi si attivano al massimo una volta per giorno di calendario.
- Gli avvisi durano fino a cinque secondi e non mettono in pausa il gioco né bloccano i comandi.
- Supporto opzionale per Generic Mod Config Menu.

## Requisiti

- Stardew Valley
- SMAPI 4.0.0 o versione successiva
- .NET 6 SDK solo se vuoi compilare la mod dal codice sorgente
- Generic Mod Config Menu è opzionale

## Installazione Da Nexus Mods

True Clock potrà essere installata anche da Nexus Mods quando la pagina della mod sarà pubblicata.

- Nexus Mods Stardew Valley: https://www.nexusmods.com/stardewvalley
- Pagina della mod: da aggiungere quando l'ID di Nexus Mods sarà disponibile

Scarica l'archivio della mod da Nexus Mods, quindi estrailo nella cartella `Mods` di Stardew Valley.

## Installazione Dal ZIP Di Release

1. Installa SMAPI da https://smapi.io/.
2. Scarica l'ultimo `TrueClock.zip` dalla pagina GitHub Releases.
3. Estrai lo ZIP nella cartella `Mods` di Stardew Valley.
4. Verifica che la cartella finale sia così:

```text
Stardew Valley/
  Mods/
    TrueClock/
      manifest.json
      TrueClock.dll
      i18n/
```

5. Avvia il gioco tramite SMAPI.

Al primo avvio, la mod crea `config.json` nella cartella della mod `TrueClock`.

## Configurazione

Se Generic Mod Config Menu è installato, apri le impostazioni della mod in gioco e configura da lì la posizione dell'orologio e gli avvisi. La posizione dell'orologio usa coordinate numeriche dell'HUD: `ClockX` sposta l'orologio orizzontalmente e `ClockY` lo sposta verticalmente.

La posizione predefinita è quella originale in alto a destra. In `config.json`, `ClockX` ha valore predefinito `-1`, che indica alla mod di calcolare automaticamente la coordinata X in alto a destra per la dimensione attuale dell'UI. Quando un giocatore modifica il valore X nel menu delle impostazioni, la mod salva la coordinata numerica scelta.

Senza Generic Mod Config Menu, modifica manualmente `config.json` dopo il primo avvio. La mod mantiene sempre esattamente cinque slot di avviso.

Esempio:

```json
{
  "Use24HourClock": true,
  "ClockX": -1,
  "ClockY": 8,
  "Alerts": [
    {
      "Enabled": true,
      "Hour": 8,
      "Minute": 30,
      "Message": "È ora di controllare la fattoria"
    }
  ]
}
```

`ClockX` e `ClockY` usano coordinate pixel dell'HUD. `ClockX: -1` mantiene la posizione automatica predefinita in alto a destra; usa `0` o un valore superiore per una posizione X personalizzata fissa. `ClockY` ha valore predefinito `8`.

Le ore usano l'ora locale reale in formato 24 ore, da `0` a `23`. I minuti usano `0` a `59`.

## Compilare Dal Codice Sorgente

Installa il .NET 6 SDK, quindi esegui:

```bash
dotnet build -c Release
```

Il workflow di release di questo repository compila la mod e pubblica un file ZIP in GitHub Releases.

## ZIP Di Release Su GitHub

Le release ufficiali sono generate da GitHub Actions. Quando viene inviata una tag di versione come `v1.0.0`, il workflow compila la mod, crea `TrueClock.zip` e lo allega alla GitHub Release.

## Autore

Regivaldo (Sun)  
Email: regivaldorfs@gmail.com  
Sito web: https://regivaldo.com.br  
Nexus Mods: https://www.nexusmods.com/profile/regivaldorfs
