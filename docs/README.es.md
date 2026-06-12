# True Clock

| Idioma |  |  |  |  |  |
|---|---|---|---|---|---|
| [English](../README.md) | [Português do Brasil](README.pt.md) | **Español** | [日本語](README.ja.md) | [Français](README.fr.md) | [Italiano](README.it.md) |

True Clock es un mod SMAPI para Stardew Valley que muestra la hora local real en el HUD del juego. También admite hasta cinco alertas configurables en tiempo real, con un globo de reloj sobre el jugador, un pequeño mensaje en el HUD y un sonido breve de alarma.

El reloj usa la hora local de tu computadora, no la hora interna de Stardew Valley.

## Funciones

- Muestra la hora local real debajo del área de estado superior izquierda.
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

Si Generic Mod Config Menu está instalado, abre la configuración del mod dentro del juego y configura el reloj y las alertas allí.

Sin Generic Mod Config Menu, edita manualmente `config.json` después del primer inicio. El mod siempre mantiene exactamente cinco espacios de alerta.

Ejemplo:

```json
{
  "Use24HourClock": true,
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
