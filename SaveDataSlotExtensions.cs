using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Il2Cpp;

namespace Tesla2DebugMod
{
    public static class SaveDataSlotExtensions
    {
        public static SaveDataSlot DeepCopy(this SaveDataSlot obj)
        {
            if (obj == null)
                return default;

            SaveDataSlot copy = new SaveDataSlot();
            obj.CopyTo(copy);
            return copy;
        }

        public static void CopyTo(this SaveDataSlot from, SaveDataSlot to)
        {
            to.activitiesUnlocked = new Il2CppSystem.Collections.Generic.List<int>(from.activitiesUnlocked.Cast<Il2CppSystem.Collections.Generic.IEnumerable<int>>());
            to.axeUnlocked = from.axeUnlocked;
            to.blinkUnlocked = from.blinkUnlocked;
            to.blinkWireAxeUnlocked = from.blinkWireAxeUnlocked;
            to.cloakUnlocked = from.cloakUnlocked;
            to.dateModified = from.dateModified;
            to.doubleJumpUnlocked = from.doubleJumpUnlocked;
            to.fafnirBossFightBeaten = from.fafnirBossFightBeaten;
            to.galvanBossFightBeaten = from.galvanBossFightBeaten;
            to.gameWasCompletedOnce = from.gameWasCompletedOnce;
            to.halvtannBossFightBeaten = from.halvtannBossFightBeaten;
            to.hasMetGalvan = from.hasMetGalvan;
            to.hulderBossfightBeaten = from.hulderBossfightBeaten;
            to.HulderUnderworldChaseProgression = from.HulderUnderworldChaseProgression;
            to.hulder_DarkRoomLevelChaseDone = from.hulder_DarkRoomLevelChaseDone;
            to.hulder_GrueEyesLevelChaseDone = from.hulder_GrueEyesLevelChaseDone;
            to.hulder_PreBossDiscoverTraversed = from.hulder_PreBossDiscoverTraversed;
            to.hulder_PreBossLevelChaseDone = from.hulder_PreBossLevelChaseDone;
            to.invasionSequenceDone = from.invasionSequenceDone;
            to.mapShapesUnlocked = from.mapShapesUnlocked;
            to.mapShapesUnlocked = new Il2CppSystem.Collections.Generic.List<String>(from.mapShapesUnlocked.Cast<Il2CppSystem.Collections.Generic.IEnumerable<String>>());
            to.mapUnlocked = from.mapUnlocked;
            to.mjolnirUnlocked = from.mjolnirUnlocked;
            to.mooseBossFightBeaten = from.mooseBossFightBeaten;
            to.name = from.name;
            to.omniBlinkUnlocked = from.omniBlinkUnlocked;
            to.powerSlideUnlocked = from.powerSlideUnlocked;
            to.redCloakUnlocked = from.redCloakUnlocked;
            to.respawnFacingRight = from.respawnFacingRight;
            to.respawnPoint = from.respawnPoint;
            to.respawnScene = from.respawnScene;
            to.savedCharges = new Il2CppSystem.Collections.Generic.List<SavedCharge>();
            foreach (SavedCharge item in from.savedCharges)
            {
                to.savedCharges.Add(item.DeepCopy());
            }
            to.savedResetInfos = new Il2CppSystem.Collections.Generic.List<SavedResetInfo>();
            foreach (SavedResetInfo item in from.savedResetInfos)
            {
                to.savedResetInfos.Add(item.DeepCopy());
            }
            to.scrollsPickedUp = new Il2CppSystem.Collections.Generic.List<int>(from.scrollsPickedUp.Cast<Il2CppSystem.Collections.Generic.IEnumerable<int>>());
            to.scrollsSeenInCollection = new Il2CppSystem.Collections.Generic.List<int>(from.scrollsSeenInCollection.Cast<Il2CppSystem.Collections.Generic.IEnumerable<int>>());
            to.secretsMapUnlocked = from.secretsMapUnlocked;
            to.timeSpent = from.timeSpent;
            to.triggersSet = new Il2CppSystem.Collections.Generic.List<String>(from.triggersSet.Cast<Il2CppSystem.Collections.Generic.IEnumerable<String>>());
            to.trollMiniBossFightBeaten = from.trollMiniBossFightBeaten;
            to.version = from.version;
            to.vikingBlimpOnTheHunt = from.vikingBlimpOnTheHunt;
            to.vikingBlimpPosition = from.vikingBlimpPosition;
            to.waterblinkUnlocked = from.waterblinkUnlocked;
        }

        public static void CopyTo(this SavedCharge from, SavedCharge to)
        {
            to.saveID = from.saveID;
            to.charge = from.charge;
        }

        public static SavedCharge DeepCopy(this SavedCharge obj)
        {
            if (obj == null)
                return default;

            SavedCharge copy = new SavedCharge();
            obj.CopyTo(copy);
            return copy;
        }

        public static void CopyTo(this SavedResetInfo from, SavedResetInfo to)
        {
            to.saveID = from.saveID;
            to.position = from.position;
            to.rotationEuler = from.rotationEuler;
            to.scale = from.scale;
            to.isKinematic = from.isKinematic;
            to.angularDrag = from.angularDrag;
            to.drag = from.drag;
        }

        public static SavedResetInfo DeepCopy(this SavedResetInfo obj)
        {
            if (obj == null)
                return default;

            SavedResetInfo copy = new SavedResetInfo();
            obj.CopyTo(copy);
            return copy;
        }
    }
}
