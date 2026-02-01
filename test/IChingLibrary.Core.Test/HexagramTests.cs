using System.Globalization;
using IChingLibrary.Core;

namespace IChingLibrary.Core.Test;

public class HexagramTests
{
    private static readonly CultureInfo En = new("en");
    #region 乾宫测试

    [Fact]
    public void Hexagram_TheCreative_ShouldHaveCorrectProperties()
    {
        // Assert - 乾卦 (乾上乾下)
        Assert.Equal(Trigram.Qian, Hexagram.TheCreative.Upper);
        Assert.Equal(Trigram.Qian, Hexagram.TheCreative.Lower);
        Assert.Equal(Trigram.Qian, Hexagram.TheCreative.Palace);
        Assert.Equal("The Creative", Hexagram.TheCreative.ToString(En));
    }

    [Fact]
    public void Hexagram_ComingToMeet_ShouldHaveCorrectProperties()
    {
        // Assert - 姤卦 (乾上巽下)
        Assert.Equal(Trigram.Qian, Hexagram.ComingToMeet.Upper);
        Assert.Equal(Trigram.Xun, Hexagram.ComingToMeet.Lower);
        Assert.Equal(Trigram.Qian, Hexagram.ComingToMeet.Palace);
        Assert.Equal("Coming to Meet", Hexagram.ComingToMeet.ToString(En));
    }

    [Fact]
    public void Hexagram_Retreat_ShouldHaveCorrectProperties()
    {
        // Assert - 遁卦 (乾上艮下)
        Assert.Equal(Trigram.Qian, Hexagram.Retreat.Upper);
        Assert.Equal(Trigram.Gen, Hexagram.Retreat.Lower);
        Assert.Equal(Trigram.Qian, Hexagram.Retreat.Palace);
        Assert.Equal("Retreat", Hexagram.Retreat.ToString(En));
    }

    [Fact]
    public void Hexagram_Standstill_ShouldHaveCorrectProperties()
    {
        // Assert - 否卦 (乾上坤下)
        Assert.Equal(Trigram.Qian, Hexagram.Standstill.Upper);
        Assert.Equal(Trigram.Kun, Hexagram.Standstill.Lower);
        Assert.Equal(Trigram.Qian, Hexagram.Standstill.Palace);
        Assert.Equal("Standstill", Hexagram.Standstill.ToString(En));
    }

    [Fact]
    public void Hexagram_Contemplation_ShouldHaveCorrectProperties()
    {
        // Assert - 观卦 (巽上坤下)
        Assert.Equal(Trigram.Xun, Hexagram.Contemplation.Upper);
        Assert.Equal(Trigram.Kun, Hexagram.Contemplation.Lower);
        Assert.Equal(Trigram.Qian, Hexagram.Contemplation.Palace);
        Assert.Equal("Contemplation", Hexagram.Contemplation.ToString(En));
    }

    [Fact]
    public void Hexagram_SplittingApart_ShouldHaveCorrectProperties()
    {
        // Assert - 剥卦 (艮上坤下)
        Assert.Equal(Trigram.Gen, Hexagram.SplittingApart.Upper);
        Assert.Equal(Trigram.Kun, Hexagram.SplittingApart.Lower);
        Assert.Equal(Trigram.Qian, Hexagram.SplittingApart.Palace);
        Assert.Equal("Splitting Apart", Hexagram.SplittingApart.ToString(En));
    }

    [Fact]
    public void Hexagram_Progress_ShouldHaveCorrectProperties()
    {
        // Assert - 晋卦 (离上坤下)
        Assert.Equal(Trigram.Li, Hexagram.Progress.Upper);
        Assert.Equal(Trigram.Kun, Hexagram.Progress.Lower);
        Assert.Equal(Trigram.Qian, Hexagram.Progress.Palace);
        Assert.Equal("Progress", Hexagram.Progress.ToString(En));
    }

    [Fact]
    public void Hexagram_PossessionInGreatMeasure_ShouldHaveCorrectProperties()
    {
        // Assert - 大有卦 (离上乾下)
        Assert.Equal(Trigram.Li, Hexagram.PossessionInGreatMeasure.Upper);
        Assert.Equal(Trigram.Qian, Hexagram.PossessionInGreatMeasure.Lower);
        Assert.Equal(Trigram.Qian, Hexagram.PossessionInGreatMeasure.Palace);
        Assert.Equal("Possession in Great Measure", Hexagram.PossessionInGreatMeasure.ToString(En));
    }

    #endregion

    #region 兑宫测试

