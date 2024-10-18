
using UnityEngine;
using Verse;
using RimWorld;

namespace VanillaPlantsExpandedMorePlants
{
    public class Designator_FlowerGardenGrowingZone : Designator_ZoneAdd
    {
        protected override string NewZoneLabel
        {
            get
            {
                return "VCE_FlowerGardenGrowingZone".Translate();
            }
        }

        public Designator_FlowerGardenGrowingZone()
        {
           
            this.zoneTypeToPlace = typeof(Zone_GrowingFlowerGarden);
            this.defaultLabel = "VCE_FlowerGardenGrowingZone".Translate();
            this.defaultDesc = "VCE_FlowerGardenGrowingZoneDesc".Translate();
            this.icon = ContentFinder<Texture2D>.Get("UI/Designators/VCE_ZoneCreate_FlowerGarden", true);
            this.hotKey = KeyBindingDefOf.Misc2;
           
            this.tutorTag = "ZoneAdd_Growing";
        }

        public override AcceptanceReport CanDesignateCell(IntVec3 c)
        {
            if (!base.CanDesignateCell(c).Accepted)
            {
                return false;
            }
            float num = (ModsConfig.BiotechActive ? 0.5f : ThingDefOf.Plant_Potato.plant.fertilityMin);
            if (ModsConfig.IdeologyActive && BuildCopyCommandUtility.FindAllowedDesignator(TerrainDefOf.FungalGravel) != null)
            {
                num = Mathf.Min(num, ThingDefOf.Plant_Nutrifungus.plant.fertilityMin);
            }
            if (c.GetFertility(base.Map) < num)
            {
                return false;
            }
            return true;
        }



        protected override Zone MakeNewZone()
        {
            return new Zone_GrowingFlowerGarden(Find.CurrentMap.zoneManager);
        }
    }
}
