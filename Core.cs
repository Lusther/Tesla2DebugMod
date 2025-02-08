using MelonLoader;
using UnityEngine;
using Il2Cpp;
using UnityEngine.InputSystem;
using System.Collections;

[assembly: MelonInfo(typeof(Tesla2DebugMod.Core), "Tesla2DebugMod", "1.2.0", "Lusther", null)]
[assembly: MelonGame("Rain", "Teslagrad 2")]

namespace Tesla2DebugMod
{
    public class Core : MelonMod
    {
        private Rect warningLabel = new(20, 20, 1000, 50);
        private Rect notificationLabel = new(20, 50, 1000, 50);
        private Rect positionLabel = new(1600, 20, 1000, 50);
        private Rect velocityLabel = new(1600, 50, 1000, 50);
        private Rect graphRect = new Rect(1500, 80, 300, 200);

        private static InputAction savePosAction;
        private static InputAction loadPosAction;
        private static InputAction saveSlotAction;
        private static InputAction loadSlotAction;
        private static InputAction toggleDebugMoveAction;
        private static InputAction debugMoveAction;
        private static InputAction menuAction;
        private static InputAction trainingToolMenuAction;
        private static InputAction startTrainingTrial;
        private static InputAction showDebugInfoAction;

        private List<float> velocityValues = new List<float>();
        private const int maxGraphPoints = 2000;

        private static Vector2 savedPos;

        private static String notificationString;

        private static object cleanNotificationCoroutine;

        private static SaveDataSlot currentSaveDataSlot;
        private static SaveDataSlot savedSaveDataSlot;

        private static AbilityMenu abilityMenu;

        private static bool isDebugMoveActivated;
        private static bool isTrainingMenuShowed;
        private static bool isDebugInfoShowed;

        public override void OnInitializeMelon()
        {

            savePosAction = new InputAction("SavePos", binding: "<Keyboard>/F1");
            savePosAction.Enable();

            loadPosAction = new InputAction("LoadPos", binding: "<Keyboard>/F2");
            loadPosAction.Enable();

            saveSlotAction = new InputAction("SaveSlot", binding: "<Keyboard>/F3");
            saveSlotAction.Enable();

            loadSlotAction = new InputAction("LoadSlot", binding: "<Keyboard>/F4");
            loadSlotAction.Enable();

            toggleDebugMoveAction = new InputAction("ToggleDebugMove", binding: "<Keyboard>/F5");
            toggleDebugMoveAction.Enable();

            debugMoveAction = new InputAction("DebugMove");
            debugMoveAction.AddCompositeBinding("2DVector")
                .With("Up", "<Keyboard>/w")
                .With("Down", "<Keyboard>/s")
                .With("Left", "<Keyboard>/a")
                .With("Right", "<Keyboard>/d");
            debugMoveAction.Enable();

            menuAction = new InputAction("Menu", binding: "<Keyboard>/F6");
            menuAction.Enable();

            trainingToolMenuAction = new InputAction("TrainingToolMenu", binding: "<Keyboard>/F7");
            trainingToolMenuAction.Enable();

            startTrainingTrial = new InputAction("StartTrainingTrial", binding: "<Keyboard>/F8");
            startTrainingTrial.Enable();

            showDebugInfoAction = new InputAction("ShowDebugInfo", binding: "<Keyboard>/F9");
            showDebugInfoAction.Enable();

            notificationString = "";

            abilityMenu = new AbilityMenu();
            
            isDebugMoveActivated = false;
            isTrainingMenuShowed = false;
            isDebugInfoShowed = false;

            LoggerInstance.Msg("Initialized.");
        }

        public override void OnLateUpdate()
        {
            if (savePosAction.WasPerformedThisFrame())
            {
                savePos();
            }

            if (loadPosAction.WasPerformedThisFrame())
            {
                loadPos();
            }

            if (saveSlotAction.WasPerformedThisFrame())
            {
                SaveSlot();
            }

            if (loadSlotAction.WasPerformedThisFrame())
            {
                LoadSlot();
            }

            if (menuAction.WasPerformedThisFrame())
            {
                abilityMenu.ToggleWindow();
            }

            if (toggleDebugMoveAction.WasPerformedThisFrame())
            {
                ToggleDebugMove();
            }

            if (trainingToolMenuAction.WasPerformedThisFrame())
            {
                isTrainingMenuShowed = !isTrainingMenuShowed;
            }

            if (startTrainingTrial.WasPerformedThisFrame())
            {
                MelonCoroutines.Start(TrainingTool.StartTrainingTrial());
            }

            if (showDebugInfoAction.WasPerformedThisFrame())
            {
                isDebugInfoShowed = !isDebugInfoShowed;
            }



            if (Player.instance is not null)
            {
                if (!Player.instance.Cast<UnityEngine.Object>().HasBeenDestroyed())
                {
                    Melon<Tesla2DebugMod.Core>.Logger.Msg($"{Player.instance}");
                    HandleDebugMove();
                    abilityMenu.OnLateUpdate();
                    TrainingTool.OnLateUpdate();
                    UpdateGraph(Math.Abs(getVelocity().x));
                }
            }
        }