    [Fact]
    public void Hexagram_TheJoyous_ShouldHaveCorrectProperties()
    {
        // Assert - 兑卦 (兑上兑下)
        Assert.Equal(Trigram.Dui, Hexagram.TheJoyous.Upper);
        Assert.Equal(Trigram.Dui, Hexagram.TheJoyous.Lower);
        Assert.Equal(Trigram.Dui, Hexagram.TheJoyous.Palace);
        Assert.Equal("The Joyous", Hexagram.TheJoyous.ToString(En));
    }

    [Fact]
    public void Hexagram_Oppression_ShouldHaveCorrectProperties()
    {
        // Assert - 困卦 (兑上坎下)
        Assert.Equal(Trigram.Dui, Hexagram.Oppression.Upper);
        Assert.Equal(Trigram.Kan, Hexagram.Oppression.Lower);
        Assert.Equal(Trigram.Dui, Hexagram.Oppression.Palace);
        Assert.Equal("Oppression", Hexagram.Oppression.ToString(En));
    }

    [Fact]
    public void Hexagram_GatheringTogether_ShouldHaveCorrectProperties()
    {
        // Assert - 萃卦 (兑上坤下)
        Assert.Equal(Trigram.Dui, Hexagram.GatheringTogether.Upper);
        Assert.Equal(Trigram.Kun, Hexagram.GatheringTogether.Lower);
        Assert.Equal(Trigram.Dui, Hexagram.GatheringTogether.Palace);
        Assert.Equal("Gathering Together", Hexagram.GatheringTogether.ToString(En));
    }

    [Fact]
    public void Hexagram_Influence_ShouldHaveCorrectProperties()
    {
        // Assert - 咸卦 (兑上艮下)
        Assert.Equal(Trigram.Dui, Hexagram.Influence.Upper);
        Assert.Equal(Trigram.Gen, Hexagram.Influence.Lower);
        Assert.Equal(Trigram.Dui, Hexagram.Influence.Palace);
        Assert.Equal("Influence", Hexagram.Influence.ToString(En));
    }

    [Fact]
    public void Hexagram_Obstruction_ShouldHaveCorrectProperties()
    {
        // Assert - 蹇卦 (坎上艮下)
        Assert.Equal(Trigram.Kan, Hexagram.Obstruction.Upper);
        Assert.Equal(Trigram.Gen, Hexagram.Obstruction.Lower);
        Assert.Equal(Trigram.Dui, Hexagram.Obstruction.Palace);
        Assert.Equal("Obstruction", Hexagram.Obstruction.ToString(En));
    }

    [Fact]
    public void Hexagram_Modesty_ShouldHaveCorrectProperties()
    {
        // Assert - 谦卦 (坤上艮下)
        Assert.Equal(Trigram.Kun, Hexagram.Modesty.Upper);
        Assert.Equal(Trigram.Gen, Hexagram.Modesty.Lower);
        Assert.Equal(Trigram.Dui, Hexagram.Modesty.Palace);
        Assert.Equal("Modesty", Hexagram.Modesty.ToString(En));
    }

    [Fact]
    public void Hexagram_PreponderanceOfTheSmall_ShouldHaveCorrectProperties()
    {
        // Assert - 小过卦 (震上艮下)
        Assert.Equal(Trigram.Zhen, Hexagram.PreponderanceOfTheSmall.Upper);
        Assert.Equal(Trigram.Gen, Hexagram.PreponderanceOfTheSmall.Lower);
        Assert.Equal(Trigram.Dui, Hexagram.PreponderanceOfTheSmall.Palace);
        Assert.Equal("Preponderance of the Small", Hexagram.PreponderanceOfTheSmall.ToString(En));
    }

    [Fact]
    public void Hexagram_TheMarryingMaiden_ShouldHaveCorrectProperties()
    {
        // Assert - 归妹卦 (震上兑下)
        Assert.Equal(Trigram.Zhen, Hexagram.TheMarryingMaiden.Upper);
        Assert.Equal(Trigram.Dui, Hexagram.TheMarryingMaiden.Lower);
        Assert.Equal(Trigram.Dui, Hexagram.TheMarryingMaiden.Palace);
        Assert.Equal("The Marrying Maiden", Hexagram.TheMarryingMaiden.ToString(En));
    }

    #endregion

    #region 离宫测试

    [Fact]
    public void Hexagram_TheClinging_ShouldHaveCorrectProperties()
    {
        // Assert - 离卦 (离上离下)
        Assert.Equal(Trigram.Li, Hexagram.TheClinging.Upper);
        Assert.Equal(Trigram.Li, Hexagram.TheClinging.Lower);
        Assert.Equal(Trigram.Li, Hexagram.TheClinging.Palace);
        Assert.Equal("The Clinging", Hexagram.TheClinging.ToString(En));
    }

