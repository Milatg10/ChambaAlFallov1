# 🎮 Chamba al Fallo V1

![License](https://img.shields.io/badge/License-MIT-green)
![Status](https://img.shields.io/badge/Status-En__Desarrollo-yellow)

> **Chamba al Fallo** es un RPG inversivo que mezcla acción, exploración y puzzles, donde tendrás que salvar tu trabajo universitario antes de que se acabe el tiempo… ¡o fracasar en el intento!

## 📋 Tabla de Contenidos

1. [📌 Sobre el Proyecto](#sobre-el-proyecto)  
2. [✨ Historia](#✨-historia)  
3. [🎯 Características](#✨-características)  
4. [⚙️ Instalación y Configuración](#⚙️-instalación-y-configuración)  
5. [🎮 Controles](#🎮-controles)  
6. [📂 Estructura del Proyecto](#📂-estructura-del-proyecto)  
7. [🛠️ Tecnologías y Plugins](#🛠️-tecnologías-y-plugins)  
8. [👥 Créditos y Contribuidores](#👥-créditos-y-contribuidores)  
9. [📄 Licencia](#📄-licencia)

---

## 📌 Sobre el Proyecto

* **Nombre:** Chamba al Fallo V1  
* **Género:** RPG inversivo con elementos de puzzles y narrativa  
* **Estado Actual:** En desarrollo  
* **Motivación:** Creado como proyecto para la asignatura de ID.  
* **Repositorio:** https://github.com/Milatg10/ChambaAlFallov1 :contentReference[oaicite:0]{index=0}

---

## ✨ Historia

Eres **Manuel**, un universitario de la Escuela Politécnica de Cáceres que estudia Ingeniería Informática de Software.  
Acabas de llegar a tu piso después de clase… y **el tiempo está en tu contra**.

📜 Debes entregar tu proyecto de programación antes de las **00:00**.  
La entrega acabará y tu nota dependerá de si **puedes terminarlo y entregarlo a tiempo**.

Explora un mundo que alterna entre **escenarios 3D y niveles 2D**, enfrenta puzzles, supera bugs inesperados y toma decisiones que te llevarán a **uno de tres finales distintos**:

🎯 *¿Entregarás tu trabajo a tiempo?*  
💥 *¿Perderás el proyecto en el caos del último minuto?*  
🌀 *¿O te rendirás ante los bugs y desafíos?*

¡Adéntrate en esta aventura y demuestra si tienes lo que hay que tener para sobresalir bajo presión!

---

## ✨ Características

Estas son las mecánicas y sistemas que actualmente incluye el juego:

* ✔️ Sistema de movimiento basico del jugador.  
* ✔️ Recogida objetos.  
* ✔️ Dialogos en el mundo que guien al usuario.  
* ✔️ Niveles con puzzles que desafían lógica y tiempo.  
* ✔️ Enemigos con IA básica .  
* ⬜ Sistema de guardado/ checkpoints (Pendiente).  

---

## ⚙️ Instalación y Configuración

Sigue estos pasos para ejecutar el proyecto en tu máquina local.

### 🧩 Requisitos Previos

* **Unity Hub** instalado.  
* **Unity Editor Versión:** *2021.3.11f1* (recomendado para compatibilidad).  

Unity Hub te permite instalar y administrar versiones del motor fácilmente. :contentReference[oaicite:1]{index=1}

### 🚀 Pasos

1. Clona el repositorio:
    ```bash
    git clone https://github.com/Milatg10/ChambaAlFallov1
    ```
2. Abre **Unity Hub**.
3. Haz clic en **"Add" (Añadir)** y selecciona la carpeta del proyecto que acabas de clonar.
4. Espera a que Unity importe los assets (puede tardar unos minutos).
5. Abre la escena principal en `Assets/Creator Kit - RPG/Scenes/menu` o la escena que corresponda (asegúrate de revisar las carpetas para encontrarla).

---

## 🎮 Controles

| Acción        | Teclado / Gamepad |
|---------------|-------------------|
| Moverse       | `W`, `A`, `S`, `D` |
| Interactuar   | `Space  `          |
| Menu | `Mouse`             |

---

## 📂 Estructura del Proyecto

Guía general de carpetas dentro de `Assets/`:

```plaintext
+---Creator Kit - RPG
|   +---Art
|   |   +---Animations
|   |   +---Animators
|   |   +---MinijuegoMila
|   |   +---MinijuegoAda
|   |   +---Object3d
|   |   +---Sprites
|   |   |   +---3d
|   |   |   +---Animated Environment
|   |   |   +---Characters
|   |   |   +---Environment
|   |   |   +---Floors
|   |   |   +---Minijuego_Mila
|   |   |   +---Minijuego_Ada
|   |   |   +---Skyboxes
|   |   |   \---UI
|   |   +---TileMapPalettes
|   |   \---Tiles
|   +---Audio
|   +---Cutscene
|   +---Editor
|   +---Gameplay Prefabs
|   +---Materials
|   +---Prefabs
|   +---Scenes //Escenas del juego
|   +---Scripts
|   |   +---Core
|   |   +---Final
|   |   +---Gameplay
|   |   +---Menu
|   |   +---Minijuego_Mila
|   |   +---Minijuego_Ada
|   |   +---Tiles
|   |   \---UI
|   +---Settings
|   \---Shaders
+---Data
```

---

## 👥 Créditos y Contribuidores

* [**Milatg10**](https://github.com/Milatg10) – Desarrollo enemigos y mazmorras. 
* [**RinaHodge**](https://github.com/RinaHodge) – Desarrollo del mundo 2d y entorno.
* [**AdaXiang**](https://github.com/AdaXiang) – Desarrollo puzzles.
* [**uni-msg**](https://github.com/uni-msg) – Desarrollo escenarios 3d y finales.

---

## 📄 Licencia

Este proyecto está bajo licencia **MIT**.
