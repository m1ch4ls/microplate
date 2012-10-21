using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Microplate;
using Microplate.Namings;
using NSpec;

namespace MicroplateSpecs.Namings
{
    class describe_NumericDirect : nspec
    {
        private IPositionNaming naming;
        private Format format;

        void before_each()
        {
            naming = new NumericDirect();
            format = new Format(8, 12, naming);
        }

        void given_position_name()
        {
            it["'1' should correspond x=0 and y=0"] = () => naming.GetCoords("1", format).should_be(new Point(0, 0));
            it["'13' should correspond x=1 and y=0"] = () => naming.GetCoords("13", format).should_be(new Point(1, 0));
            it["'12' should correspond x=0 and y=11"] = () => naming.GetCoords("12", format).should_be(new Point(0, 11));
            it["'53' should correspond x=4 and y=4"] = () => naming.GetCoords("53", format).should_be(new Point(4, 4));
            it["'95' should correspond x=7 and y=10"] = () => naming.GetCoords("95", format).should_be(new Point(7, 10));

            it["'123 ' should not fail"] = () => naming.GetCoords("123 ", format).should_be(new Point(7, 11));
            it["'123 321 456' should not fail"] = () => naming.GetCoords("123 321 456", format).should_be(new Point(0, 0));
            it["'blbakd bllba' should not fail"] = () => naming.GetCoords("blbakd bllba", format).should_be(new Point(0, 0));
            it["'1.123' should not fail"] = () => naming.GetCoords("1.123", format).should_be(new Point(0, 0));
        }
    }
}