    [Fact]
    public void Hexagram_TheWanderer_ShouldHaveCorrectProperties()
    {
        // Assert - 旅卦 (离上艮下)
        Assert.Equal(Trigram.Li, Hexagram.TheWanderer.Upper);
        Assert.Equal(Trigram.Gen, Hexagram.TheWanderer.Lower);
        Assert.Equal(Trigram.Li, Hexagram.TheWanderer.Palace);
        Assert.Equal("The Wanderer", Hexagram.TheWanderer.ToString(En));
    }

    [Fact]
    public void Hexagram_TheCauldron_ShouldHaveCorrectProperties()
    {
        // Assert - 鼎卦 (离上巽下)
        Assert.Equal(Trigram.Li, Hexagram.TheCauldron.Upper);
        Assert.Equal(Trigram.Xun, Hexagram.TheCauldron.Lower);
        Assert.Equal(Trigram.Li, Hexagram.TheCauldron.Palace);
        Assert.Equal("The Cauldron", Hexagram.TheCauldron.ToString(En));
    }

    [Fact]
    public void Hexagram_BeforeCompletion_ShouldHaveCorrectProperties()
    {
        // Assert - 未济卦 (离上坎下)
        Assert.Equal(Trigram.Li, Hexagram.BeforeCompletion.Upper);
        Assert.Equal(Trigram.Kan, Hexagram.BeforeCompletion.Lower);
        Assert.Equal(Trigram.Li, Hexagram.BeforeCompletion.Palace);
        Assert.Equal("Before Completion", Hexagram.BeforeCompletion.ToString(En));
    }

    [Fact]
    public void Hexagram_YouthfulFolly_ShouldHaveCorrectProperties()
    {
        // Assert - 蒙卦 (艮上坎下)
        Assert.Equal(Trigram.Gen, Hexagram.YouthfulFolly.Upper);
        Assert.Equal(Trigram.Kan, Hexagram.YouthfulFolly.Lower);
        Assert.Equal(Trigram.Li, Hexagram.YouthfulFolly.Palace);
        Assert.Equal("Youthful Folly", Hexagram.YouthfulFolly.ToString(En));
    }

    [Fact]
    public void Hexagram_Dispersion_ShouldHaveCorrectProperties()
    {
        // Assert - 涣卦 (巽上坎下)
        Assert.Equal(Trigram.Xun, Hexagram.Dispersion.Upper);
        Assert.Equal(Trigram.Kan, Hexagram.Dispersion.Lower);
        Assert.Equal(Trigram.Li, Hexagram.Dispersion.Palace);
        Assert.Equal("Dispersion", Hexagram.Dispersion.ToString(En));
    }

    [Fact]
    public void Hexagram_Conflict_ShouldHaveCorrectProperties()
    {
        // Assert - 讼卦 (乾上坎下)
        Assert.Equal(Trigram.Qian, Hexagram.Conflict.Upper);
        Assert.Equal(Trigram.Kan, Hexagram.Conflict.Lower);
        Assert.Equal(Trigram.Li, Hexagram.Conflict.Palace);
        Assert.Equal("Conflict", Hexagram.Conflict.ToString(En));
    }

    [Fact]
    public void Hexagram_FellowshipWithMen_ShouldHaveCorrectProperties()
    {
        // Assert - 同人卦 (乾上离下)
        Assert.Equal(Trigram.Qian, Hexagram.FellowshipWithMen.Upper);
        Assert.Equal(Trigram.Li, Hexagram.FellowshipWithMen.Lower);
        Assert.Equal(Trigram.Li, Hexagram.FellowshipWithMen.Palace);
        Assert.Equal("Fellowship with Men", Hexagram.FellowshipWithMen.ToString(En));
    }

    #endregion

    #region 震宫测试

    [Fact]
    public void Hexagram_TheArousing_ShouldHaveCorrectProperties()
    {
        // Assert - 震卦 (震上震下)
        Assert.Equal(Trigram.Zhen, Hexagram.TheArousing.Upper);
        Assert.Equal(Trigram.Zhen, Hexagram.TheArousing.Lower);
        Assert.Equal(Trigram.Zhen, Hexagram.TheArousing.Palace);
        Assert.Equal("The Arousing", Hexagram.TheArousing.ToString(En));
    }

    [Fact]
    public void Hexagram_Enthusiasm_ShouldHaveCorrectProperties()
    {
        // Assert - 豫卦 (震上坤下)
        Assert.Equal(Trigram.Zhen, Hexagram.Enthusiasm.Upper);
        Assert.Equal(Trigram.Kun, Hexagram.Enthusiasm.Lower);
        Assert.Equal(Trigram.Zhen, Hexagram.Enthusiasm.Palace);
        Assert.Equal("Enthusiasm", Hexagram.Enthusiasm.ToString(En));
    }

