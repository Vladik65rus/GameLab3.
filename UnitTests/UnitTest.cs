using Xunit;

public class UnitTests
{
    [Fact]
    public void Unit_ShouldTakeDamage()
    {
        var unit = new Unit("Warrior", 100, 30, 10);
        unit.TakeDamage(30);
        Assert.Equal(70, unit.Health);
    }

    [Fact]
    public void Unit_ShouldBeAliveAfterTakingLessDamageThanHealth()
    {
        var unit = new Unit("Mage", 50, 20, 5);
        unit.TakeDamage(30);
        Assert.True(unit.IsAlive());
    }

    [Fact]
    public void Unit_ShouldBeDeadAfterTakingFatalDamage()
    {
        var unit = new Unit("Tank", 100, 50, 30);
        unit.TakeDamage(120);  // Слишком много урона
        Assert.False(unit.IsAlive());
    }
}
