using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2CppHumanizer;
using UnityEngine;
using Il2Cpp;

namespace Tesla2DebugMod
{
    internal class AbilityMenu
    {
        private bool showGui;

        private bool blinkUnlocked;
        private bool mjolnirUnlocked;
        private bool cloakUnlocked;
        private bool powerSlideUnlocked;
        private bool waterblinkUnlocked;
        private bool axeUnlocked;
        private bool blinkWireAxeUnlocked;
        private bool doubleJumpUnlocked;
        private bool mapUnlocked;
        private bool omniBlinkUnlocked;
        private bool redCloakUnlocked;
        private bool secretsMapUnlocked;


        public AbilityMenu()
        {
            showGui = false;

            blinkUnlocked = false;
            mjolnirUnlocked = false;
            cloakUnlocked = false;
            powerSlideUnlocked = false;
            waterblinkUnlocked = false;
            axeUnlocked = false;
            blinkWireAxeUnlocked = false;
            doubleJumpUnlocked = false;
            mapUnlocked = false;
            omniBlinkUnlocked = false;
            redCloakUnlocked = false;
            secretsMapUnlocked = false;
        }

        public void OnLateUpdate()
        {
            if (Game.activeSlot != null)
            {
                blinkUnlocked = Game.blinkUnlocked;
                mjolnirUnlocked = Game.mjolnirUnlocked;
                cloakUnlocked = Game.cloakUnlocked;
                powerSlideUnlocked = Game.powerSlideUnlocked;
                waterblinkUnlocked = Game.waterblinkUnlocked;
                axeUnlocked = Game.axeUnlocked;
                blinkWireAxeUnlocked = Game.blinkWireAxeUnlocked;
                doubleJumpUnlocked = Game.doubleJumpUnlocked;
                mapUnlocked = Game.mapUnlocked;
                omniBlinkUnlocked = Game.omniBlinkUnlocked;
                redCloakUnlocked = Game.redCloakUnlocked;
                secretsMapUnlocked = Game.secretsMapUnlocked;
            }
        }

        public void OnGUI()
        {
            if (showGui)
            {
                GUI.Window(0, new Rect(20, 20, 250, 400), (GUI.WindowFunction)DrawWindowContent, "Mod Controls");
            }
        }

        private void DrawWindowContent(int id)
        {
            // Create a checkbox for the boolean property
            bool blinkCheckboxValue = GUILayout.Toggle(blinkUnlocked, "blink");
            bool mjolnirCheckboxValue = GUILayout.Toggle(mjolnirUnlocked, "mjolnir");
            bool cloakCheckboxValue = GUILayout.Toggle(cloakUnlocked, "cloak");
            bool powerSlideCheckboxValue = GUILayout.Toggle(powerSlideUnlocked, "powerSlide");
            bool waterblinkCheckboxValue = GUILayout.Toggle(waterblinkUnlocked, "waterblink");
            bool axeCheckboxValue = GUILayout.Toggle(axeUnlocked, "axe");
            bool blinkWireAxeCheckboxValue = GUILayout.Toggle(blinkWireAxeUnlocked, "blinkWireAxe");
            bool doubleJumpCheckboxValue = GUILayout.Toggle(doubleJumpUnlocked, "doubleJump");
            bool mapCheckboxValue = GUILayout.Toggle(mapUnlocked, "map");
            bool omniBlinkCheckboxValue = GUILayout.Toggle(omniBlinkUnlocked, "omniBlink");
            bool redCloakCheckboxValue = GUILayout.Toggle(redCloakUnlocked, "redCloak");
            bool secretsMapCheckboxValue = GUILayout.Toggle(secretsMapUnlocked, "secretsMap");

            if (blinkCheckboxValue != blinkUnlocked)
            {
                blinkUnlocked = blinkCheckboxValue;
                onBlinkCheckboxChanged(blinkUnlocked);
            }

            if (mjolnirCheckboxValue != mjolnirUnlocked)
            {
                mjolnirUnlocked = mjolnirCheckboxValue;
                onMjolnirCheckboxChanged(mjolnirUnlocked);
            }

            if (cloakCheckboxValue != cloakUnlocked)
            {
                cloakUnlocked = cloakCheckboxValue;
                onCloakCheckboxChanged(cloakUnlocked);
            }

            if (powerSlideCheckboxValue != powerSlideUnlocked)
            {
                powerSlideUnlocked = powerSlideCheckboxValue;
                onPowerSlideCheckboxChanged(powerSlideUnlocked);
            }

            if (waterblinkCheckboxValue != waterblinkUnlocked)
            {
                waterblinkUnlocked = waterblinkCheckboxValue;
                onWaterblinkCheckboxChanged(waterblinkUnlocked);
            }

            if (axeCheckboxValue != axeUnlocked)
            {
                axeUnlocked = axeCheckboxValue;
                onAxeCheckboxChanged(axeUnlocked);
            }

            if (blinkWireAxeCheckboxValue != blinkWireAxeUnlocked)
            {
                blinkWireAxeUnlocked = blinkWireAxeCheckboxValue;
                onBlinkWireAxeCheckboxChanged(blinkWireAxeUnlocked);
            }

            if (doubleJumpCheckboxValue != doubleJumpUnlocked)
            {
                doubleJumpUnlocked = doubleJumpCheckboxValue;
                onDoubleJumpCheckboxChanged(doubleJumpUnlocked);
            }

            if (mapCheckboxValue != mapUnlocked)
            {
                mapUnlocked = mapCheckboxValue;
                onMapCheckboxChanged(mapUnlocked);
            }

            if (omniBlinkCheckboxValue != omniBlinkUnlocked)
            {
                omniBlinkUnlocked = omniBlinkCheckboxValue;
                onOmniBlinkCheckboxChanged(omniBlinkUnlocked);
            }

            if (redCloakCheckboxValue != redCloakUnlocked)
            {
                redCloakUnlocked = redCloakCheckboxValue;
                onRedCloakCheckboxChanged(redCloakUnlocked);
            }

            if (secretsMapCheckboxValue != secretsMapUnlocked)
            {
                secretsMapUnlocked = secretsMapCheckboxValue;
                onSecretsMapCheckboxChanged(secretsMapUnlocked);
            }

            // Add a button to toggle GUI visibility
            if (GUILayout.Button("Close"))
            {
                showGui = false;
            }

            // Allow the window to be dragged
            GUI.DragWindow();
        }

