﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Windows.Forms;
using Microplate;
using Microplate.Types;
using Moq;
using NSpec;

namespace MicroplateSpecs
{
    class describe_Plate : nspec
    {
        Plate plate;

        class SomeData : IData
        {
            public void GetObjectData(SerializationInfo info, StreamingContext context)
            {
                throw new NotImplementedException();
            }

            public Image ToImage()
            {
                throw new NotImplementedException();
            }

            public Control ToControl()
            {
                throw new NotImplementedException();
            }
        }

        class Dummy
        {
             
        }

        void when_initializing_new_plate_with_a_valid_type()
        {
            var typeMock = new Mock<IPlateType>();
            typeMock.SetupGet(format => format.Format).Returns(new Format(8, 12, PositionNamings.Default));
            it["type should have valid format"] = () => typeMock.Object.Format.IsValid().should_be_true();
            before = () => plate = new Plate(typeMock.Object, typeof(SomeData));            
            it["plate should exists"] = () => plate.should_not_be_null();
            it["contains the IPlateType"] = () => plate.Type.should_not_be_null();
            it["format exists"] = () => plate.Type.Format.should_not_be_null();
            it["width should be 12"] = () => plate.Width.should_be(12);
            it["height should be 8"] = () => plate.Height.should_be(8);
        }


        private Mock<IPlateType> failMock;

        void when_the_parameters_are_wrong()
        {
            before = () => failMock = new Mock<IPlateType>();

            it["given null type"] = expect<ArgumentException>(() => new Plate(null, typeof (SomeData)));

            context["given invalid format"] = () =>
            {
                act = () => failMock.SetupGet(format => format.Format).Returns(new Format(0, 0, PositionNamings.Default));
                it["should complain on format"] = expect<ArgumentException>(() => new Plate(failMock.Object, typeof(SomeData)));
            };

            context["given invalid data type"] = () =>
            {
                act = () => failMock.SetupGet(format => format.Format).Returns(new Format(1, 1, PositionNamings.Default));
                it["should complain on data type"] = expect<ArgumentException>(() => new Plate(failMock.Object, typeof(Dummy)));
            };
        }
    }
}