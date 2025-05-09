# Renasci
Repositorio del Videojuego Renasci <br>
Versión del Proyecto: 0.3.0 <br><br>
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

# 🖼️ Update 3 - Sights to See (Visual Update)

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

# ⚔️ Contenido Ampliado y Sistema de Reliquias (v0.2.0)

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

## ⚔️ Jugador & Esqueleto Guerrero (v0.1.0)

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
