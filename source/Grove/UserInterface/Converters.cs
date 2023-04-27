﻿namespace Grove.UserInterface
{
  using System;
  using System.Collections.Generic;
  using System.Globalization;
  using System.IO;
  using System.Linq;
  using System.Windows;
  using System.Windows.Data;
  using System.Windows.Media;
  using Infrastructure;
  using Media;

  public static class Converters
  {
    public static AutoPassToImageConverter AutoPassToImage = new AutoPassToImageConverter();
    public static BooleanToVisibilityConverter BooleanToVisibility = new BooleanToVisibilityConverter();
    public static CardColorToCardTemplateConverter CardColorToCardTemplate = new CardColorToCardTemplateConverter();
    public static CardNameToCardImageConverter CardIllustrationNameToCardImage = new CardNameToCardImageConverter();
    public static CharacterCountToFontSizeConverter CharacterCountToFontSize = new CharacterCountToFontSizeConverter();
    public static LifeToColorConverter LifeToColor = new LifeToColorConverter();
    public static AvatarIdToAvatarImageConverter AvatarIdToAvatarImage = new AvatarIdToAvatarImageConverter();
    public static ManaCostToManaSymbolImagesConverter ManaCostToManaSymbolImages = new ManaCostToManaSymbolImagesConverter();
    public static SetAndRarityToSetImageConverter SetAndRaritytoSetImage = new SetAndRarityToSetImageConverter();
    public static ManaSymbolListToImagesConverter ManaSymbolListToImages = new ManaSymbolListToImagesConverter();
    public static MarkerBrushConverter MarkerBrush = new MarkerBrushConverter();
    public static NullToCollapsedConverter NullToCollapsed = new NullToCollapsedConverter();
    public static ZeroToCollapsedConverter ZeroToCollapsed = new ZeroToCollapsedConverter();
    public static NonZeroToCollapsedConverter NonZeroToCollapsed = new NonZeroToCollapsedConverter();
    public static RatingConverter Rating = new RatingConverter();
    public static BaseStrengthChangeToColorConverter BaseStrengthChangeToColor = new BaseStrengthChangeToColorConverter();

    public class AutoPassToImageConverter : IValueConverter
    {
      private readonly Dictionary<Pass, string> _imageNames = new Dictionary<Pass, string>
        {
          {Pass.Always, "circle-transparent.png"},
          {Pass.Active, "circle-yellow.png"},
          {Pass.Passive, "circle-green.png"},
          {Pass.Never, "circle-red.png"},
        };

      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
        var pass = (Pass)value;
        return MediaLibrary.GetImage(_imageNames[pass]);
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
        throw new NotImplementedException();
      }
    }

    public class AvatarIdToAvatarImageConverter : IValueConverter
    {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
        var id = (int)value;
        return MediaLibrary.GetAvatar(id);
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
        throw new NotImplementedException();
      }
    }

    public class BooleanToVisibilityConverter : IValueConverter
    {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
        var invert = false;

        if (parameter != null)
        {
          invert = Boolean.Parse(parameter.ToString());
        }

        var booleanValue = (bool)value;

        return ((booleanValue && !invert) || (!booleanValue && invert))
          ? Visibility.Visible
          : Visibility.Collapsed;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
        throw new NotImplementedException();
      }
    }

    public class CardColorToCardTemplateConverter : IValueConverter
    {
      public const string Artifact = "artifact-card";
      public const string Black = "black-card";
      public const string Blue = "blue-card";
      public const string Green = "green-card";
      public const string Land = "land-card";
      public const string Multi = "multi-card";
      public const string Red = "red-card";
      public const string White = "white-card";

      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
        return MediaLibrary.GetImage(
          GetTemplateName((CardColor[])value) + ".png");
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
        throw new NotImplementedException();
      }

      public string GetTemplateName(CardColor[] colors)
      {
        if (colors.Length > 1)
          return Multi;

        var color = colors[0];

        if (color == CardColor.White)
          return White;

        if (color == CardColor.Blue)
          return Blue;

        if (color == CardColor.Black)
          return Black;

        if (color == CardColor.Red)
          return Red;

        if (color == CardColor.Green)
          return Green;

        if (color == CardColor.Colorless)
          return Artifact;

        return Land;
      }
    }

    public class CardNameToCardImageConverter : IValueConverter
    {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
        var cardName = (string)value;
        return MediaLibrary.GetCardImage(GetCardImageName(cardName));
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
        throw new NotImplementedException();
      }

      public string GetCardImageName(string cardName)
      {
        var invalidChars = Path.GetInvalidFileNameChars();

        var invalidCharsRemoved = cardName
          .Where(x => !invalidChars.Contains(x))
          .ToArray();

        return new string(invalidCharsRemoved)
          .Trim()
          .ToLower();
      }
    }

    public class CharacterCountToFontSizeConverter : IValueConverter
    {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
        var characterCount = (int)value;

        if (characterCount < 40)
          return 18;

        if (characterCount < 60)
          return 17;

        if (characterCount < 90)
          return 16;

        if (characterCount < 120)
          return 15;

        if (characterCount < 160)
          return 14;

        if (characterCount < 220)
          return 13;

        if (characterCount < 260)
          return 12;

        if (characterCount < 300)
          return 11;

        return 10;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
        throw new NotImplementedException();
      }
    }

    public class LifeToColorConverter : IValueConverter
    {
      private readonly SolidColorBrush _highLife = new SolidColorBrush(Color.FromArgb(0xff, 0xb1, 0xff, 0x00));
      //#ffb1FF00

      private readonly SolidColorBrush _lowLife = new SolidColorBrush(Color.FromArgb(0xff, 0xe3, 0x00, 0x00));
      // #ffE30000

      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
        var life = (int)value;

        return life > 9 ? _highLife : _lowLife;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
        throw new NotImplementedException();
      }
    }

    public class ManaCostToManaSymbolImagesConverter : IValueConverter
    {
      private static readonly Rel[] Map = new[]
        {
          new Rel {Color = c => c.IsWhite, Symbol = "w"},
          new Rel {Color = c => c.IsBlue, Symbol = "u"},
          new Rel {Color = c => c.IsBlack, Symbol = "b"},
          new Rel {Color = c => c.IsRed, Symbol = "r"},
          new Rel {Color = c => c.IsGreen, Symbol = "g"},
        };

      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
        var manaAmount = (ManaAmount)value;

        if (manaAmount == null)
          return new ImageSource[] { };

        if (manaAmount.Converted == 0)
          return new[] { MediaLibrary.GetImage("0.png") };

        var images = new List<string>();

        int? colorless = null;
        foreach (var single in manaAmount)
        {
          if (single.Color.IsColorless)
          {
            colorless = single.Count;
            continue;
          }

          var symbol = Map.First(x => x.Color(single.Color));

          for (var i = 0; i < single.Count; i++)
          {
            var symbolValue = single.Color.IsPhyrexian
              ? symbol.Symbol + "p"
              : symbol.Symbol;
            images.Add(symbolValue);
          }
        }

        if (colorless != null)
          images.Insert(0, colorless.ToString());

        return images.Select(x => MediaLibrary.GetImage(x + ".png"));
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
        throw new NotImplementedException();
      }

      private class Rel
      {
        public Func<ManaColor, bool> Color;
        public string Symbol;
      }
    }

    public class ManaSymbolListToImagesConverter : IValueConverter
    {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
        var symbols = (IEnumerable<string>)value;

        if (symbols.None())
          return new ImageSource[] { };

        return symbols.Select(symbolName => MediaLibrary.GetImage(symbolName + ".png"));
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
        throw new NotImplementedException();
      }
    }

    public class MarkerBrushConverter : IValueConverter
    {
      private const float GoldenRatioConjugate = 0.618033988749895f;
      private readonly Dictionary<int, int> _idToMarker = new Dictionary<int, int>();
      private int _nextMarker = 1;
      private readonly SolidColorBrush _default = new SolidColorBrush(Color.FromArgb(0x00, 0x00, 0x00, 0x00));

      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
        var state = (int)value;

        if (state == 0)
          return _default;

        int marker;
        if (!_idToMarker.TryGetValue(state, out marker))
        {
          marker = _nextMarker++;
          _idToMarker.Add(state, marker);
        }

        var h = (GoldenRatioConjugate * marker) % 1;
        var rgb = ColorUtils.HsvToRgb(h, 0.7f, 0.95f);

        return new SolidColorBrush(Color.FromArgb(0xcc, rgb[0], rgb[1], rgb[2]));
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
        throw new NotImplementedException();
      }
    }

    public class RatingConverter : IValueConverter
    {
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
        var rating = (int)value;

        var stars = new List<ImageSource>();

        for (var i = 0; i < rating; i++)
        {
          stars.Add(MediaLibrary.GetImage("star.png"));
        }

        return stars;
      }

      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
        throw new NotImplementedException();
      }
    }

    public class SetAndRarityToSetImageConverter : IMultiValueConverter
    {
      public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
      {
        if (values.Any(x => DependencyProperty.UnsetValue.Equals(x)))
          return DependencyProperty.UnsetValue;

        var set = (string)values[0];
        var rarity = (Rarity?)values[1];

        return MediaLibrary.GetSetImage(set, rarity);
      }

      public object[] ConvertBack(object values, Type[] targetType, object parameter, CultureInfo culture)
      {
        throw new NotImplementedException();
      }
    }
  }

  public class BaseStrengthChangeToColorConverter : IMultiValueConverter
  {
    private readonly SolidColorBrush _noChange = new SolidColorBrush(Color.FromArgb(0xff, 0xff, 0xff, 0xff));
    private readonly SolidColorBrush _decrease = new SolidColorBrush(Color.FromArgb(0xff, 0xe3, 0x00, 0x00));
    private readonly SolidColorBrush _increase = new SolidColorBrush(Color.FromArgb(0xff, 0xb1, 0xff, 0x00));

    public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
      if (values.Any(x => x == null || DependencyProperty.UnsetValue.Equals(x)))
        return _noChange;

      var baseStrength = (int)values[0];
      var currentStrength = (int)values[1];

      if (baseStrength == currentStrength)
        return _noChange;

      if (baseStrength > currentStrength)
        return _decrease;

      return _increase;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }

  public class ZeroToCollapsedConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return value.Equals(0) ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }

  public class NonZeroToCollapsedConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return value.Equals(0) ? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }

  public class NullToCollapsedConverter : IValueConverter
  {
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
      return (value == null || value.Equals(String.Empty)) ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
      throw new NotImplementedException();
    }
  }
}