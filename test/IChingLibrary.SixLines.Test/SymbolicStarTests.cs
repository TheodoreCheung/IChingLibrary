using IChingLibrary.Core;

namespace IChingLibrary.SixLines.Test;

public class SymbolicStarTests
{
    [Fact]
    public void SymbolicStar_PresetValues_ShouldReturnCorrectValue()
    {
        // 基于日干的神�?
        Assert.Equal(1, SymbolicStar.Nobleman.Value);
        Assert.Equal(2, SymbolicStar.SalarySpirit.Value);
        Assert.Equal(3, SymbolicStar.CultureFlourish.Value);
        Assert.Equal(6, SymbolicStar.YangBlade.Value);

        // 基于三合局的神�?
        Assert.Equal(4, SymbolicStar.PostHorse.Value);
        Assert.Equal(5, SymbolicStar.PeachBlossom.Value);
        Assert.Equal(9, SymbolicStar.GeneralsStar.Value);
        Assert.Equal(10, SymbolicStar.Canopy.Value);
        Assert.Equal(11, SymbolicStar.StarOfStrategy.Value);
        Assert.Equal(8, SymbolicStar.DisasterMalignity.Value);
        Assert.Equal(7, SymbolicStar.RobberyMalignity.Value);
        Assert.Equal(12, SymbolicStar.DeathSpirit.Value);

        // 其他神煞
        Assert.Equal(13, SymbolicStar.CelestialPhysician.Value);
        Assert.Equal(14, SymbolicStar.HeavenlyJoy.Value);
        Assert.Equal(15, SymbolicStar.MarriageBed.Value);
        Assert.Equal(16, SymbolicStar.BridalChamber.Value);
    }

    [Fact]
    public void SymbolicStar_ToString_ShouldReturnLabel()
    {
        Assert.Equal("Nobleman", SymbolicStar.Nobleman.Label);
        Assert.Equal("SalarySpirit", SymbolicStar.SalarySpirit.Label);
        Assert.Equal("CultureFlourish", SymbolicStar.CultureFlourish.Label);
        Assert.Equal("YangBlade", SymbolicStar.YangBlade.Label);
        Assert.Equal("PostHorse", SymbolicStar.PostHorse.Label);
        Assert.Equal("PeachBlossom", SymbolicStar.PeachBlossom.Label);
        Assert.Equal("GeneralsStar", SymbolicStar.GeneralsStar.Label);
        Assert.Equal("Canopy", SymbolicStar.Canopy.Label);
        Assert.Equal("StarOfStrategy", SymbolicStar.StarOfStrategy.Label);
        Assert.Equal("DisasterMalignity", SymbolicStar.DisasterMalignity.Label);
        Assert.Equal("RobberyMalignity", SymbolicStar.RobberyMalignity.Label);
        Assert.Equal("DeathSpirit", SymbolicStar.DeathSpirit.Label);
        Assert.Equal("CelestialPhysician", SymbolicStar.CelestialPhysician.Label);
        Assert.Equal("HeavenlyJoy", SymbolicStar.HeavenlyJoy.Label);
        Assert.Equal("MarriageBed", SymbolicStar.MarriageBed.Label);
        Assert.Equal("BridalChamber", SymbolicStar.BridalChamber.Label);
    }

    [Fact]
    public void SymbolicStar_Equals_ShouldWorkCorrectly()
    {
        var nobleman1 = SymbolicStar.Nobleman;
        var nobleman2 = SymbolicStar.Nobleman;
        var salarySpirit = SymbolicStar.SalarySpirit;

        Assert.Equal(nobleman1, nobleman2);
        Assert.NotEqual(nobleman1, salarySpirit);
        Assert.True(nobleman1 == nobleman2);
        Assert.True(nobleman1 != salarySpirit);
    }

    [Fact]
    public void SymbolicStar_GetAll_ShouldReturnAllPresetElements()
    {
        var all = SymbolicStar.GetAll().ToList();

        Assert.Equal(16, all.Count);
        Assert.Contains(SymbolicStar.Nobleman, all);
        Assert.Contains(SymbolicStar.SalarySpirit, all);
        Assert.Contains(SymbolicStar.CultureFlourish, all);
        Assert.Contains(SymbolicStar.YangBlade, all);
        Assert.Contains(SymbolicStar.PostHorse, all);
        Assert.Contains(SymbolicStar.PeachBlossom, all);
        Assert.Contains(SymbolicStar.GeneralsStar, all);
        Assert.Contains(SymbolicStar.Canopy, all);
        Assert.Contains(SymbolicStar.StarOfStrategy, all);
        Assert.Contains(SymbolicStar.DisasterMalignity, all);
        Assert.Contains(SymbolicStar.RobberyMalignity, all);
        Assert.Contains(SymbolicStar.DeathSpirit, all);
        Assert.Contains(SymbolicStar.CelestialPhysician, all);
        Assert.Contains(SymbolicStar.HeavenlyJoy, all);
        Assert.Contains(SymbolicStar.MarriageBed, all);
        Assert.Contains(SymbolicStar.BridalChamber, all);
    }

    [Fact]
    public void SymbolicStar_CreateCustom_ShouldReturnUniqueStars()
    {
        var custom1 = SymbolicStar.CreateCustom("CustomStar1");
        var custom2 = SymbolicStar.CreateCustom("CustomStar2");
        var custom3 = SymbolicStar.CreateCustom("CustomStar1");

        Assert.True(custom1.Value > 16);
        Assert.True(custom2.Value > 16);
        Assert.True(custom3.Value > 16);
        Assert.NotEqual(custom1.Value, custom2.Value);
        Assert.NotEqual(custom2.Value, custom3.Value);
        Assert.NotEqual(custom1.Value, custom3.Value);

        Assert.Equal("CustomStar1", custom1.Label);
        Assert.Equal("CustomStar2", custom2.Label);
        Assert.Equal("CustomStar1", custom3.Label);

        Assert.NotEqual(custom1, custom3);
    }

    [Fact]
    public void SymbolicStar_CreateCustom_ThreadSafety()
    {
        const int threadCount = 5;
        const int starsPerThread = 20;
        var stars = new List<SymbolicStar>();
        var lockObj = new object();

        var threads = new List<Thread>();
        for (int i = 0; i < threadCount; i++)
        {
            var thread = new Thread(() =>
            {
                var localStars = new List<SymbolicStar>();
                for (int j = 0; j < starsPerThread; j++)
                {
                    localStars.Add(SymbolicStar.CreateCustom($"ThreadStar{i}_{j}"));
                }
                lock (lockObj)
                {
                    stars.AddRange(localStars);
                }
            });
            threads.Add(thread);
        }

        foreach (var thread in threads)
        {
            thread.Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

        Assert.Equal(threadCount * starsPerThread, stars.Count);

        // 检查所有值都是唯一�?
        var values = stars.Select(s => s.Value).ToList();
        var uniqueValues = new HashSet<byte>(values);
        Assert.Equal(values.Count, uniqueValues.Count);
    }
}
