using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2Cpp;
using UnityEngine;

namespace Tesla2DebugMod
{
    internal static class TrainingToolMenu
    {
        public enum Direction
        {
            None,
            N,
            NE,
            E,
            SE,
            S,
            SO,
            O,
            NO,
        }

        private static Rect windowRect = new(50, 50, 300, 300);

        public static Vector2 startingPos;
        public static Vector2 endingPos;
        public static Direction endingDirection = Direction.None;

        private static Chrono chrono = new();

        public static void OnGUI()
        {
            // Create the window
            GUI.Window(0, windowRect, (UnityEngine.GUI.WindowFunction)DrawGridWindow, "Grid Window");
        }

        static void DrawGridWindow(int windowID)
        {
            if (GUI.Button(new Rect(20, 20, 100, 20), "Set Start Pos"))
            {
                startingPos = getPosition();
            }
            GUI.Label(new Rect(20, 40, 100, 20), "Starting Pos");
            GUI.Label(new Rect(20, 60, 100, 20), $"x : {startingPos.x}");
            GUI.Label(new Rect(20, 80, 100, 20), $"y : {startingPos.y}");

            if (GUI.Button(new Rect(20, 100, 100, 20), "Set End Pos"))
            {
                endingPos = getPosition();
            }
            GUI.Label(new Rect(20, 120, 100, 20), "End pos");
            GUI.Label(new Rect(20, 140, 100, 20), $"x : {endingPos.x}");
            GUI.Label(new Rect(20, 160, 100, 20), $"y : {endingPos.y}");
            GUI.Label(new Rect(20, 180, 100, 20), $"Direction : {endingDirection}");

            if (GUI.Button(new Rect(20, 200, 20, 20), "↖"))
            {
                endingDirection = Direction.NO;
            }
            if (GUI.Button(new Rect(40, 200, 20, 20), "↑"))
            {
                endingDirection = Direction.N;
            }
            if (GUI.Button(new Rect(60, 200, 20, 20), "↗"))
            {
                endingDirection = Direction.NE;
            }
            if (GUI.Button(new Rect(20, 220, 20, 20), "←"))
            {
                endingDirection = Direction.O;
            }
            if (GUI.Button(new Rect(60, 220, 20, 20), "→"))
            {
                endingDirection = Direction.E;
            }
            if (GUI.Button(new Rect(20, 240, 20, 20), "↙"))
            {
                endingDirection = Direction.SO;
            }
            if (GUI.Button(new Rect(40, 240, 20, 20), "↓"))
            {
                endingDirection = Direction.S;
            }
            if (GUI.Button(new Rect(60, 240, 20, 20), "↘"))
            {
                endingDirection = Direction.SE;
            }

            if (GUI.Button(new Rect(20, 280, 20, 20), "L"))
            {
                PlayHandler playHandler = new PlayHandler();
                playHandler.LoadGame(Game.activeSlot);
            }

            // Allow the window to be draggable
            GUI.DragWindow();
        }

        static Vector2 getPosition()
        {
            return Player.instance.transform.position;
        }
    }
}