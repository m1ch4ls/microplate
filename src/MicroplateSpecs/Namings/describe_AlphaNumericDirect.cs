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
    class describe_AlphaNumericDirect : nspec
    {
        private IPositionNaming naming;
        private Format format;

        void before_each()
        {
            naming = new AlphaNumericDirect();
            format = new Format(500, 500, naming);
        }

        void given_position_name()
        {
            it["'A3' should correspond x=0 and y=2"] = () => naming.GetCoords("A3", format).should_be(new Point(0, 2));
            it["'B5' should correspond x=1 and y=4"] = () => naming.GetCoords("B5", format).should_be(new Point(1, 4));
            it["'b6' should correspond x=1 and y=4"] = () => naming.GetCoords("B6", format).should_be(new Point(1, 5));

            it["'AA ' should not fail"] = () => naming.GetCoords("AA ", format).should_be(new Point(0, 0));
            it["'AA 321 456' should not fail"] = () => naming.GetCoords("AA 321 456", format).should_be(new Point(0, 0));
            it["'blbakd bllba' should not fail"] = () => naming.GetCoords("blbakd bllba", format).should_be(new Point(0, 0));
            it["'A.3' should not fail"] = () => naming.GetCoords("A. 3", format).should_be(new Point(0, 0));
        }
    }
}