    [Fact]
    public void Hexagram_Deliverance_ShouldHaveCorrectProperties()
    {
        // Assert - 解卦 (震上坎下)
        Assert.Equal(Trigram.Zhen, Hexagram.Deliverance.Upper);
        Assert.Equal(Trigram.Kan, Hexagram.Deliverance.Lower);
        Assert.Equal(Trigram.Zhen, Hexagram.Deliverance.Palace);
        Assert.Equal("Deliverance", Hexagram.Deliverance.ToString(En));
    }

    [Fact]
    public void Hexagram_Duration_ShouldHaveCorrectProperties()
    {
        // Assert - 恒卦 (震上巽下)
        Assert.Equal(Trigram.Zhen, Hexagram.Duration.Upper);
        Assert.Equal(Trigram.Xun, Hexagram.Duration.Lower);
        Assert.Equal(Trigram.Zhen, Hexagram.Duration.Palace);
        Assert.Equal("Duration", Hexagram.Duration.ToString(En));
    }

    [Fact]
    public void Hexagram_PushingUpward_ShouldHaveCorrectProperties()
    {
        // Assert - 升卦 (坤上巽下)
        Assert.Equal(Trigram.Kun, Hexagram.PushingUpward.Upper);
        Assert.Equal(Trigram.Xun, Hexagram.PushingUpward.Lower);
        Assert.Equal(Trigram.Zhen, Hexagram.PushingUpward.Palace);
        Assert.Equal("Pushing Upward", Hexagram.PushingUpward.ToString(En));
    }

    [Fact]
    public void Hexagram_TheWell_ShouldHaveCorrectProperties()
    {
        // Assert - 井卦 (坎上巽下)
        Assert.Equal(Trigram.Kan, Hexagram.TheWell.Upper);
        Assert.Equal(Trigram.Xun, Hexagram.TheWell.Lower);
        Assert.Equal(Trigram.Zhen, Hexagram.TheWell.Palace);
        Assert.Equal("The Well", Hexagram.TheWell.ToString(En));
    }

    [Fact]
    public void Hexagram_PreponderanceOfTheGreat_ShouldHaveCorrectProperties()
    {
        // Assert - 大过卦 (兑上巽下)
        Assert.Equal(Trigram.Dui, Hexagram.PreponderanceOfTheGreat.Upper);
        Assert.Equal(Trigram.Xun, Hexagram.PreponderanceOfTheGreat.Lower);
        Assert.Equal(Trigram.Zhen, Hexagram.PreponderanceOfTheGreat.Palace);
        Assert.Equal("Preponderance of the Great", Hexagram.PreponderanceOfTheGreat.ToString(En));
    }

    [Fact]
    public void Hexagram_Following_ShouldHaveCorrectProperties()
    {
        // Assert - 随卦 (兑上震下)
        Assert.Equal(Trigram.Dui, Hexagram.Following.Upper);
        Assert.Equal(Trigram.Zhen, Hexagram.Following.Lower);
        Assert.Equal(Trigram.Zhen, Hexagram.Following.Palace);
        Assert.Equal("Following", Hexagram.Following.ToString(En));
    }

    #endregion

    #region 巽宫测试

    [Fact]
    public void Hexagram_TheGentle_ShouldHaveCorrectProperties()
    {
        // Assert - 巽卦 (巽上巽下)
        Assert.Equal(Trigram.Xun, Hexagram.TheGentle.Upper);
        Assert.Equal(Trigram.Xun, Hexagram.TheGentle.Lower);
        Assert.Equal(Trigram.Xun, Hexagram.TheGentle.Palace);
        Assert.Equal("The Gentle", Hexagram.TheGentle.ToString(En));
    }

    [Fact]
    public void Hexagram_TheTamingPowerOfTheSmall_ShouldHaveCorrectProperties()
    {
        // Assert - 小畜卦 (巽上乾下)
        Assert.Equal(Trigram.Xun, Hexagram.TheTamingPowerOfTheSmall.Upper);
        Assert.Equal(Trigram.Qian, Hexagram.TheTamingPowerOfTheSmall.Lower);
        Assert.Equal(Trigram.Xun, Hexagram.TheTamingPowerOfTheSmall.Palace);
        Assert.Equal("The Taming Power of the Small", Hexagram.TheTamingPowerOfTheSmall.ToString(En));
    }

    [Fact]
    public void Hexagram_TheFamily_ShouldHaveCorrectProperties()
    {
        // Assert - 家人卦 (巽上离下)
        Assert.Equal(Trigram.Xun, Hexagram.TheFamily.Upper);
        Assert.Equal(Trigram.Li, Hexagram.TheFamily.Lower);
        Assert.Equal(Trigram.Xun, Hexagram.TheFamily.Palace);
        Assert.Equal("The Family", Hexagram.TheFamily.ToString(En));
    }

