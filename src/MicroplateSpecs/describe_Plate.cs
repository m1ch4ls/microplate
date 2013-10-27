using System;
using System.Drawing;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
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

        void when_initializing_new_plate_with_a_valid_type()
        {
            var typeMock = new Mock<IPlateType>();
            typeMock.SetupGet(format => format.Format).Returns(new Format(8, 12, PositionNamings.Default));
            it["type should have valid format"] = () => typeMock.Object.Format.IsValid().should_be_true();
            before = () => plate = new Plate(typeMock.Object);            
            it["plate should exists"] = () => plate.should_not_be_null();
            it["contains the IPlateType"] = () => plate.Type.should_not_be_null();
            it["format exists"] = () => plate.Type.Format.should_not_be_null();
            it["width should be 12"] = () => plate.Width.should_be(12);
            it["height should be 8"] = () => plate.Height.should_be(8);
        }

        void given_valid_plate_object()
        {
            var typeMock = new Mock<IPlateType>();
            typeMock.SetupGet(format => format.Format).Returns(new Format(8, 12, PositionNamings.Default));
            
            context["when using copy constructor"] = () =>
            {
                before = () => plate = new Plate(typeMock.Object);
                it["should copy the plate with no problem"] = () => (new Plate(plate)).should_not_be_null();
                it["should not be the same"] = () => (new Plate(plate)).should_not_be_same(plate);
                it["copy should have the same Type"] = () => (new Plate(plate)).Type.should_be_same(plate.Type);
                //it["copy should have the same DataType"] = () => (new Plate(plate)).DataType.should_be_same(plate.DataType);
                it["copy should have the same Content"] = () => (new Plate(plate)).Content.should_be_same(plate.Content);
            };

            //context["when converting to typed plate"] = () =>
            //{
            //    before = () => plate = new Plate(typeMock.Object);
            //    it["should convert the plate if the type is correct"] = () => Plate.ToPlate<SomeData>(plate).should_not_be_null();
            //    it["should fail if type is wrong"] = expect<ArgumentException>(() => Plate.ToPlate<SomeOtherData>(plate));
            //    it["copy should have the same Type"] = () => (Plate.ToPlate<SomeData>(plate)).Type.should_be_same(plate.Type);
            //    it["copy should have the same DataType"] = () => (Plate.ToPlate<SomeData>(plate)).DataType.should_be_same(plate.DataType);
            //    it["copy should have the same Content"] = () => (Plate.ToPlate<SomeData>(plate)).Content.should_be_same(plate.Content);
            //};

            context["when serializing plate"] = () =>
            {
                // some real values
                before = () =>
                         {
                             plate = new Plate(new DefaultType());
                             plate[1].Value = "Serialize";
                         };
                var stream = new MemoryStream();
                var formatter = new BinaryFormatter();
                Plate copy = null;

                it["should serialize with no errors"] = () => formatter.Serialize(stream, plate);
                //it["should serialize even typed"] = () => 
                //{ 
                //    stream = new MemoryStream();
                //    formatter.Serialize(stream, typedPlate);
                //};
                it["should output something to the stream"] = () => stream.Length.should_be_greater_than(0);
                it["should deserialize with no errors"] = () =>
                {
                    stream.Position = 0;
                    copy = (Plate)formatter.Deserialize(stream);
                    copy.should_not_be_null();
                };
                it["should not be the same as copy"] = () => plate.should_not_be_same(copy);
                it["should have the same content as copy"] =
                    () => copy[1].Value.should_be(plate[1].Value);
            };

            context["when serializing to xml"] = () =>
            {
                before = () => plate = new Plate(new DefaultType());

                var stream = new MemoryStream();

                it["should save to xml with no errors"] = () => { plate.Save(stream); plate.Save("tmp.xml"); };
                it["should produce nonempty stream"] = () => stream.Length.should_be_greater_than(0);
                it["should deserialize from xml with no errors"] = () =>
                                                                   {
                                                                       stream.Position = 0;
                                                                       Plate.FromStream(stream).should_not_be_null();
                                                                   };
            };
        }

        private Mock<IPlateType> failMock;

        void when_the_parameters_are_wrong()
        {
            before = () => failMock = new Mock<IPlateType>();

            it["given null type"] = expect<ArgumentException>(() => new Plate(null));

            context["given invalid format"] = () =>
            {
                act = () => failMock.SetupGet(format => format.Format).Returns(new Format(0, 0, PositionNamings.Default));
                it["should complain on format"] = expect<ArgumentException>(() => new Plate(failMock.Object));
            };

            //context["given invalid data type"] = () =>
            //{
            //    act = () => failMock.SetupGet(format => format.Format).Returns(new Format(1, 1, PositionNamings.Default));
            //    it["should complain on data type"] = expect<ArgumentException>(() => new Plate(failMock.Object, typeof(Dummy)));
            //};
        }
    }
}
