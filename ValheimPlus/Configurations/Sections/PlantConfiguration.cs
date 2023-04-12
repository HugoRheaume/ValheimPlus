namespace ValheimPlus.Configurations.Sections
{
    public class PlantConfiguration : ServerSyncConfig<PlantConfiguration>
    {
        public bool showDuration { get; internal set; } = false;
        public float beechSeeds { get; internal set; } = 3000;

    }
}