# Renasci
Repositorio del Videojuego Renasci <br>
Versión del Proyecto: 0.1.0 <br><br>
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

<br> <br>

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