        private void onBlinkCheckboxChanged(bool value)
        {
            if(Game.activeSlot != null)
            {
                Game.activeSlot.blinkUnlocked = value;
            }
        }

        private void onMjolnirCheckboxChanged(bool value)
        {
            if(Game.activeSlot != null)
            {
                Game.activeSlot.mjolnirUnlocked = value;
            }
        }

        private void onCloakCheckboxChanged(bool value)
        {
            if(Game.activeSlot != null)
            {
                Game.activeSlot.cloakUnlocked = value;
            }
        }

        private void onPowerSlideCheckboxChanged(bool value)
        {
            if(Game.activeSlot != null)
            {
                Game.activeSlot.powerSlideUnlocked = value;
            }
        }

        private void onWaterblinkCheckboxChanged(bool value)
        {
            if(Game.activeSlot != null)
            {
                Game.activeSlot.waterblinkUnlocked = value;
            }
        }

        private void onAxeCheckboxChanged(bool value)
        {
            if(Game.activeSlot != null)
            {
                Game.activeSlot.axeUnlocked = value;
            }
        }

        private void onBlinkWireAxeCheckboxChanged(bool value)
        {
            if(Game.activeSlot != null)
            {
                Game.activeSlot.blinkWireAxeUnlocked = value;
            }
        }

        private void onDoubleJumpCheckboxChanged(bool value)
        {
            if(Game.activeSlot != null)
            {
                Game.activeSlot.doubleJumpUnlocked = value;
            }
        }

        private void onMapCheckboxChanged(bool value)
        {
            if(Game.activeSlot != null)
            {
                Game.activeSlot.mapUnlocked = value;
            }
        }

        private void onOmniBlinkCheckboxChanged(bool value)
        {
            if(Game.activeSlot != null)
            {
                Game.activeSlot.omniBlinkUnlocked = value;
            }
        }

        private void onRedCloakCheckboxChanged(bool value)
        {
            if(Game.activeSlot != null)
            {
                Game.activeSlot.redCloakUnlocked = value;
            }
        }

        private void onSecretsMapCheckboxChanged(bool value)
        {
            if (Game.activeSlot != null)
            {
                Game.activeSlot.secretsMapUnlocked = value;
            }
        }

        public void hideWindow()
        {
            showGui = false;
        }

        public void showWindow()
        {
            showGui = true;
        }

        public void ToggleWindow()
        {
            showGui = !showGui;
        }
    }
}
