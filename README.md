# Renasci
Repositorio del Videojuego Renasci <br>
Versión del Proyecto: 1.0.0 <br><br>
<i>(1.X.X Para la Build Final / X.1.X Para cada Release o Actualización Grande / X.X.1 Parches y Hotfixes)</i>

<br><br>

<b>Integrantes: </b>
* Carlos Domínguez Pita de la Vega
* Francesc Duart Baldovi
* Jose Zafrilla Ruiz

<br><br>

<b> Notas de los Desarrolladores: </b> <br>
Rama Main: Rama que tiene todos los cambios aprobados por el momento en cada release <br>
Rama NEW-Producción: Rama en la que se trabaja. <br>

<br><br><br>

# Actualizaciones

---
## Actualización 1.1.1

### Mejoras y Nuevas Características
* **Fundidos en Menús:** Se han añadido transiciones de fundido suaves a los menús para una sensación más pulida.
* **Sistemas de Música:**
    * Resuelto el sistema de música de combate para transiciones fluidas durante las batallas (antes no sonaba en todos los enemigos).
    * Resuelto el sistema de música de ambiente durante el juego (no sonaba ningún sonido)
    * Se han añadido sonidos de ambiente al menú principal y música a los créditos
* **Decoración del Menú Principal:** El menú principal ha sido ligeramente decorado con paneles.
* **Niebla de Exploración:** Se ha implementado un sistema de niebla que revela la zona a medida que exploras.
* **Clarificación de Controles:**
    * Se ha mejorado la claridad de las ventanas de entrada de teclado/ratón y de mando.
    * Se han añadido **controles progresivos** (que aparecen al acercarse a cofres) y **controles estáticos** (ubicados en la esquina superior derecha de la pantalla) tanto para teclado/ratón como para mando.
    * El cambio de los controles si pasas de mando a teclado y ratón, y viceversa es instantáneo.
* **Vibración del Mando:** Se ha añadido vibración al mando al morir, recibir daño, hacer *dash* y disparar.
* **Efectos de Sonido:**
    * Se han añadido sonidos de puerta a los créditos y al menú principal.
* **Mejoras Menores:**
    * Se ha ajustado el texto de las notas introductorias del juego.
    * Varias otras mejoras menores.

### Corrección de Errores
* **Bug de Inicio del Juego:** Se ha corregido un error que ocasionalmente impedía que el juego se iniciara.
* **Bug de Personaje Atascado:** Se ha resuelto un problema por el cual el personaje a veces se atascaba al atacar.
* **Correcciones Generales:** Se han implementado correcciones generales de errores en todo el juego.

### Ajustes de Enemigos
* Se han ajustado las animaciones y funcionalidades de los enemigos.

---

# 🛠️ Actualización 4 - Pulido y Toques Finales

