using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Microplate;
using Microplate.Namings;
using NSpec;

namespace MicroplateSpecs.Namings
{
    class describe_NumericCoords : nspec
    {
        private IPositionNaming naming;

        void before_each()
        {
            naming = new NumericCoords();
        }

        void when_naming_position()
        {
            it["naming should be defined"] = () => naming.should_not_be_null();
            it["row 0 should be 1"] = () => naming.GetRowName(0).should_be("1");
            it["column 0 should be 1"] = () => naming.GetColName(0).should_be("1");
            it["row 224 should be 225"] = () => naming.GetRowName(224).should_be("225");
            it["column 413 should be 414"] = () => naming.GetColName(413).should_be("414");

            it["row -1 should be 1"] = () => naming.GetRowName(-1).should_be("1");
            it["row -123 should be 1"] = () => naming.GetRowName(-123).should_be("1");
            it["column -1 should be 1"] = () => naming.GetColName(-1).should_be("1");
            it["column -413 should be 1"] = () => naming.GetColName(-413).should_be("1");
        }

        void given_position_name()
        {
            it["'1,3' should correspond x=0 and y=2"] = () => naming.GetCoords("1,3").should_be(new Point(0,2));
            it["'2, 5' should correspond x=1 and y=4"] = () => naming.GetCoords("2, 5").should_be(new Point(1, 4));
            it["'2 5' should correspond x=1 and y=4"] = () => naming.GetCoords("2 5").should_be(new Point(1, 4));

            it["'123 ' should not fail"] = () => naming.GetCoords("123 ").should_be(new Point(0, 0));
            it["'123 321 456' should not fail"] = () => naming.GetCoords("123 321 456").should_be(new Point(0, 0));
            it["'blbakd bllba' should not fail"] = () => naming.GetCoords("blbakd bllba").should_be(new Point(0, 0));
            it["'1. 3' should not fail"] = () => naming.GetCoords("1. 3").should_be(new Point(0, 0));
        }
    }
}
