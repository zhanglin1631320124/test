namespace SocoShop.Entity
{
    using SkyCES.EntLib;
    using System;

    public enum InputType
    {
        [Enum("多选")]
        CheckBox = 6,
        [Enum("关键字")]
        KeyWord = 3,
        [Enum("单选")]
        Radio = 5,
        [Enum("下拉选择")]
        Select = 4,
        [Enum("单行文本")]
        Text = 1,
        [Enum("多行文本")]
        Textarea = 2
    }
}

