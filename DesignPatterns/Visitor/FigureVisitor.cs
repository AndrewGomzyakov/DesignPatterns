using System;

namespace Visitor
{
    public abstract class Figure
    {
        public abstract string Name { get; }
        public abstract void Accept(IVisitor visitor);
    }

    public class Rectangle : Figure
    {
        public override string Name => "Rectangle";
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }

        public double Width => 10;
        public double Height => 13;
    }

    public class Triangle : Figure
    {
        public override string Name => "Triangle";
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public double SideA => 10;
        public double SibeB => 8;
        public double SideC => 6;
    }

    public class Circle : Figure
    {
        public override string Name => "Circle";
        public override void Accept(IVisitor visitor)
        {
            visitor.Visit(this);
        }
        public double Radius => 10;
    }
    
    public interface IVisitor
    {
        string OperationName { get; }
        void Visit(Circle circle);
        void Visit(Triangle triangle);
        void Visit(Rectangle rectangle);
    }

    public class DrawVisitor : IVisitor
    {
        public string OperationName => "Draw";
        public double x { get; set; }
        public double y { get; set; }
        public void Visit(Circle circle)
        {
            Console.WriteLine($"Draw circle with center in {x}:{y}");
        }

        public void Visit(Triangle triangle)
        {
            Console.WriteLine($"Draw triangle with center in {x}:{y}");
        }

        public void Visit(Rectangle rectangle)
        {
            Console.WriteLine($"Draw rectangle with center in {x}:{y}");
        }
    }

    public class AreaVisitor : IVisitor
    {
        public string OperationName => "Area";
        public void Visit(Circle circle)
        {
            Console.WriteLine(Math.PI * circle.Radius * circle.Radius);
        }

        public void Visit(Triangle triangle)
        {
            var p = triangle.SibeB + triangle.SideA + triangle.SideC;
            var s = Math.Sqrt(p * (p - triangle.SibeB) * (p - triangle.SideA) * (p - triangle.SideC));
            Console.WriteLine(s);
        }

        public void Visit(Rectangle rectangle)
        {
            Console.WriteLine(rectangle.Width * rectangle.Height);
        }
    }

    public class PerimeterVisitor : IVisitor
    {
        public string OperationName => "Perimeter";
        public void Visit(Circle circle)
        {
            Console.WriteLine(2 * Math.PI * circle.Radius);
        }

        public void Visit(Triangle triangle)
        {
            Console.WriteLine(triangle.SideA + triangle.SibeB + triangle.SideC);
        }

        public void Visit(Rectangle rectangle)
        {
            Console.WriteLine(2*(rectangle.Width + rectangle.Height));
        }
    }
}