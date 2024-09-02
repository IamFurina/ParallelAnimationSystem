using Pamx.Common;
using Pamx.Common.Data;
using Pamx.Common.Enum;

namespace ParallelAnimationSystem.Core;

public static class LsMigration
{
    public static void MigrateBeatmap(IBeatmap beatmap)
    {
        // Negate prefab offsets
        foreach (var prefab in beatmap.Prefabs)
        {
            prefab.Offset = -prefab.Offset;
        }
        
        // Migrate theme color keyframes
        foreach (var o in beatmap.Objects.Concat(beatmap.Prefabs.SelectMany(x => x.BeatmapObjects)))
        {
            if (o.Type == ObjectType.LegacyHelper)
            {
                // Replace all the color events
                for (var i = 0; i < o.ColorEvents.Count; i++)
                {
                    var oldColorKeyframe = o.ColorEvents[i];
                    oldColorKeyframe.Value = new ThemeColor
                    {
                        Index = oldColorKeyframe.Value.Index,
                        EndIndex = oldColorKeyframe.Value.EndIndex,
                        Opacity = 0.35f,
                    };
                    o.ColorEvents[i] = oldColorKeyframe;
                }
            }
        }
        
        // Migrate bloom keyframes
        var events = beatmap.Events;
        for (var i = 0; i < events.Bloom.Count; i++)
        {
            var bloomKeyframe = events.Bloom[i];
            bloomKeyframe.Value = new BloomData
            {
                Intensity = bloomKeyframe.Value.Intensity,
                Diffusion = 5.0f,
            };
            events.Bloom[i] = bloomKeyframe;
        }
    }
}