        public override void OnGUI()
        {
            GUI.Label(warningLabel, "Debug Mod activated: any run done with it is invalid");
            GUI.Label(notificationLabel, notificationString);

            if (Player.instance is not null)
            {
                if (!Player.instance.Cast<UnityEngine.Object>().HasBeenDestroyed())
                {
                    abilityMenu.OnGUI();
                    TrainingTool.OnGUI();
                    if (isTrainingMenuShowed)
                    {
                        TrainingToolMenu.OnGUI();
                    }

                    if (isDebugInfoShowed)
                    {
                        GUI.Label(positionLabel, $"Position: {getPosition().x} | {getPosition().y}");
                        GUI.Label(velocityLabel, $"Velocity: {getVelocity().x} | {getVelocity().y}");
                        GUI.Box(graphRect, "Velocity Graph");
                        DrawVelocityGraph(new Rect(10, 10, 300, 150));
                    }
                }
            }
        }

        private static void savePos()
        {
            try
            {
                savedPos = Player.instance.localBody.position;
                Melon<Tesla2DebugMod.Core>.Logger.Msg($"Saving Position : {savedPos}");
                DrawText($"Saving Position : {savedPos}");
            }
            catch
            {
                DrawText($"Saving Failed");
            }
            
        }

        private static void loadPos()
        {
            try
            {
                Melon<Tesla2DebugMod.Core>.Logger.Msg($"Loading Position : {savedPos}");
                Player.instance.localBody.velocity = Vector3.zero;
                Player.instance.transform.position = new Vector3(savedPos.x, savedPos.y, Player.instance.transform.position.z);
                DrawText("Loading Position");
            }
            catch
            {
                DrawText($"Loading failed");
            }
        }

        public static void DrawText(String message)
        {
            if (cleanNotificationCoroutine != null)
                MelonCoroutines.Stop(cleanNotificationCoroutine);
            notificationString = message;
            cleanNotificationCoroutine = MelonCoroutines.Start(WaitAndCleanNotification());
        }

        public static IEnumerator WaitAndCleanNotification()
        {
            yield return new WaitForSeconds(5);
            notificationString = "";
            cleanNotificationCoroutine = null;
        }

        public static void SaveSlot()
        {
            Melon<Tesla2DebugMod.Core>.Logger.Msg("Saving Slot");
            DrawText("Saving Slot");

            currentSaveDataSlot = Game.activeSlot;
            savedSaveDataSlot = currentSaveDataSlot.DeepCopy();
        }

        public static void LoadSlot()
        {
            Melon<Tesla2DebugMod.Core>.Logger.Msg("Loading Slot");
            DrawText("Loading Slot");

            savedSaveDataSlot.CopyTo(currentSaveDataSlot);
        }

        public static void ToggleDebugMove()
        {
            isDebugMoveActivated = !isDebugMoveActivated;

            Melon<Tesla2DebugMod.Core>.Logger.Msg($"ToggleDebugMove : {isDebugMoveActivated}");
            DrawText($"ToggleDebugMove : {isDebugMoveActivated}");

            Player.instance.localBody.isKinematic = isDebugMoveActivated;
            Player.instance.movement.enabled = !isDebugMoveActivated;
            Player.MainCollider.enabled = !isDebugMoveActivated;
            Player.DeathHandler.deathCollider.enabled = !isDebugMoveActivated;
        }

        public static void HandleDebugMove()
        {
            if (isDebugMoveActivated)
            {
                try
                {
                    Vector2 inputAxe = debugMoveAction.ReadValue<Vector2>();
                    Player.instance.transform.position += new Vector3(inputAxe.x, inputAxe.y, 0) * 0.05f;
                }
                catch (InvalidOperationException)
                {
                    Melon<Tesla2DebugMod.Core>.Logger.Msg("The given TValue type does not match the value type of the control or composite currently driving the action.");
                }
            }
        }

        public static Vector2 getVelocity()
        {
            return Player.instance.localBody.velocity;
        }

        public static Vector2 getPosition()
        {
            return Player.instance.transform.position;
        }

        private void DrawVelocityGraph(Rect rect)
        {
            CreateLineMaterial();

            if (velocityValues.Count < 2) return;

            lineMaterial.SetPass(0);
            GL.PushMatrix();
            GL.Viewport(new Rect(graphRect.x, Screen.height - graphRect.y - graphRect.height, graphRect.width, graphRect.height));
            GL.LoadOrtho();

            GL.Begin(GL.LINES);
            GL.Color(Color.green);

            float maxVelocity = 30f;
            for (int i = 1; i < velocityValues.Count; i++)
            {
                float x1 = (float)(i - 1) / maxGraphPoints;
                float x2 = (float)i / maxGraphPoints;
                float y1 = Mathf.Clamp01(velocityValues[i - 1] / maxVelocity);
                float y2 = Mathf.Clamp01(velocityValues[i] / maxVelocity);

                GL.Vertex(new Vector3(x1, y1, 0));
                GL.Vertex(new Vector3(x2, y2, 0));
            }

            GL.End();
            GL.PopMatrix();
        }

        private void UpdateGraph(float velocity)
        {
            velocityValues.Add(velocity);
            if (velocityValues.Count > maxGraphPoints)
            {
                velocityValues.RemoveAt(0); // Keep the graph within the point limit
            }
        }

        static Material lineMaterial;
        static void CreateLineMaterial()
        {
            if (!lineMaterial)
            {
                // Unity has a built-in shader that is useful for drawing
                // simple colored things.
                Shader shader = Shader.Find("Hidden/Internal-Colored");
                lineMaterial = new Material(shader);
                lineMaterial.hideFlags = HideFlags.HideAndDontSave;
                // Turn on alpha blending
                lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
                lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
                // Turn backface culling off
                lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
                // Turn off depth writes
                lineMaterial.SetInt("_ZWrite", 0);
            }
        }
    }
}