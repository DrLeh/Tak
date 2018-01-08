using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tak
{
    public class History
    {
        //move number determined by the index
        public List<HistoryRow> Rows { get; set; }

        public void TrackMove(Color color)
        {

        }

        public override string ToString()
        {
            var sb = new StringBuilder();

            var counter = 1;
            foreach (var row in Rows)
            {
                sb.Append(counter)
                    .Append(". ")
                    .Append(row.LightMove.Representation)
                    .Append("  ")
                    .Append(row.DarkMove.Representation);
                counter++;
            }

            return sb.ToString();
        }
    }

    public class HistoryRow
    {
        public HistoryItem LightMove { get; set; } = HistoryItem.Empty(Color.Light);
        public HistoryItem DarkMove { get; set; } = HistoryItem.Empty(Color.Dark);

        public bool IsComplete => !LightMove.IsEmpty && !DarkMove.IsEmpty;
    }

    public class HistoryItem
    {
        public Color Color { get; set; }
        public string Representation { get; set; } = "";

        public bool IsEmpty => string.IsNullOrWhiteSpace(Representation);

        public static HistoryItem Empty(Color c) => new HistoryItem { Color = c };
    }
}
