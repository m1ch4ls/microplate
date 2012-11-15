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
    class describe_AlphaNumericCoords : nspec
    {
        private IPositionNaming naming;
        private Format format;

        void before_each()
        {
            naming = new AlphaNumericCoords();
            format = new Format(500, 500, naming);
        }

        void when_naming_position()
        {
            it["naming should be defined"] = () => naming.should_not_be_null();
            it["format should be defined"] = () => format.should_not_be_null();
            it["row 0 should be A"] = () => naming.GetRowName(0, format).should_be("A");
            it["column 0 should be 1"] = () => naming.GetColName(0, format).should_be("1");
            it["row 25 should be Z"] = () => naming.GetRowName(25, format).should_be("Z");
            it["row 26 should be AA"] = () => naming.GetRowName(26, format).should_be("AA");
            it["row 224 should be HQ"] = () => naming.GetRowName(224, format).should_be("HQ");
            it["column 413 should be 414"] = () => naming.GetColName(413, format).should_be("414");

            context["given number greater than format size"] = () =>
            {
                it["row 1231 should be 499"] = () => naming.GetRowName(1231, format).should_be("SE");
                it["col 1231 should be 499"] = () => naming.GetColName(1231, format).should_be("499");
            };

            it["row -1 should be A"] = () => naming.GetRowName(-1, format).should_be("A");
            it["row -123 should be A"] = () => naming.GetRowName(-123, format).should_be("A");
            it["column -1 should be 1"] = () => naming.GetColName(-1, format).should_be("1");
            it["column -413 should be 1"] = () => naming.GetColName(-413, format).should_be("1");
        }

        void given_position_name()
        {
            it["'A,3' should correspond x=0 and y=2"] = () => naming.GetCoords("A,3", format).should_be(new Point(0, 2));
            it["'B, 5' should correspond x=1 and y=4"] = () => naming.GetCoords("B, 5", format).should_be(new Point(1, 4));
            it["'b 5' should correspond x=1 and y=4"] = () => naming.GetCoords("B 5", format).should_be(new Point(1, 4));

            it["'AA ' should not fail"] = () => naming.GetCoords("AA ", format).should_be(new Point(0, 0));
            it["'AA 321 456' should not fail"] = () => naming.GetCoords("AA 321 456", format).should_be(new Point(0, 0));
            it["'blbakd bllba' should not fail"] = () => naming.GetCoords("blbakd bllba", format).should_be(new Point(0, 0));
            it["'A. 3' should not fail"] = () => naming.GetCoords("A. 3", format).should_be(new Point(0, 0));
        }
    }
}
