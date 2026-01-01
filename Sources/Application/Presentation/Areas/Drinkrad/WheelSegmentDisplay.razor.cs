using Microsoft.AspNetCore.Components;

namespace DrinkBuddy.Presentation.Areas.Drinkrad
{
    public partial class WheelSegmentDisplay
    {
        [Parameter]
        [EditorRequired]
        public required Rad.WheelSegment Segment { get; set; }
    }
}