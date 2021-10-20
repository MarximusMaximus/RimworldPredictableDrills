using UnityEngine;
using Verse;

namespace PredictableDrills
{
    /// <summary>
    /// Mod class for Predictable Deep Drills
    /// </summary>
    public class PredictableDrill : Mod
    {
        /// <summary>
        /// A convenience property to get the settings statically
        /// </summary>
        /// <value>The settings.</value>
        public static PredictableDrillSettings Settings { get; private set; }

        public PredictableDrill(ModContentPack content) : base(content)
        {
            Settings = GetSettings<PredictableDrillSettings>();
        }

        /// <summary>
        /// Draws the settings window
        /// </summary>
        /// <param name="inRect">In rect.</param>
        public override void DoSettingsWindowContents(Rect inRect)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(inRect);

            listingStandard.CheckboxLabeled("Allow drilling for non-native stone", ref Settings.dropNonMapStone,
                "If enabled, deep drills on a patch of stone can drill up that kind of stone, even if it's not native" +
                " to the map.");

            listingStandard.CheckboxLabeled("Allow drilling for stone in stoneless biomes", ref Settings.alwaysDropStone,
                "If enabled, deep drills on a patch of stone can drill up stone chunks, even if the biome you're in" +
                " would not normally allow drilling for stone (such as sea ice).");

            listingStandard.GapLine();

            if (listingStandard.ButtonText("Reset All Settings"))
            {
                Dialog_MessageBox confirmDialog = new Dialog_MessageBox(
                    "Reset all Predictable Deep Drill settings to default?",
                    "Reset", Settings.ResetAll,
                    "Cancel", null,
                    buttonADestructive: true
                );
                Find.WindowStack.Add(confirmDialog);
            }

            listingStandard.End();
        }

        /// <summary>
        /// Override SettingsCategory to show up in the list of settings.
        /// </summary>
        /// <returns> The (translated) mod name. </returns>
        public override string SettingsCategory()
        {
            //TODO translate
            return "Predictable Deep Drills";
        }
    }

    /// <summary>
    /// Mod settings for Predictable Deep Drills
    /// </summary>
    public class PredictableDrillSettings : ModSettings
    {
        /// <summary>
        /// If <see langword="true"/>, deep drills will always drill up the stone type below them, even if it's not
        /// normally available on the map.
        /// </summary>
        public bool dropNonMapStone;

        /// <summary>
        /// If <see langword="true"/>, deep drills placed on stone will drop stone even when the map normally does not have
        /// stone, such as on sea ice.
        /// </summary>
        public bool alwaysDropStone;

        /// <summary>
        /// Resets all settings to their default values.
        /// </summary>
        public void ResetAll()
        {
            dropNonMapStone = false;
            alwaysDropStone = false;
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref dropNonMapStone, nameof(dropNonMapStone), false);
            Scribe_Values.Look(ref alwaysDropStone, nameof(alwaysDropStone), false);
        }
    }
}
