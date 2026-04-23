using SkiaSharp;
using System.Collections.Generic;

namespace TekstilDoktoru.Models
{
    public class PatternPiece
    {
        public string Name { get; set; }
        public List<SKPoint> Points { get; set; } = new();
    }
}