using ChatAIze.Abstractions.Settings;

namespace ChatAIze.PluginApi.Settings;

public class DecimalSetting : IDecimalSetting
{
    public required string Key { get; set; }
    
    public required string Title { get; set; }
    
    public string? Description { get; set; }
    
    public double DefaultValue { get; set; }
    
    public double MinValue { get; set; }
    
    public double MaxValue { get; set; }
}