    [Fact]
    public void Hexagram_Increase_ShouldHaveCorrectProperties()
    {
        // Assert - 益卦 (巽上震下)
        Assert.Equal(Trigram.Xun, Hexagram.Increase.Upper);
        Assert.Equal(Trigram.Zhen, Hexagram.Increase.Lower);
        Assert.Equal(Trigram.Xun, Hexagram.Increase.Palace);
        Assert.Equal("Increase", Hexagram.Increase.ToString(En));
    }

    [Fact]
    public void Hexagram_Innocence_ShouldHaveCorrectProperties()
    {
        // Assert - 无妄卦 (乾上震下)
        Assert.Equal(Trigram.Qian, Hexagram.Innocence.Upper);
        Assert.Equal(Trigram.Zhen, Hexagram.Innocence.Lower);
        Assert.Equal(Trigram.Xun, Hexagram.Innocence.Palace);
        Assert.Equal("Innocence", Hexagram.Innocence.ToString(En));
    }

    [Fact]
    public void Hexagram_BitingThrough_ShouldHaveCorrectProperties()
    {
        // Assert - 噬嗑卦 (离上震下)
        Assert.Equal(Trigram.Li, Hexagram.BitingThrough.Upper);
        Assert.Equal(Trigram.Zhen, Hexagram.BitingThrough.Lower);
        Assert.Equal(Trigram.Xun, Hexagram.BitingThrough.Palace);
        Assert.Equal("Biting Through", Hexagram.BitingThrough.ToString(En));
    }

    [Fact]
    public void Hexagram_TheCornersOfTheMouth_ShouldHaveCorrectProperties()
    {
        // Assert - 颐卦 (艮上震下)
        Assert.Equal(Trigram.Gen, Hexagram.TheCornersOfTheMouth.Upper);
        Assert.Equal(Trigram.Zhen, Hexagram.TheCornersOfTheMouth.Lower);
        Assert.Equal(Trigram.Xun, Hexagram.TheCornersOfTheMouth.Palace);
        Assert.Equal("The Corners of the Mouth", Hexagram.TheCornersOfTheMouth.ToString(En));
    }

    [Fact]
    public void Hexagram_WorkOnTheDecayed_ShouldHaveCorrectProperties()
    {
        // Assert - 蛊卦 (艮上巽下)
        Assert.Equal(Trigram.Gen, Hexagram.WorkOnTheDecayed.Upper);
        Assert.Equal(Trigram.Xun, Hexagram.WorkOnTheDecayed.Lower);
        Assert.Equal(Trigram.Xun, Hexagram.WorkOnTheDecayed.Palace);
        Assert.Equal("Work on the Decayed", Hexagram.WorkOnTheDecayed.ToString(En));
    }

    #endregion

    #region 坎宫测试

    [Fact]
    public void Hexagram_TheAbysmal_ShouldHaveCorrectProperties()
    {
        // Assert - 坎卦 (坎上坎下)
        Assert.Equal(Trigram.Kan, Hexagram.TheAbysmal.Upper);
        Assert.Equal(Trigram.Kan, Hexagram.TheAbysmal.Lower);
        Assert.Equal(Trigram.Kan, Hexagram.TheAbysmal.Palace);
        Assert.Equal("The Abysmal", Hexagram.TheAbysmal.ToString(En));
    }

    [Fact]
    public void Hexagram_Limitation_ShouldHaveCorrectProperties()
    {
        // Assert - 节卦 (坎上兑下)
        Assert.Equal(Trigram.Kan, Hexagram.Limitation.Upper);
        Assert.Equal(Trigram.Dui, Hexagram.Limitation.Lower);
        Assert.Equal(Trigram.Kan, Hexagram.Limitation.Palace);
        Assert.Equal("Limitation", Hexagram.Limitation.ToString(En));
    }

    [Fact]
    public void Hexagram_DifficultyAtTheBeginning_ShouldHaveCorrectProperties()
    {
        // Assert - 屯卦 (坎上震下)
        Assert.Equal(Trigram.Kan, Hexagram.DifficultyAtTheBeginning.Upper);
        Assert.Equal(Trigram.Zhen, Hexagram.DifficultyAtTheBeginning.Lower);
        Assert.Equal(Trigram.Kan, Hexagram.DifficultyAtTheBeginning.Palace);
        Assert.Equal("Difficulty at the Beginning", Hexagram.DifficultyAtTheBeginning.ToString(En));
    }

    [Fact]
    public void Hexagram_AfterCompletion_ShouldHaveCorrectProperties()
    {
        // Assert - 既济卦 (坎上离下)
        Assert.Equal(Trigram.Kan, Hexagram.AfterCompletion.Upper);
        Assert.Equal(Trigram.Li, Hexagram.AfterCompletion.Lower);
        Assert.Equal(Trigram.Kan, Hexagram.AfterCompletion.Palace);
        Assert.Equal("After Completion", Hexagram.AfterCompletion.ToString(En));
    }