## 🧟‍♂️ Enemigos
- Nuevas animaciones para enemigos, incluyendo:
  - Idle
  - Ataque (2 para el Esqueleto Mago)
  - Daño recibido
  - Muerte
  - Bloquear (Esqueleto Guerrero)
  - Teleport (Esqueleto Mago

## 🌍 Localización
- Implementado sistema de localización multilingüe:
  - Español
  - Valencià
  - Inglés

## 📜 Cambios Generales
- Revisión y pulido general de contenido previo.
- Debugging y corrección de errores en múltiples sistemas y escenas.

## 🗿 Arte y Entorno
- Modelado y escultura del Sepulcro como hero asset principal.
- Sistema completo de bakeo de iluminación.
- Mejoras de postprocesado global en escena.

## ✨ VFX y Audio
- VFX extensivos aplicados a:
  - Enemigos
  - Reliquias
  - Entorno y habilidades especiales
- Sustitución de efectos de sonido antiguos.
- Nuevos efectos de sonido añadidos en múltiples acciones, interfaces y eventos.

## 🎬 UI y Escenas
- Menú principal rediseñado con nuevo aspecto y disposición.
- Escena de créditos añadida.
- Créditos accesibles desde el menú y al finalizar la partida.

## 🧩 Misceláneo
- Múltiples mejoras de calidad visual, rendimiento y jugabilidad.
- Ajustes finos en animaciones, tiempos, feedback visual y audio.

# 🖼️ Actualización 3 - Visual & Arte

## 🎨 Arte y Visuales
- Creación de texturas con Substance para:
  - Salas
  - Reliquias activas y pasivas
  - Todos los props del juego
- Modelado de:
  - Personaje Principal
  - Esqueleto Base
- Sistema de partículas implementado:
  - Fuego
  - Antorchas
  - Condensación
- Nuevos shaders:
  - Agua
  - Desintegración
  - Integración

## 🔓 Interacción y Jugabilidad
- Sistema de puertas añadido:
  - Las puertas se abren automáticamente cuando se derrotan todos los enemigos en la sala.
- Funcionalidad de nuevas reliquias:
  - Reliquia de Hielo: congela o ralentiza enemigos.
  - Reliquia de Viento: empuja enemigos o interactúa con elementos del entorno.
  - Ambas reliquias interactúan directamente con enemigos.

## 🧩 Puzles
- Añadidos los dos puzles restantes:
  - Lago congelado
  - Escombros derribados

## 🕺 Animaciones y Rigging
- Rigs creados para:
  - Personaje principal
  - Esqueleto base
- Animaciones base implementadas para el personaje principal.

## 🧪 Interfaz de Usuario
- Iniciado el rediseño del UI (Makeover):
  - Primera versión de la nueva barra de vida implementada.

## 🐞 Corrección de Errores
- Mejoras y correcciones de bugs menores en distintas áreas del juego.

<br> <br>

# ⚔️ Actualización 2 - Contenido Ampliado y Sistema de Reliquias (v0.2.0)

## 🧬 Mejoras al Personaje Principal
- Todas las estadísticas del personaje ahora son funcionales.
- Se ha añadido inmunidad temporal durante el dash y tras recibir daño.
- Nuevas acciones disponibles:
  - Interactuar con objetos.
  - Usar reliquias activas.
  - Cambiar entre reliquias.
  - Atacar a distancia.

## 🏺 Sistema de Reliquias
- Añadidas Reliquias Pasivas:
  - Modeladas en 3D.
  - Aplican efectos automáticamente al recogerlas.
- Añadidas Reliquias Activas:
  - Se pueden usar con botón asignado.
  - Algunas están ligadas a puzles o mecánicas específicas.
- Las reliquias ahora pueden caer como drop de enemigos o de cofres.
- Se muestra texto en pantalla indicando qué estadística ha aumentado al recoger una reliquia.

## ☠️ Nuevos Enemigos
- Esqueleto Arquero:
  - Ataca a distancia.
  - Se reposiciona para mantener una distancia segura del jugador.
- Esqueleto Mago:
  - Ataques mágicos a distancia:
    - Orbe perseguidor.
    - Rayo de barrido.
  - Si el jugador se acerca, intenta huir.

## 🔊 Audio y Música
- Añadidos efectos de sonido para el personaje (pasos, ataque, daño, etc).
- Sonidos ambientales para mayor inmersión.
- Música:
  - Track de exploración.
  - Track de combate.
  - Sistema dinámico con transiciones suaves (fade in/out) según detección de enemigos.
  - Las pistas se reproducen en bucle de forma continua.

## 🗺️ Mapas y Entornos
- Añadidos módulos de salas:
  - Suelos, paredes y puertas reutilizables.
- Props beta mejorados:
  - Nuevo modelado con detalles más limpios.
  - Efectos de desgaste y envejecimiento aplicados.
- Modelo de puerta para el menú (falta esculpido final).

## 🎥 Sistema de Cámaras
- Cámaras dinámicas que siguen al jugador en salas grandes.
- Cámaras fijas para salas pequeñas o eventos específicos.

## 🎁 Cofres y Loot
- Cofres con animación de apertura.
- Posibilidad de contener reliquias y drops.

## ⚠️ Trampas
- Añadidas trampas funcionales:
  - Pinchos.
  - Empuje.
  - Confusión (afecta temporalmente el control del jugador).

## ⚔️ Actualización 1 - Jugador & Esqueleto Guerrero (v0.1.0)

<br>

### 🎮 **Jugador - Personaje Principal**  
✅ Se ha añadido un personaje controlable con las siguientes características:  
- Movimiento en todas las direcciones.  
- Habilidad de ataque y dash para desplazarse rápidamente.  
- Estados: **Vivo** y **Muerto**.  
- Animaciones: **Idle**, **Caminar**, **Atacar** y **Muerte**.  
- Estadísticas: **Vida**, **Daño**, **Cooldown del dash**, **Velocidad de movimiento**.  
- Efecto visual blanco al ser golpeado y empujado hacia atrás.  

<br>

### ☠️ **Enemigo - Esqueleto Guerrero**  
✅ Nuevo enemigo con IA que patrulla y persigue al jugador. Sus características incluyen:  
- Estados: **Idle**, **Patrullando**, **Persiguiendo**, **Atacando** y **Bloqueando**.  
- Mecánicas:  
  - Patrulla pasillos y áreas específicas.  
  - Persigue al jugador si está cerca y deja de hacerlo si se aleja.  
  - **Fase de ataque:** Bloquea ataques entrantes y luego contraataca (momento en el que es vulnerable).  
  - Recibe un empuje al ser golpeado, con un efecto visual rojo.  
- Animaciones: **Idle**, **Patrullar**, **Perseguir**, **Atacar** y **Bloquear**.  
- Estadísticas: **Vida**, **Daño**, **Velocidad de movimiento**, **Velocidad de ataque**, **Fuerza de empuje**, **Distancia de detección**, **Resistencia al empuje**.  
- **Drop de Curación** al morir.  
- Implementación de **NavMesh** para navegación.  
- **Modelo temporal** para el enemigo.  

<br>

### 🖥️ **UI & Menús**  
✅ Se han añadido los siguientes menús y mejoras en la interfaz:  
- **Menú Principal:** Opciones para **Jugar**, **Ajustes** y **Salir**.  
- **Menú de Ajustes:**  
  - **Audio:** 3 sliders para ajustar volumen.
    - **General**
    - **Efectos de Sonidos**
    - **Música**
  - **Video:** Opción de resolución y pantalla completa.  
- **Menú de Pausa** para detener la partida.  
- **Menú de Victoria** y **Menú de Derrota**.  
- **Mejoras visuales:**  
  - Efectos de **hover** en elementos interactivos.  
  - **Cursor personalizado**, que cambia según el contexto.  
  - **Logo de la empresa** en la pantalla inicial y en el menú principal.  
  - **Versión del juego** visible en el menú principal.  

<br>

### 🏰 **Escenarios y Props**  
✅ Se han añadido mejoras en el entorno del juego:  
- **Mapa temporal** para la demostración.  
- **Props** con **colisiones** y **materiales** adecuados para mayor inmersión.  



<br><br>
## Inicio del Proyecto (v0.0.0)
* Creación del Repositorio en Github
* Creación del Proyecto en Unity (2022.3.20f1)
* Creación del Readme
