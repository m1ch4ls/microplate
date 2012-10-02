using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microplate.Design
{
    /// <summary>
    /// This is just a set of possible usage examples. It's not meant to be compiled or used for any purpose.
    /// </summary>
    class UsageExamples
    {
        /*
         * What do I want to do with the Plate?
         */

        private Example1()
        {
            var plate = new Plate();

            foreach (var cell in plate)
            {
                // do something for all cells in the plate
            }

            plate[123].ToString(); // access position by integer
            plate[12, 34].ToString(); // access by cords
            plate["A12"].ToString(); // alphanumeric access
            plate["A", 12].ToString(); // cords like alphanumeric access

            plate.ToString(); // I want to get formatted ASCII table
            plate.ToImage(); // a picture; TODO: think about format, something simple, 
                             // preferably windows compatible vector format
        }

        private constructors()
        {
            // too complicated
            var plate = new Plate(plateType, typeof(ContentClass)); // possible 5

            // can't use generic type for dynamic loading
            var plate = new Plate<ContentType>(plateType); // not possible
        }

        private foo()
        {
            // lets have a plate
            var plate = new Plate();

            plate.Serialize(); // think about serialization - binary or xml?
        }

        private Example2()
        {
            // TODO: think more about file format, file extension etc...
            Plate plate = Plate.FromFile(@"Some\file\somewhere\here.plt");

            Plate plate = Plate.FromFile(stream);

            // now I have a plate
            var type = plate.Type;
            Console.Out.WriteLine(type.Name);
        }
    }
}