    [Fact]
    public void Hexagram_Revolution_ShouldHaveCorrectProperties()
    {
        // Assert - 革卦 (兑上离下)
        Assert.Equal(Trigram.Dui, Hexagram.Revolution.Upper);
        Assert.Equal(Trigram.Li, Hexagram.Revolution.Lower);
        Assert.Equal(Trigram.Kan, Hexagram.Revolution.Palace);
        Assert.Equal("Revolution", Hexagram.Revolution.ToString(En));
    }

    [Fact]
    public void Hexagram_Abundance_ShouldHaveCorrectProperties()
    {
        // Assert - 丰卦 (震上离下)
        Assert.Equal(Trigram.Zhen, Hexagram.Abundance.Upper);
        Assert.Equal(Trigram.Li, Hexagram.Abundance.Lower);
        Assert.Equal(Trigram.Kan, Hexagram.Abundance.Palace);
        Assert.Equal("Abundance", Hexagram.Abundance.ToString(En));
    }

    [Fact]
    public void Hexagram_DarkeningOfTheLight_ShouldHaveCorrectProperties()
    {
        // Assert - 明夷卦 (坤上离下)
        Assert.Equal(Trigram.Kun, Hexagram.DarkeningOfTheLight.Upper);
        Assert.Equal(Trigram.Li, Hexagram.DarkeningOfTheLight.Lower);
        Assert.Equal(Trigram.Kan, Hexagram.DarkeningOfTheLight.Palace);
        Assert.Equal("Darkening of the Light", Hexagram.DarkeningOfTheLight.ToString(En));
    }

    [Fact]
    public void Hexagram_TheArmy_ShouldHaveCorrectProperties()
    {
        // Assert - 师卦 (坤上坎下)
        Assert.Equal(Trigram.Kun, Hexagram.TheArmy.Upper);
        Assert.Equal(Trigram.Kan, Hexagram.TheArmy.Lower);
        Assert.Equal(Trigram.Kan, Hexagram.TheArmy.Palace);
        Assert.Equal("The Army", Hexagram.TheArmy.ToString(En));
    }

    #endregion

    #region 艮宫测试

    [Fact]
    public void Hexagram_KeepingStill_ShouldHaveCorrectProperties()
    {
        // Assert - 艮卦 (艮上艮下)
        Assert.Equal(Trigram.Gen, Hexagram.KeepingStill.Upper);
        Assert.Equal(Trigram.Gen, Hexagram.KeepingStill.Lower);
        Assert.Equal(Trigram.Gen, Hexagram.KeepingStill.Palace);
        Assert.Equal("Keeping Still", Hexagram.KeepingStill.ToString(En));
    }

    [Fact]
    public void Hexagram_Grace_ShouldHaveCorrectProperties()
    {
        // Assert - 贲卦 (艮上离下)
        Assert.Equal(Trigram.Gen, Hexagram.Grace.Upper);
        Assert.Equal(Trigram.Li, Hexagram.Grace.Lower);
        Assert.Equal(Trigram.Gen, Hexagram.Grace.Palace);
        Assert.Equal("Grace", Hexagram.Grace.ToString(En));
    }

    [Fact]
    public void Hexagram_TheTamingPowerOfTheGreat_ShouldHaveCorrectProperties()
    {
        // Assert - 大畜卦 (艮上乾下)
        Assert.Equal(Trigram.Gen, Hexagram.TheTamingPowerOfTheGreat.Upper);
        Assert.Equal(Trigram.Qian, Hexagram.TheTamingPowerOfTheGreat.Lower);
        Assert.Equal(Trigram.Gen, Hexagram.TheTamingPowerOfTheGreat.Palace);
        Assert.Equal("The Taming Power of the Great", Hexagram.TheTamingPowerOfTheGreat.ToString(En));
    }

    [Fact]
    public void Hexagram_Decrease_ShouldHaveCorrectProperties()
    {
        // Assert - 损卦 (艮上兑下)
        Assert.Equal(Trigram.Gen, Hexagram.Decrease.Upper);
        Assert.Equal(Trigram.Dui, Hexagram.Decrease.Lower);
        Assert.Equal(Trigram.Gen, Hexagram.Decrease.Palace);
        Assert.Equal("Decrease", Hexagram.Decrease.ToString(En));
    }

    [Fact]
    public void Hexagram_Opposition_ShouldHaveCorrectProperties()
    {
        // Assert - 睽卦 (离上兑下)
        Assert.Equal(Trigram.Li, Hexagram.Opposition.Upper);
        Assert.Equal(Trigram.Dui, Hexagram.Opposition.Lower);
        Assert.Equal(Trigram.Gen, Hexagram.Opposition.Palace);
        Assert.Equal("Opposition", Hexagram.Opposition.ToString(En));
    }

