using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphicsPackage1802731
{
    public partial class GrafPack : Form
    {

        //Variable Declarations
        Graphics g;
        Pen blackpen, red;

        private int sides = 0;
        private PointF mouseDownLocation, currentMouseLocation;
        private PointF boundRect1, boundRect2;
        private PointF offset;

        private Shape previewShape, selectedShape;
        private LinkedList<Shape> shapes;

        enum Operation
        {
            NoOperation,
            CreateCircle,
            CreateIsoceles,
            CreateRightAngled,
            CreatePolygon,
            CreateSquare,
            CreateRectangle,
            TranslateShape,
            RotateShape,
            ScaleShape,
            ReflectShapeX,
            ReflectShapeY,
            DeleteShape,
            TestCase
        }


        //Initialisation
        Operation currentOperation = Operation.NoOperation;

        public GrafPack()
        {
            initialiseForm();

            initialiseGraphics();

            initialiseMenu();

            shapes = new LinkedList<Shape>();
            
        }

        private void initialiseForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.White;
        }

        private void initialiseGraphics()
        {
            g = this.CreateGraphics();
            blackpen = new Pen(Color.Black);
            red = new Pen(Color.Red);
        }

        private void initialiseMenu()
        {
            // The following approach uses menu items coupled with mouse clicks
            MainMenu mainMenu = new MainMenu();
            MenuItem createItem = new MenuItem();
            MenuItem transformItem = new MenuItem();
            MenuItem translateItem = new MenuItem();
            MenuItem rotateItem = new MenuItem();
            MenuItem scaleItem = new MenuItem();
            MenuItem reflectItemX = new MenuItem();
            MenuItem reflectItemY = new MenuItem();
            MenuItem deleteItem = new MenuItem();
            MenuItem squareItem = new MenuItem();
            MenuItem rectangleItem = new MenuItem();
            MenuItem triangleItem = new MenuItem();

            MenuItem createRightAngled = new MenuItem();
            MenuItem createIsoceles = new MenuItem();
            MenuItem createEquilateral = new MenuItem();
            MenuItem createPolygon = new MenuItem();
            MenuItem createRegular = new MenuItem();
            MenuItem createIrregular = new MenuItem();
            MenuItem createCircle = new MenuItem();
            MenuItem createLine = new MenuItem();

            MenuItem createPentagon = new MenuItem();
            MenuItem createHexagon = new MenuItem();
            MenuItem createHeptagon = new MenuItem();
            MenuItem createOctagon = new MenuItem();
            MenuItem createNonagon = new MenuItem();
            MenuItem createDecagon = new MenuItem();


            MenuItem quitItem = new MenuItem();

            createItem.Text = "&Create";
            squareItem.Text = "&Square";
            rectangleItem.Text = "&Rectangle";
            triangleItem.Text = "&Triangle";
            transformItem.Text = "&Transform";
            translateItem.Text = "&Move";
            rotateItem.Text = "&Rotate";
            scaleItem.Text = "&Resisze";
            reflectItemX.Text = "&Mirror X";
            reflectItemY.Text = "&Mirror Y";
            deleteItem.Text = "&Delete";
            quitItem.Text = "&Quit";

            createCircle.Text = "&Circle";
            createRightAngled.Text = "&RightAngled";
            createIsoceles.Text = "&Isoceles";
            createEquilateral.Text = "&Equilateral";

            createPolygon.Text = "&Polygon";
            createPentagon.Text = "&Pentagon";
            createHexagon.Text = "&Hexagon";
            createHeptagon.Text = "&Heptagon";
            createOctagon.Text = "&Octagon";
            createNonagon.Text = "&Nonagon";
            createDecagon.Text = "&Decagon";


            mainMenu.MenuItems.Add(createItem);
            mainMenu.MenuItems.Add(transformItem);
            mainMenu.MenuItems.Add(deleteItem);
            mainMenu.MenuItems.Add(quitItem);

            createItem.MenuItems.Add(createCircle);
            createItem.MenuItems.Add(triangleItem);
            createItem.MenuItems.Add(squareItem);
            createItem.MenuItems.Add(rectangleItem);
            createItem.MenuItems.Add(createPolygon);

            triangleItem.MenuItems.Add(createEquilateral);
            triangleItem.MenuItems.Add(createIsoceles);
            triangleItem.MenuItems.Add(createRightAngled);

            createPolygon.MenuItems.Add(createPentagon);
            createPolygon.MenuItems.Add(createHexagon);
            createPolygon.MenuItems.Add(createHeptagon);
            createPolygon.MenuItems.Add(createOctagon);
            createPolygon.MenuItems.Add(createNonagon);
            createPolygon.MenuItems.Add(createDecagon);

            transformItem.MenuItems.Add(translateItem);
            transformItem.MenuItems.Add(rotateItem);
            transformItem.MenuItems.Add(scaleItem);
            transformItem.MenuItems.Add(reflectItemX);
            transformItem.MenuItems.Add(reflectItemY);

            createCircle.Click += new System.EventHandler(this.createCircle);
            squareItem.Click += new System.EventHandler(this.createSquare);
            rectangleItem.Click += new System.EventHandler(this.createRectangle);

            createEquilateral.Click += new System.EventHandler(this.createEquilateral);
            createRightAngled.Click += new System.EventHandler(this.createRightAngled);
            createIsoceles.Click += new System.EventHandler(this.createIsoceles);

            createPentagon.Click += new System.EventHandler(this.createPentagon);
            createHexagon.Click += new System.EventHandler(this.createHexagon);
            createHeptagon.Click += new System.EventHandler(this.createHeptagon);
            createOctagon.Click += new System.EventHandler(this.createOctagon);
            createNonagon.Click += new System.EventHandler(this.createNonagon);
            createDecagon.Click += new System.EventHandler(this.createDecagon);



            translateItem.Click += new System.EventHandler(this.translateItem);
            rotateItem.Click += new System.EventHandler(this.rotateItem);
            scaleItem.Click += new System.EventHandler(this.scaleItem);
            reflectItemX.Click += new System.EventHandler(this.reflectItemX);
            reflectItemY.Click += new System.EventHandler(this.reflectItemY);
            deleteItem.Click += new System.EventHandler(this.deleteItem);
            quitItem.Click += new System.EventHandler(this.quitItem);


            this.Menu = mainMenu;
            this.MouseMove += mouseMove;
            this.MouseUp += mouseUp;
            this.MouseDown += mouseDown;

        }

        //Event Listeners
        private void createCircle(object sender, EventArgs e)
        {
            currentOperation = Operation.CreateCircle;
            MessageBox.Show("Click and drag to create a circle");
        }
        private void createSquare(object sender, EventArgs e)
        {
            currentOperation = Operation.CreateSquare;
            MessageBox.Show("Click and drag to create a square");
        }
        private void createRectangle(object sender, EventArgs e)
        {
            currentOperation = Operation.CreateRectangle;
            MessageBox.Show("Click and drag to create a rectangle");
        }

        private void createRightAngled(object sender, EventArgs e)
        {
            MessageBox.Show("Click and drag to create a triangle");
            currentOperation = Operation.CreateRightAngled;
        }

        private void createIsoceles(object sender, EventArgs e)
        {
            MessageBox.Show("Click and drag to create a triangle");
            currentOperation = Operation.CreateIsoceles;
        }

        private void createEquilateral(object sender, EventArgs e)
        {
            MessageBox.Show("Click and drag to create a triangle");
            currentOperation = Operation.CreatePolygon;
            sides = 3;
        }

        private void createPentagon(object sender, EventArgs e)
        {
            MessageBox.Show("Click and drag to create a Pentagon");
            currentOperation = Operation.CreatePolygon;
            sides = 5;
        }

        private void createHexagon(object sender, EventArgs e)
        {
            MessageBox.Show("Click and drag to create a Hexagon");
            currentOperation = Operation.CreatePolygon;
            sides = 6;
        }

        private void createHeptagon(object sender, EventArgs e)
        {
            MessageBox.Show("Click and drag to create a Heptagon");
            currentOperation = Operation.CreatePolygon;
            sides = 7;
        }

        private void createOctagon(object sender, EventArgs e)
        {
            MessageBox.Show("Click and drag to create a Octagon");
            currentOperation = Operation.CreatePolygon;
            sides = 8;
        }

        private void createNonagon(object sender, EventArgs e)
        {
            MessageBox.Show("Click and drag to create a Nonagon");
            currentOperation = Operation.CreatePolygon;
            sides = 9;
        }

        private void createDecagon(object sender, EventArgs e)
        {
            MessageBox.Show("Click and drag to create a Decagon");
            currentOperation = Operation.CreatePolygon;
            sides = 10;
        }

        private void deleteItem(object sender, EventArgs e)
        {
            MessageBox.Show("Click corner of shape to delete");
            currentOperation = Operation.DeleteShape;
        }

        private void translateItem(object sender, EventArgs e)
        {
            MessageBox.Show("Click corner of shape to move");
            currentOperation = Operation.TranslateShape;
        }

        private void rotateItem(object sender, EventArgs e)
        {
            MessageBox.Show("Click corner of shape to rotate");
            currentOperation = Operation.RotateShape;
        }

        private void scaleItem(object sender, EventArgs e)
        {
            MessageBox.Show("Click corner of shape to resize");
            currentOperation = Operation.ScaleShape;
        }

        private void reflectItemX(object sender, EventArgs e)
        {
            MessageBox.Show("Click corner of shape to mirror in the X axis");
            currentOperation = Operation.ReflectShapeX;
        }

        private void reflectItemY(object sender, EventArgs e)
        {
            MessageBox.Show("Click corner of shape to mirror in the Y axis");
            currentOperation = Operation.ReflectShapeY;
        }

        private void quitItem(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }


        //Observers
        private void mouseDown(object sender, MouseEventArgs e)//Mouse Down Observer for initial mouse click operations
        {
            //Set first point of operation
            if (e.Button == MouseButtons.Left)
            {
                mouseDownLocation = new PointF(e.X, e.Y);
                if (currentOperation == Operation.TranslateShape || currentOperation == Operation.RotateShape || currentOperation == Operation.ScaleShape || currentOperation == Operation.ReflectShapeX || currentOperation == Operation.ReflectShapeY || currentOperation == Operation.DeleteShape)
                {
                    if(shapes.Count==0)
                    {
                        currentOperation = Operation.NoOperation;
                    }
                    else
                    {
                        selectedShape = SelectShape(mouseDownLocation);
                        selectedShape.selected = true;

                        offset = new PointF(mouseDownLocation.X - selectedShape.Position.X, mouseDownLocation.Y - selectedShape.Position.Y);
                    }

                }
            }
        }

        
        private void mouseUp(object sender, MouseEventArgs e)//Mouse Up observer for drawing and saving final mouse position of shape creation, manipulation and deletion operations.
        {
            PointF target, position;
            float scaleFactorX, scaleFactorY;
            double angle;

            //confirm mouse position as second point
            if (e.Button == MouseButtons.Left)
            {
                currentMouseLocation = new PointF(e.X, e.Y);

                switch (currentOperation)
                {
                    case Operation.CreateCircle:

                        previewShape = new regularPolygon(mouseDownLocation, currentMouseLocation, 100);
                        shapes.AddLast(previewShape);
                        break;

                    case Operation.CreateRightAngled:
                        previewShape = new TriangleRightAngled(mouseDownLocation, currentMouseLocation);
                        shapes.AddLast(previewShape);
                        break;

                    case Operation.CreateIsoceles:
                        previewShape = new TriangleIsoceles(mouseDownLocation, currentMouseLocation);
                        shapes.AddLast(previewShape);
                        break;

                    case Operation.CreatePolygon:
                        previewShape = new regularPolygon(mouseDownLocation, currentMouseLocation, sides);
                        shapes.AddLast(previewShape);
                        break;

                    case Operation.CreateSquare:

                        previewShape = new Square(mouseDownLocation, currentMouseLocation);
                        shapes.AddLast(previewShape);
                        break;

                    case Operation.CreateRectangle:
                        previewShape = new Rectangle(mouseDownLocation, currentMouseLocation);
                        shapes.AddLast(previewShape);
                        break;

                    case Operation.TranslateShape:
                        target = new PointF(currentMouseLocation.X - offset.X, currentMouseLocation.Y - offset.Y);

                        previewShape = selectedShape;
                        previewShape.translate(target);
                        previewShape.selected = false;
                        break;

                    case Operation.RotateShape:

                        //Calculate angle between 2 vectors
                        angle = Math.Atan2(currentMouseLocation.Y, currentMouseLocation.X) - Math.Atan2(mouseDownLocation.Y, mouseDownLocation.X);
                        angle = angle * 2;

                        previewShape = selectedShape;
                        position = previewShape.Position;
                        previewShape.rotate(angle, position);
                        previewShape.selected = false;
                        break;

                    case Operation.ScaleShape:
                        // Calculate scale factor
                        scaleFactorX = (currentMouseLocation.X / mouseDownLocation.X);
                        scaleFactorY = (currentMouseLocation.Y / mouseDownLocation.Y);

                        previewShape = selectedShape;
                        previewShape.scale(new PointF(scaleFactorX, scaleFactorY), selectedShape.Position);
                        previewShape.selected = false;
                        break;

                    case Operation.ReflectShapeX:
                        previewShape = selectedShape;
                        previewShape.reflect(1f, selectedShape.Position);
                        previewShape.selected = false;
                        break;

                    case Operation.ReflectShapeY:
                        previewShape = selectedShape;
                        previewShape.reflect(-1f, selectedShape.Position);
                        previewShape.selected = false;
                        break;

                    case Operation.DeleteShape:
                        shapes.Remove(selectedShape);

                        break;
                    default:
                    

                        break;
                }
                Draw();
                currentOperation = Operation.NoOperation;

            }
        }

        
        private void mouseMove(object sender, MouseEventArgs e)//Mouse move observer for drawing banding preview shapes
        {
            PointF target;
            double angle;
            float scaleFactorX, scaleFactorY;

            //preview operation release at current mouse position as second point
            if (e.Button == MouseButtons.Left)
            {
                currentMouseLocation = new PointF(e.X, e.Y);

                switch (currentOperation)
                {

                    case Operation.CreateCircle:

                        previewShape = new regularPolygon(mouseDownLocation, currentMouseLocation, 100);
                        Draw();
                        previewShape.draw(g, blackpen);
                        break;

                    case Operation.CreateRightAngled:
                        previewShape = new TriangleRightAngled(mouseDownLocation, currentMouseLocation);
                        Draw();
                        previewShape.draw(g, blackpen);
                        break;

                    case Operation.CreateIsoceles:
                        previewShape = new TriangleIsoceles(mouseDownLocation, currentMouseLocation);
                        Draw();
                        previewShape.draw(g, blackpen);
                        break;

                    case Operation.CreatePolygon:
                        previewShape = new regularPolygon(mouseDownLocation, currentMouseLocation, sides);
                        Draw();
                        previewShape.draw(g, blackpen);
                        break;

                    case Operation.CreateSquare:

                        previewShape = new Square(mouseDownLocation, currentMouseLocation);
                        Draw();
                        previewShape.draw(g, blackpen);
                        break;

                    case Operation.CreateRectangle:
                        previewShape = new Rectangle(mouseDownLocation, currentMouseLocation);
                        Draw();
                        previewShape.draw(g, blackpen);
                        break;

                    case Operation.TranslateShape:

                        target = new PointF(currentMouseLocation.X - offset.X, currentMouseLocation.Y - offset.Y);

                        previewShape = selectedShape.deepCopy();
                        Draw();
                        previewShape.translate(target);
                        previewShape.draw(g, blackpen);
                        break;
                    case Operation.RotateShape:

                        //Calculate angle between 2 vectors
                        angle = Math.Atan2(currentMouseLocation.Y, currentMouseLocation.X) - Math.Atan2(mouseDownLocation.Y, mouseDownLocation.X);
                        angle = angle * 2;

                        previewShape = selectedShape.deepCopy();
                        Draw();
                        previewShape.rotate(angle, selectedShape.Position);
                        previewShape.draw(g, blackpen);
                        break;

                    case Operation.ScaleShape:

                        //Calculate scale factor
                        scaleFactorX = (currentMouseLocation.X / mouseDownLocation.X);
                        scaleFactorY = (currentMouseLocation.Y / mouseDownLocation.Y);

                        previewShape = selectedShape.deepCopy();
                        Draw();
                        previewShape.scale(new PointF(scaleFactorX, scaleFactorY), selectedShape.Position);
                        previewShape.draw(g, blackpen);
                        break;

                    case Operation.ReflectShapeX:
                        previewShape = selectedShape.deepCopy();
                        Draw();
                        previewShape.reflect(1f, selectedShape.Position);
                        previewShape.draw(g, blackpen);
                        break;

                    case Operation.ReflectShapeY:
                        previewShape = selectedShape.deepCopy();
                        Draw();
                        previewShape.reflect(-1f, selectedShape.Position);
                        previewShape.draw(g, blackpen);
                        break;

                    default:

                        break;

                }
            }
        }

        //Internal Methods

        private void Draw()//Internal method for drawing all currently stored shapes to the screen
        {
            g.Clear(this.BackColor);

            foreach(Shape shape in shapes)
            {
                if (shape.selected)
                {
                    shape.draw(g, red);
                }
                else
                {
                    shape.draw(g, blackpen);
                }
                
            }
        }

        private Shape SelectShape(PointF point) //Internal method for returning selected shape from data structure, by finding closes vertex to clicked point
        {
            
            Shape selectedShape=shapes.First.Value;
            float currentDistance=getDistanceMagnitude(point, selectedShape.Points.First.Value);
            float checkDistance;


            foreach (Shape shape in shapes)
            {
                foreach(PointF shapePoint in shape.Points)
                {
                    checkDistance = getDistanceMagnitude(point, shapePoint);
                    if(currentDistance > checkDistance)
                    {
                        selectedShape = shape;
                        currentDistance = checkDistance;
                    }
                }
            }

            return selectedShape;
        }

        private float getDistanceMagnitude(PointF pointA, PointF pointB) //Internal method for getting unsigned distance magnitude between two points
        {
            float distance = (float)Math.Sqrt(Math.Pow( (pointB.X - pointA.X) , 2) + Math.Pow(pointB.Y - pointA.Y, 2));

            if (distance < 0)
            {
                distance = distance * -1;
            }

            return distance;
        }
    }

    #region Shapes
    abstract class Shape
    {
        protected PointF position;
        protected LinkedList<PointF> points;
        protected PointF[] boundRectangle;

        public PointF Position
        {
            get { return position; }
            set { position = value; }
        }

        public LinkedList<PointF> Points
        {
            get { return points; }
            set { points = value; }
        }

        public bool selected = false;

        protected void setBoundingRectangle(PointF keyPt, PointF oppPt) //Internal method for generating a bounding rectangle for a shape given 2 opposite points
        {

            //Add points to to bounding rectangle array denoted by keyPt = point1 and oppPt = point 3
            boundRectangle[0] = (keyPt);
            boundRectangle[1] = (new PointF(oppPt.X, keyPt.Y));
            boundRectangle[2] = (oppPt);
            boundRectangle[3] = (new PointF(keyPt.X, oppPt.Y));

            //Set position to be centre of rectangle
            position = new PointF((keyPt.X + oppPt.X) / 2, (keyPt.Y + oppPt.Y) / 2);
        }

        protected void setBoundingSquare(PointF keyPt, PointF oppPt) //Internal method for generating a bounding square for a shape given 2 opposite points
        {
            float dx, dy, d;

            //Constrain oppPt so the later bounding rectangle call is constrained to a square
            dx = oppPt.X - keyPt.X;
            dy = oppPt.Y - keyPt.Y;

            if (dx < dy)
            {
                //constrain y dimension
                d = dy - dx;
                oppPt.Y = oppPt.Y - d;
            }
            else
            {
                //constrain x dimension
                d = dx - dy;
                oppPt.X = oppPt.X - d;
            }

            setBoundingRectangle(keyPt, oppPt);

        }

        public Shape deepCopy()//Public method for creating a deep copy clone of the object
        {
            Shape copy = (Shape)this.MemberwiseClone();
            copy.position = this.position;
            copy.points = this.points;
            copy.boundRectangle = this.boundRectangle;
            copy.selected = false;
            return copy;
        }

        public void draw(Graphics g, Pen pen) //Public method for drawing lines between shapes' points.
        {
            PointF[] pointArray = points.ToArray();
            g.DrawLines(pen, pointArray);
            g.DrawLine(pen, pointArray[pointArray.Length - 1], pointArray[0]);

        }

        public void printPoints()
        {
            string str = "";
            foreach (PointF point in points)
            {
                str = str + "(" + point.X + ", " + point.Y + ") ";
            }
            MessageBox.Show(str);
        }

        public void translate(PointF target) //Translate Centre to target point
        {
            float X, Y;
            LinkedList<PointF> pointsCopy = new LinkedList<PointF>();

            X = target.X - position.X;
            Y = target.Y - position.Y;
            //MessageBox.Show("translating " + position.X + ", " + position.Y + " by " + X + ", " + Y);

            Matrix translationMatrix = new Matrix(new float[][] { new float[] { 1f, 0f, 0f }, new float[] { 0f, 1, 0f }, new float[] { X, Y, 1f } });

            //Translate Centre Point of shape
            Matrix positionMatrix = new Matrix(new float[3] { position.X, position.Y, 1 });
            positionMatrix = MatrixMultiplier.Multiply(positionMatrix, translationMatrix);

            this.Position = new PointF(positionMatrix.GetMatrix()[0][0], positionMatrix.GetMatrix()[0][1]);

            //MessageBox.Show("to " + position.X + ", " + position.Y);

            //Translate all Shape Points
            foreach (PointF point in points)
            {
                positionMatrix = new Matrix(new float[3] { point.X, point.Y, 1 });
                positionMatrix = MatrixMultiplier.Multiply(positionMatrix, translationMatrix);
                pointsCopy.AddLast(new PointF(positionMatrix.GetMatrix()[0][0], positionMatrix.GetMatrix()[0][1]));
            }
            this.Points = pointsCopy;


        }

        public void rotate(double target, PointF rotationPoint) //Rotate about clockwise about rotationPoint by target angle (radians)
        {
            LinkedList<PointF> pointsCopy = new LinkedList<PointF>();
            PointF[] pointsCopyArray = points.ToArray();
            Matrix positionMatrix = new Matrix(new float[3] { position.X, position.Y, 1 });
            Matrix rotationMatrix = new Matrix(new float[][] { new float[] { (float)Math.Cos(target), (float)Math.Sin(target), 0f }, new float[] { (float)Math.Sin(target) * (-1), (float)Math.Cos(target), 0f }, new float[] { 0f, 0f, 1f } });

            //translate shape so rotation point at origin
            pointsCopyArray = internalTranslate(pointsCopyArray, new PointF(rotationPoint.X * -1, rotationPoint.Y * -1));

            //Rotate all Shape Points
            foreach (PointF point in pointsCopyArray)
            {
                positionMatrix = new Matrix(new float[3] { point.X, point.Y, 1 });
                positionMatrix = MatrixMultiplier.Multiply(positionMatrix, rotationMatrix);
                pointsCopy.AddLast(new PointF(positionMatrix.GetMatrix()[0][0], positionMatrix.GetMatrix()[0][1]));
            }

            //reverse translation
            pointsCopyArray = pointsCopy.ToArray();
            pointsCopyArray = internalTranslate(pointsCopyArray, rotationPoint);
            pointsCopy = arrayToLinkedList(pointsCopyArray);

            this.Points = pointsCopy;

        }

        public void scale(PointF target, PointF scalePoint) //Scale shape by X and Y scale factor stored in PointF
        {
            LinkedList<PointF> pointsCopy = new LinkedList<PointF>();
            PointF[] pointsCopyArray = points.ToArray();
            Matrix scaleMatrix = new Matrix(new float[][] { new float[] { target.X, 0f, 0f }, new float[] { 0f, target.Y, 0f }, new float[] { 0f, 0f, 1f } });
            Matrix positionMatrix;

            //translate shape so scale point point at origin
            pointsCopyArray = internalTranslate(pointsCopyArray, new PointF(scalePoint.X * -1, scalePoint.Y * -1));

            //Multiply all Shape Points by scale factor
            foreach (PointF point in pointsCopyArray)
            {
                positionMatrix = new Matrix(new float[3] { point.X, point.Y, 1f });
                positionMatrix = MatrixMultiplier.Multiply(positionMatrix, scaleMatrix);
                pointsCopy.AddLast(new PointF(positionMatrix.GetMatrix()[0][0], positionMatrix.GetMatrix()[0][1]));
            }
            //reverse translation
            pointsCopyArray = pointsCopy.ToArray();
            pointsCopyArray = internalTranslate(pointsCopyArray, scalePoint);
            pointsCopy = arrayToLinkedList(pointsCopyArray);

            this.Points = pointsCopy;
        }

        public void reflect(float input, PointF reflectPoint)
        {
            LinkedList<PointF> pointsCopy = new LinkedList<PointF>();
            PointF[] pointsCopyArray = points.ToArray();
            Matrix reflectionMatrix = new Matrix(new float[][] { new float[] { -input, 0f, 0f }, new float[] { 0f, input, 0f }, new float[] { 0f, 0f, 1f } });
            Matrix positionMatrix;

            //translate shape so reflect point point at origin
            pointsCopyArray = internalTranslate(pointsCopyArray, new PointF(reflectPoint.X * -1, reflectPoint.Y * -1));

            //Reflect all Shape Points in X axis
            foreach (PointF point in pointsCopyArray)
            {
                positionMatrix = new Matrix(new float[3] { point.X, point.Y, 1 });
                positionMatrix = MatrixMultiplier.Multiply(positionMatrix, reflectionMatrix);
                pointsCopy.AddLast(new PointF(positionMatrix.GetMatrix()[0][0], positionMatrix.GetMatrix()[0][1]));
            }
            //reverse translation
            pointsCopyArray = pointsCopy.ToArray();
            pointsCopyArray = internalTranslate(pointsCopyArray, reflectPoint);
            pointsCopy = arrayToLinkedList(pointsCopyArray);

            this.Points = pointsCopy;
        }

        private PointF[] internalTranslate(PointF[] shapePoints, PointF translationVector)//Internal use translation method for use in internal translations as part of transformation steps
        {
            PointF[] copyPoints = new PointF[shapePoints.Length];
            int copyIndex = 0;
            Matrix outMatrix, pointMatrix;

            //declare translation matrix
            float[][] translationStructure = new float[3][]
                {
                new float[3]{1, 0, 0},
                new float[3]{0, 1, 0},
                new float[3]{translationVector.X, translationVector.Y, 1}
                };
            Matrix translationMatrix = new Matrix(translationStructure);

            foreach (PointF vertex in shapePoints)
            {
                pointMatrix = new Matrix(new float[3] { vertex.X, vertex.Y, 1 });

                //Apply translation matrix to point to get new point
                outMatrix = MatrixMultiplier.Multiply(pointMatrix, translationMatrix);

                copyPoints[copyIndex] = new PointF(outMatrix.GetMatrix()[0][0], outMatrix.GetMatrix()[0][1]);
                copyIndex++;
            }

            return copyPoints;
        }

        private LinkedList<PointF> arrayToLinkedList(PointF[] input) //Internal Method for converting a point array back to a linked list of points
        {
            LinkedList<PointF> output = new LinkedList<PointF>();
            foreach (PointF point in input)
            {
                output.AddLast(point);
            }
            return output;
        }

        public void RecalculateCentre() //Internal/public method that recalculates the origin point of a shape
        {
            float x = 0, y=0, n=0;

            foreach(PointF point in points)
            {
                x = x + point.X;
                y = y + point.Y;
            }
            position = new PointF(x / n, y / n);
            
        }

        

    }

    class Line : Shape
    {
        public Line(PointF pointA, PointF pointB)
        {
            points = new LinkedList<PointF>();
            points.AddLast(pointA);
            points.AddLast(pointB);
            RecalculateCentre();
        }

        public Line(PointF point)
        {
            points = new LinkedList<PointF>();
            points.AddLast(point);
        }

        public void addPoint(PointF point)
        {
            points.AddLast(point);
            RecalculateCentre();
        }

        new public void draw(Graphics g, Pen pen) //Public method for drawing lines between shapes' points.
        {
            PointF[] pointArray = points.ToArray();
            g.DrawLines(pen, pointArray);
        }
    }

    class IrregularPolygon : Shape
    {
        public IrregularPolygon(PointF pointA, PointF pointB)
        {
            points = new LinkedList<PointF>();
            points.AddLast(pointA);
            points.AddLast(pointB);
            RecalculateCentre();
        }

        public IrregularPolygon(PointF point)
        {
            points = new LinkedList<PointF>();
            points.AddLast(point);
        }

        public void addPoint(PointF point)
        {
            points.AddLast(point);
            RecalculateCentre();
        }
    }

    class Square : Shape
    {
        public Square(PointF keyPt, PointF oppPt)   // constructor
        {

            points = new LinkedList<PointF>();
            boundRectangle = new PointF[4];

            setBoundingSquare(keyPt, oppPt);

            foreach (PointF point in boundRectangle)
            {
                points.AddLast(point);
            }
        }
    }

    class Rectangle : Shape
    {

        public Rectangle(PointF keyPt, PointF oppPt) // constructor
        {
            points = new LinkedList<PointF>();
            boundRectangle = new PointF[4];

            setBoundingRectangle(keyPt, oppPt);

            foreach (PointF point in boundRectangle)
            {
                points.AddLast(point);
            }

        }

    }

    class TriangleRightAngled : Shape
    {
        public TriangleRightAngled(PointF keyPt, PointF oppPt) // constructor
        {
            points = new LinkedList<PointF>();
            boundRectangle = new PointF[4];

            setBoundingRectangle(keyPt, oppPt);

            Points.AddLast(boundRectangle[0]);
            Points.AddLast(boundRectangle[2]);
            Points.AddLast(boundRectangle[3]);
            RecalculateCentre();

        }
    }

    class TriangleIsoceles : Shape
    {
        public TriangleIsoceles(PointF keyPt, PointF oppPt) // constructor
        {
            points = new LinkedList<PointF>();
            boundRectangle = new PointF[4];

            setBoundingRectangle(keyPt, oppPt);

            Points.AddLast(MatrixMultiplier.GetMidpoint(boundRectangle[0], boundRectangle[1]));
            Points.AddLast(boundRectangle[2]);
            Points.AddLast(boundRectangle[3]);
            RecalculateCentre();

        }
    }

    class regularPolygon : Shape
    {
        public regularPolygon(PointF keyPt, PointF oppPt, int N)   // constructor, that produces regular N sided polygon defined in a square
        {

            points = new LinkedList<PointF>();
            boundRectangle = new PointF[4];
            Line line;

            setBoundingSquare(keyPt, oppPt);

            line = new Line(position, MatrixMultiplier.GetMidpoint(boundRectangle[0], boundRectangle[3]));
            Points.AddLast(line.Points.Last.Value);

            for (int i = 1; i < N; i++)
            {
                line.rotate((2 * Math.PI) / (N), position);
                Points.AddLast(line.Points.Last.Value);

            }

        }
    }
    #endregion

    #region Matrix Classes
    public class Matrix //data class for storing matrices and use in matrix operations, and other static math operations
    {
        private float[][] matrix;

        public Matrix(float[][] inputMatrix)
        {
            matrix = inputMatrix;
        }

        public Matrix(float[] inputMatrix)
        {
            matrix = new float[1][];
            matrix[0] = inputMatrix;
        }

        public float[][] GetMatrix()
        {
            return matrix;
        }

        public int GetRows()
        {
            return matrix.Length;
        }

        public int GetColumns()
        {
            return matrix[0].Length;
        }

        public void PrintMatrix()
        {
            Console.WriteLine("Printing Matrix:");

            for (int i = 0; i < this.GetRows(); i++)
            {
                Console.Write("| ");
                for (int j = 0; j < this.GetColumns(); j++)
                {

                    if (j == this.GetColumns() - 1)
                    {
                        Console.Write(matrix[i][j] + " ");
                    }
                    else
                    {
                        Console.Write(matrix[i][j] + ", ");
                    }

                }
                Console.WriteLine("|");
            }
        }
    }

    public static class MatrixMultiplier //static class for performing matrix multiplication operations
    {

        public static Matrix Multiply(Matrix inputMatrixA, Matrix inputMatrixB)
        {
            Matrix outputMatrix;
            float[][] multipliedMatrix = new float[inputMatrixA.GetRows()][];

            if (inputMatrixA.GetColumns() == inputMatrixB.GetRows())
            {
                //Valid Matrix multiplication
                for (int i = 0; i < inputMatrixA.GetRows(); i++)
                {
                    multipliedMatrix[i] = new float[inputMatrixB.GetColumns()];
                    for (int j = 0; j < inputMatrixB.GetColumns(); j++)
                    {
                        //Dot Product of row i and column j inserted into [i][j]
                        multipliedMatrix[i][j] = DotProduct(inputMatrixA.GetMatrix(), inputMatrixB.GetMatrix(), i, j);
                    }
                }
                outputMatrix = new Matrix(multipliedMatrix);

            }
            else
            {
                //Invalid Matrix multiplication
                MessageBox.Show("invalid Matrix multiplication");
                outputMatrix = inputMatrixA;
            }
            return outputMatrix;
        }

        public static PointF GetMidpoint(PointF pointA, PointF pointB ) //Method for getting the midpoint of 2 points
        {
            return (new PointF((pointA.X + pointB.X) / 2, (pointA.Y + pointB.Y) / 2));
        }

        private static float DotProduct(float[][] inputMatrixA, float[][] inputMatrixB, int rowIndex, int columnIndex) //internal method for dot product operations as part of matrix multiplication
        {
            float[] row, column;
            float result = 0;

            //extract row from Matrix A
            row = inputMatrixA[rowIndex];

            //extract column from Matrix B
            column = new float[inputMatrixB.Length];
            for (int i = 0; i < inputMatrixB.Length; i++)
            {
                column[i] = inputMatrixB[i][columnIndex];
            }

            //dot product row and column
            for (int i = 0; i < row.Length; i++)
            {
                result = result + (row[i] * column[i]);
            }
            return result;
        }
    }
    #endregion


}
