# True Clock

![Banner de True Clock](../screenshot/banner.png)

| Idioma |  |  |  |  |  |
|---|---|---|---|---|---|
| [English](../README.md) | [Português do Brasil](README.pt.md) | **Español** | [日本語](README.ja.md) | [Français](README.fr.md) | [Italiano](README.it.md) |

True Clock es un mod SMAPI para Stardew Valley que muestra la hora local real en el HUD del juego. También admite hasta cinco alertas configurables en tiempo real, con un globo de reloj sobre el jugador, un pequeño mensaje en el HUD y un sonido breve de alarma.

El reloj usa la hora local de tu computadora, no la hora del juego de Stardew Valley.

## Capturas De Pantalla

![Reloj en la posición predeterminada de la esquina superior derecha](../screenshot/game.jpg)

El reloj comienza en la posición predeterminada de la esquina superior derecha.

![Reloj movido con ajustes personalizados de X e Y](../screenshot/clock-position.jpg)

Los jugadores pueden mover el reloj cambiando su posición X e Y.

![Página de configuración con posición del reloj y alertas](../screenshot/config.jpg)

La página de configuración incluye opciones de posición del reloj y ajustes de alertas.

## Funciones

- Muestra la hora local real en el HUD.
- Permite establecer la posición del reloj con coordenadas numéricas X e Y.
- Usa el estilo de UI nativo de Stardew Valley.
- Admite formato de 24 horas o AM/PM.
- Admite hasta cinco alertas configurables.
- Cada alerta se puede activar o desactivar por separado.
- Cada alerta tiene hora, minuto y un mensaje opcional.
- Las alertas se activan como máximo una vez por día calendario.
- Las alertas duran hasta cinco segundos y no pausan el juego ni bloquean la entrada.
- Compatibilidad opcional con Generic Mod Config Menu.

## Requisitos

- Stardew Valley
- SMAPI 4.0.0 o superior
- .NET 6 SDK solo si quieres compilar el mod desde el código fuente
- Generic Mod Config Menu es opcional

## Instalación Desde Nexus Mods

True Clock también se podrá instalar desde Nexus Mods cuando se publique la página del mod.

- Nexus Mods Stardew Valley: https://www.nexusmods.com/stardewvalley
- Página del mod: se agregará cuando el ID de Nexus Mods esté disponible

Descarga el archivo del mod desde Nexus Mods y extráelo dentro de la carpeta `Mods` de Stardew Valley.

## Instalación Desde El ZIP De Release

1. Instala SMAPI desde https://smapi.io/.
2. Descarga el `TrueClock.zip` más reciente desde la página de Releases de GitHub.
3. Extrae el ZIP dentro de la carpeta `Mods` de Stardew Valley.
4. Verifica que la carpeta final se vea así:

```text
Stardew Valley/
  Mods/
    TrueClock/
      manifest.json
      TrueClock.dll
      i18n/
```

5. Inicia el juego mediante SMAPI.

En el primer inicio, el mod crea `config.json` dentro de la carpeta del mod `TrueClock`.

## Configuración

Si Generic Mod Config Menu está instalado, abre la configuración del mod dentro del juego y configura allí la posición del reloj y las alertas. La posición del reloj usa coordenadas numéricas del HUD: `ClockX` mueve el reloj horizontalmente y `ClockY` lo mueve verticalmente.

La posición predeterminada es la ubicación original en la esquina superior derecha. En `config.json`, `ClockX` tiene `-1` como valor predeterminado, lo que indica al mod que calcule automáticamente la coordenada X de la esquina superior derecha según el tamaño actual de la UI. Cuando un jugador cambia el valor X en el menú de configuración, el mod guarda la coordenada numérica elegida.

Sin Generic Mod Config Menu, edita manualmente `config.json` después del primer inicio. El mod siempre mantiene exactamente cinco espacios de alerta.

Ejemplo:

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
      "Message": "Hora de revisar la granja"
    }
  ]
}
```

`ClockX` y `ClockY` usan coordenadas de píxeles del HUD. `ClockX: -1` mantiene el valor predeterminado automático de la esquina superior derecha; usa `0` o más para una posición X personalizada fija. `ClockY` tiene `8` como valor predeterminado.

Las horas usan la hora local real en formato de 24 horas, de `0` a `23`. Los minutos usan `0` a `59`.

## Compilar Desde El Código Fuente

Instala el .NET 6 SDK y ejecuta:

```bash
dotnet build -c Release
```

El workflow de release de este repositorio compila el mod y publica un archivo ZIP en GitHub Releases.

## ZIP De Release En GitHub

Las releases oficiales se generan con GitHub Actions. Cuando se envía una etiqueta de versión como `v1.0.0`, el workflow compila el mod, crea `TrueClock.zip` y lo adjunta a la Release de GitHub.

## Autor

Regivaldo (Sun)  
Email: regivaldorfs@gmail.com  
Sitio web: https://regivaldo.com.br  
Nexus Mods: https://www.nexusmods.com/profile/regivaldorfs
