namespace Features.Shed.Upgrade
{
    internal sealed class StubUpgradeHandler : IUpgradeHandler
    {
        public static readonly IUpgradeHandler Default = new StubUpgradeHandler();

        public void Upgrade(IUpgradable upgradable) { }
    }
}
