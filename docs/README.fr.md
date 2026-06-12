# True Clock

| Langue |  |  |  |  |  |
|---|---|---|---|---|---|
| [English](../README.md) | [Português do Brasil](README.pt.md) | [Español](README.es.md) | [日本語](README.ja.md) | **Français** | [Italiano](README.it.md) |

True Clock est un mod SMAPI pour Stardew Valley qui affiche l'heure locale réelle dans le HUD du jeu. Il prend aussi en charge jusqu'à cinq alertes configurables en temps réel, avec une bulle d'horloge au-dessus du joueur, un petit message dans le HUD et un court son d'alarme.

L'horloge utilise l'heure locale de votre ordinateur, pas l'heure interne de Stardew Valley.

## Fonctionnalités

- Affiche l'heure locale réelle sous la zone de statut en haut à gauche.
- Utilise le style d'interface natif de Stardew Valley.
- Prend en charge le format 24 heures ou AM/PM.
- Prend en charge jusqu'à cinq alertes configurables.
- Chaque alerte peut être activée ou désactivée séparément.
- Chaque alerte possède une heure, une minute et un message facultatif.
- Les alertes se déclenchent au maximum une fois par jour civil.
- Les alertes durent jusqu'à cinq secondes et ne mettent pas le jeu en pause ni ne bloquent les commandes.
- Prise en charge facultative de Generic Mod Config Menu.

## Prérequis

- Stardew Valley
- SMAPI 4.0.0 ou plus récent
- .NET 6 SDK uniquement si vous voulez compiler le mod depuis le code source
- Generic Mod Config Menu est facultatif

## Installation Depuis Nexus Mods

True Clock pourra aussi être installé depuis Nexus Mods lorsque la page du mod sera publiée.

- Nexus Mods Stardew Valley: https://www.nexusmods.com/stardewvalley
- Page du mod: à ajouter lorsque l'ID Nexus Mods sera disponible

Téléchargez l'archive du mod depuis Nexus Mods, puis extrayez-la dans le dossier `Mods` de Stardew Valley.

## Installation Depuis Le ZIP De Release

1. Installez SMAPI depuis https://smapi.io/.
2. Téléchargez le dernier `TrueClock.zip` depuis la page GitHub Releases.
3. Extrayez le ZIP dans le dossier `Mods` de Stardew Valley.
4. Vérifiez que le dossier final ressemble à ceci:

```text
Stardew Valley/
  Mods/
    TrueClock/
      manifest.json
      TrueClock.dll
      i18n/
```

5. Lancez le jeu via SMAPI.

Au premier lancement, le mod crée `config.json` dans le dossier du mod `TrueClock`.

## Configuration

Si Generic Mod Config Menu est installé, ouvrez les paramètres du mod en jeu et configurez l'horloge et les alertes depuis ce menu.

Sans Generic Mod Config Menu, modifiez manuellement `config.json` après le premier lancement. Le mod conserve toujours exactement cinq emplacements d'alerte.

Exemple:

```json
{
  "Use24HourClock": true,
  "Alerts": [
    {
      "Enabled": true,
      "Hour": 8,
      "Minute": 30,
      "Message": "Il est temps de vérifier la ferme"
    }
  ]
}
```

Les heures utilisent l'heure locale réelle au format 24 heures, de `0` à `23`. Les minutes utilisent `0` à `59`.

## Compiler Depuis Le Code Source

Installez le .NET 6 SDK, puis exécutez:

```bash
dotnet build -c Release
```

Le workflow de release de ce dépôt compile le mod et publie un fichier ZIP dans GitHub Releases.

## ZIP De Release GitHub

Les releases officielles sont générées par GitHub Actions. Lorsqu'une balise de version comme `v1.0.0` est poussée, le workflow compile le mod, crée `TrueClock.zip` et l'ajoute à la GitHub Release.

## Auteur

Regivaldo (Sun)  
Email: regivaldorfs@gmail.com  
Site web: https://regivaldo.com.br  
Nexus Mods: https://www.nexusmods.com/profile/regivaldorfs
