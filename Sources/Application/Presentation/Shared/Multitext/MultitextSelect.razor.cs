using Microsoft.AspNetCore.Components;

namespace DrinkBuddy.Presentation.Shared.Multitext
{
    public partial class MultitextSelect
    {
        [Parameter]
        [EditorRequired]
        public IList<string> Values { get; set; } = null!;

        [Parameter]
        [EditorRequired]
        public EventCallback<IList<string>> ValuesChanged { get; set; }

        private string InputValue { get; set; } = string.Empty;

        private async Task AddAsync()
        {
            if (!string.IsNullOrWhiteSpace(InputValue) && !Values.Contains(InputValue))
            {
                Values.Add(InputValue);
                InputValue = string.Empty;
                await ValuesChanged.InvokeAsync(Values);
            }
        }

        private async Task DeleteAsync(string val)
        {
            Values.Remove(val);
            await ValuesChanged.InvokeAsync(Values);
        }
    }
}