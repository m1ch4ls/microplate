using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NSpec;

namespace MicroplateSpecs
{
    public class describe_world : nspec
    {
        void given_the_world_has_not_come_to_an_end()
        {
            it["Hello World should be Hello World"] = () => "Hello World".should_be("Hello World");
        }
    }
}
