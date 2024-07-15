namespace KitchenChaos.Scripts.Common.SingleConfigs
{
    public class BaseSingleConfig<TConfig> where TConfig : class, new()
    {
        public static TConfig Current;
    }
}
