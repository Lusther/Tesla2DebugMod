using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2Cpp;
using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;
using MelonLoader;

namespace Tesla2DebugMod
{
    internal static class TrainingTool
    {
        public enum TrainingToolState
        {
            idle,
            countdown,
            running,
            result
        }

        private static TrainingToolState currentState = TrainingToolState.idle;

        private static Rect chronoCoords = new(900, 20, 120, 50);

        private static bool isMovementActive;

        private static string chronoString;

        private static Chrono chrono = new Chrono();

        private static GUIStyle chronoStyle = CreateChronoStyle();
        private static GUIStyle CreateChronoStyle()
        {
            chronoStyle = new GUIStyle();
            chronoStyle.alignment = TextAnchor.MiddleCenter;
            chronoStyle.fontSize = 30;
            chronoStyle.normal.textColor = Color.white;
            return chronoStyle;
        }

        public static void OnGUI()
        {   
            if (currentState == TrainingToolState.countdown 
                || currentState == TrainingToolState.running 
                || currentState == TrainingToolState.result)
            {
                GUI.Label(chronoCoords, chronoString, chronoStyle);
            }
        }

        public static void OnLateUpdate()
        {
            if (currentState == TrainingToolState.running)
            {
                if (isFinishLineCrossed())
                {
                    MelonCoroutines.Start(FinishTrainingTrial());
                }

                chronoString = chrono.GetTime();
            }
        }

        private static bool isFinishLineCrossed()
        {
            Vector2 finishLineDirection = new Dictionary<TrainingToolMenu.Direction, Vector2> {
                { TrainingToolMenu.Direction.N, new Vector2(0, 1) },
                { TrainingToolMenu.Direction.NE, new Vector2(1, 1) },
                { TrainingToolMenu.Direction.NO, new Vector2(-1, 1) },
                { TrainingToolMenu.Direction.E, new Vector2(1, 0) },
                { TrainingToolMenu.Direction.O, new Vector2(-1, 0) },
                { TrainingToolMenu.Direction.S, new Vector2(0, -1) },
                { TrainingToolMenu.Direction.SE, new Vector2(1, -1) },
                { TrainingToolMenu.Direction.SO, new Vector2(-1, -1) }
            }[TrainingToolMenu.endingDirection];

            float angle = Vector2.Angle((Vector2)Player.instance.transform.position - TrainingToolMenu.endingPos, finishLineDirection);

            return angle < 90;
        }

        private static void ToggleMovement(bool state)
        {
            isMovementActive = state;

            Player.instance.localBody.isKinematic = !isMovementActive;
            Player.instance.movement.enabled = isMovementActive;
        }

        public static IEnumerator StartTrainingTrial()
        {
            if (currentState == TrainingToolState.countdown || currentState == TrainingToolState.result)
            {
                yield break;
            }

            if (!(TrainingToolMenu.startingPos != Vector2.zero 
                && TrainingToolMenu.endingPos != Vector2.zero 
                && TrainingToolMenu.endingDirection != TrainingToolMenu.Direction.None))
            {
                yield break;
            }

            Melon<Tesla2DebugMod.Core>.Logger.Msg("StartTrainingTrial");

            currentState = TrainingToolState.countdown;
            ToggleMovement(false);
            Player.instance.transform.position = TrainingToolMenu.startingPos;

            for (int i = 3; i > 0; i--)
            {
                chronoString = i.ToString();
                yield return new WaitForSeconds(1);
            }

            ToggleMovement(true);
            chrono.Reset();
            chrono.Start();

            currentState = TrainingToolState.running;
        }

        private static IEnumerator FinishTrainingTrial()
        {
            chrono.Stop();
            currentState = TrainingToolState.result;
            yield return new WaitForSeconds(5);

            currentState = TrainingToolState.idle;
            chrono.Reset();
        }
    }
}
