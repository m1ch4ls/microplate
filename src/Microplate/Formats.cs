using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microplate
{
    /// <summary>
    /// Predefined plate formats and factory methods.
    /// </summary>
    public static class Formats
    {
        /// <summary>
        /// Few defined standard formats: 96-well, 386-well,
        /// 
        /// NOTE: Position naming is set to <see cref="PositionNamings.Default"/>
        /// </summary>
        public static readonly Dictionary<string, Format> Defined = new Dictionary<string, Format>()
                                                              {
                                                                  {"96-well", new Format(12, 8, PositionNamings.Default)},
                                                                  {"384-well", new Format(24, 16, PositionNamings.Default)},
                                                              };
        public static Format SingleTubes(int n)
        {
            return new Format(n, 1, PositionNamings.Default);
        }
    }
}
