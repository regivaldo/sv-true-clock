# True Clock

| Lingua |  |  |  |  |  |
|---|---|---|---|---|---|
| [English](../README.md) | [Português do Brasil](README.pt.md) | [Español](README.es.md) | [日本語](README.ja.md) | [Français](README.fr.md) | **Italiano** |

True Clock è una mod SMAPI per Stardew Valley che mostra l'ora locale reale nell'HUD del gioco. Supporta anche fino a cinque avvisi configurabili in tempo reale, con un fumetto con orologio sopra il giocatore, un piccolo messaggio nell'HUD e un breve suono di allarme.

L'orologio usa l'ora locale del computer, non l'orario interno di Stardew Valley.

## Funzionalità

- Mostra l'ora locale reale sotto l'area di stato in alto a sinistra.
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

Se Generic Mod Config Menu è installato, apri le impostazioni della mod in gioco e configura l'orologio e gli avvisi da lì.

Senza Generic Mod Config Menu, modifica manualmente `config.json` dopo il primo avvio. La mod mantiene sempre esattamente cinque slot di avviso.

Esempio:

```json
{
  "Use24HourClock": true,
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