    [Fact]
    public void Hexagram_Treading_ShouldHaveCorrectProperties()
    {
        // Assert - 履卦 (乾上兑下)
        Assert.Equal(Trigram.Qian, Hexagram.Treading.Upper);
        Assert.Equal(Trigram.Dui, Hexagram.Treading.Lower);
        Assert.Equal(Trigram.Gen, Hexagram.Treading.Palace);
        Assert.Equal("Treading", Hexagram.Treading.ToString(En));
    }

    [Fact]
    public void Hexagram_InnerTruth_ShouldHaveCorrectProperties()
    {
        // Assert - 中孚卦 (巽上兑下)
        Assert.Equal(Trigram.Xun, Hexagram.InnerTruth.Upper);
        Assert.Equal(Trigram.Dui, Hexagram.InnerTruth.Lower);
        Assert.Equal(Trigram.Gen, Hexagram.InnerTruth.Palace);
        Assert.Equal("Inner Truth", Hexagram.InnerTruth.ToString(En));
    }

    [Fact]
    public void Hexagram_Development_ShouldHaveCorrectProperties()
    {
        // Assert - 渐卦 (巽上艮下)
        Assert.Equal(Trigram.Xun, Hexagram.Development.Upper);
        Assert.Equal(Trigram.Gen, Hexagram.Development.Lower);
        Assert.Equal(Trigram.Gen, Hexagram.Development.Palace);
        Assert.Equal("Development", Hexagram.Development.ToString(En));
    }

    #endregion

    #region 坤宫测试

    [Fact]
    public void Hexagram_TheReceptive_ShouldHaveCorrectProperties()
    {
        // Assert - 坤卦 (坤上坤下)
        Assert.Equal(Trigram.Kun, Hexagram.TheReceptive.Upper);
        Assert.Equal(Trigram.Kun, Hexagram.TheReceptive.Lower);
        Assert.Equal(Trigram.Kun, Hexagram.TheReceptive.Palace);
        Assert.Equal("The Receptive", Hexagram.TheReceptive.ToString(En));
    }

    [Fact]
    public void Hexagram_Return_ShouldHaveCorrectProperties()
    {
        // Assert - 复卦 (坤上震下)
        Assert.Equal(Trigram.Kun, Hexagram.Return.Upper);
        Assert.Equal(Trigram.Zhen, Hexagram.Return.Lower);
        Assert.Equal(Trigram.Kun, Hexagram.Return.Palace);
        Assert.Equal("Return", Hexagram.Return.ToString(En));
    }

    [Fact]
    public void Hexagram_Approach_ShouldHaveCorrectProperties()
    {
        // Assert - 临卦 (坤上兑下)
        Assert.Equal(Trigram.Kun, Hexagram.Approach.Upper);
        Assert.Equal(Trigram.Dui, Hexagram.Approach.Lower);
        Assert.Equal(Trigram.Kun, Hexagram.Approach.Palace);
        Assert.Equal("Approach", Hexagram.Approach.ToString(En));
    }

    [Fact]
    public void Hexagram_Peace_ShouldHaveCorrectProperties()
    {
        // Assert - 泰卦 (坤上乾下)
        Assert.Equal(Trigram.Kun, Hexagram.Peace.Upper);
        Assert.Equal(Trigram.Qian, Hexagram.Peace.Lower);
        Assert.Equal(Trigram.Kun, Hexagram.Peace.Palace);
        Assert.Equal("Peace", Hexagram.Peace.ToString(En));
    }

    [Fact]
    public void Hexagram_ThePowerOfTheGreat_ShouldHaveCorrectProperties()
    {
        // Assert - 大壮卦 (震上乾下)
        Assert.Equal(Trigram.Zhen, Hexagram.ThePowerOfTheGreat.Upper);
        Assert.Equal(Trigram.Qian, Hexagram.ThePowerOfTheGreat.Lower);
        Assert.Equal(Trigram.Kun, Hexagram.ThePowerOfTheGreat.Palace);
        Assert.Equal("The Power of the Great", Hexagram.ThePowerOfTheGreat.ToString(En));
    }

    [Fact]
    public void Hexagram_BreakThrough_ShouldHaveCorrectProperties()
    {
        // Assert - 夬卦 (兑上乾下)
        Assert.Equal(Trigram.Dui, Hexagram.BreakThrough.Upper);
        Assert.Equal(Trigram.Qian, Hexagram.BreakThrough.Lower);
        Assert.Equal(Trigram.Kun, Hexagram.BreakThrough.Palace);
        Assert.Equal("Break-Through", Hexagram.BreakThrough.ToString(En));
    }

