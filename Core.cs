using MelonLoader;
using UnityEngine;
using Il2Cpp;
using UnityEngine.InputSystem;
using System.Collections;
using static Il2Cpp.Interop;
using Il2CppSystem.Security.Cryptography;

[assembly: MelonInfo(typeof(Tesla2DebugMod.Core), "Tesla2DebugMod", "1.1.0", "Lusther", null)]
[assembly: MelonGame("Rain", "Teslagrad 2")]

namespace Tesla2DebugMod
{
    public class Core : MelonMod
    {

        private static InputAction savePosAction;
        private static InputAction loadPosAction;
        private static InputAction saveSlotAction;
        private static InputAction loadSlotAction;
        private static InputAction toggleDebugMoveAction;
        private static InputAction debugMoveAction;
        private static InputAction menuAction;


        private static Vector2 savedPos;

        private static String notificationString;

        private static object cleanNotificationCoroutine;

        private static SaveDataSlot currentSaveDataSlot;
        private static SaveDataSlot savedSaveDataSlot;

        private static AbilityMenu abilityMenu;

        private static bool isDebugMoveActivated;

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

            notificationString = "";

            abilityMenu = new AbilityMenu();
            
            isDebugMoveActivated = false;

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

            HandleDebugMove();
            abilityMenu.OnLateUpdate(); 
        }

        public override void OnGUI()
        {
            GUI.Label(new Rect(20, 20, 1000, 50), "Debug Mod activated: any run done with it is invalid");
            GUI.Label(new Rect(20, 50, 1000, 50), notificationString);

            abilityMenu.OnGUI();
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
    }
}