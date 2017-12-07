using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using TagsCloudVisualization.Infrastructure.IAlgorithm;

namespace TagsCloudVisualization.Infrastructure.Algotithms.CircleAlgorithm
{
    public class CircularCloudLayouter : IVizualizationAlgorithm
    {
        private readonly Spiral spiral;
        private readonly TagValidator validator;

        public CircularCloudLayouter(Point centre, Size imageSize)
        {
            spiral = new Spiral(centre, imageSize.Width, imageSize.Height);
            Rectangles = new List<Rectangle>();
            validator = new TagValidator(imageSize.Width, imageSize.Height);
        }

        private List<Rectangle> Rectangles { get; }

        public Rectangle PutNextRectangle(Size rectangleSize)
        {
            if (!validator.SizeIsCorrect(rectangleSize))
                throw new ArgumentException("rectangle width or height are incorrect");
            foreach (var point in spiral.GetNextPoint())
            {
                if (IsIntersectingWithRectangles(point, rectangleSize)) continue;
                var newRectangle = new Rectangle(
                    new Point(point.X - rectangleSize.Width / 2, point.Y - rectangleSize.Height / 2), rectangleSize);
                if (!validator.RectangleIsCorrect(newRectangle))
                    throw new TagValidatorException("It is impossible to arrange rectangle on spiral.\n" +
                                                    "Rectangle goes beyond the boundaries of the map");
                Rectangles.Add(newRectangle);
                return newRectangle;
            }
            throw new TagValidatorException("Can't get next rectangle");
        }

        private bool IsIntersectingWithRectangles(Point possibleCenterRectangle, Size size)
        {
            var point = new Point(possibleCenterRectangle.X - size.Width / 2, 
                possibleCenterRectangle.Y - size.Height / 2);
            var possibleRectangle = new Rectangle(point, size);
            return Rectangles.Any(r => r.IntersectsWith(possibleRectangle));
        }
    }
}