    [Fact]
    public void Hexagram_Waiting_ShouldHaveCorrectProperties()
    {
        // Assert - 需卦 (坎上乾下)
        Assert.Equal(Trigram.Kan, Hexagram.Waiting.Upper);
        Assert.Equal(Trigram.Qian, Hexagram.Waiting.Lower);
        Assert.Equal(Trigram.Kun, Hexagram.Waiting.Palace);
        Assert.Equal("Waiting", Hexagram.Waiting.ToString(En));
    }

    [Fact]
    public void Hexagram_HoldingTogether_ShouldHaveCorrectProperties()
    {
        // Assert - 比卦 (坎上坤下)
        Assert.Equal(Trigram.Kan, Hexagram.HoldingTogether.Upper);
        Assert.Equal(Trigram.Kun, Hexagram.HoldingTogether.Lower);
        Assert.Equal(Trigram.Kun, Hexagram.HoldingTogether.Palace);
        Assert.Equal("Holding Together", Hexagram.HoldingTogether.ToString(En));
    }

    #endregion

    #region 通用方法测试

    [Fact]
    public void Hexagram_Value_ShouldBeCalculatedFromTrigrams()
    {
        // Act & Assert - Value = (Upper << 3) | Lower
        Assert.Equal((byte)(0b111 << 3 | 0b111), Hexagram.TheCreative.Value);
        Assert.Equal((byte)(0b111 << 3 | 0b110), Hexagram.ComingToMeet.Value);
        Assert.Equal((byte)(0b000 << 3 | 0b000), Hexagram.TheReceptive.Value);
    }

    [Fact]
    public void Hexagram_Create_ShouldReturnCorrectHexagram()
    {
        // Act
        var hexagram = Hexagram.Create(Trigram.Qian, Trigram.Xun);

        // Assert
        Assert.Equal(Trigram.Qian, hexagram.Upper);
        Assert.Equal(Trigram.Xun, hexagram.Lower);
        Assert.Equal(Hexagram.ComingToMeet, hexagram);
    }

    [Fact]
    public void Hexagram_Create_WithSameTrigrams_ShouldReturnSameHexagram()
    {
        // Act
        var hexagram1 = Hexagram.Create(Trigram.Qian, Trigram.Qian);
        var hexagram2 = Hexagram.Create(Trigram.Qian, Trigram.Qian);

        // Assert
        Assert.Equal(hexagram1, hexagram2);
        Assert.Same(hexagram1, hexagram2);
    }

    [Fact]
    public void Hexagram_Equals_ShouldWorkCorrectly()
    {
        // Arrange
        var creative1 = Hexagram.TheCreative;
        var creative2 = Hexagram.Create(Trigram.Qian, Trigram.Qian);
        var comingToMeet = Hexagram.ComingToMeet;

        // Assert
        Assert.Equal(creative1, creative2);
        Assert.NotEqual(creative1, comingToMeet);
        Assert.True(creative1 == creative2);
        Assert.True(creative1 != comingToMeet);
    }

    [Fact]
    public void Hexagram_Create_ShouldCalculateValueCorrectly()
    {
        // Arrange
        var testData = new (Trigram Upper, Trigram Lower, byte ExpectedValue)[]
        {
            (Trigram.Qian, Trigram.Qian, (byte)(0b111 << 3 | 0b111)),
            (Trigram.Qian, Trigram.Xun, (byte)(0b111 << 3 | 0b110)),
            (Trigram.Kun, Trigram.Kun, (byte)(0b000 << 3 | 0b000))
        };

        // Act & Assert
        foreach (var (upper, lower, expectedValue) in testData)
        {
            var hexagram = Hexagram.Create(upper, lower);
            Assert.Equal(expectedValue, hexagram.Value);
        }
    }

    [Fact]
    public void Hexagram_GetAll_ShouldReturn64Hexagrams()
    {
        // Act
        var allHexagrams = Hexagram.GetAll().ToList();

        // Assert
        Assert.Equal(64, allHexagrams.Count);
    }

    [Fact]
    public void Hexagram_FromValue_ShouldReturnCorrectHexagram()
    {
        // Act & Assert
        Assert.Equal(Hexagram.TheCreative, Hexagram.FromValue((byte)(0b111 << 3 | 0b111)));
        Assert.Equal(Hexagram.ComingToMeet, Hexagram.FromValue((byte)(0b111 << 3 | 0b110)));
        Assert.Equal(Hexagram.TheReceptive, Hexagram.FromValue((byte)(0b000 << 3 | 0b000)));
    }

    [Fact]
    public void Hexagram_AllValues_ShouldBeUnique()
    {
        // Act
        var allHexagrams = Hexagram.GetAll();
        var values = allHexagrams.Select(h => h.Value).ToList();

        // Assert
        Assert.Equal(64, values.Distinct().Count());
    }

    #endregion
}
